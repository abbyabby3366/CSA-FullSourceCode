const express = require("express");
const router = express.Router();
const jwt = require("jsonwebtoken");
const Member = require("../models/Member");
const Admin = require("../models/Admin");
const Tac = require("../models/Tac");
const bcrypt = require("bcryptjs");

// @route    POST api/auth/member/register
// @desc     Register a new member
// @access   Public
router.post("/member/register", async (req, res) => {
  const { firstName, lastName, phoneNumber, password, referrerCode, tacCode } =
    req.body;

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
      firstName,
      lastName,
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
  const { firstName, lastName, phoneNumber, password, referrerCode, tacCode } =
    req.body;

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
      firstName,
      lastName,
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

    // Send via WhatsApp server
    const waServerUrl =
      process.env.WHATSAPP_SERVER_URL || "http://localhost:3182";
    const message = `[CSA] Your verification code is: ${code}. Valid for 5 minutes.`;

    const waResponse = await fetch(`${waServerUrl}/send-message`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ phoneNumber, message }),
    });

    const waData = await waResponse.json();

    if (waData.success) {
      res.json({ msg: "Verification code sent successfully" });
    } else {
      console.error("WhatsApp Server Error:", waData.error);
      res.status(500).json({ msg: "Failed to send verification code" });
    }
  } catch (err) {
    console.error("Send TAC Error:", err.message);
    res.status(500).json({ msg: err.message || "Server Error" });
  }
});

module.exports = router;
