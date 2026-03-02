const mongoose = require("mongoose");

const SurveyBookingSchema = new mongoose.Schema({
  member: {
    type: mongoose.Schema.Types.ObjectId,
    ref: "Member",
    required: true,
  },
  bookingDate: {
    type: Date,
    required: true,
  },
  timeSlot: {
    type: String,
    enum: ["12-2pm", "5-7pm"],
    required: true,
  },
  status: {
    type: String,
    enum: ["Confirmed", "Cancelled"],
    default: "Confirmed",
  },
  createDate: {
    type: Date,
    default: Date.now,
  },
  lastUpdate: {
    type: Date,
    default: Date.now,
  },
});

module.exports = mongoose.model("SurveyBooking", SurveyBookingSchema);
