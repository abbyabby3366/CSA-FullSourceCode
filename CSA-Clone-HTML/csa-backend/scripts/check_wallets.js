const mongoose = require('mongoose');
const Member = require('../models/Member');
const Transaction = require('../models/Transaction');
require('dotenv').config();

async function check() {
    try {
        await mongoose.connect(process.env.MONGODB_URI);
        console.log('Connected to MongoDB');

        const members = await Member.find({ walletCash: { $gt: 0 } }).select('firstName lastName memberCode walletCash');
        console.log('Members with Balance > 0:', members.length);
        
        for (const m of members) {
            const trxs = await Transaction.find({ member: m._id });
            const trxSum = trxs.reduce((sum, t) => sum + t.amount, 0);
            console.log(`Member: ${m.firstName} ${m.lastName} (${m.memberCode})`);
            console.log(`- WalletCash: ${m.walletCash}`);
            console.log(`- Transaction Sum: ${trxSum}`);
            console.log(`- Transaction Count: ${trxs.length}`);
        }

    } catch (err) {
        console.error(err);
    } finally {
        await mongoose.disconnect();
    }
}

check();
