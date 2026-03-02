const express = require("express");
const router = express.Router();
const auth = require("../middleware/auth");
const Settings = require("../models/Settings");

// Middleware to check if user is admin
const adminOnly = (req, res, next) => {
  if (req.user.role !== "admin") {
    return res.status(403).json({ msg: "Access denied: Admin only" });
  }
  next();
};

// @route    GET api/settings
// @desc     Get all settings
router.get("/", async (req, res) => {
  try {
    const settings = await Settings.find();
    res.json(settings);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    GET api/settings/:key
// @desc     Get setting by key
router.get("/:key", async (req, res) => {
  try {
    const setting = await Settings.findOne({ key: req.params.key });
    if (!setting) {
      return res.status(404).json({ msg: "Setting not found" });
    }
    res.json(setting);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    POST api/settings
// @desc     Create or update a setting
router.post("/", [auth, adminOnly], async (req, res) => {
  const { key, value } = req.body;

  try {
    let setting = await Settings.findOne({ key });

    if (setting) {
      // Update
      setting.value = value;
      setting.updatedAt = Date.now();
      await setting.save();
    } else {
      // Create
      setting = new Settings({
        key,
        value,
      });
      await setting.save();
    }

    res.json(setting);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

module.exports = router;
