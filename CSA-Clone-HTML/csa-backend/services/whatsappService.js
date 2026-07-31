const fetch = globalThis.fetch || require("node-fetch");

/**
 * Send WhatsApp OTP via VerifyWay 3rd Party API
 * @param {string} recipient - Phone number with country code (e.g. 60128817823)
 * @param {string|number} code - The OTP code to send
 * @returns {Promise<{success: boolean, data?: any, error?: string}>}
 */
async function sendWhatsAppOTP(recipient, code) {
  try {
    if (!recipient) {
      return { success: false, error: "Recipient phone number is required" };
    }
    if (!code) {
      return { success: false, error: "OTP code is required" };
    }

    const cleanPhone = String(recipient).replace(/[^\d]/g, "");
    const apiUrl = process.env.VERIFYWAY_API_URL || "https://api.verifyway.com/api/v1/";
    const apiToken = process.env.VERIFYWAY_API_TOKEN;

    if (!apiToken) {
      console.error("❌ VERIFYWAY_API_TOKEN is missing in environment configuration.");
      return { success: false, error: "VerifyWay API token not configured" };
    }

    console.log(`📱 Sending WhatsApp OTP to ${cleanPhone} via VerifyWay...`);

    const response = await fetch(apiUrl, {
      method: "POST",
      headers: {
        "Authorization": `Bearer ${apiToken}`,
        "Content-Type": "application/json",
        "Accept": "application/json",
      },
      body: JSON.stringify({
        recipient: cleanPhone,
        type: "otp",
        code: String(code),
        channel: "whatsapp",
      }),
    });

    const data = await response.json().catch(() => null);

    if (response.ok) {
      console.log(`✅ WhatsApp OTP sent successfully to ${cleanPhone}:`, data);
      return { success: true, data };
    } else {
      console.error(`❌ VerifyWay API Error (${response.status}):`, data);
      return {
        success: false,
        error: data?.message || data?.error || `VerifyWay API returned status ${response.status}`,
      };
    }
  } catch (error) {
    console.error("❌ Error in sendWhatsAppOTP:", error.message);
    return { success: false, error: error.message };
  }
}

/**
 * Send custom WhatsApp message / notification
 * @param {string} recipient - Phone number
 * @param {string} message - Message text
 * @returns {Promise<{success: boolean, data?: any, error?: string}>}
 */
async function sendWhatsAppMessage(recipient, message) {
  try {
    if (!recipient || !message) {
      return { success: false, error: "Recipient and message are required" };
    }

    const cleanPhone = String(recipient).replace(/[^\d]/g, "");
    const apiUrl = process.env.VERIFYWAY_API_URL || "https://api.verifyway.com/api/v1/";
    const apiToken = process.env.VERIFYWAY_API_TOKEN;

    if (!apiToken) {
      console.error("❌ VERIFYWAY_API_TOKEN is missing in environment configuration.");
      return { success: false, error: "VerifyWay API token not configured" };
    }

    console.log(`📱 Sending WhatsApp message to ${cleanPhone} via VerifyWay...`);

    const response = await fetch(apiUrl, {
      method: "POST",
      headers: {
        "Authorization": `Bearer ${apiToken}`,
        "Content-Type": "application/json",
        "Accept": "application/json",
      },
      body: JSON.stringify({
        recipient: cleanPhone,
        type: "otp",
        code: message,
        channel: "whatsapp",
      }),
    });

    const data = await response.json().catch(() => null);

    if (response.ok) {
      console.log(`✅ WhatsApp message sent successfully to ${cleanPhone}`);
      return { success: true, data };
    } else {
      console.error(`❌ VerifyWay API Error (${response.status}):`, data);
      return {
        success: false,
        error: data?.message || data?.error || `VerifyWay API returned status ${response.status}`,
      };
    }
  } catch (error) {
    console.error("❌ Error in sendWhatsAppMessage:", error.message);
    return { success: false, error: error.message };
  }
}

module.exports = {
  sendWhatsAppOTP,
  sendWhatsAppMessage,
};
