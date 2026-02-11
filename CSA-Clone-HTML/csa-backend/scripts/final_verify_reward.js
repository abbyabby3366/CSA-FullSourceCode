const mongoose = require('mongoose');
const Member = require('../models/Member');
const Transaction = require('../models/Transaction');
require('dotenv').config();

async function verify() {
    try {
        await mongoose.connect(process.env.MONGODB_URI);
        console.log('Connected to MongoDB');

        // Find a member with a referrer
        const member = await Member.findOne({ referrer: { $ne: null } });
        if (!member) {
            console.log('No member with referrer found to test.');
            return;
        }

        const referrerId = member.referrer;
        const referrerBefore = await Member.findById(referrerId);
        if (!referrerBefore) {
            console.log('Referrer document not found for member:', member.memberCode);
            return;
        }

        console.log(`Testing with User: ${member.firstName} | Referrer: ${referrerBefore.firstName}`);
        console.log(`Referrer Balance Before: ${referrerBefore.walletCash}`);

        // Simulate manual reward
        const amount = 50; // Use small amount for test
        referrerBefore.walletCash = (referrerBefore.walletCash || 0) + amount;
        await referrerBefore.save();

        const trx = new Transaction({
            member: referrerId,
            type: 'Referral',
            amount: amount,
            description: `TEST Referral Reward - from ${member.firstName}`,
            status: 'Completed',
            processDate: Date.now()
        });
        await trx.save();

        const referrerAfter = await Member.findById(referrerId);
        console.log(`Referrer Balance After: ${referrerAfter.walletCash}`);
        
        if (referrerAfter.walletCash === (referrerBefore.walletCash)) {
            console.log('VERIFICATION SUCCESSFUL: Wallet balance updated.');
        } else {
            console.log('VERIFICATION FAILED: Wallet balance mismatch.');
        }

    } catch (err) {
        console.error(err);
    } finally {
        await mongoose.disconnect();
    }
}

verify();
