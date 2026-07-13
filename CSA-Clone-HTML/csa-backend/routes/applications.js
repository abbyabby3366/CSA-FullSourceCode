const express = require("express");
const router = express.Router();
const auth = require("../middleware/auth");
const multer = require("multer");
const upload = require("../middleware/upload");
const Application = require("../models/Application");

// @route    GET api/applications/my
// @desc     Get all applications for current member
// @access   Private
router.get("/my", auth, async (req, res) => {
  try {
    const apps = await Application.find({ member: req.user.id }).sort({
      createDate: -1,
    });
    res.json(apps);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    POST api/applications/submit
// @desc     Submit new application with files
// @access   Private
router.post("/submit", auth, (req, res) => {
  const uploadFields = upload.fields([
    { name: "icFront", maxCount: 1 },
    { name: "icBack", maxCount: 1 },
    { name: "payslip", maxCount: 1 },
  ]);

  uploadFields(req, res, async (err) => {
    if (err) {
      if (err instanceof multer.MulterError) {
        return res.status(400).json({ msg: `Upload error: ${err.message}` });
      } else if (err === "Error: Images, PDFs, and Docs only!") {
        return res.status(400).json({ msg: err });
      }
      return res.status(400).json({ msg: err.message || err });
    }

    try {
      // Check if user already has an approved application (status 6)
      const existingApprovedApp = await Application.findOne({
        member: req.user.id,
        applicationStatus: 6,
      });

      if (existingApprovedApp) {
        return res.status(400).json({
          msg: "You have already been approved and cannot submit another application.",
        });
      }

      const files = req.files;
      if (!files || !files.icFront || !files.icBack || !files.payslip) {
        return res
          .status(400)
          .json({ msg: "Please upload all required documents." });
      }

      let details = {};
      if (req.body.details) {
        try {
          details =
            typeof req.body.details === "string"
              ? JSON.parse(req.body.details)
              : req.body.details;
        } catch (err) {
          return res.status(400).json({ msg: "Invalid JSON in details field" });
        }
      }

      const newApp = new Application({
        member: req.user.id,
        details: {
          ...details,
          icFrontFile: files.icFront[0].key,
          icBackFile: files.icBack[0].key,
          payslipFile: files.payslip[0].key,
        },
        applicationStatus: 1, // Processing
      });

      const app = await newApp.save();

      // Construct full URLs for response (using custom domain)
      const baseUrl = `https://${process.env.S3_BUCKET_NAME}`;

      // Convert to plain object to add full URLs
      const responseData = app.toObject();
      responseData.details.icFrontFileUrl = `${baseUrl}/${app.details.icFrontFile}`;
      responseData.details.icBackFileUrl = `${baseUrl}/${app.details.icBackFile}`;
      responseData.details.payslipFileUrl = `${baseUrl}/${app.details.payslipFile}`;

      res.json({
        msg: "Application submitted successfully!",
        application: responseData,
      });
    } catch (err) {
      console.error(err.message);
      res.status(500).send("Server Error");
    }
  });
});

module.exports = router;
