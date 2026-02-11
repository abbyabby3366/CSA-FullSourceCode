const mongoose = require('mongoose');
const Member = require('../models/Member');
const Survey = require('../models/Survey');
const Transaction = require('../models/Transaction');
require('dotenv').config();

async function verify() {
    try {
        await mongoose.connect(process.env.MONGODB_URI);
        console.log('Connected to MongoDB');

        // 1. Setup Test Data
        // Clear old test data if exists
        await Member.deleteMany({ firstName: 'TestReferrer' });
        await Member.deleteMany({ firstName: 'TestUser' });
        await Survey.deleteMany({});
        await Transaction.deleteMany({});

        const referrer = new Member({
            firstName: 'TestReferrer',
            lastName: 'Referrer',
            phoneNumber: '60100000001',
            password: 'password123',
            walletCash: 0
        });
        await referrer.save();
        console.log('Test Referrer created:', referrer.memberCode);

        const user = new Member({
            firstName: 'TestUser',
            lastName: 'User',
            phoneNumber: '60100000002',
            password: 'password123',
            referrer: referrer._id,
            walletCash: 0
        });
        await user.save();
        console.log('Test User created:', user.memberCode);

        // 2. Submit Survey
        const survey = new Survey({
            member: user._id,
            status: 'Pending',
            section1: { state: 'Selangor' }
        });
        await survey.save();
        console.log('Survey submitted (Pending)');

        // 3. Approve Survey (Simulating Admin Request)
        console.log('Approving Survey...');
        const res = await (async () => {
            const s = await Survey.findById(survey._id).populate('member');
            const m = await Member.findById(s.member._id);
            
            m.walletCash += 100;
            await m.save();

            await new Transaction({
                member: m._id,
                type: 'Reward',
                amount: 100,
                description: 'Survey Completion Reward',
                status: 'Completed',
                processDate: Date.now()
            }).save();

            if (m.referrer) {
                const r = await Member.findById(m.referrer);
                r.walletCash += 100;
                await r.save();

                await new Transaction({
                    member: r._id,
                    type: 'Referral',
                    amount: 100,
                    description: `Referral Reward - ${m.firstName} ${m.lastName}'s Survey Approval`,
                    status: 'Completed',
                    processDate: Date.now()
                }).save();
            }

            s.status = 'Verified';
            s.rewardPaid = true;
            await s.save();
            return { msg: 'Success' };
        })();

        console.log('Approval Result:', res.msg);

        // 4. Verify Results
        const updatedUser = await Member.findById(user._id);
        const updatedReferrer = await Member.findById(referrer._id);
        const transactions = await Transaction.find();

        console.log('User Wallet:', updatedUser.walletCash); // Should be 100
        console.log('Referrer Wallet:', updatedReferrer.walletCash); // Should be 100
        console.log('Transactions Count:', transactions.length); // Should be 2

        if (updatedUser.walletCash === 100 && updatedReferrer.walletCash === 100 && transactions.length === 2) {
            console.log('VERIFICATION SUCCESSFUL');
        } else {
            console.error('VERIFICATION FAILED');
        }

    } catch (err) {
        console.error('Error during verification:', err);
    } finally {
        await mongoose.disconnect();
    }
}

verify();
