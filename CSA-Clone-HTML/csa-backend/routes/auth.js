const express = require("express");
const router = express.Router();
const jwt = require("jsonwebtoken");
const Member = require("../models/Member");
const Admin = require("../models/Admin");
const Tac = require("../models/Tac");
const bcrypt = require("bcryptjs");
const { sendWhatsAppOTP } = require("../services/whatsappService");

// @route    POST api/auth/member/register
// @desc     Register a new member
// @access   Public
router.post("/member/register", async (req, res) => {
  const { fullName, phoneNumber, password, referrerCode, tacCode } = req.body;

  try {
    // Verify TAC
    const tac = await Tac.findOne({ phoneNumber, code: tacCode });
    if (!tac) {
      return res
        .status(400)
        .json({ msg: "Invalid or expired verification code" });
    }

    let member = await Member.findOne({ phoneNumber, memberType: 1 });
    if (member) {
      return res.status(400).json({ msg: "Member already exists" });
    }

    let referrerId = null;
    if (referrerCode) {
      const referrer = await Member.findOne({ memberCode: referrerCode });
      if (referrer) {
        referrerId = referrer._id;
      }
    }

    member = new Member({
      fullName,
      phoneNumber,
      password,
      referrer: referrerId,
      memberType: 1, // Explicitly Member
    });

    await member.save();

    // Increment referrer's referral count
    if (referrerId) {
      await Member.findByIdAndUpdate(referrerId, {
        $inc: { referralAmount: 1 },
      });
    }

    // Delete TAC after successful registration
    await Tac.deleteOne({ _id: tac._id });

    const payload = {
      user: { id: member.id, role: "member" },
    };

    jwt.sign(
      payload,
      process.env.JWT_SECRET,
      { expiresIn: "24h" },
      (err, token) => {
        if (err) throw err;
        const memberData = member.toObject();
        delete memberData.password;
        res.json({ token, member: memberData });
      },
    );
  } catch (err) {
    console.error(err.message);
    res.status(500).json({ msg: err.message || "Server Error" });
  }
});

// @route    POST api/auth/agent/register
// @desc     Register a new agent
// @access   Public
router.post("/agent/register", async (req, res) => {
  const { fullName, phoneNumber, password, referrerCode, tacCode } = req.body;

  try {
    // Verify TAC
    const tac = await Tac.findOne({ phoneNumber, code: tacCode });
    if (!tac) {
      return res
        .status(400)
        .json({ msg: "Invalid or expired verification code" });
    }

    let agent = await Member.findOne({ phoneNumber, memberType: 2 });
    if (agent) {
      return res.status(400).json({ msg: "Agent already exists" });
    }

    let referrerId = null;
    if (referrerCode) {
      const referrer = await Member.findOne({ memberCode: referrerCode });
      if (referrer) {
        referrerId = referrer._id;
      }
    }

    agent = new Member({
      fullName,
      phoneNumber,
      password,
      referrer: referrerId,
      memberType: 2, // Explicitly Agent
    });

    await agent.save();

    // Increment referrer's referral count
    if (referrerId) {
      await Member.findByIdAndUpdate(referrerId, {
        $inc: { referralAmount: 1 },
      });
    }

    // Delete TAC after successful registration
    await Tac.deleteOne({ _id: tac._id });

    const payload = {
      user: { id: agent.id, role: "agent" },
    };

    jwt.sign(
      payload,
      process.env.JWT_SECRET,
      { expiresIn: "24h" },
      (err, token) => {
        if (err) throw err;
        const agentData = agent.toObject();
        delete agentData.password;
        res.json({ token, member: agentData });
      },
    );
  } catch (err) {
    console.error(err.message);
    res.status(500).json({ msg: err.message || "Server Error" });
  }
});

