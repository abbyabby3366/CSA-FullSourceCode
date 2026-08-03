const express = require("express");
const router = express.Router();
const auth = require("../middleware/auth");
const multer = require("multer");
const upload = require("../middleware/upload");
const Application = require("../models/Application");
const Member = require("../models/Member");

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
          if (details.icNumber) {
            details.icNumber = details.icNumber.toString().replace(/-/g, "").trim();
          }
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
          icFrontHistory: [
            { file: files.icFront[0].key, uploadedAt: new Date(), uploadedBy: "member", note: "Initial submission" },
          ],
          icBackHistory: [
            { file: files.icBack[0].key, uploadedAt: new Date(), uploadedBy: "member", note: "Initial submission" },
          ],
          payslipHistory: [
            { file: files.payslip[0].key, uploadedAt: new Date(), uploadedBy: "member", note: "Initial submission" },
          ],
        },
        applicationStatus: 1, // Processing
      });

      const app = await newApp.save();

      // Update Member profile with submitted application details
      const updateFields = {};
      if (details.fullName) updateFields.fullName = details.fullName;
      if (details.icNumber) updateFields.icNumber = details.icNumber;
      if (details.phoneNumber) updateFields.contactPhone = details.phoneNumber;
      if (details.email) updateFields.email = details.email;
      if (details.employmentDetails) {
        if (details.employmentDetails.employerName) updateFields.companyName = details.employmentDetails.employerName;
        if (details.employmentDetails.jobTitle) updateFields.occupation = details.employmentDetails.jobTitle;
        if (details.employmentDetails.salaryRange) {
          if (details.employmentDetails.salaryRange === "1") updateFields.salary = 2500;
          else if (details.employmentDetails.salaryRange === "2") updateFields.salary = 4000;
          else if (details.employmentDetails.salaryRange === "3") updateFields.salary = 6000;
          else if (!isNaN(parseFloat(details.employmentDetails.salaryRange))) updateFields.salary = parseFloat(details.employmentDetails.salaryRange);
        }
      }
      if (Object.keys(updateFields).length > 0) {
        await Member.findByIdAndUpdate(req.user.id, { $set: updateFields });
      }

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
      console.error("Submission Error:", err.message);
      res.status(500).json({ msg: "Server Error", error: err.message });
    }
  });
});

// @route    POST api/applications/reupload-document
// @desc     Re-upload application document (payslip, icFront, icBack)
// @access   Private
router.post("/reupload-document", auth, (req, res) => {
  const uploadSingle = upload.single("document");

  uploadSingle(req, res, async (err) => {
    if (err) {
      if (err instanceof multer.MulterError) {
        return res.status(400).json({ msg: `Upload error: ${err.message}` });
      }
      return res.status(400).json({ msg: err.message || err });
    }

    try {
      if (!req.file) {
        return res.status(400).json({ msg: "Please select a file to upload." });
      }

      const { docType, applicationId, note } = req.body;
      const validDocTypes = ["payslip", "icFront", "icBack"];

      if (!validDocTypes.includes(docType)) {
        return res.status(400).json({ msg: "Invalid document type." });
      }

      let query = {};
      if (applicationId) {
        query._id = applicationId;
        if (req.user.role === "member") {
          query.member = req.user.id;
        }
      } else {
        query.member = req.user.id;
      }

      const app = await Application.findOne(query).sort({ createDate: -1 });
      if (!app) {
        return res.status(404).json({ msg: "Application not found." });
      }

      const fileKey = req.file.key;
      const historyField = `${docType}History`;
      const fileField = `${docType}File`;

      if (!app.details) app.details = {};

      // Ensure history array exists (for legacy records)
      if (!Array.isArray(app.details[historyField])) {
        app.details[historyField] = [];
        if (app.details[fileField]) {
          app.details[historyField].push({
            file: app.details[fileField],
            uploadedAt: app.createDate || app.lastUpdate || new Date(),
            uploadedBy: req.user.role || "member",
            note: "Initial file",
          });
        }
      }

      // Push new upload to history
      app.details[historyField].push({
        file: fileKey,
        uploadedAt: new Date(),
        uploadedBy: req.user.role || "member",
        note: note || "Re-uploaded document",
      });

      // Update current active file pointer
      app.details[fileField] = fileKey;
      app.lastUpdate = new Date();

      app.markModified("details");
      await app.save();

      const baseUrl = `https://${process.env.S3_BUCKET_NAME}`;
      const responseData = app.toObject();

      res.json({
        msg: `${docType} updated successfully!`,
        application: responseData,
        newFileUrl: `${baseUrl}/${fileKey}`,
      });
    } catch (error) {
      console.error("Re-upload Error:", error.message);
      res.status(500).json({ msg: "Server Error", error: error.message });
    }
  });
});

module.exports = router;
