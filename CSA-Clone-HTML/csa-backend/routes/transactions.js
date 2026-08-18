const express = require("express");
const router = express.Router();
const auth = require("../middleware/auth");
const Member = require("../models/Member");
const Transaction = require("../models/Transaction");

// Middleware to check if user is admin or subadmin
const adminOrSubadmin = (req, res, next) => {
  if (req.user.role !== "admin" && req.user.role !== "subadmin") {
    return res.status(403).json({ msg: "Access denied" });
  }
  next();
};

// @route    GET api/admin/transactions
// @desc     Get all transactions (with member and admin details)
// @access   Private (Admin/Subadmin)
router.get("/", [auth, adminOrSubadmin], async (req, res) => {
  try {
    const transactions = await Transaction.find()
      .populate("member", "fullName memberCode phoneNumber walletCash walletPoint memberType")
      .populate("admin", "name email")
      .sort({ createDate: -1 });

    res.json(transactions);
  } catch (err) {
    console.error("Error fetching transactions:", err.message);
    res.status(500).send("Server Error");
  }
});

// @route    POST api/admin/transactions
// @desc     Admin manually add a transaction (Credit / Debit)
// @access   Private (Admin/Subadmin)
router.post("/", [auth, adminOrSubadmin], async (req, res) => {
  const { memberId, type, action, amount, currencyType, description } = req.body;

  if (!memberId) {
    return res.status(400).json({ msg: "Member is required" });
  }

  const parsedAmount = Number(amount);
  if (isNaN(parsedAmount) || parsedAmount <= 0) {
    return res.status(400).json({ msg: "Please enter a valid amount greater than 0" });
  }

  const validActions = ["credit", "debit"];
  if (!validActions.includes(action)) {
    return res.status(400).json({ msg: "Action must be either 'credit' or 'debit'" });
  }

  const validCurrencies = ["Cash", "Point"];
  const targetCurrency = currencyType || "Cash";
  if (!validCurrencies.includes(targetCurrency)) {
    return res.status(400).json({ msg: "Invalid currency type" });
  }

  const validTypes = ["Reward", "Referral", "Withdrawal", "Bonus", "Adjustment"];
  const targetType = type || "Adjustment";
  if (!validTypes.includes(targetType)) {
    return res.status(400).json({ msg: "Invalid transaction type" });
  }

  try {
    const member = await Member.findById(memberId);
    if (!member) {
      return res.status(404).json({ msg: "Member not found" });
    }

    const isDebit = action === "debit";
    const signedAmount = isDebit ? -parsedAmount : parsedAmount;

    // Check balance if debiting
    if (isDebit) {
      if (targetCurrency === "Cash") {
        const currentBalance = Number(member.walletCash) || 0;
        if (currentBalance < parsedAmount) {
          return res.status(400).json({
            msg: `Insufficient cash balance. Available: RM ${currentBalance.toFixed(2)}`,
          });
        }
      } else if (targetCurrency === "Point") {
        const currentPoints = Number(member.walletPoint) || 0;
        if (currentPoints < parsedAmount) {
          return res.status(400).json({
            msg: `Insufficient point balance. Available: ${currentPoints} points`,
          });
        }
      }
    }

    // Apply wallet adjustment
    if (targetCurrency === "Cash") {
      member.walletCash = (Number(member.walletCash) || 0) + signedAmount;
      member.lastUpdateWalletCash = Date.now();
    } else if (targetCurrency === "Point") {
      member.walletPoint = (Number(member.walletPoint) || 0) + signedAmount;
      member.lastUpdateWalletPoint = Date.now();
    }

    await member.save();

    // Create transaction record
    const transaction = new Transaction({
      member: member._id,
      admin: req.user.id,
      type: targetType,
      amount: signedAmount,
      currencyType: targetCurrency,
      description: description || "Admin Manual Adjustment",
      status: "Completed",
      processDate: Date.now(),
    });

    await transaction.save();

    const populatedTransaction = await Transaction.findById(transaction._id)
      .populate("member", "fullName memberCode phoneNumber walletCash walletPoint memberType")
      .populate("admin", "name email");

    res.json({
      msg: "Transaction created successfully",
      transaction: populatedTransaction,
      member: {
        _id: member._id,
        fullName: member.fullName,
        memberCode: member.memberCode,
        walletCash: member.walletCash,
        walletPoint: member.walletPoint,
      },
    });
  } catch (err) {
    console.error("Error creating transaction:", err.message);
    res.status(500).send("Server Error");
  }
});

module.exports = router;
