const mongoose = require("mongoose");

const TransactionSchema = new mongoose.Schema({
  member: {
    type: mongoose.Schema.Types.ObjectId,
    ref: "Member",
    required: true,
  },
  admin: { type: mongoose.Schema.Types.ObjectId, ref: "Admin" },

  type: {
    type: String,
    enum: ["Reward", "Referral", "Withdrawal", "Bonus", "Adjustment"],
    required: true,
  },
  amount: { type: Number, required: true },

  currencyType: { type: String, enum: ["Cash", "Point"], default: "Cash" },

  description: { type: String },
  status: {
    type: String,
    enum: ["Pending", "Completed", "Rejected"],
    default: "Pending",
  },

  createDate: { type: Date, default: Date.now },
  processDate: { type: Date },
  receiptUrl: { type: String },
});

module.exports = mongoose.model("Transaction", TransactionSchema);
