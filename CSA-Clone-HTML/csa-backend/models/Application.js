const mongoose = require("mongoose");

const ApplicationSchema = new mongoose.Schema({
  member: {
    type: mongoose.Schema.Types.ObjectId,
    ref: "Member",
    required: true,
  },
  applicationStatus: { type: Number, default: 0 }, // 0-Pre-checking, etc.
  customerStatus: { type: Number, default: 1 }, // 1-Eligible, 2-Burst

  // Referral info
  referrerMember: { type: mongoose.Schema.Types.ObjectId, ref: "Member" },

  // Admin assignments
  admins: {
    am: { type: mongoose.Schema.Types.ObjectId, ref: "Admin" },
    pfc: { type: mongoose.Schema.Types.ObjectId, ref: "Admin" },
    rm: { type: mongoose.Schema.Types.ObjectId, ref: "Admin" },
    um: { type: mongoose.Schema.Types.ObjectId, ref: "Admin" },
    pa: { type: mongoose.Schema.Types.ObjectId, ref: "Admin" },
  },

  // Detailed Application Sections (Simplified as per request)
  details: {
    programEvent: { type: String, required: false },
    fullName: { type: String, required: true },
    icNumber: { type: String, required: true },
    phoneNumber: { type: String, required: true },
    email: { type: String, required: true },
    employmentDetails: {
      employerName: { type: String, required: true },
      jobTitle: { type: String, required: true },
      salaryRange: { type: String, required: true }, // Below 3k, 3001-5k, 5k and Above
    },
    icFrontFile: { type: String },
    icBackFile: { type: String },
    payslipFile: { type: String }, // Optional attachment but usually expected

    ramciReport: { file: String, lastUpdate: Date },
    ccrisDocument: { file: String, lastUpdate: Date },
    eligibility: { status: Number, lastUpdate: Date },

    financials: {
      salaryGross: { type: Number },
      salaryDeduction: { type: Number },
      netIncome: { type: Number },
      commitmentOutstanding: { type: Number },
      commitmentInstallment: { type: Number },
    },

    loanStatus: {
      approvedAmount: { type: Number },
      approvedDate: { type: Date },
      signingDate: { type: Date },
      disbursementDate: { type: Date },
    },
  },

  rejection: {
    reason: { type: String },
    date: { type: Date },
    admin: { type: mongoose.Schema.Types.ObjectId, ref: "Admin" },
  },

  createDate: { type: Date, default: Date.now },
  lastUpdate: { type: Date, default: Date.now },
  rewardPaid: { type: Boolean, default: false },
});

module.exports = mongoose.model("Application", ApplicationSchema);