// @route    POST api/auth/member/login
// @desc     Authenticate member & get token
router.post("/member/login", async (req, res) => {
  const { phoneNumber, password } = req.body;

  try {
    let member = await Member.findOne({ phoneNumber, memberType: 1 });
    if (!member) {
      return res.status(400).json({ msg: "Invalid Credentials" });
    }

    const isMatch = await member.comparePassword(password);
    if (!isMatch) {
      return res.status(400).json({ msg: "Invalid Credentials" });
    }

    const payload = {
      user: {
        id: member.id,
        role: "member",
      },
    };

    jwt.sign(
      payload,
      process.env.JWT_SECRET,
      { expiresIn: "24h" },
      (err, token) => {
        if (err) throw err;
        const memberData = member.toObject();
        delete memberData.password;
        res.json({ token, member: memberData });
      },
    );
  } catch (err) {
    console.error(err.message);
    res.status(500).json({ msg: err.message || "Server Error" });
  }
});

// @route    POST api/auth/agent/login
// @desc     Authenticate agent & get token
router.post("/agent/login", async (req, res) => {
  const { phoneNumber, password } = req.body;

  try {
    let agent = await Member.findOne({ phoneNumber, memberType: 2 });
    if (!agent) {
      return res.status(400).json({ msg: "Invalid Credentials" });
    }

    const isMatch = await agent.comparePassword(password);
    if (!isMatch) {
      return res.status(400).json({ msg: "Invalid Credentials" });
    }

    const payload = {
      user: {
        id: agent.id,
        role: "agent",
      },
    };

    jwt.sign(
      payload,
      process.env.JWT_SECRET,
      { expiresIn: "24h" },
      (err, token) => {
        if (err) throw err;
        const agentData = agent.toObject();
        delete agentData.password;
        res.json({ token, member: agentData });
      },
    );
  } catch (err) {
    console.error(err.message);
    res.status(500).json({ msg: err.message || "Server Error" });
  }
});

// @route    POST api/auth/admin/login
// @desc     Authenticate admin & get token
router.post("/admin/login", async (req, res) => {
  const { email, password } = req.body;

  try {
    let admin = await Admin.findOne({ email });
    if (!admin) {
      return res.status(400).json({ msg: "Invalid Credentials" });
    }

    const isMatch = await admin.comparePassword(password);
    if (!isMatch) {
      return res.status(400).json({ msg: "Invalid Credentials" });
    }

    const payload = {
      user: { id: admin.id, role: "admin" },
    };

    jwt.sign(
      payload,
      process.env.JWT_SECRET,
      { expiresIn: "24h" },
      (err, token) => {
        if (err) throw err;
        res.json({ token });
      },
    );
  } catch (err) {
    console.error(err.message);
    res.status(500).json({ msg: err.message || "Server Error" });
  }
});

// @route    POST api/auth/send-tac
// @desc     Send TAC via WhatsApp
// @access   Public
router.post("/send-tac", async (req, res) => {
  const { phoneNumber } = req.body;

  if (!phoneNumber) {
    return res.status(400).json({ msg: "Phone number is required" });
  }

  try {
    // Generate 6-digit code
    const code = Math.floor(100000 + Math.random() * 900000).toString();

    // Store in DB (update if exists)
    await Tac.findOneAndUpdate(
      { phoneNumber },
      { code, createdAt: Date.now() },
      { upsert: true, new: true },
    );

    // Also log this TAC request permanently
    const TacLog = require("../models/TacLog");
    const newTacLog = new TacLog({
      phoneNumber,
      code,
    });
    await newTacLog.save();

    // Send via VerifyWay WhatsApp API
    const result = await sendWhatsAppOTP(phoneNumber, code);

    if (result.success) {
      res.json({ msg: "Verification code sent successfully" });
    } else {
      console.error("WhatsApp Error:", result.error);
      res.status(500).json({ msg: "Failed to send verification code" });
    }
  } catch (err) {
    console.error("Send TAC Error:", err.message);
    res.status(500).json({ msg: err.message || "Server Error" });
  }
});

// @route    POST api/auth/member/forgot-password
// @desc     Send TAC for password reset
// @access   Public
router.post("/member/forgot-password", async (req, res) => {
  const { phoneNumber } = req.body;

  if (!phoneNumber) {
    return res.status(400).json({ msg: "Phone number is required" });
  }

  try {
    // Check if member exists
    let member = await Member.findOne({ phoneNumber, memberType: 1 });
    if (!member) {
      return res
        .status(400)
        .json({ msg: "No member found with this phone number" });
    }

    // Generate 6-digit code
    const code = Math.floor(100000 + Math.random() * 900000).toString();

    // Store in DB (update if exists)
    await Tac.findOneAndUpdate(
      { phoneNumber },
      { code, createdAt: Date.now() },
      { upsert: true, new: true },
    );

    // Send via VerifyWay WhatsApp API
    const result = await sendWhatsAppOTP(phoneNumber, code);

    if (result.success) {
      res.json({ msg: "Reset code sent successfully via WhatsApp" });
    } else {
      console.error("WhatsApp Error:", result.error);
      res.status(500).json({ msg: "Failed to send reset code" });
    }
  } catch (err) {
    console.error("Forgot Password Error:", err.message);
    res.status(500).json({ msg: err.message || "Server Error" });
  }
});

