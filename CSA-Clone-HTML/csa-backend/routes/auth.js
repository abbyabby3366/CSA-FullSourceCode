const express = require('express');
const router = express.Router();
const jwt = require('jsonwebtoken');
const Member = require('../models/Member');
const Admin = require('../models/Admin');
const bcrypt = require('bcryptjs');

// @route    POST api/auth/member/register
// @desc     Register a new member
// @access   Public
router.post('/member/register', async (req, res) => {
    const { firstName, lastName, phoneNumber, password, referrerCode } = req.body;

    try {
        let member = await Member.findOne({ phoneNumber });
        if (member) {
            return res.status(400).json({ msg: 'Member already exists' });
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
            referrer: referrerId
        });

        await member.save();

        const payload = {
            user: { id: member.id, role: 'member' }
        };

        jwt.sign(payload, process.env.JWT_SECRET, { expiresIn: '24h' }, (err, token) => {
            if (err) throw err;
            const memberData = member.toObject();
            delete memberData.password;
            res.json({ token, member: memberData });
        });
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

// @route    POST api/auth/member/login
// @desc     Authenticate member & get token
router.post('/member/login', async (req, res) => {
    const { phoneNumber, password } = req.body;

    try {
        let member = await Member.findOne({ phoneNumber });
        if (!member) {
            return res.status(400).json({ msg: 'Invalid Credentials' });
        }

        const isMatch = await member.comparePassword(password);
        if (!isMatch) {
            return res.status(400).json({ msg: 'Invalid Credentials' });
        }

        const payload = {
            user: { id: member.id, role: 'member' }
        };

        jwt.sign(payload, process.env.JWT_SECRET, { expiresIn: '24h' }, (err, token) => {
            if (err) throw err;
            res.json({ token });
        });
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

// @route    POST api/auth/admin/login
// @desc     Authenticate admin & get token
router.post('/admin/login', async (req, res) => {
    const { email, password } = req.body;

    try {
        let admin = await Admin.findOne({ email });
        if (!admin) {
            return res.status(400).json({ msg: 'Invalid Credentials' });
        }

        const isMatch = await admin.comparePassword(password);
        if (!isMatch) {
            return res.status(400).json({ msg: 'Invalid Credentials' });
        }

        const payload = {
            user: { id: admin.id, role: 'admin' }
        };

        jwt.sign(payload, process.env.JWT_SECRET, { expiresIn: '24h' }, (err, token) => {
            if (err) throw err;
            res.json({ token });
        });
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

module.exports = router;
