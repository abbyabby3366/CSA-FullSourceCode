const express = require("express");
const router = express.Router();
const auth = require("../middleware/auth");
const upload = require("../middleware/upload");
const Carousel = require("../models/Carousel");

// Middleware to check if user is admin
const adminOnly = (req, res, next) => {
  if (req.user.role !== "admin") {
    return res.status(403).json({ msg: "Access denied: Admin only" });
  }
  next();
};

// @route    GET api/carousel
// @desc     Get all carousel images
// @access   Public
router.get("/", async (req, res) => {
  try {
    const images = await Carousel.find().sort({ order: 1, createdAt: -1 });
    res.json(images);
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

// @route    POST api/carousel
// @desc     Upload a new carousel image
// @access   Private (Admin only)
router.post(
  "/",
  [auth, adminOnly, upload.single("image")],
  async (req, res) => {
    try {
      if (!req.file) {
        return res.status(400).json({ msg: "Please upload an image" });
      }

      const { title, order } = req.body;

      const baseUrl = `https://${process.env.S3_BUCKET_NAME}`;
      const newCarousel = new Carousel({
        imageUrl: `${baseUrl}/${req.file.key}`, // Manually construct URL with custom domain
        title,
        order: order || 0,
      });

      const carousel = await newCarousel.save();
      res.json(carousel);
    } catch (err) {
      console.error(err.message);
      res.status(500).send("Server Error");
    }
  },
);

// @route    DELETE api/carousel/:id
// @desc     Delete a carousel image
// @access   Private (Admin only)
router.delete("/:id", [auth, adminOnly], async (req, res) => {
  try {
    const carousel = await Carousel.findById(req.params.id);
    if (!carousel) {
      return res.status(404).json({ msg: "Carousel image not found" });
    }

    await Carousel.findByIdAndDelete(req.params.id);
    res.json({ msg: "Carousel image removed" });
  } catch (err) {
    console.error(err.message);
    if (err.kind === "ObjectId") {
      return res.status(404).json({ msg: "Carousel image not found" });
    }
    res.status(500).send("Server Error");
  }
});

// @route    PUT api/carousel/reorder
// @desc     Update carousel image orders
// @access   Private (Admin only)
router.put("/reorder", [auth, adminOnly], async (req, res) => {
  const { orders } = req.body; // Array of { id, order }
  try {
    const promises = orders.map((item) =>
      Carousel.findByIdAndUpdate(item.id, { order: item.order }),
    );
    await Promise.all(promises);
    res.json({ msg: "Order updated" });
  } catch (err) {
    console.error(err.message);
    res.status(500).send("Server Error");
  }
});

module.exports = router;
