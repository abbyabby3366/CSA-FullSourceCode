const express = require('express');
const router = express.Router();
const auth = require('../middleware/auth');
const upload = require('../middleware/upload');
const Application = require('../models/Application');

// @route    GET api/applications/my
// @desc     Get all applications for current member
// @access   Private
router.get('/my', auth, async (req, res) => {
    try {
        const apps = await Application.find({ member: req.user.id }).sort({ createDate: -1 });
        res.json(apps);
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

// @route    POST api/applications/submit
// @desc     Submit new application with files
// @access   Private
router.post('/submit', [auth, upload.fields([
    { name: 'icFrontBack', maxCount: 1 },
    { name: 'payslip', maxCount: 1 },
    { name: 'offerLetter', maxCount: 1 }
])], async (req, res) => {
    try {
        const files = req.files;
        let details = {};
        if (req.body.details) {
            try {
                details = typeof req.body.details === 'string' ? JSON.parse(req.body.details) : req.body.details;
            } catch (err) {
                console.error('JSON parsing error for details:', err);
                return res.status(400).json({ msg: 'Invalid JSON in details field' });
            }
        }

        const newApp = new Application({
            member: req.user.id,
            details: {
                ...details,
                icFile: files.icFrontBack ? files.icFrontBack[0].key : null,
                payslipFile: files.payslip ? files.payslip[0].key : null,
                offerLetterFile: files.offerLetter ? files.offerLetter[0].key : null
            },
            applicationStatus: 1 // Processing
        });

        const app = await newApp.save();
        
        // Construct full URLs for response (using custom domain)
        const baseUrl = `https://${process.env.S3_BUCKET_NAME}`;
        
        // Convert to plain object to add full URLs without modifying the saved DB record permanently in memory
        const responseData = app.toObject();
        responseData.details.icFileUrl = app.details.icFile ? `${baseUrl}/${app.details.icFile}` : null;
        responseData.details.payslipFileUrl = app.details.payslipFile ? `${baseUrl}/${app.details.payslipFile}` : null;
        responseData.details.offerLetterFileUrl = app.details.offerLetterFile ? `${baseUrl}/${app.details.offerLetterFile}` : null;

        res.json({
            msg: 'Application submitted successfully!',
            application: responseData
        });
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

module.exports = router;
