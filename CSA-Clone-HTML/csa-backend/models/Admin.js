const mongoose = require('mongoose');
const bcrypt = require('bcryptjs');

const AdminSchema = new mongoose.Schema({
    name: { type: String, required: true },
    email: { type: String, required: true, unique: true },
    password: { type: String, required: true },
    isSuperAdmin: { type: Boolean, default: false },
    role: { type: String, default: 'Moderator' }, // Can be linked to a Role model later
    
    status: { type: Number, default: 1 }, // 1-Active, 2-Inactive
    lastLogin: { type: Date },
    createDate: { type: Date, default: Date.now }
});

// Compare password
AdminSchema.methods.comparePassword = async function(candidatePassword) {
    return candidatePassword === this.password;
};

module.exports = mongoose.model('Admin', AdminSchema);
