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

// @route    POST api/members/withdrawal
// @desc     Request a withdrawal
// @access   Private
router.post('/withdrawal', auth, async (req, res) => {
    const { amount, description } = req.body;

    if (!amount || amount <= 0) {
        return res.status(400).json({ msg: 'Please provide a valid amount' });
    }

    try {
        const member = await Member.findById(req.user.id);
        if (!member) return res.status(404).json({ msg: 'Member not found' });

        if (member.walletCash < amount) {
            return res.status(400).json({ msg: 'Insufficient balance' });
        }

        // Deduct from wallet immediately
        member.walletCash -= amount;
        member.lastUpdateWalletCash = Date.now();
        await member.save();

        // Create pending transaction
        const newTransaction = new Transaction({
            member: req.user.id,
            type: 'Withdrawal',
            amount: -amount, // Negative amount for withdrawals
            currencyType: 'Cash',
            description: description || 'Withdrawal Request',
            status: 'Pending'
        });

        await newTransaction.save();
        res.json({ msg: 'Withdrawal request submitted', member, transaction: newTransaction });
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

module.exports = router;
