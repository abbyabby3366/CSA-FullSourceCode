const mongoose = require("mongoose");

const TacSchema = new mongoose.Schema({
  phoneNumber: {
    type: String,
    required: true,
    unique: true,
  },
  code: {
    type: String,
    required: true,
  },
  createdAt: {
    type: Date,
    default: Date.now,
    expires: 300, // 5 minutes TTL
  },
});

module.exports = mongoose.model("Tac", TacSchema);
