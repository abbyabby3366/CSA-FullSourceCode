const express = require("express");
const router = express.Router();
const auth = require("../middleware/auth");
const Admin = require("../models/Admin");
const Member = require("../models/Member");
const Application = require("../models/Application");

const Transaction = require("../models/Transaction");

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
      .populate("referrer", "firstName lastName memberCode")
      .sort({ createDate: -1 });

    // Attach isApproved: true if member has at least one approved application (status 6)
    const memberIds = members.map((m) => m._id);
    const approvedApps = await Application.find({
      member: { $in: memberIds },
      applicationStatus: 6,
    }).select("member");
    const approvedSet = new Set(approvedApps.map((a) => a.member.toString()));

    const result = members.map((m) => ({
      ...m.toObject(),
      isApproved: approvedSet.has(m._id.toString()),
    }));

    res.json(result);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    GET api/admin/applications
// @desc     Get all applications
router.get("/applications", [auth, adminOnly], async (req, res) => {
  try {
    const apps = await Application.find()
      .populate(
        "member",
        "firstName lastName phoneNumber memberCode memberType",
      )
      .sort({ createDate: -1 });
    res.json(apps);
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

    // Trigger RM100 Reward on status 6 (Settlement)
    if (status == 6 && !app.rewardPaid) {
      const member = await Member.findById(app.member);
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

        // 2. Reward the Upline (RM100)
        // Check Application.referrerMember first, then Member.referrer
        const uplineId = app.referrerMember || member.referrer;
        if (uplineId) {
          const referrer = await Member.findById(uplineId);
          if (referrer) {
            referrer.walletCash += 100;
            await referrer.save();

            const referralReward = new Transaction({
              member: referrer._id,
              type: "Referral",
              amount: 100,
              description: `Referral Reward - ${member.firstName} ${member.lastName}'s Application Settlement`,
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
    const pendingApprovals = await Member.countDocuments({ status: 0 }); // Assuming 0 is pending approval

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
        "firstName lastName phoneNumber bankName bankAccountNumber bankAccountName memberCode memberType",
      )
      .sort({ createDate: -1 });
    res.json(withdrawals);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    POST api/admin/withdrawal/:id/status
// @desc     Update withdrawal status
router.post("/withdrawal/:id/status", [auth, adminOnly], async (req, res) => {
  const { status } = req.body;
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
    res.json(transaction);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    GET api/admin/agents
// @desc     Get all agents
router.get("/agents", [auth, adminOnly], async (req, res) => {
  try {
    const agents = await Member.find({ memberType: 2 })
      .select("-password")
      .populate("referrer", "firstName lastName memberCode")
      .sort({ agentApplicationDate: -1 });
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
        "firstName lastName memberCode phoneNumber state createDate memberType",
      )
      .sort({ createDate: -1 });
    res.json(referrals);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

module.exports = router;
