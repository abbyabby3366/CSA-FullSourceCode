const express = require('express');
const router = express.Router();
const auth = require('../middleware/auth');
const Announcement = require('../models/Announcement');

// @route    GET api/announcements
// @desc     Get all announcements for the logged-in member
// @access   Private
router.get('/', auth, async (req, res) => {
    try {
        const announcements = await Announcement.find({ member: req.user.id }).sort({ createdAt: -1 });
        res.json(announcements);
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

// @route    PUT api/announcements/read/:id
// @desc     Mark an announcement as read
// @access   Private
router.put('/read/:id', auth, async (req, res) => {
    try {
        const announcement = await Announcement.findById(req.params.id);
        if (!announcement) {
            return res.status(404).json({ msg: 'Announcement not found' });
        }

        // Check user
        if (announcement.member.toString() !== req.user.id) {
            return res.status(401).json({ msg: 'User not authorized' });
        }

        announcement.isRead = true;
        await announcement.save();

        res.json(announcement);
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

// @route    DELETE api/announcements/:id
// @desc     Delete an announcement
// @access   Private
router.delete('/:id', auth, async (req, res) => {
    try {
        const announcement = await Announcement.findById(req.params.id);
        if (!announcement) {
            return res.status(404).json({ msg: 'Announcement not found' });
        }

        // Check user
        if (announcement.member.toString() !== req.user.id) {
            return res.status(401).json({ msg: 'User not authorized' });
        }

        await announcement.deleteOne();

        res.json({ msg: 'Announcement removed' });
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

module.exports = router;
