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

window.downloadFile = async function(url, filename) {
    try {
        const response = await fetch(url);
        if (!response.ok) throw new Error('Network response was not ok');
        const blob = await response.blob();
        const blobUrl = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = blobUrl;
        a.download = filename || url.substring(url.lastIndexOf('/') + 1);
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        window.URL.revokeObjectURL(blobUrl);
    } catch (error) {
        console.error('Download failed, falling back to new tab:', error);
        window.open(url, '_blank');
    }
};
