async function initMemberPortal() {
    const token = localStorage.getItem('token');
    if (!token) {
        window.location.href = 'index.html';
        return;
    }

    try {
        const response = await fetch('http://localhost:5000/api/members/me', {
            headers: {
                'x-auth-token': token
            }
        });

        if (response.status === 401) {
            localStorage.removeItem('token');
            window.location.href = 'index.html';
            return;
        }

        const member = await response.json();
        window.currentMember = member;

        // Apply role permissions
        applyRolePermissions(member.memberType);

        // Update Header UI
        $('.user-name-text').text(member.firstName + ' ' + member.lastName);
        $('.user-name-sub-text').text(member.memberType === 2 ? 'Agent' : 'Member');
        $('.dropdown-header').text('Welcome ' + member.firstName + '!');
        
        if (member.profileImage) {
            $('.header-profile-user').attr('src', 'http://localhost:5000/' + member.profileImage);
        }

        // Handle Logout
        $('.dropdown-item:contains("Logout")').on('click', function(e) {
            e.preventDefault();
            localStorage.removeItem('token');
            localStorage.removeItem('userRole');
            window.location.href = 'index.html';
        });

        return member;
    } catch (err) {
        console.error('Error initializing portal:', err);
    }
}

function applyRolePermissions(role) {
    // 1: Member, 2: Agent
    if (role === 2) {
        $('[data-role="agent-only"]').show();
        $('[data-role="member-only"]').hide();
    } else {
        $('[data-role="agent-only"]').hide();
        $('[data-role="member-only"]').show();
    }
}

$(document).ready(function() {
    initMemberPortal();
});
