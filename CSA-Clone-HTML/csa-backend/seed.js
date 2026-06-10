const mongoose = require('mongoose');
const bcrypt = require('bcryptjs');
const Admin = require('./models/Admin');
const Member = require('./models/Member');
const Application = require('./models/Application');
const Transaction = require('./models/Transaction');
const SurveyBooking = require('./models/SurveyBooking');
const Tac = require('./models/Tac');
const TacLog = require('./models/TacLog');
const Announcement = require('./models/Announcement');
const Carousel = require('./models/Carousel');
const Settings = require('./models/Settings');
require('dotenv').config();

const seedData = async () => {
    try {
        await mongoose.connect(process.env.MONGODB_URI);
        console.log('Connected to MongoDB for seeding...');

        // Clear existing data (all collections)
        await Promise.all([
            Admin.deleteMany({}),
            Member.deleteMany({}),
            Application.deleteMany({}),
            Transaction.deleteMany({}),
            SurveyBooking.deleteMany({}),
            Tac.deleteMany({}),
            TacLog.deleteMany({}),
            Announcement.deleteMany({}),
            Carousel.deleteMany({}),
            Settings.deleteMany({})
        ]);
        console.log('Cleared all collections.');

        // Create Super Admin
        const adminPassword = await bcrypt.hash('admin123', 10);
        const admin = new Admin({
            name: 'System Admin',
            email: 'admin@gmail.com',
            password: 'admin123', // Model will hash it again if pre-save is active, or use hashed directly
            isSuperAdmin: true,
            role: 'Super Admin'
        });
        await admin.save();
        console.log('Admin seeded: admin@gmail.com / admin123');

        // Create a Sample Member
        const member = new Member({
            memberCode: 'CSA001',
            fullName: 'John Doe',
            firstName: 'John',
            lastName: 'Doe',
            phoneNumber: '007',
            password: '123',
            memberType: 1, // Regular Member
            status: 'approved', // Active
            walletCash: 100
        });
        await member.save();
        console.log('Member seeded: 007 / 123');

        // Create a Sample Agent
        const agent = new Member({
            memberCode: 'CSA-AGENT-01',
            fullName: 'Agent Smith',
            firstName: 'Agent',
            lastName: 'Smith',
            phoneNumber: '001',
            password: '123',
            memberType: 2, // Agent
            status: 'approved', // Active
            walletCash: 500
        });
        await agent.save();
        console.log('Agent seeded: 001 / 123');

        console.log('Seeding completed successfully.');
        process.exit();
    } catch (err) {
        console.error('Seeding error: ', err);
        process.exit(1);
    }
};

seedData();
