function renderMemberNavBar() {
    const currentPage = window.location.pathname.split("/").pop() || 'index.html';

    const headerHTML = `
            <div class="layout-width">
                <div class="navbar-header">
                    <div class="d-flex">
                        <!-- logo -->
                        <div class="navbar-brand-box horizontal-logo">
                            <a href="dashboard.html" class="logo logo-dark">
                                <span class="logo-sm">
                                    <img src="../assets/images/logos/yabam-logo-dark.png" alt="" height="22">
                                </span>
                                <span class="logo-lg">
                                    <img src="../assets/images/logos/yabam-logo-dark.png" alt="" height="37">
                                </span>
                            </a>

                            <a href="dashboard.html" class="logo logo-light">
                                <span class="logo-sm">
                                    <img src="../assets/images/logos/logo-main.png" alt="" height="22">
                                </span>
                                <span class="logo-lg">
                                    <img src="../assets/images/logos/logo-light.png" alt="" height="37">
                                </span>
                            </a>
                        </div>

                        <button type="button" class="btn btn-sm px-3 fs-16 header-item vertical-menu-btn topnav-hamburger" id="topnav-hamburger-icon">
                            <span class="hamburger-icon">
                                <span></span>
                                <span></span>
                                <span></span>
                            </span>
                        </button>
                    </div>

                    <div class="d-flex align-items-center">
                        <!-- Admin Portal Toggle -->
                        <div class="ms-1 header-item d-none d-sm-flex">
                            <a href="../dashboard.html" class="btn btn-icon btn-topbar btn-ghost-secondary rounded-circle" title="Switch to Admin Portal">
                                <i class='ri-settings-3-line fs-22'></i>
                            </a>
                        </div>

                        <div class="dropdown ms-sm-1 header-item topbar-user">
                            <button type="button" class="btn" id="page-header-user-dropdown" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <span class="d-flex align-items-center">
                                    <img class="rounded-circle header-profile-user" src="../assets/images/users/user-dummy-img.jpg" alt="Header Avatar">
                                    <span class="text-start ms-xl-2">
                                        <span class="d-none d-xl-inline-block ms-1 fw-medium user-name-text">Member User</span>
                                        <span class="d-none d-xl-block ms-1 fs-12 user-name-sub-text">Active Member</span>
                                    </span>
                                </span>
                            </button>

                            <div class="dropdown-menu dropdown-menu-end">
                                <h6 class="dropdown-header">Welcome Member!</h6>
                                <a class="dropdown-item" href="profile.html"><i class="mdi mdi-account-circle text-muted fs-16 align-middle me-1"></i> <span class="align-middle">Profile</span></a>
                                <div class="dropdown-divider"></div>
                                <a class="dropdown-item logout-member" href="#"><i class="mdi mdi-logout text-muted fs-16 align-middle me-1"></i> <span class="align-middle">Logout</span></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>`;

    const sidebarHTML = `
            <!-- logo -->
            <div class="navbar-brand-box">
                <!-- dark -->
                <a href="dashboard.html" class="logo logo-dark">
                    <span class="logo-sm">
                        <img src="../assets/images/logos/logo-main.png" alt="" height="22">
                    </span>
                    <span class="logo-lg">
                        <img src="../assets/images/logos/logo-light.png" alt="" height="37">
                    </span>
                </a>

                <!-- light -->
                <a href="dashboard.html" class="logo logo-light">
                    <span class="logo-sm">
                        <img src="../assets/images/logos/yabam-logo-dark.png" alt="" height="22">
                    </span>
                    <span class="logo-lg">
                        <img src="../assets/images/logos/yabam-logo-dark.png" alt="" height="37">
                    </span>
                </a>

                <button type="button" class="btn btn-sm p-0 fs-20 header-item float-end btn-vertical-sm-hover" id="vertical-hover">
                    <i class="ri-record-circle-line"></i>
                </button>
            </div>

            <div id="scrollbar">
                <div class="container-fluid">
                    <div id="two-column-menu"></div>

                    <ul class="navbar-nav" id="navbar-nav">
                        <li class="menu-title"><span data-key="t-menu">Menu</span></li>
                        <li class="nav-item">
                            <a class="nav-link menu-link \${currentPage === 'dashboard.html' ? 'active' : ''}" href="dashboard.html">
                                <i class="ri-home-8-line"></i><span data-key="t-dashboard">Dashboards </span>
                            </a>
                        </li>

                        <li class="nav-item">
                            <a class="nav-link menu-link \${currentPage === 'survey.html' || currentPage === 'survey-details.html' ? 'active' : ''}" href="#surveys" data-bs-toggle="collapse" role="button" aria-expanded="\${currentPage === 'survey.html' || currentPage === 'survey-details.html' ? 'true' : 'false'}" aria-controls="surveys">
                                <i class="ri-survey-line"></i><span data-key="t-surveys">Surveys </span>
                            </a>
                            <div class="collapse menu-dropdown \${currentPage === 'survey.html' || currentPage === 'survey-details.html' ? 'show' : ''}" id="surveys">
                                <ul class="nav nav-sm flex-column">
                                    <li class="nav-item">
                                        <a href="survey.html" class="nav-link \${currentPage === 'survey.html' ? 'active' : ''}">YABAM</a>
                                    </li>
                                </ul>
                            </div>
                        </li>

                        <li class="nav-item">
                            <a class="nav-link menu-link \${currentPage === 'apply.html' || currentPage === 'application-status.html' || currentPage === 'application-details.html' ? 'active' : ''}" href="#application-manager" data-bs-toggle="collapse" role="button" aria-expanded="\${currentPage === 'apply.html' || currentPage === 'application-status.html' || currentPage === 'application-details.html' ? 'true' : 'false'}" aria-controls="application-manager">
                                <i class="mdi mdi-file-document-edit-outline"></i><span data-key="t-application-manager">Application Manager </span>
                            </a>
                            <div class="collapse menu-dropdown \${currentPage === 'apply.html' || currentPage === 'application-status.html' || currentPage === 'application-details.html' ? 'show' : ''}" id="application-manager">
                                <ul class="nav nav-sm flex-column">
                                    <li class="nav-item">
                                        <a href="apply.html" class="nav-link \${currentPage === 'apply.html' ? 'active' : ''}">Apply Now </a>
                                    </li>
                                    <li class="nav-item">
                                        <a href="application-status.html" class="nav-link \${currentPage === 'application-status.html' ? 'active' : ''}">Application Status </a>
                                    </li>
                                </ul>
                            </div>
                        </li>

                        <li class="nav-item">
                            <a class="nav-link menu-link \${currentPage === 'referrals.html' || currentPage === 'become-agent.html' ? 'active' : ''}" href="#referral" data-bs-toggle="collapse" role="button" aria-expanded="\${currentPage === 'referrals.html' || currentPage === 'become-agent.html' ? 'true' : 'false'}" aria-controls="referral">
                                <i class="ri-user-add-line"></i><span data-key="t-referral">Peserta </span>
                            </a>
                            <div class="collapse menu-dropdown \${currentPage === 'referrals.html' || currentPage === 'become-agent.html' ? 'show' : ''}" id="referral">
                                <ul class="nav nav-sm flex-column">
                                    <li class="nav-item" data-role="agent-only" style="display: none;">
                                        <a href="referrals.html" class="nav-link \${currentPage === 'referrals.html' ? 'active' : ''}">Team Management </a>
                                    </li>
                                    <li class="nav-item" data-role="member-only">
                                        <a href="become-agent.html" class="nav-link \${currentPage === 'become-agent.html' ? 'active' : ''}">Menjadi Pegawai </a>
                                    </li>
                                </ul>
                            </div>
                        </li>

                        <li class="menu-title"><i class="ri-more-fill"></i><span data-key="t-user">User</span></li>

                        <li class="nav-item">
                            <a class="nav-link menu-link \${currentPage === 'profile.html' || currentPage === 'profile-management.html' ? 'active' : ''}" href="#user-profile" data-bs-toggle="collapse" role="button" aria-expanded="\${currentPage === 'profile.html' || currentPage === 'profile-management.html' ? 'true' : 'false'}" aria-controls="user-profile">
                                <i class="mdi mdi-card-account-details-outline"></i><span data-key="t-user-profile">My Profile </span>
                            </a>
                            <div class="collapse menu-dropdown \${currentPage === 'profile.html' || currentPage === 'profile-management.html' ? 'show' : ''}" id="user-profile">
                                <ul class="nav nav-sm flex-column">
                                    <li class="nav-item">
                                        <a href="profile.html" class="nav-link \${currentPage === 'profile.html' ? 'active' : ''}">User Profile </a>
                                    </li>
                                    <li class="nav-item">
                                        <a href="profile-management.html" class="nav-link \${currentPage === 'profile-management.html' ? 'active' : ''}">Profile Management </a>
                                    </li>
                                </ul>
                            </div>
                        </li>

                        <li class="nav-item">
                            <a class="nav-link menu-link \${currentPage === 'wallet.html' ? 'active' : ''}" href="wallet.html">
                                <i class="ri-wallet-line"></i><span >Ganjaran </span>
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="sidebar-background"></div>`;

    // Inject Header
    const topbar = document.getElementById('page-topbar');
    if (topbar) {
        topbar.innerHTML = headerHTML;
    }

    // Inject Sidebar
    const sideMenu = document.querySelector('.navbar-menu');
    if (sideMenu) {
        sideMenu.innerHTML = sidebarHTML;
    }

    // Handle Logout
    if (window.jQuery) {
        $('.logout-member').on('click', function(e) {
            e.preventDefault();
            localStorage.removeItem('token');
            localStorage.removeItem('userRole');
            window.location.href = 'index.html';
        });
    } else {
        document.querySelectorAll('.logout-member').forEach(el => {
            el.addEventListener('click', function(e) {
                e.preventDefault();
                localStorage.removeItem('token');
                localStorage.removeItem('userRole');
                window.location.href = 'index.html';
            });
        });
    }

    // Update Role visibility (based on memberHelper.js logic)
    const updateRoleMenu = () => {
        const userRole = localStorage.getItem('userRole');
        if (userRole === 'agent') {
            document.querySelectorAll('[data-role="agent-only"]').forEach(el => el.style.display = 'block');
            document.querySelectorAll('[data-role="member-only"]').forEach(el => el.style.display = 'none');
        } else {
            document.querySelectorAll('[data-role="agent-only"]').forEach(el => el.style.display = 'none');
            document.querySelectorAll('[data-role="member-only"]').forEach(el => el.style.display = 'block');
        }
    };
    updateRoleMenu();
}

// Run immediately - script is placed at the bottom of the body
renderMemberNavBar();
