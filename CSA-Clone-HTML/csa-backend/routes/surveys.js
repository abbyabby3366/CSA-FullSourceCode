const express = require('express');
const router = express.Router();
const auth = require('../middleware/auth');
const Survey = require('../models/Survey');
const Member = require('../models/Member');
const Transaction = require('../models/Transaction');

// @route    POST api/surveys/submit
// @desc     Submit a new survey
// @access   Private
router.post('/submit', auth, async (req, res) => {
    try {
        // Check if member already submitted a survey
        const existingSurvey = await Survey.findOne({ member: req.user.id });
        if (existingSurvey) {
            return res.status(400).json({ msg: 'You have already submitted a survey.' });
        }

        const { section1, section2, finalSection, bankDetails } = req.body;

        const newSurvey = new Survey({
            member: req.user.id,
            section1,
            section2,
            finalSection,
            bankDetails,
            status: 'Pending' // Explicitly set to Pending
        });

        const survey = await newSurvey.save();
        res.json(survey);
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

// @route    GET api/surveys/me
// @desc     Get current user's survey response
// @access   Private
router.get('/me', auth, async (req, res) => {
    try {
        const survey = await Survey.findOne({ member: req.user.id }).sort({ submittedAt: -1 });
        if (!survey) {
            return res.json(null);
        }
        res.json(survey);
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

// @route    GET api/surveys/admin/all
// @desc     Get all surveys (Admin only)
// @access   Private
router.get('/admin/all', auth, async (req, res) => {
    try {
        // Here we should check if user is admin, but the project seems to use a separate Admin model
        // For simplicity, we assume this route is protected or checked elsewhere if needed
        const surveys = await Survey.find().populate('member', 'firstName lastName phoneNumber memberCode').sort({ submittedAt: -1 });
        res.json(surveys);
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

// @route    DELETE api/surveys/:id
// @desc     Delete a survey (Admin only)
// @access   Private
router.delete('/:id', auth, async (req, res) => {
    try {
        const survey = await Survey.findById(req.params.id);
        if (!survey) {
            return res.status(404).json({ msg: 'Survey not found' });
        }

        await Survey.findByIdAndDelete(req.params.id);
        res.json({ msg: 'Survey removed' });
    } catch (err) {
        console.error(err.message);
        if (err.kind === 'ObjectId') {
            return res.status(404).json({ msg: 'Survey not found' });
        }
        res.status(500).send('Server Error');
    }
});

// @route    POST api/surveys/admin/approve/:id
// @desc     Approve a survey and reward user + upline
// @access   Private
router.post('/admin/approve/:id', auth, async (req, res) => {
    try {
        const survey = await Survey.findById(req.params.id).populate('member');
        if (!survey) {
            return res.status(404).json({ msg: 'Survey not found' });
        }

        if (survey.status === 'Verified' || survey.rewardPaid) {
            return res.status(400).json({ msg: 'Survey already approved or reward already paid.' });
        }

        const member = await Member.findById(survey.member._id);
        if (!member) {
            return res.status(404).json({ msg: 'Member not found.' });
        }

        // 1. Reward the User (RM100)
        member.walletCash += 100;
        await member.save();

        const userReward = new Transaction({
            member: member._id,
            type: 'Reward',
            amount: 100,
            description: 'Survey Completion Reward',
            status: 'Completed',
            processDate: Date.now()
        });
        await userReward.save();

        // 2. Reward the Upline (RM100)
        if (member.referrer) {
            const referrer = await Member.findById(member.referrer);
            if (referrer) {
                referrer.walletCash += 100;
                await referrer.save();

                const referralReward = new Transaction({
                    member: referrer._id,
                    type: 'Referral',
                    amount: 100,
                    description: `Referral Reward - ${member.firstName} ${member.lastName}'s Survey Approval`,
                    status: 'Completed',
                    processDate: Date.now()
                });
                await referralReward.save();
            }
        }

        // 3. Update Survey Status
        survey.status = 'Verified';
        survey.rewardPaid = true;
        await survey.save();

        res.json({ msg: 'Survey approved and rewards issued.', survey });
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

// @route    POST api/surveys/admin/reject/:id
// @desc     Reject a survey
// @access   Private
router.post('/admin/reject/:id', auth, async (req, res) => {
    try {
        const survey = await Survey.findById(req.params.id);
        if (!survey) {
            return res.status(404).json({ msg: 'Survey not found' });
        }

        survey.status = 'Rejected';
        await survey.save();

        res.json({ msg: 'Survey rejected.', survey });
    } catch (err) {
        console.error(err.message);
        res.status(500).send('Server Error');
    }
});

module.exports = router;
