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

// ==========================================
// Global Name Casing Standardisation Helper
// ==========================================

function uppercaseNames(obj) {
    if (obj === null || obj === undefined) {
        return obj;
    }
    if (Array.isArray(obj)) {
        return obj.map(item => uppercaseNames(item));
    }
    if (typeof obj === 'object') {
        const result = {};
        for (const key in obj) {
            if (Object.prototype.hasOwnProperty.call(obj, key)) {
                const val = obj[key];
                if (typeof val === 'string' && 
                    ['fullName', 'bankAccountName', 'name', 'firstName', 'lastName', 'referredByName', 'agentName', 'memberName'].includes(key)) {
                    result[key] = val.toUpperCase();
                } else {
                    result[key] = uppercaseNames(val);
                }
            }
        }
        return result;
    }
    return obj;
}

// Intercept window.fetch to automatically uppercase name fields in JSON responses
const originalFetch = window.fetch;
window.fetch = async function(...args) {
    const response = await originalFetch(...args);
    // Overwrite the json() method on the response object
    const originalJson = response.json;
    response.json = async function() {
        const data = await originalJson.call(this);
        return uppercaseNames(data);
    };
    return response;
};

// Intercept jQuery AJAX and input elements to force uppercase names
function applyNameInterceptors() {
    // Intercept AJAX responses via dataFilter
    if (window.jQuery) {
        jQuery.ajaxSetup({
            dataFilter: function(data, type) {
                if (type === 'json' || !type) {
                    try {
                        const parsed = JSON.parse(data);
                        const transformed = uppercaseNames(parsed);
                        return JSON.stringify(transformed);
                    } catch (e) {}
                }
                return data;
            }
        });
        
        // Auto-detect and capitalize name input fields
        setupNameInputCasing();
    }
}

function setupNameInputCasing() {
    if (!window.jQuery) return;
    
    // Find all text inputs
    jQuery('input[type="text"], input:not([type]), textarea').each(function() {
        const input = jQuery(this);
        if (input.data('name-casing-setup')) return;
        
        let isName = false;
        
        // 1. Check ID
        const id = (input.attr('id') || '').toLowerCase();
        if (id && (id.includes('name') || id.includes('fullname'))) {
            if (!id.includes('company') && !id.includes('bankname') && !id.includes('employer')) {
                isName = true;
            }
        }
        
        // 2. Check placeholder
        const placeholder = (input.attr('placeholder') || '').toLowerCase();
        if (placeholder && placeholder.includes('name')) {
            if (!placeholder.includes('company') && !placeholder.includes('bank') && !placeholder.includes('employer')) {
                isName = true;
            }
        }
        
        // 3. Check labels associated with the input
        let labelText = '';
        const idAttr = input.attr('id');
        if (idAttr) {
            labelText = jQuery(`label[for="${idAttr}"]`).text();
        }
        if (!labelText) {
            labelText = input.closest('.mb-3, .col-lg-6, .col-md-6, .col-lg-12, .col-md-12').find('label').text() || 
                        input.prev('label').text() || 
                        input.parent('label').text();
        }
        
        if (labelText) {
            const s = labelText.toLowerCase();
            if ((s.includes('name') || s.includes('nric')) && 
                !s.includes('company') && 
                !s.includes('bank name') && 
                !s.includes('employer')) {
                isName = true;
            }
        }
        
        if (isName) {
            input.css('text-transform', 'uppercase');
            input.data('name-casing-setup', true);
            
            // Force the value to be uppercase
            input.on('input blur change', function() {
                if (this.value) {
                    this.value = this.value.toUpperCase();
                }
            });
        }
    });
}

// Initialise interceptors
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', applyNameInterceptors);
} else {
    applyNameInterceptors();
}

// Re-run input setup on potential dynamic DOM changes/clicks/focus
if (window.jQuery) {
    jQuery(document).on('focusin click shown.bs.modal', function() {
        setupNameInputCasing();
    });
} else {
    document.addEventListener('focusin', function() {
        setupNameInputCasing();
    });
    document.addEventListener('click', function() {
        setupNameInputCasing();
    });
}

