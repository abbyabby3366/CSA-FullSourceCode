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
        const details = req.body.details ? JSON.parse(req.body.details) : {};

        const newApp = new Application({
            member: req.user.id,
            details: {
                ...details,
                icFile: files.icFrontBack ? files.icFrontBack[0].path : null,
                payslipFile: files.payslip ? files.payslip[0].path : null,
                offerLetterFile: files.offerLetter ? files.offerLetter[0].path : null
            },
            applicationStatus: 1 // Processing
        });

        const app = await newApp.save();
        res.json(app);
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

module.exports = router;
