const express = require('express');
const router = express.Router();
const auth = require('../middleware/auth');
const Admin = require('../models/Admin');
const Member = require('../models/Member');
const Application = require('../models/Application');

// Middleware to check if user is admin
const adminOnly = (req, res, next) => {
    if (req.user.role !== 'admin') {
        return res.status(403).json({ msg: 'Access denied: Admin only' });
    }
    next();
};

// @route    GET api/admin/members
// @desc     Get all members
router.get('/members', [auth, adminOnly], async (req, res) => {
    try {
        const members = await Member.find().select('-password').sort({ createDate: -1 });
        res.json(members);
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

// @route    GET api/admin/applications
// @desc     Get all applications
router.get('/applications', [auth, adminOnly], async (req, res) => {
    try {
        const apps = await Application.find()
            .populate('member', 'firstName lastName phoneNumber')
            .sort({ createDate: -1 });
        res.json(apps);
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

// @route    GET api/admin/admins
// @desc     Get all admin users
router.get('/admins', [auth, adminOnly], async (req, res) => {
    try {
        const admins = await Admin.find().select('-password');
        res.json(admins);
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

// @route    POST api/admin/application/:id/status
// @desc     Update application status
router.post('/application/:id/status', [auth, adminOnly], async (req, res) => {
    const { status, reason } = req.body;
    try {
        let app = await Application.findById(req.params.id);
        if (!app) return res.status(404).json({ msg: 'Application not found' });

        app.applicationStatus = status;
        if (reason) app.rejection = { reason, date: Date.now(), admin: req.user.id };
        
        await app.save();
        res.json(app);
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

// @route    PATCH api/admin/application/:id/assign
// @desc     Assign admins to application
router.patch('/application/:id/assign', [auth, adminOnly], async (req, res) => {
    try {
        let app = await Application.findById(req.params.id);
        if (!app) return res.status(404).json({ msg: 'Application not found' });

        const { am, pfc, rm, um, pa } = req.body;
        
        app.admins = {
            ...app.admins,
            am: am || app.admins.am,
            pfc: pfc || app.admins.pfc,
            rm: rm || app.admins.rm,
            um: um || app.admins.um,
            pa: pa || app.admins.pa
        };

        app.lastUpdate = Date.now();
        await app.save();
        res.json(app);
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

// @route    GET api/admin/stats
// @desc     Get dashboard statistics
router.get('/stats', [auth, adminOnly], async (req, res) => {
    try {
        const totalMembers = await Member.countDocuments();
        const totalApps = await Application.countDocuments();
        const pendingApps = await Application.countDocuments({ applicationStatus: 1 });
        const pendingApprovals = await Member.countDocuments({ status: 0 }); // Assuming 0 is pending approval

        res.json({
            totalMembers,
            totalApps,
            pendingApps,
            pendingApprovals
        });
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

module.exports = router;
