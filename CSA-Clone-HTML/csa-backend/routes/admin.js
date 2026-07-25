const express = require("express");
const router = express.Router();
const auth = require("../middleware/auth");
const Admin = require("../models/Admin");
const Member = require("../models/Member");
const Application = require("../models/Application");

const Transaction = require("../models/Transaction");
const { sendWhatsAppMessage } = require("../services/whatsappService");

// Middleware to check if user is admin
const adminOnly = (req, res, next) => {
  if (req.user.role !== "admin") {
    return res.status(403).json({ msg: "Access denied: Admin only" });
  }
  next();
};

// @route    GET api/admin/members
// @desc     Get all members
router.get("/members", [auth, adminOnly], async (req, res) => {
  try {
    const members = await Member.find()
      .select("-password")
      .populate("referrer", "fullName memberCode")
      .sort({ createDate: -1 });

    // Attach isApproved: true if member has at least one approved application (status 6)
    const memberIds = members.map((m) => m._id);
    const approvedApps = await Application.find({
      member: { $in: memberIds },
      applicationStatus: 6,
    }).select("member");
    const approvedSet = new Set(approvedApps.map((a) => a.member.toString()));

    // Get latest application status for each member
    const latestApps = await Application.aggregate([
      { $sort: { createDate: -1 } },
      {
        $group: {
          _id: "$member",
          latestStatus: { $first: "$applicationStatus" },
        },
      },
    ]);
    const appStatusMap = {};
    latestApps.forEach((a) => {
      if (a._id) {
        appStatusMap[a._id.toString()] = a.latestStatus;
      }
    });

    const result = members.map((m) => {
      const mObj = m.toObject();
      return {
        ...mObj,
        isApproved: approvedSet.has(m._id.toString()),
        latestAppStatus: appStatusMap[m._id.toString()] !== undefined ? appStatusMap[m._id.toString()] : null,
      };
    });

    res.json(result);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    GET api/admin/member/:id
// @desc     Get member by ID
router.get("/member/:id", [auth, adminOnly], async (req, res) => {
  try {
    const member = await Member.findById(req.params.id)
      .select("-password")
      .populate("referrer", "fullName memberCode");

    if (!member) {
      return res.status(404).json({ msg: "Member not found" });
    }

    res.json(member);
  } catch (err) {
    console.error(err.message);
    if (err.kind === "ObjectId") {
      return res.status(404).json({ msg: "Member not found" });
    }
    res.status(500).send("Server Error");
  }
});

// @route    POST api/admin/member/:id/status
// @desc     Update member status
router.post("/member/:id/status", [auth, adminOnly], async (req, res) => {
  const { status } = req.body;
  try {
    let member = await Member.findById(req.params.id);
    if (!member) return res.status(404).json({ msg: "Member not found" });

    member.status = status;
    member.lastUpdate = Date.now();

    await member.save();
    res.json(member);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    GET api/admin/applications
// @desc     Get all applications
router.get("/applications", [auth, adminOnly], async (req, res) => {
  try {
    const apps = await Application.aggregate([
      { $sort: { createDate: -1 } },
      {
        $group: {
          _id: "$member",
          latestApp: { $first: "$$ROOT" },
        },
      },
      { $replaceRoot: { newRoot: "$latestApp" } },
      { $sort: { createDate: -1 } },
    ]);

    // Populate the aggregated results
    const populatedApps = await Application.populate(apps, [
      {
        path: "member",
        select: "fullName phoneNumber memberCode memberType referrer",
        populate: {
          path: "referrer",
          select: "memberCode",
        },
      },
      { path: "referrerMember", select: "memberCode" },
    ]);

    res.json(populatedApps);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    GET api/admin/admins
// @desc     Get all admin users
router.get("/admins", [auth, adminOnly], async (req, res) => {
  try {
    const admins = await Admin.find().select("-password");
    res.json(admins);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    POST api/admin/application/:id/status
// @desc     Update application status
router.post("/application/:id/status", [auth, adminOnly], async (req, res) => {
  const { status, reason } = req.body;
  try {
    let app = await Application.findById(req.params.id);
    if (!app) return res.status(404).json({ msg: "Application not found" });

    app.applicationStatus = status;
    if (reason)
      app.rejection = { reason, date: Date.now(), admin: req.user.id };

    const member = await Member.findById(app.member);
    if (member) {
      if (status == 6) {
        member.status = "approved";
      } else if (status == 7 || status == 10) {
        member.status = "rejected";
      }
      await member.save();
    }

    // Trigger RM100 Reward on status 6 (Settlement)
    if (status == 6 && !app.rewardPaid) {
      if (member) {
        // 1. Reward the User (RM100)
        member.walletCash += 100;
        await member.save();

        const userReward = new Transaction({
          member: member._id,
          type: "Reward",
          amount: 100,
          description: "Application Approval Reward (Settlement)",
          status: "Completed",
          processDate: Date.now(),
        });
        await userReward.save();

        // 2. Reward the Upline (RM50)
        // Check Application.referrerMember first, then Member.referrer
        const uplineId = app.referrerMember || member.referrer;
        if (uplineId) {
          const referrer = await Member.findById(uplineId);
          if (referrer) {
            referrer.walletCash += 50;
            referrer.referralCommission =
              (referrer.referralCommission || 0) + 50;
            await referrer.save();

            const referralReward = new Transaction({
              member: referrer._id,
              type: "Referral",
              amount: 50,
              description: `Referral Reward - ${member.fullName}'s Application Settlement`,
              status: "Completed",
              processDate: Date.now(),
            });
            await referralReward.save();
          }
        }

        app.rewardPaid = true;
      }
    }

    await app.save();
    res.json(app);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    PATCH api/admin/application/:id/assign
// @desc     Assign admins to application
router.patch("/application/:id/assign", [auth, adminOnly], async (req, res) => {
  try {
    let app = await Application.findById(req.params.id);
    if (!app) return res.status(404).json({ msg: "Application not found" });

    const { am, pfc, rm, um, pa } = req.body;

    app.admins = {
      ...app.admins,
      am: am || app.admins.am,
      pfc: pfc || app.admins.pfc,
      rm: rm || app.admins.rm,
      um: um || app.admins.um,
      pa: pa || app.admins.pa,
    };

    app.lastUpdate = Date.now();
    await app.save();
    res.json(app);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    GET api/admin/stats
// @desc     Get dashboard statistics
router.get("/stats", [auth, adminOnly], async (req, res) => {
  try {
    const totalMembers = await Member.countDocuments();
    const totalApps = await Application.countDocuments();
    const pendingApps = await Application.countDocuments({
      applicationStatus: 1,
    });
    const pendingApprovals = await Member.countDocuments({ status: { $in: ["pending", 0, 1] } }); // Pending approval

    res.json({
      totalMembers,
      totalApps,
      pendingApps,
      pendingApprovals,
    });
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    GET api/admin/withdrawals
// @desc     Get all withdrawal requests
router.get("/withdrawals", [auth, adminOnly], async (req, res) => {
  try {
    const withdrawals = await Transaction.find({ type: "Withdrawal" })
      .populate(
        "member",
        "fullName phoneNumber bankName bankAccountNumber bankAccountName memberCode memberType",
      )
      .sort({ createDate: -1 });
    res.json(withdrawals);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

const upload = require("../middleware/upload");

// @route    POST api/admin/withdrawal/:id/status
// @desc     Update withdrawal status
router.post(
  "/withdrawal/:id/status",
  [auth, adminOnly, upload.single("receipt")],
  async (req, res) => {
    const { status, receiptUrl: manualReceiptUrl } = req.body;
    try {
      let transaction = await Transaction.findById(req.params.id);
      if (!transaction)
        return res.status(404).json({ msg: "Transaction not found" });

      if (transaction.status !== "Pending") {
        return res.status(400).json({ msg: "Transaction already processed" });
      }

      transaction.status = status;
      transaction.processDate = Date.now();
      transaction.admin = req.user.id;

      // Handle receipt upload
      if (req.file) {
        const baseUrl = `https://${process.env.S3_BUCKET_NAME}`;
        transaction.receiptUrl = `${baseUrl}/${req.file.key}`;
      } else if (manualReceiptUrl) {
        transaction.receiptUrl = manualReceiptUrl;
      }

      if (status === "Rejected") {
        // Refund the member
        const member = await Member.findById(transaction.member);
        if (member) {
          member.walletCash += Math.abs(transaction.amount);
          member.lastUpdateWalletCash = Date.now();
          await member.save();
        }
      }

      await transaction.save();

      // Notify via WhatsApp if approved
      if (status === "Completed") {
        try {
          const member = await Member.findById(transaction.member);
          if (member && member.phoneNumber) {
            const amountStr = Math.abs(transaction.amount).toLocaleString(
              undefined,
              { minimumFractionDigits: 2 },
            );
            const message = `[iBelanja Survey] Your withdrawal request of RM ${amountStr} has been approved and processed. Reference: ${transaction._id}`;

            await sendWhatsAppMessage(member.phoneNumber, message);
          }
        } catch (waErr) {
          console.error("WhatsApp Notification Error:", waErr.message);
          // Don't fail the whole request if WA fails
        }
      }

      res.json(transaction);
    } catch (err) {
      console.error(err.message);
      res.status(500).send("Server Error");
    }
  },
);

// @route    GET api/admin/agents
// @desc     Get all agents
router.get("/agents", [auth, adminOnly], async (req, res) => {
  try {
    const agents = await Member.aggregate([
      { $match: { memberType: 2 } },
      {
        $lookup: {
          from: "members",
          localField: "_id",
          foreignField: "referrer",
          as: "referrals",
        },
      },
      {
        $lookup: {
          from: "members",
          localField: "referrer",
          foreignField: "_id",
          as: "referrerInfo",
        },
      },
      {
        $addFields: {
          referralAmount: { $size: "$referrals" },
          referrer: { $arrayElemAt: ["$referrerInfo", 0] },
        },
      },
      {
        $project: {
          password: 0,
          referrals: 0,
          referrerInfo: 0,
        },
      },
      { $sort: { agentApplicationDate: -1 } },
    ]);
    res.json(agents);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    GET api/admin/agents/:id/referrals
// @desc     Get all members referred by a specific agent
router.get("/agents/:id/referrals", [auth, adminOnly], async (req, res) => {
  try {
    const referrals = await Member.find({ referrer: req.params.id })
      .select(
        "fullName memberCode phoneNumber state createDate memberType status",
      )
      .sort({ createDate: -1 });
    res.json(referrals);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    GET api/admin/tac-logs
// @desc     Get all TAC logs
router.get("/tac-logs", [auth, adminOnly], async (req, res) => {
  try {
    const TacLog = require("../models/TacLog");
    const logs = await TacLog.find().sort({ createdAt: -1 });
    res.json(logs);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    GET api/admin/members/export
// @desc     Export members and latest applications to CSV
router.get("/members/export", [auth, adminOnly], async (req, res) => {
  try {
    const baseUrl = `https://${process.env.S3_BUCKET_NAME}`;

    // Get all members (skipping agents)
    const members = await Member.find({ memberType: 1 })
      .populate("referrer", "fullName memberCode")
      .sort({ createDate: -1 });

    // Get latest application for each member
    const apps = await Application.aggregate([
      { $sort: { createDate: -1 } },
      {
        $group: {
          _id: "$member",
          latestApp: { $first: "$$ROOT" },
        },
      },
    ]);

    const appMap = {};
    apps.forEach((a) => {
      appMap[a._id.toString()] = a.latestApp;
    });

    const headers = [
      "Member Code",
      "Name",
      "Phone no",
      "Ic",
      "Gender",
      "State",
      "City",
      "Postcode",
      "Street Address 1",
      "Street Address 2",
      "Bank Name",
      "Bank Account Name",
      "Bank Account Number",
      "Wallet Cash",
      "Member Status",
      "Join Date",
      "Member Payslip URL",
      "Latest Application Status",
      "Latest Application Date",
      "Employer Name",
      "Job Title",
      "Salary Range",
      "Gross Salary",
      "Net Income",
      "Application IC Front URL",
      "Application IC Back URL",
      "Application Payslip URL",
    ];

    const getAppStatusLabel = (status) => {
      switch (status) {
        case 0: return "Pre-checking";
        case 1: return "Processing";
        case 2: return "Referrer Approved";
        case 3: return "Admin Approved";
        case 4: return "Verification";
        case 5: return "Signing";
        case 6: return "Settled";
        case 7: return "Rejected";
        default: return `Status ${status}`;
      }
    };

    const rows = [headers];
    for (const member of members) {
      const app = appMap[member._id.toString()];

      const row = [
        member.memberCode || "",
        member.fullName || "",
        member.phoneNumber || "",
        member.icNumber || "",
        member.gender || "",
        member.state || "",
        member.city || "",
        member.postcode || "",
        member.streetAddress1 || "",
        member.streetAddress2 || "",
        member.bankName || "",
        member.bankAccountName || "",
        member.bankAccountNumber || "",
        member.walletCash !== undefined ? member.walletCash : 0,
        member.status || "",
        member.createDate ? new Date(member.createDate).toISOString() : "",
        member.payslipImage || "",
        app ? getAppStatusLabel(app.applicationStatus) : "No Application",
        app && app.createDate ? new Date(app.createDate).toISOString() : "",
        app && app.details && app.details.employmentDetails ? app.details.employmentDetails.employerName : "",
        app && app.details && app.details.employmentDetails ? app.details.employmentDetails.jobTitle : "",
        app && app.details && app.details.employmentDetails ? app.details.employmentDetails.salaryRange : "",
        app && app.details && app.details.financials ? app.details.financials.salaryGross : "",
        app && app.details && app.details.financials ? app.details.financials.netIncome : "",
        app && app.details && app.details.icFrontFile ? `${baseUrl}/${app.details.icFrontFile}` : "",
        app && app.details && app.details.icBackFile ? `${baseUrl}/${app.details.icBackFile}` : "",
        app && app.details && app.details.payslipFile ? `${baseUrl}/${app.details.payslipFile}` : "",
      ];
      rows.push(row);
    }

    const csvContent = rows
      .map((row) =>
        row
          .map((val) => {
            if (val === undefined || val === null) return "";
            let str = String(val);
            str = str.replace(/"/g, '""');
            if (/[",\r\n]/.test(str)) {
              str = `"${str}"`;
            }
            return str;
          })
          .join(","),
      )
      .join("\n");

    res.setHeader("Content-Type", "text/csv");
    res.setHeader(
      "Content-Disposition",
      "attachment; filename=members_and_applications_export.csv",
    );
    res.send(csvContent);
  } catch (err) {
    console.error("CSV Export Error:", err.message);
    res.status(500).send("Server Error");
  }
});

module.exports = router;
