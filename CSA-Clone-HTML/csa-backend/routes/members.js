const express = require('express');
const router = express.Router();
const auth = require('../middleware/auth');
const Member = require('../models/Member');
const Transaction = require('../models/Transaction');

// @route    GET api/members/me
// @desc     Get current member profile
// @access   Private
router.get('/me', auth, async (req, res) => {
    try {
        const member = await Member.findById(req.user.id).select('-password').populate('referrer', 'firstName lastName memberCode');
        res.json(member);
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

// @route    GET api/members/transactions
// @desc     Get current member's transactions
// @access   Private
router.get('/transactions', auth, async (req, res) => {
    try {
        const transactions = await Transaction.find({ member: req.user.id }).sort({ createDate: -1 });
        res.json(transactions);
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

// @route    POST api/members/update
// @desc     Update member profile
// @access   Private
router.post('/update', auth, async (req, res) => {
    try {
        let member = await Member.findById(req.user.id);
        if (!member) return res.status(404).json({ msg: 'Member not found' });

        const updates = req.body;
        // Prevent password update via this route
        delete updates.password;

        member = await Member.findByIdAndUpdate(
            req.user.id,
            { $set: updates },
            { new: true }
        ).select('-password');

        res.json(member);
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

module.exports = router;
