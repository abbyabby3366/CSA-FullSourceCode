const mongoose = require('mongoose');
const Member = require('../models/Member');
const Survey = require('../models/Survey');
const Transaction = require('../models/Transaction');
require('dotenv').config();

async function debug() {
    try {
        await mongoose.connect(process.env.MONGODB_URI);
        console.log('Connected to MongoDB');

        console.log('\n--- All Surveys ---');
        const surveys = await Survey.find().populate('member');
        for (const s of surveys) {
            const memberName = s.member ? `${s.member.firstName} ${s.member.lastName}` : 'N/A';
            console.log(`Survey ID: ${s._id} | Member: ${memberName} | Status: ${s.status} | RewardPaid: ${s.rewardPaid}`);
            
            if (s.member && s.member.referrer) {
                console.log(`  - Member Referrer ID: ${s.member.referrer}`);
                const referrer = await Member.findById(s.member.referrer);
                console.log(`  - Referrer Found: ${referrer ? (referrer.firstName + ' ' + referrer.lastName) : 'NOT FOUND'}`);
            }
        }

    } catch (err) {
        console.error('Debug Error:', err);
    } finally {
        await mongoose.disconnect();
    }
}

debug();
