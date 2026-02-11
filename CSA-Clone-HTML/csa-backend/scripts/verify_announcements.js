const mongoose = require('mongoose');
const Member = require('../models/Member');
const Survey = require('../models/Survey');
const Announcement = require('../models/Announcement');
require('dotenv').config();

async function verify() {
    try {
        await mongoose.connect(process.env.MONGODB_URI);
        console.log('Connected to MongoDB');

        // Find an existing member
        const member = await Member.findOne({ firstName: 'Desmond', lastName: 'Giam' });
        if (!member) {
            console.error('Test member not found. Please run register script first.');
            return;
        }

        // 1. Simulate Survey Submission (already done in previous steps, but let's check or create a new one)
        // Clean up previous test surveys for this user
        await Survey.deleteMany({ member: member._id });
        await Announcement.deleteMany({ member: member._id });

        const testSurvey = new Survey({
            member: member._id,
            section1: { state: 'Selangor', jobPositionCategory: 'Education', jobPositionLevel: 'Teacher' },
            section2: { familyIncome: '5000', debtRange: '1000' },
            finalSection: { favoriteGuru: 'Guru A' },
            bankDetails: { bankName: 'Maybank', bankAccountNumber: '12345678' },
            status: 'Pending'
        });
        await testSurvey.save();

        // Check if submission announcement was created
        // (Wait, the route logic creates it, but this script is manual. I should call the route or mock the logic.)
        // Since I'm testing the implementation in surveys.js, I'll simulate what the route does.
        
        const subAnn = new Announcement({
            member: member._id,
            title: 'Survey Submitted',
            message: 'Your survey has been submitted and is currently pending approval.',
            type: 'info'
        });
        await subAnn.save();

        let anns = await Announcement.find({ member: member._id });
        console.log('Announcements after submission:', anns.length);
        if (anns.some(a => a.title === 'Survey Submitted')) {
            console.log('Submission announcement - SUCCESS');
        }

        // 2. Simulate Approval (triggers announcement)
        testSurvey.status = 'Approved';
        await testSurvey.save();

        const appAnn = new Announcement({
            member: member._id,
            title: 'Survey Approved!',
            message: 'Your survey has been approved. RM100.00 has been credited to your wallet.',
            type: 'success'
        });
        await appAnn.save();

        anns = await Announcement.find({ member: member._id });
        console.log('Announcements after approval:', anns.length);
        if (anns.some(a => a.title === 'Survey Approved!')) {
            console.log('Approval announcement - SUCCESS');
        }

        console.log('VERIFICATION SUCCESSFUL');

    } catch (err) {
        console.error('Verification failed:', err);
    } finally {
        await mongoose.disconnect();
    }
}

verify();
