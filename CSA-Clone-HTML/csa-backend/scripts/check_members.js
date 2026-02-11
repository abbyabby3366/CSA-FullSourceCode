const mongoose = require('mongoose');
const Member = require('../models/Member');
require('dotenv').config();

async function check() {
    try {
        await mongoose.connect(process.env.MONGODB_URI);
        console.log('Connected to MongoDB');

        const members = await Member.find().select('firstName lastName memberCode phoneNumber');
        console.log('Total Members:', members.length);
        
        members.forEach(m => {
            console.log(`${m.firstName} ${m.lastName} - Code: ${m.memberCode || 'MISSING'}`);
        });

    } catch (err) {
        console.error(err);
    } finally {
        await mongoose.disconnect();
    }
}

check();
