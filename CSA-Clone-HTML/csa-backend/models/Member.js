const mongoose = require('mongoose');
const bcrypt = require('bcryptjs');

const MemberSchema = new mongoose.Schema({
    memberCode: { type: String, unique: true },
    firstName: { type: String, required: true },
    lastName: { type: String, required: true },
    email: { type: String, unique: true, sparse: true },
    phoneNumber: { type: String, required: true, unique: true },
    password: { type: String, required: true },
    icNumber: { type: String },
    birthdate: { type: Date },
    gender: { type: String }, // Male, Female
    
    // Referrer
    referrer: { type: mongoose.Schema.Types.ObjectId, ref: 'Member' },
    referralType: { type: Number }, // 1-Direct, etc.
    referralAmount: { type: Number, default: 0 },

    // Career Detail
    companyName: { type: String },
    companyType: { type: Number, default: 1 }, // 1-Government, 2-Private
    occupation: { type: String },
    salary: { type: Number },
    retirementAge: { type: Number },

    // Address
    streetAddress1: { type: String },
    streetAddress2: { type: String },
    city: { type: String },
    state: { type: String },
    postcode: { type: String },
    country: { type: String, default: 'Malaysia' },

    // Bank Detail
    bankName: { type: String },
    bankAccountName: { type: String },
    bankAccountNumber: { type: String },

    // Wallet
    walletCash: { type: Number, default: 0 },
    walletPoint: { type: Number, default: 0 },
    lastUpdateWalletCash: { type: Date },
    lastUpdateWalletPoint: { type: Date },

    // Role & Status
    memberType: { type: Number, default: 1 }, // 1-Member, 2-Agent, 3-Hero
    status: { type: Number, default: 1 }, // 1-WaitingApproval, 2-Active, 3-Inactive
    
    profileImage: { type: String }, // File path or ID
    icImage: { type: String },

    createDate: { type: Date, default: Date.now },
    lastLogin: { type: Date }
});

// Generate memberCode before saving
MemberSchema.pre('save', async function() {
    if (this.isNew && !this.memberCode) {
        try {
            // Find the member with the highest numeric memberCode
            // Lexicographical sort fails when mixed with alphabetic prefixes (e.g. "CSA001" > "10001")
            // We filter for numeric-only codes and use numericOrdering collation for correct sorting.
            const lastMember = await this.constructor.findOne(
                { memberCode: { $regex: /^\d+$/ } }, 
                { memberCode: 1 }, 
                { 
                    sort: { memberCode: -1 },
                    collation: { locale: 'en_US', numericOrdering: true }
                }
            );
            
            let nextCode = 10001; // Default starting code
            if (lastMember && lastMember.memberCode) {
                const lastCode = parseInt(lastMember.memberCode);
                if (!isNaN(lastCode)) {
                    nextCode = lastCode + 1;
                }
            }
            this.memberCode = nextCode.toString();
        } catch (err) {
            console.error('Error generating memberCode:', err);
            throw err;
        }
    }
});

// Compare password
MemberSchema.methods.comparePassword = async function(candidatePassword) {
    return candidatePassword === this.password;
};

module.exports = mongoose.model('Member', MemberSchema);
