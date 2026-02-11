/**
 * Global Configuration
 * Detection for Local vs Production API
 */
const CONFIG = {
    // CHANGE THIS: Replace with your actual production backend URL (e.g., https://api.yourdomain.com)
    PRODUCTION_API_URL: 'https://YOUR_PRODUCTION_DOMAIN_HERE.com',
    
    LOCAL_API_URL: 'http://localhost:5000'
};

window.API_BASE_URL = (window.location.hostname === 'localhost' || window.location.hostname === '127.0.0.1')
    ? CONFIG.LOCAL_API_URL
    : CONFIG.PRODUCTION_API_URL;

console.log('API Base URL initialized as:', window.API_BASE_URL);
