const mongoose = require('mongoose');

const ApplicationSchema = new mongoose.Schema({
    member: { type: mongoose.Schema.Types.ObjectId, ref: 'Member', required: true },
    applicationStatus: { type: Number, default: 0 }, // 0-Pre-checking, etc.
    customerStatus: { type: Number, default: 1 }, // 1-Eligible, 2-Burst
    
    // Referral info
    referrerMember: { type: mongoose.Schema.Types.ObjectId, ref: 'Member' },
    
    // Admin assignments
    admins: {
        am: { type: mongoose.Schema.Types.ObjectId, ref: 'Admin' },
        pfc: { type: mongoose.Schema.Types.ObjectId, ref: 'Admin' },
        rm: { type: mongoose.Schema.Types.ObjectId, ref: 'Admin' },
        um: { type: mongoose.Schema.Types.ObjectId, ref: 'Admin' },
        pa: { type: mongoose.Schema.Types.ObjectId, ref: 'Admin' }
    },

    // Detailed Application Sections (Consolidated from Application1-10 SQL tables)
    details: {
        ramciReport: { file: String, lastUpdate: Date },
        ccrisDocument: { file: String, lastUpdate: Date },
        eligibility: { status: Number, lastUpdate: Date },
        
        financials: {
            salaryGross: { type: Number },
            salaryDeduction: { type: Number },
            netIncome: { type: Number },
            commitmentOutstanding: { type: Number },
            commitmentInstallment: { type: Number }
        },

        loanStatus: {
            approvedAmount: { type: Number },
            approvedDate: { type: Date },
            signingDate: { type: Date },
            disbursementDate: { type: Date }
        }
    },

    rejection: {
        reason: { type: String },
        date: { type: Date },
        admin: { type: mongoose.Schema.Types.ObjectId, ref: 'Admin' }
    },

    createDate: { type: Date, default: Date.now },
    lastUpdate: { type: Date, default: Date.now }
});

module.exports = mongoose.model('Application', ApplicationSchema);
