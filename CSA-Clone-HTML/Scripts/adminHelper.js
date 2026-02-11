async function initAdminPortal() {
    const token = localStorage.getItem('token');
    const role = localStorage.getItem('userRole');

    if (!token || role !== 'admin') {
        window.location.href = 'index.html';
        return;
    }

    try {
        // We could add a verify token endpoint if needed
        // For now, we trust the token stored
        
        // Update Header UI (We might need an api/admin/me endpoint)
        // $('.user-name-text').text('Admin USER');

        // Handle Logout
        $('.dropdown-item:contains("Logout")').on('click', function(e) {
            e.preventDefault();
            localStorage.removeItem('token');
            localStorage.removeItem('userRole');
            window.location.href = 'index.html';
        });

    } catch (err) {
        console.error('Error initializing admin portal:', err);
    }
}

$(document).ready(function() {
    initAdminPortal();
});

async function apiCall(endpoint, method = 'GET', body = null) {
    const token = localStorage.getItem('token');
    const options = {
        method,
        headers: {
            'Content-Type': 'application/json',
            'x-auth-token': token
        }
    };
    if (body) options.body = JSON.stringify(body);

    const response = await fetch(window.API_BASE_URL + '/api' + endpoint, options);
    if (response.status === 401 || response.status === 403) {
        localStorage.removeItem('token');
        window.location.href = 'index.html';
        return;
    }
    return await response.json();
}
