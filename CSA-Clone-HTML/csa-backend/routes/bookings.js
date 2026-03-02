const express = require("express");
const router = express.Router();
const auth = require("../middleware/auth");
const SurveyBooking = require("../models/SurveyBooking");
const Application = require("../models/Application");

// @route    GET api/bookings/my
// @desc     Get current user's active booking
// @access   Private
router.get("/my", auth, async (req, res) => {
  try {
    const booking = await SurveyBooking.findOne({
      member: req.user.id,
      status: "Confirmed",
    });
    res.json(booking);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    POST api/bookings
// @desc     Create a new survey booking
// @access   Private
router.post("/", auth, async (req, res) => {
  const { bookingDate, timeSlot } = req.body;

  try {
    // 1. Check if user has an approved application
    const app = await Application.findOne({
      member: req.user.id,
      applicationStatus: 6,
    });
    if (!app) {
      return res
        .status(403)
        .json({ msg: "Only approved members can book surveys" });
    }

    // 2. Check if user already has an active booking
    const existing = await SurveyBooking.findOne({
      member: req.user.id,
      status: "Confirmed",
    });
    if (existing) {
      return res
        .status(400)
        .json({ msg: "You already have an active booking" });
    }

    // 3. Validate booking date (Mon-Fri)
    const date = new Date(bookingDate);
    const day = date.getDay();
    if (day === 0 || day === 6) {
      return res
        .status(400)
        .json({ msg: "Bookings are only available Monday to Friday" });
    }

    // 4. Validate time slot
    if (!["12-2pm", "5-7pm"].includes(timeSlot)) {
      return res.status(400).json({ msg: "Invalid time slot" });
    }

    const newBooking = new SurveyBooking({
      member: req.user.id,
      bookingDate: date,
      timeSlot,
    });

    const booking = await newBooking.save();
    res.json(booking);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    PUT api/bookings/:id
// @desc     Update a survey booking
// @access   Private
router.put("/:id", auth, async (req, res) => {
  const { bookingDate, timeSlot } = req.body;

  try {
    let booking = await SurveyBooking.findById(req.params.id);
    if (!booking) return res.status(404).json({ msg: "Booking not found" });

    // Make sure user owns booking
    if (booking.member.toString() !== req.user.id) {
      return res.status(401).json({ msg: "Not authorized" });
    }

    // Validate date (Mon-Fri)
    if (bookingDate) {
      const date = new Date(bookingDate);
      const day = date.getDay();
      if (day === 0 || day === 6) {
        return res
          .status(400)
          .json({ msg: "Bookings are only available Monday to Friday" });
      }
      booking.bookingDate = date;
    }

    if (timeSlot) {
      if (!["12-2pm", "5-7pm"].includes(timeSlot)) {
        return res.status(400).json({ msg: "Invalid time slot" });
      }
      booking.timeSlot = timeSlot;
    }

    booking.lastUpdate = Date.now();
    await booking.save();
    res.json(booking);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    DELETE api/bookings/:id
// @desc     Cancel/Delete a survey booking
// @access   Private
router.delete("/:id", auth, async (req, res) => {
  try {
    let booking = await SurveyBooking.findById(req.params.id);
    if (!booking) return res.status(404).json({ msg: "Booking not found" });

    if (booking.member.toString() !== req.user.id) {
      return res.status(401).json({ msg: "Not authorized" });
    }

    // Instead of actual delete, we can mark as Cancelled or just delete
    // User said "cancel the booking", let's delete for simplicity of "one booking at one time"
    await SurveyBooking.findByIdAndDelete(req.params.id);
    res.json({ msg: "Booking cancelled" });
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

module.exports = router;
