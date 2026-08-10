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

// Middleware to check if user is admin or subadmin
const adminOrSubadmin = (req, res, next) => {
  if (req.user.role !== "admin" && req.user.role !== "subadmin") {
    return res.status(403).json({ msg: "Access denied" });
  }
  next();
};

// @route    GET api/admin/members
// @desc     Get members (supports subadmin scoping and unassigned filter)
router.get("/members", [auth, adminOrSubadmin], async (req, res) => {
  try {
    let query = {};

    if (req.user.role === "subadmin") {
      const assignedAgents = await Member.find({ memberType: 2, subadmin: req.user.id }).select("_id");
      const assignedAgentIds = assignedAgents.map((a) => a._id);
      query.referrer = { $in: assignedAgentIds };
    } else if (req.query.filter === "no_agent") {
      const allAgents = await Member.find({ memberType: 2 }).select("_id");
      const allAgentIds = allAgents.map((a) => a._id);
      query.$or = [{ referrer: { $nin: allAgentIds } }, { referrer: null }, { referrer: { $exists: false } }];
    } else if (req.query.filter === "assigned_subadmin") {
      const assignedAgents = await Member.find({
        memberType: 2,
        subadmin: { $exists: true, $ne: null },
      }).select("_id");
      const assignedAgentIds = assignedAgents.map((a) => a._id);
      if (assignedAgentIds.length > 0) {
        query.referrer = { $in: assignedAgentIds };
      } else {
        query._id = null; // Return empty result when no agents have subadmins
      }
    }

    const members = await Member.find(query)
      .select("-password")
      .populate({
        path: "referrer",
        select: "fullName memberCode subadmin memberType",
        populate: { path: "subadmin", select: "name email" },
      })
      .populate("subadmin", "name email")
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
      { $match: { member: { $in: memberIds } } },
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

    // Collect IC numbers from applications submitted by these members
    const memberApps = await Application.find({
      member: { $in: memberIds },
      "details.icNumber": { $exists: true, $ne: "" },
    }).select("member details.icNumber");

    const appIcMap = {};
    memberApps.forEach((a) => {
      if (a.member && a.details && a.details.icNumber) {
        const cleanIc = a.details.icNumber.toString().replace(/-/g, "").trim();
        const memKey = a.member.toString();
        if (!appIcMap[memKey]) {
          appIcMap[memKey] = new Set();
        }
        appIcMap[memKey].add(cleanIc);
      }
    });

    const result = members.map((m) => {
      const mObj = m.toObject();
      const memIdStr = m._id.toString();
      const icSet = appIcMap[memIdStr] || new Set();
      if (m.icNumber) {
        icSet.add(m.icNumber.toString().replace(/-/g, "").trim());
      }
      const allIcs = Array.from(icSet);
      const effectiveIc = m.icNumber || (allIcs.length > 0 ? allIcs[0] : "");

      return {
        ...mObj,
        icNumber: effectiveIc,
        allIcNumbers: allIcs,
        isApproved: approvedSet.has(memIdStr),
        latestAppStatus: appStatusMap[memIdStr] !== undefined ? appStatusMap[memIdStr] : null,
      };
    });

    let filteredResult = result;
    if (req.query.status !== undefined && req.query.status !== "" && req.query.status !== "all") {
      if (req.query.status === "no_app" || req.query.status === "no_application") {
        filteredResult = result.filter((m) => m.latestAppStatus === null);
      } else {
        const targetStatus = parseInt(req.query.status, 10);
        if (!isNaN(targetStatus)) {
          filteredResult = result.filter((m) => m.latestAppStatus === targetStatus);
        }
      }
    }

    res.json(filteredResult);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    GET api/admin/member/:id
// @desc     Get member by ID
router.get("/member/:id", [auth, adminOrSubadmin], async (req, res) => {
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
// @desc     Get applications (supports subadmin scoping and subadmin filter)
router.get("/applications", [auth, adminOrSubadmin], async (req, res) => {
  try {
    let memberMatch = {};

    if (req.user.role === "subadmin") {
      const assignedAgents = await Member.find({ memberType: 2, subadmin: req.user.id }).select("_id");
      const assignedAgentIds = assignedAgents.map((a) => a._id);
      const networkMembers = await Member.find({
        $or: [{ _id: { $in: assignedAgentIds } }, { referrer: { $in: assignedAgentIds } }],
      }).select("_id");
      const networkMemberIds = networkMembers.map((m) => m._id);
      memberMatch = { member: { $in: networkMemberIds } };
    } else if (req.query.subadmin) {
      // Admin filtering by a specific subadmin
      const mongoose = require("mongoose");
      const subadminObjId = new mongoose.Types.ObjectId(req.query.subadmin);
      const assignedAgents = await Member.find({ memberType: 2, subadmin: subadminObjId }).select("_id");
      const assignedAgentIds = assignedAgents.map((a) => a._id);
      if (assignedAgentIds.length > 0) {
        const networkMembers = await Member.find({
          $or: [{ _id: { $in: assignedAgentIds } }, { referrer: { $in: assignedAgentIds } }],
        }).select("_id");
        const networkMemberIds = networkMembers.map((m) => m._id);
        memberMatch = { member: { $in: networkMemberIds } };
      } else {
        memberMatch = { member: null }; // No results
      }
    }

    const apps = await Application.aggregate([
      ...(Object.keys(memberMatch).length ? [{ $match: memberMatch }] : []),
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
        select: "fullName phoneNumber memberCode memberType referrer subadmin icNumber",
        populate: [
          {
            path: "referrer",
            select: "fullName memberCode subadmin",
            populate: { path: "subadmin", select: "name email" },
          },
          { path: "subadmin", select: "name email" },
        ],
      },
      { path: "referrerMember", select: "fullName memberCode" },
      { path: "approval.admin", select: "name email role" },
      { path: "rejection.admin", select: "name email role" },
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
router.post("/application/:id/status", [auth, adminOrSubadmin], async (req, res) => {
  const { status, reason } = req.body;
  try {
    let app = await Application.findById(req.params.id);
    if (!app) return res.status(404).json({ msg: "Application not found" });

    app.applicationStatus = status;
    if (status == 6) {
      app.approval = { admin: req.user.id, date: Date.now() };
    }
    if (reason)
      app.rejection = { reason, date: Date.now(), admin: req.user.id };

    const member = await Member.findById(app.member);
    if (member) {
      if (status == 6) {
        member.status = "approved";
      } else if (status == 7 || status == 10) {
        member.status = "rejected";
      } else {
        member.status = "pending";
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

// @route    PUT api/admin/application/:id/edit-details
// @desc     Edit application personal details (admin/subadmin) with audit trail
router.put("/application/:id/edit-details", [auth, adminOrSubadmin], async (req, res) => {
  try {
    const app = await Application.findById(req.params.id);
    if (!app) return res.status(404).json({ msg: "Application not found" });

    const admin = await Admin.findById(req.user.id).select("name role");
    if (!admin) return res.status(403).json({ msg: "Admin not found" });

    const { fullName, phoneNumber, icNumber, email } = req.body;
    if (!app.details) app.details = {};

    // Field mapping: submitted field → schema path, label, and Member model field
    const fieldMap = [
      { key: "fullName", label: "Full Name", memberField: "fullName" },
      { key: "phoneNumber", label: "Phone Number", memberField: "phoneNumber" },
      { key: "icNumber", label: "IC Number", memberField: "icNumber" },
      { key: "email", label: "Email Address", memberField: "email" },
    ];

    const incoming = { fullName, phoneNumber, icNumber, email };
    const changes = [];

    for (const f of fieldMap) {
      const newVal = incoming[f.key];
      if (newVal === undefined || newVal === null) continue;

      const cleanNew = f.key === "icNumber" ? newVal.toString().replace(/-/g, "").trim() : newVal.trim();
      const oldVal = (app.details[f.key] || "").toString();

      if (cleanNew !== oldVal) {
        changes.push({
          field: f.key,
          fieldLabel: f.label,
          oldValue: oldVal,
          newValue: cleanNew,
          editedBy: req.user.id,
          editedByName: admin.name,
          editedByRole: admin.role,
          editedAt: new Date(),
        });
        app.details[f.key] = cleanNew;
      }
    }

    if (changes.length === 0) {
      return res.status(400).json({ msg: "No changes detected." });
    }

    if (!app.editHistory) app.editHistory = [];
    app.editHistory.push(...changes);
    app.lastUpdate = new Date();
    app.markModified("details");
    app.markModified("editHistory");
    await app.save();

    // Sync changes to Member model
    const memberUpdate = {};
    for (const c of changes) {
      const mapping = fieldMap.find((f) => f.key === c.field);
      if (mapping && mapping.memberField) {
        memberUpdate[mapping.memberField] = c.newValue;
        // Member model has both phoneNumber and contactPhone
        if (mapping.memberField === "phoneNumber") {
          memberUpdate.contactPhone = c.newValue;
        }
      }
    }
    if (Object.keys(memberUpdate).length > 0 && app.member) {
      await Member.findByIdAndUpdate(app.member, { $set: memberUpdate });
    }

    // Re-fetch with populated editHistory
    const updated = await Application.findById(app._id).populate("editHistory.editedBy", "name role");
    res.json({ msg: "Application details updated successfully.", application: updated, changes });
  } catch (err) {
    console.error("Edit details error:", err.message);
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
// @desc     Get agents (supports subadmin scoping)
router.get("/agents", [auth, adminOrSubadmin], async (req, res) => {
  try {
    let matchCondition = { memberType: 2 };
    if (req.user.role === "subadmin") {
      const mongoose = require("mongoose");
      matchCondition.subadmin = new mongoose.Types.ObjectId(req.user.id);
    }

    const agents = await Member.aggregate([
      { $match: matchCondition },
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
        $lookup: {
          from: "admins",
          localField: "subadmin",
          foreignField: "_id",
          as: "subadminInfo",
        },
      },
      {
        $addFields: {
          referralAmount: { $size: "$referrals" },
          referrer: { $arrayElemAt: ["$referrerInfo", 0] },
          subadmin: { $arrayElemAt: ["$subadminInfo", 0] },
        },
      },
      {
        $project: {
          password: 0,
          referrals: 0,
          referrerInfo: 0,
          subadminInfo: 0,
          "subadmin.password": 0,
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
router.get("/agents/:id/referrals", [auth, adminOrSubadmin], async (req, res) => {
  try {
    const referrals = await Member.find({ referrer: req.params.id })
      .select(
        "fullName memberCode phoneNumber state createDate memberType status icNumber salary",
      )
      .sort({ createDate: -1 });

    const referralIds = referrals.map((r) => r._id);
    const latestApps = await Application.aggregate([
      { $match: { member: { $in: referralIds } } },
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

    const result = referrals.map((ref) => {
      const refObj = ref.toObject();
      return {
        ...refObj,
        latestAppStatus: appStatusMap[ref._id.toString()] !== undefined ? appStatusMap[ref._id.toString()] : null,
      };
    });

    res.json(result);
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
router.get("/members/export", [auth, adminOrSubadmin], async (req, res) => {
  try {
    const baseUrl = `https://${process.env.S3_BUCKET_NAME}`;

    let query = { memberType: 1 };

    if (req.user.role === "subadmin") {
      const assignedAgents = await Member.find({ memberType: 2, subadmin: req.user.id }).select("_id");
      const assignedAgentIds = assignedAgents.map((a) => a._id);
      query.referrer = { $in: assignedAgentIds };
    } else if (req.query.subadmin) {
      const mongoose = require("mongoose");
      const subadminObjId = new mongoose.Types.ObjectId(req.query.subadmin);
      const assignedAgents = await Member.find({ memberType: 2, subadmin: subadminObjId }).select("_id");
      const assignedAgentIds = assignedAgents.map((a) => a._id);
      if (assignedAgentIds.length > 0) {
        query.referrer = { $in: assignedAgentIds };
      } else {
        query._id = null; // No results
      }
    }

    const members = await Member.find(query)
      .populate({
        path: "referrer",
        select: "fullName memberCode subadmin",
        populate: { path: "subadmin", select: "name email" },
      })
      .populate("subadmin", "name email")
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

    const ALL_COLUMNS = [
      { id: "subadmin", label: "Subadmin" },
      { id: "referral_name", label: "Referral Name" },
      { id: "full_name", label: "Full Name (Participant)" },
      { id: "ic", label: "IC" },
      { id: "contact_no", label: "Contact No" },
      { id: "employment_status", label: "Employment Status" },
      { id: "company_name", label: "Company Name" },
      { id: "job_title", label: "Occupation / Job Title" },
      { id: "state_of_employment", label: "State of Employment" },
      { id: "ic_front_url", label: "Application IC Front URL" },
      { id: "ic_back_url", label: "Application IC Back URL" },
      { id: "payslip_url", label: "Application Payslip URL" },
      { id: "join_date", label: "Join Date" },
      { id: "bank_name", label: "Bank Name" },
      { id: "bank_account_name", label: "Bank Account Name" },
      { id: "bank_account_number", label: "Bank Account Number" },
    ];

    let selectedColumnIds = null;
    if (req.query.columns) {
      selectedColumnIds = req.query.columns.split(",").map((c) => c.trim()).filter(Boolean);
    }

    const activeColumns = selectedColumnIds && selectedColumnIds.length > 0
      ? ALL_COLUMNS.filter((col) => selectedColumnIds.includes(col.id))
      : ALL_COLUMNS;

    const headers = activeColumns.map((col) => col.label);
    const rows = [headers];

    for (const member of members) {
      const app = appMap[member._id.toString()];

      let subadminName = "";
      if (member.subadmin && member.subadmin.name) {
        subadminName = member.subadmin.name;
      } else if (member.referrer && member.referrer.subadmin && member.referrer.subadmin.name) {
        subadminName = member.referrer.subadmin.name;
      }

      let referralName = member.referrer && member.referrer.fullName ? member.referrer.fullName : "";
      let fullName = member.fullName || (app && app.details && app.details.fullName ? app.details.fullName : "");
      let ic = member.icNumber || (app && app.details && app.details.icNumber ? app.details.icNumber : "");
      let contactNo = member.phoneNumber || (app && app.details && app.details.phoneNumber ? app.details.phoneNumber : "");

      let employmentStatus = app && app.details && app.details.employmentDetails && app.details.employmentDetails.employmentStatus
        ? app.details.employmentDetails.employmentStatus
        : (member.employmentStatus || "");

      let companyName = app && app.details && app.details.employmentDetails && app.details.employmentDetails.employerName
        ? app.details.employmentDetails.employerName.toUpperCase()
        : (member.companyName || "");

      let occupation = app && app.details && app.details.employmentDetails && app.details.employmentDetails.jobTitle
        ? app.details.employmentDetails.jobTitle
        : (member.occupation || "");

      let stateOfEmployment = app && app.details && app.details.employmentDetails && app.details.employmentDetails.employmentState
        ? app.details.employmentDetails.employmentState
        : (member.employmentState || "");

      let icFrontUrl = app && app.details && app.details.icFrontFile ? `${baseUrl}/${app.details.icFrontFile}` : "";
      let icBackUrl = app && app.details && app.details.icBackFile ? `${baseUrl}/${app.details.icBackFile}` : "";
      let payslipUrl = app && app.details && app.details.payslipFile ? `${baseUrl}/${app.details.payslipFile}` : "";

      let joinDate = member.createDate ? new Date(member.createDate).toISOString() : "";
      let bankName = member.bankName || "";
      let bankAccountName = member.bankAccountName || "";
      let bankAccountNumber = member.bankAccountNumber || "";

      const rowValues = {
        subadmin: subadminName,
        referral_name: referralName,
        full_name: fullName,
        ic: ic,
        contact_no: contactNo,
        employment_status: employmentStatus,
        company_name: companyName,
        job_title: occupation,
        state_of_employment: stateOfEmployment,
        ic_front_url: icFrontUrl,
        ic_back_url: icBackUrl,
        payslip_url: payslipUrl,
        join_date: joinDate,
        bank_name: bankName,
        bank_account_name: bankAccountName,
        bank_account_number: bankAccountNumber,
      };

      const row = activeColumns.map((col) => rowValues[col.id]);
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
      "attachment; filename=members_export.csv",
    );
    res.send(csvContent);
  } catch (err) {
    console.error("CSV Export Error:", err.message);
    res.status(500).send("Server Error");
  }
});

// @route    GET api/admin/subadmins
// @desc     Get all subadmin users
router.get("/subadmins", [auth, adminOnly], async (req, res) => {
  try {
    const subadmins = await Admin.find({ role: "subadmin" }).select("-password");
    res.json(subadmins);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    POST api/admin/subadmins
// @desc     Create a subadmin user
router.post("/subadmins", [auth, adminOnly], async (req, res) => {
  const { name, email, password } = req.body;
  if (!name || !email || !password) {
    return res.status(400).json({ msg: "Please provide all required fields" });
  }

  try {
    let existing = await Admin.findOne({ email });
    if (existing) {
      return res.status(400).json({ msg: "An admin account already exists with this email" });
    }

    const newSubadmin = new Admin({
      name,
      email,
      password,
      role: "subadmin",
      isSuperAdmin: false,
      status: 1,
    });

    await newSubadmin.save();
    const result = newSubadmin.toObject();
    delete result.password;
    res.json(result);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    PUT api/admin/subadmin/:id
// @desc     Update a subadmin user
router.put("/subadmin/:id", [auth, adminOnly], async (req, res) => {
  const { name, email, password, status } = req.body;
  try {
    let subadmin = await Admin.findById(req.params.id);
    if (!subadmin) return res.status(404).json({ msg: "Subadmin not found" });

    if (name) subadmin.name = name;
    if (email) subadmin.email = email;
    if (password) subadmin.password = password;
    if (status !== undefined) subadmin.status = status;

    await subadmin.save();
    const result = subadmin.toObject();
    delete result.password;
    res.json(result);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    DELETE api/admin/subadmin/:id
// @desc     Delete a subadmin user
router.delete("/subadmin/:id", [auth, adminOnly], async (req, res) => {
  try {
    await Admin.findByIdAndDelete(req.params.id);
    // Unassign agents from this subadmin
    await Member.updateMany({ subadmin: req.params.id }, { $unset: { subadmin: "" } });
    res.json({ msg: "Subadmin deleted successfully" });
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    POST api/admin/agent/:id/assign-subadmin
// @desc     Assign an agent to a subadmin
router.post("/agent/:id/assign-subadmin", [auth, adminOnly], async (req, res) => {
  const { subadminId } = req.body;
  try {
    let agent = await Member.findById(req.params.id);
    if (!agent) return res.status(404).json({ msg: "Agent not found" });
    if (agent.memberType !== 2) return res.status(400).json({ msg: "User is not an agent" });

    if (!subadminId || subadminId === "" || subadminId === "unassign") {
      agent.subadmin = undefined;
    } else {
      const subadmin = await Admin.findById(subadminId);
      if (!subadmin) return res.status(404).json({ msg: "Subadmin not found" });
      agent.subadmin = subadminId;
    }

    await agent.save();
    res.json({ msg: "Agent subadmin updated successfully", agent });
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

module.exports = router;
