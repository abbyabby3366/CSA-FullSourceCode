const mongoose = require('mongoose');
const bcrypt = require('bcryptjs');
const Admin = require('./models/Admin');
const Member = require('./models/Member');
require('dotenv').config();

const seedData = async () => {
    try {
        await mongoose.connect(process.env.MONGODB_URI);
        console.log('Connected to MongoDB for seeding...');

        // Clear existing data
        await Admin.deleteMany({});
        await Member.deleteMany({});

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
            firstName: 'John',
            lastName: 'Doe',
            phoneNumber: '007',
            password: '123',
            memberType: 1, // Regular Member
            status: 2, // Active
            walletCash: 100
        });
        await member.save();
        console.log('Member seeded: 007 / 123');

        // Create a Sample Agent
        const agent = new Member({
            memberCode: 'CSA-AGENT-01',
            firstName: 'Agent',
            lastName: 'Smith',
            phoneNumber: '001',
            password: '123',
            memberType: 2, // Agent
            status: 2, // Active
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
