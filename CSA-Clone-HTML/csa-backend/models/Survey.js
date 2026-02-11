const mongoose = require('mongoose');

const SurveySchema = new mongoose.Schema({
    member: {
        type: mongoose.Schema.Types.ObjectId,
        ref: 'Member',
        required: true
    },
    section1: {
        state: String,
        jobPositionCategory: String,
        jobPositionLevel: String,
        employerName: String
    },
    section2: {
        familyIncome: String,
        debtRange: String
    },
    finalSection: {
        favoriteGuru: String
    },
    bankDetails: {
        bankName: String,
        bankAccountNumber: String
    },
    status: {
        type: String,
        enum: ['Pending', 'Completed', 'Verified'],
        default: 'Completed'
    },
    submittedAt: {
        type: Date,
        default: Date.now
    }
});

module.exports = mongoose.model('Survey', SurveySchema);
