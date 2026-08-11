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
      employmentStatus: { type: String },
      employerName: { type: String, required: true },
      jobTitle: { type: String, required: true },
      employmentState: { type: String },
      salaryRange: { type: String, required: true }, // Below 3k, 3001-5k, 5k and Above
      retirementAge: { type: Number },
    },
    icFrontFile: { type: String },
    icBackFile: { type: String },
    payslipFile: { type: String }, // Optional attachment but usually expected
    ctosConsentFile: { type: String },

    // Survey & Consent Questions
    maritalStatus: { type: String },
    partnerOccupation: { type: String },
    favoriteRestaurant: { type: String },
    monthlyFoodSpend: { type: String },
    interestFbFranchise: { type: String },
    declarationConsent: { type: Boolean, default: false },
    pdpaConsent: { type: Boolean, default: false },

    icFrontHistory: [
      {
        file: { type: String, required: true },
        uploadedAt: { type: Date, default: Date.now },
        uploadedBy: { type: String, default: "member" },
        note: { type: String },
      },
    ],
    icBackHistory: [
      {
        file: { type: String, required: true },
        uploadedAt: { type: Date, default: Date.now },
        uploadedBy: { type: String, default: "member" },
        note: { type: String },
      },
    ],
    payslipHistory: [
      {
        file: { type: String, required: true },
        uploadedAt: { type: Date, default: Date.now },
        uploadedBy: { type: String, default: "member" },
        note: { type: String },
      },
    ],
    ctosConsentHistory: [
      {
        file: { type: String, required: true },
        uploadedAt: { type: Date, default: Date.now },
        uploadedBy: { type: String, default: "member" },
        note: { type: String },
      },
    ],

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

  // Audit trail for admin/subadmin edits to application details
  editHistory: [
    {
      field: { type: String, required: true },
      fieldLabel: { type: String, required: true },
      oldValue: { type: String },
      newValue: { type: String },
      editedBy: { type: mongoose.Schema.Types.ObjectId, ref: "Admin" },
      editedByName: { type: String },
      editedByRole: { type: String },
      editedAt: { type: Date, default: Date.now },
    },
  ],

  rejection: {
    reason: { type: String },
    date: { type: Date },
    admin: { type: mongoose.Schema.Types.ObjectId, ref: "Admin" },
  },

  approval: {
    admin: { type: mongoose.Schema.Types.ObjectId, ref: "Admin" },
    date: { type: Date },
  },

  createDate: { type: Date, default: Date.now },
  lastUpdate: { type: Date, default: Date.now },
  rewardPaid: { type: Boolean, default: false },
});

module.exports = mongoose.model("Application", ApplicationSchema);
