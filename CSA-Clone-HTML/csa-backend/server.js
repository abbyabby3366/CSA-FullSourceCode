const express = require("express");
const mongoose = require("mongoose");
const cors = require("cors");
const bodyParser = require("body-parser");
const path = require("path");
require("dotenv").config({ path: path.resolve(__dirname, "../../.env") });
require("dotenv").config();

const app = express();

// Middleware
app.use(cors());
app.use(bodyParser.json());

// Public ping endpoint for uptime monitoring
app.get("/ping", (req, res) => {
  res.json({ status: "ok" });
});

// DB Connection
mongoose
  .connect(process.env.MONGODB_URI)
  .then(() => console.log("MongoDB Connected..."))
  .catch((err) => console.log("MongoDB Connection Error: ", err));

// Route to serve the Member login page at the root
app.get("/", (req, res) => {
  res.redirect("/member/index.html");
});

// Serve static files from the frontend directory (parent folder)
app.use(express.static(path.join(__dirname, "..")));

// Define Routes
app.use("/api/auth", require("./routes/auth"));
app.use("/api/members", require("./routes/members"));
app.use("/api/applications", require("./routes/applications"));
app.use("/api/admin", require("./routes/admin"));
app.use("/api/announcements", require("./routes/announcements"));
app.use("/api/carousel", require("./routes/carousel"));
app.use("/api/settings", require("./routes/settings"));
app.use("/api/bookings", require("./routes/bookings"));
app.use("/api/admin/transactions", require("./routes/transactions"));
app.use("/api/transactions", require("./routes/transactions"));

// Catch-all route for /admin/*: redirect all unmatched /admin/* page requests back to index.html
app.get(/^\/admin\//, (req, res) => {
  res.redirect("/index.html");
});

// Global Express Error Handling Middleware
app.use((err, req, res, next) => {
  console.error("Unhandled Backend Error:", err.stack || err);
  const statusCode = err.status || err.statusCode || 500;
  res.status(statusCode).json({
    msg: err.message || "An unexpected server error occurred."
  });
});

const PORT = process.env.PORT || 5000;

app.listen(PORT, () => {
  console.log(`Server started on port ${PORT}`);
});
