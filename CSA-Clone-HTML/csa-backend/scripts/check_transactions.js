const mongoose = require('mongoose');
const Transaction = require('../models/Transaction');
require('dotenv').config();

async function check() {
    try {
        await mongoose.connect(process.env.MONGODB_URI);
        console.log('Connected to MongoDB');

        const transactions = await Transaction.find().sort({ createDate: -1 });
        console.log('Total Transactions:', transactions.length);
        
        for (const t of transactions) {
            console.log(`- ID: ${t._id} | Member: ${t.member} | Type: ${t.type} | Amount: ${t.amount} | Description: ${t.description}`);
        }

    } catch (err) {
        console.error(err);
    } finally {
        await mongoose.disconnect();
    }
}

check();
