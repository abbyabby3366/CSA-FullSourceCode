/**
 * Global Configuration
 * Automatically detects the API Base URL based on where the site is hosted.
 */

// Use the current origin (e.g., https://yourdomain.com) as the base.
// If we are on localhost, we explicitly point to port 5000 for the backend.
window.API_BASE_URL = (window.location.hostname === 'localhost' || window.location.hostname === '127.0.0.1')
    ? 'http://localhost:5000'
    : window.location.origin;

console.log('API Base URL automatically set to:', window.API_BASE_URL);