// @route    POST api/auth/member/reset-password
// @desc     Verify TAC and reset password
// @access   Public
router.post("/member/reset-password", async (req, res) => {
  const { phoneNumber, tacCode, newPassword } = req.body;

  if (!phoneNumber || !tacCode || !newPassword) {
    return res.status(400).json({ msg: "Please provide all fields" });
  }

  try {
    // Verify TAC
    const tac = await Tac.findOne({ phoneNumber, code: tacCode });
    if (!tac) {
      return res.status(400).json({ msg: "Invalid or expired reset code" });
    }

    // Find member and update password
    let member = await Member.findOne({ phoneNumber, memberType: 1 });
    if (!member) {
      return res.status(400).json({ msg: "Member not found" });
    }

    member.password = newPassword;
    await member.save();

    // Delete TAC after successful reset
    await Tac.deleteOne({ _id: tac._id });

    res.json({
      msg: "Password has been reset successfully. You can now login.",
    });
  } catch (err) {
    console.error("Reset Password Error:", err.message);
    res.status(500).json({ msg: err.message || "Server Error" });
  }
});

// @route    POST api/auth/agent/forgot-password
// @desc     Send TAC for agent password reset
// @access   Public
router.post("/agent/forgot-password", async (req, res) => {
  const { phoneNumber } = req.body;

  if (!phoneNumber) {
    return res.status(400).json({ msg: "Phone number is required" });
  }

  try {
    // Check if agent exists
    let agent = await Member.findOne({ phoneNumber, memberType: 2 });
    if (!agent) {
      return res
        .status(400)
        .json({ msg: "No agent found with this phone number" });
    }

    // Generate 6-digit code
    const code = Math.floor(100000 + Math.random() * 900000).toString();

    // Store in DB (update if exists)
    await Tac.findOneAndUpdate(
      { phoneNumber },
      { code, createdAt: Date.now() },
      { upsert: true, new: true },
    );

    // Send via VerifyWay WhatsApp API
    const result = await sendWhatsAppOTP(phoneNumber, code);

    if (result.success) {
      res.json({ msg: "Reset code sent successfully via WhatsApp" });
    } else {
      console.error("WhatsApp Error:", result.error);
      res.status(500).json({ msg: "Failed to send reset code" });
    }
  } catch (err) {
    console.error("Agent Forgot Password Error:", err.message);
    res.status(500).json({ msg: err.message || "Server Error" });
  }
});

// @route    POST api/auth/agent/reset-password
// @desc     Verify TAC and reset agent password
// @access   Public
router.post("/agent/reset-password", async (req, res) => {
  const { phoneNumber, tacCode, newPassword } = req.body;

  if (!phoneNumber || !tacCode || !newPassword) {
    return res.status(400).json({ msg: "Please provide all fields" });
  }

  try {
    // Verify TAC
    const tac = await Tac.findOne({ phoneNumber, code: tacCode });
    if (!tac) {
      return res.status(400).json({ msg: "Invalid or expired reset code" });
    }

    // Find agent and update password
    let agent = await Member.findOne({ phoneNumber, memberType: 2 });
    if (!agent) {
      return res.status(400).json({ msg: "Agent not found" });
    }

    agent.password = newPassword;
    await agent.save();

    // Delete TAC after successful reset
    await Tac.deleteOne({ _id: tac._id });

    res.json({
      msg: "Agent password has been reset successfully. You can now login.",
    });
  } catch (err) {
    console.error("Agent Reset Password Error:", err.message);
    res.status(500).json({ msg: err.message || "Server Error" });
  }
});

module.exports = router;
