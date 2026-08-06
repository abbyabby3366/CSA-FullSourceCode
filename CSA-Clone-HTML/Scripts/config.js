/**
 * Global Configuration & Error Handling System
 * Automatically detects the API Base URL based on where the site is hosted.
 */

const isLocalEnv = /^(localhost|127\.0\.0\.1|192\.168\.\d+\.\d+|10\.\d+\.\d+\.\d+|172\.(1[6-9]|2\d|3[01])\.\d+\.\d+)$/.test(window.location.hostname);
window.API_BASE_URL = (isLocalEnv && window.location.port !== '5000')
    ? `${window.location.protocol}//${window.location.hostname}:5000`
    : window.location.origin;

console.log('API Base URL automatically set to:', window.API_BASE_URL);

// ==========================================
// Global UI Error Display Helper & Fallback Modal
// ==========================================

window.showGlobalErrorModal = function(title, message) {
    const errorTitle = title || 'Error';
    const errorMsg = message || 'An unexpected error occurred.';

    // Option 1: Use page-specific #errorModal if present in DOM
    const existingModal = document.getElementById('errorModal');
    if (existingModal) {
        const msgNode = document.getElementById('errorModalMsg');
        const titleNode = document.getElementById('errorModalTitle');
        if (titleNode) titleNode.textContent = errorTitle;
        if (msgNode) {
            msgNode.innerHTML = `<strong>${errorTitle}</strong><br><span class="text-danger mt-1 d-block">${errorMsg}</span>`;
        }
        if (window.jQuery && window.jQuery.fn && window.jQuery.fn.modal) {
            window.jQuery('#errorModal').modal('show');
            return;
        } else {
            existingModal.style.display = 'block';
            existingModal.classList.add('show');
            return;
        }
    }

    // Option 2: Dynamically create & show fallback global error modal
    let modalEl = document.getElementById('globalDynamicErrorModal');
    if (!modalEl) {
        modalEl = document.createElement('div');
        modalEl.id = 'globalDynamicErrorModal';
        modalEl.className = 'modal fade';
        modalEl.setAttribute('tabindex', '-1');
        modalEl.setAttribute('aria-hidden', 'true');
        modalEl.style.zIndex = '10000';
        modalEl.innerHTML = `
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content text-center p-4" style="border-radius: 16px; border: none; box-shadow: 0 10px 30px rgba(0,0,0,0.25);">
                    <div class="modal-body">
                        <div class="mb-3">
                            <svg width="64" height="64" viewBox="0 0 24 24" fill="none" stroke="#dc3545" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                                <path d="M10.29 3.86L1.82 18a2 2 0 0 0 1.71 3h16.94a2 2 0 0 0 1.71-3L13.71 3.86a2 2 0 0 0-3.42 0z"></path>
                                <line x1="12" y1="9" x2="12" y2="13"></line>
                                <line x1="12" y1="17" x2="12.01" y2="17"></line>
                            </svg>
                        </div>
                        <h4 id="globalDynamicErrorTitle" class="fw-bold mb-2 text-dark">Error</h4>
                        <p id="globalDynamicErrorMsg" class="text-muted mx-3 mb-4"></p>
                        <button type="button" class="btn btn-danger px-5" style="border-radius: 25px;" id="globalDynamicErrorCloseBtn">Close</button>
                    </div>
                </div>
            </div>
        `;
        document.body.appendChild(modalEl);

        const closeBtn = document.getElementById('globalDynamicErrorCloseBtn');
        if (closeBtn) {
            closeBtn.addEventListener('click', function() {
                if (window.jQuery && window.jQuery.fn && window.jQuery.fn.modal) {
                    window.jQuery('#globalDynamicErrorModal').modal('hide');
                } else {
                    modalEl.style.display = 'none';
                    modalEl.classList.remove('show');
                }
            });
        }
    }

    const titleNode = document.getElementById('globalDynamicErrorTitle');
    const msgNode = document.getElementById('globalDynamicErrorMsg');
    if (titleNode) titleNode.textContent = errorTitle;
    if (msgNode) msgNode.innerHTML = typeof errorMsg === 'string' ? errorMsg : JSON.stringify(errorMsg);

    if (window.jQuery && window.jQuery.fn && window.jQuery.fn.modal) {
        window.jQuery('#globalDynamicErrorModal').modal('show');
    } else {
        modalEl.style.display = 'block';
        modalEl.classList.add('show');
    }
};

// Global uncaught JS error listener
window.addEventListener('error', function(event) {
    console.error('Uncaught JS Error:', event.error || event.message);
    if (event.message && event.message.includes('Script error')) return;
    window.showGlobalErrorModal('Application Error', event.message || 'An unexpected error occurred.');
});

// Global unhandled promise rejection listener
window.addEventListener('unhandledrejection', function(event) {
    console.error('Unhandled Promise Rejection:', event.reason);
    const msg = event.reason ? (event.reason.message || String(event.reason)) : 'Operation failed.';
    if (msg && msg.includes('Script error')) return;
    window.showGlobalErrorModal('Error', msg);
});

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

// Intercept window.fetch to automatically handle network errors & uppercase name fields in JSON responses
const originalFetch = window.fetch;
window.fetch = async function(...args) {
    let response;
    try {
        response = await originalFetch(...args);
    } catch (err) {
        console.error('Fetch Global Network Error:', err);
        const errMsg = err.message ? err.message : 'Failed to connect to server. Please check your network connection.';
        window.showGlobalErrorModal('Network / Connection Error', errMsg);
        throw err;
    }

    // Overwrite the json() method on the response object
    const originalJson = response.json;
    response.json = async function() {
        const data = await originalJson.call(this);
        return uppercaseNames(data);
    };
    return response;
};

// Intercept jQuery AJAX and input elements to force uppercase names and catch errors
function applyNameInterceptors() {
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
            },
            error: function(jqXHR, textStatus, errorThrown) {
                console.error('jQuery AJAX Global Error:', textStatus, errorThrown, jqXHR);
                let detailMsg = errorThrown || textStatus || 'Request failed';
                if (jqXHR.responseJSON && (jqXHR.responseJSON.msg || jqXHR.responseJSON.message)) {
                    detailMsg = jqXHR.responseJSON.msg || jqXHR.responseJSON.message;
                }
                window.showGlobalErrorModal(`Request Error (HTTP ${jqXHR.status || 0})`, detailMsg);
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

// ==========================================
// Global Footer Renderer Helper
// ==========================================
function renderGlobalFooter() {
  const footerEl = document.querySelector("footer.footer");
  if (footerEl) {
    footerEl.innerHTML = `
      <div class="container-fluid">
        <div class="row">
          <div class="col-sm-6 text-center text-sm-start mb-1 mb-sm-0">
            © ${new Date().getFullYear()} iBelanja - All rights reserved.
          </div>
          <div class="col-sm-6 text-center text-sm-end">
            <div>
              Design & Develop by Neurontech Trading
            </div>
          </div>
        </div>
      </div>
    `;
  }
}

if (document.readyState === "loading") {
  document.addEventListener("DOMContentLoaded", renderGlobalFooter);
} else {
  renderGlobalFooter();
}

