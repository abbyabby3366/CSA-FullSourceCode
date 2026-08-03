const express = require("express");
const router = express.Router();
const auth = require("../middleware/auth");
const Member = require("../models/Member");
const Transaction = require("../models/Transaction");
const Application = require("../models/Application");

// @route    GET api/members/me
// @desc     Get current member profile
// @access   Private
router.get("/me", auth, async (req, res) => {
  try {
    const member = await Member.findById(req.user.id)
      .select("-password")
      .populate("referrer", "fullName memberCode");
    if (!member) return res.status(404).json({ msg: "Member not found" });

    const memberObj = member.toObject();

    // Check if member has submitted an application and merge details from the latest application
    const lastApp = await Application.findOne({ member: req.user.id }).sort({
      createDate: -1,
    });

    if (lastApp && lastApp.details) {
      const details = lastApp.details;
      if (details.fullName) memberObj.fullName = details.fullName;
      if (details.icNumber) memberObj.icNumber = details.icNumber.toString().replace(/-/g, "").trim();
      if (details.phoneNumber) memberObj.contactPhone = details.phoneNumber;
      if (details.email) memberObj.email = details.email;

      if (details.employmentDetails) {
        if (details.employmentDetails.employerName) {
          memberObj.companyName = details.employmentDetails.employerName;
        }
        if (details.employmentDetails.jobTitle) {
          memberObj.occupation = details.employmentDetails.jobTitle;
        }
        if (details.employmentDetails.salaryRange) {
          if (details.employmentDetails.salaryRange === "1") memberObj.salary = 2500;
          else if (details.employmentDetails.salaryRange === "2") memberObj.salary = 4000;
          else if (details.employmentDetails.salaryRange === "3") memberObj.salary = 6000;
          else if (!isNaN(parseFloat(details.employmentDetails.salaryRange))) memberObj.salary = parseFloat(details.employmentDetails.salaryRange);
        }
      }
    }

    res.json(memberObj);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    GET api/members/transactions
// @desc     Get current member's transactions
// @access   Private
router.get("/transactions", auth, async (req, res) => {
  try {
    const transactions = await Transaction.find({ member: req.user.id }).sort({
      createDate: -1,
    });
    res.json(transactions);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    POST api/members/update
// @desc     Update member profile
// @access   Private
router.post("/update", auth, async (req, res) => {
  try {
    let member = await Member.findById(req.user.id);
    if (!member) return res.status(404).json({ msg: "Member not found" });

    const updates = req.body;
    // Prevent password update via this route
    delete updates.password;

    member = await Member.findByIdAndUpdate(
      req.user.id,
      { $set: updates },
      { new: true },
    ).select("-password");

    res.json(member);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    POST api/members/withdrawal
// @desc     Request a withdrawal
// @access   Private
router.post("/withdrawal", auth, async (req, res) => {
  const { amount, description } = req.body;

  if (!amount || amount <= 0) {
    return res.status(400).json({ msg: "Please provide a valid amount" });
  }

  try {
    const member = await Member.findById(req.user.id);
    if (!member) return res.status(404).json({ msg: "Member not found" });

    if (member.walletCash < amount) {
      return res.status(400).json({ msg: "Insufficient balance" });
    }

    // Deduct from wallet immediately
    member.walletCash -= amount;
    member.lastUpdateWalletCash = Date.now();
    await member.save();

    // Create pending transaction
    const newTransaction = new Transaction({
      member: req.user.id,
      type: "Withdrawal",
      amount: -amount, // Negative amount for withdrawals
      currencyType: "Cash",
      description: description || "Withdrawal Request",
      status: "Pending",
    });

    await newTransaction.save();
    res.json({
      msg: "Withdrawal request submitted",
      member,
      transaction: newTransaction,
    });
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

const upload = require("../middleware/upload");

// @route    GET api/members/referrals
// @desc     Get current member's referrals
// @access   Private
router.get("/referrals", auth, async (req, res) => {
  try {
    const referrals = await Member.find({ referrer: req.user.id })
      .select(
        "fullName memberCode state status createDate referralType referralCommission",
      )
      .sort({ createDate: -1 });

    const referralIds = referrals.map((r) => r._id);
    const approvedApps = await Application.find({
      member: { $in: referralIds },
      applicationStatus: 6,
    }).select("member");
    const approvedSet = new Set(approvedApps.map((a) => a.member.toString()));

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
      if (approvedSet.has(ref._id.toString())) {
        refObj.status = "approved";
      }
      refObj.latestAppStatus = appStatusMap[ref._id.toString()] !== undefined ? appStatusMap[ref._id.toString()] : null;
      return refObj;
    });

    res.json(result);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    POST api/members/become-agent
// @desc     Apply to become an agent
// @access   Private
router.post(
  "/become-agent",
  [auth, upload.single("payslip")],
  async (req, res) => {
    try {
      let member = await Member.findById(req.user.id);
      if (!member) return res.status(404).json({ msg: "Member not found" });

      const { fullName, icNumber, phoneNumber } = req.body;

      // Update member details
      if (fullName) member.fullName = fullName;
      if (icNumber) member.icNumber = icNumber.toString().replace(/-/g, "").trim();
      if (phoneNumber) member.contactPhone = phoneNumber;

      // Set agent application fields
      member.memberType = 2; // Agent
      member.status = "pending";
      member.agentApplicationDate = Date.now();

      if (req.file) {
        member.payslipImage = req.file.location; // S3 URL
      }

      await member.save();
      res.json({ msg: "Agent application submitted successfully", member });
    } catch (err) {
      console.error(err.message);
      res.status(500).send("Server Error");
    }
  },
);

// @route    GET api/members/progress
// @desc     Get member's progress for dashboard milestones
// @access   Private
router.get("/progress", auth, async (req, res) => {
  try {
    const member = await Member.findById(req.user.id);
    if (!member) return res.status(404).json({ msg: "Member not found" });

    // 1. Profile Completion (Steps)
    const profileComplete = !!member.icNumber;

    // 2. Application Form (Steps)
    const app = await Application.findOne({ member: req.user.id }).sort({
      createDate: -1,
    });
    const appSubmitted = !!app;
    const appApproved = app ? app.applicationStatus === 6 : false;

    // 3. Agent (Steps)
    // Only show if an agent record exists with same number
    const agentRecord = await Member.findOne({
      phoneNumber: member.phoneNumber,
      memberType: 2,
    });
    const agentExists = !!agentRecord;

    res.json({
      profileComplete,
      appSubmitted,
      appApproved,
      agentExists,
      appStatus: app ? app.applicationStatus : 0,
    });
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

module.exports = router;
