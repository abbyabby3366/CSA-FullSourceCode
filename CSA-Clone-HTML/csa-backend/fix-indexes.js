const mongoose = require("mongoose");
require("dotenv").config();

const fixIndexes = async () => {
  try {
    const uri = process.env.MONGODB_URI;
    if (!uri) {
      console.error("MONGODB_URI not found in .env");
      process.exit(1);
    }

    await mongoose.connect(uri);
    console.log("Connected to MongoDB");

    const collection = mongoose.connection.collection("members");

    // Drop the old single-field unique indexes that prevent dual-role registration
    try {
      await collection.dropIndex("phoneNumber_1");
      console.log("Successfully dropped phoneNumber_1 index");
    } catch (e) {
      console.log("phoneNumber_1 index not found or already dropped");
    }

    try {
      await collection.dropIndex("email_1");
      console.log("Successfully dropped email_1 index");
    } catch (e) {
      console.log("email_1 index not found or already dropped");
    }

    try {
      await collection.dropIndex("email_1_memberType_1");
      console.log("Successfully dropped email_1_memberType_1 index");
    } catch (e) {
      console.log("email_1_memberType_1 index not found or already dropped");
    }

    console.log("Index synchronization complete.");
    process.exit(0);
  } catch (err) {
    console.error("Error fixing indexes:", err);
    process.exit(1);
  }
};

fixIndexes();
