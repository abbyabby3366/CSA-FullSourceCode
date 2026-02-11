function renderNavBar() {
    const currentPage = window.location.pathname.split("/").pop() || 'index.html';

    const headerHTML = `
        <div class="layout-width">
            <div class="navbar-header">
                <div class="d-flex">
                    <!-- logo -->
                    <div class="navbar-brand-box horizontal-logo">
                        <a href="dashboard.html" class="logo logo-dark">
                            <span class="logo-sm">
                                <img src="../assets/images/logos/logo-main.png" alt="" height="22">
                            </span>
                            <span class="logo-lg">
                                <img src="../assets/images/logos/logo-dark.png" alt="" height="37">
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
                    <!-- Member Portal Toggle -->
                    <div class="ms-1 header-item d-none d-sm-flex">
                        <a href="../member/dashboard.html" class="btn btn-icon btn-topbar btn-ghost-secondary rounded-circle" title="Switch to Member Portal">
                            <i class='ri-user-shared-line fs-22'></i>
                        </a>
                    </div>

                    <div class="dropdown ms-sm-1 header-item topbar-user">
                        <button type="button" class="btn" id="page-header-user-dropdown" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            <span class="d-flex align-items-center">
                                <img class="rounded-circle header-profile-user" src="../assets/images/users/avatar-1.jpg" alt="Header Avatar">
                                <span class="text-start ms-xl-2">
                                    <span class="d-none d-xl-inline-block ms-1 fw-medium user-name-text">Admin User</span>
                                    <span class="d-none d-xl-block ms-1 fs-12 user-name-sub-text">Super Admin</span>
                                </span>
                            </span>
                        </button>

                        <div class="dropdown-menu dropdown-menu-end">
                            <h6 class="dropdown-header">Welcome Admin!</h6>
                            <a class="dropdown-item" href="profile.html"><i class="mdi mdi-account-circle text-muted fs-16 align-middle me-1"></i> <span class="align-middle">Profile</span></a>
                            <div class="dropdown-divider"></div>
                            <a class="dropdown-item logout-admin" href="#"><i class="mdi mdi-logout text-muted fs-16 align-middle me-1"></i> <span class="align-middle">Logout</span></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `;

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
                    <img src="../assets/images/logos/logo-main.png" alt="" height="22">
                </span>
                <span class="logo-lg">
                    <img src="../assets/images/logos/logo-dark.png" alt="" height="37">
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
                    <li class="menu-title"><span data-key="t-main">Main</span></li>

                    <li class="nav-item">
                        <a class="nav-link menu-link ${currentPage === 'dashboard.html' ? 'active' : ''}" href="dashboard.html">
                            <i class="ri-home-8-line"></i> <span data-key="t-dashboard"> Dashboards </span>
                        </a>
                    </li>

                    <li class="nav-item">
                        <a class="nav-link menu-link ${currentPage === 'applications.html' ? 'active' : ''}" href="applications.html">
                            <i class="mdi mdi-application-cog-outline"></i> <span data-key="t-application-manager"> Application Management </span>
                        </a>
                    </li>

                    <li class="nav-item">
                        <a class="nav-link menu-link ${currentPage === 'surveys.html' ? 'active' : ''}" href="surveys.html">
                            <i class="ri-survey-line"></i> <span data-key="t-surveys"> Submitted Surveys </span>
                        </a>
                    </li>

                    <li class="nav-item">
                        <a class="nav-link menu-link ${['member-approval.html', 'clients.html', 'agents.html'].includes(currentPage) ? 'active' : ''}" href="#member-manager" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="member-manager">
                            <i class="ri-file-user-line"></i> <span data-key="t-member-manager"> Client Registration & Management </span>
                        </a>
                        <div class="collapse menu-dropdown ${['member-approval.html', 'clients.html', 'agents.html'].includes(currentPage) ? 'show' : ''}" id="member-manager">
                            <ul class="nav nav-sm flex-column">
                                <li class="nav-item">
                                    <a href="member-approval.html" class="nav-link ${currentPage === 'member-approval.html' ? 'active' : ''}"> Registration </a>
                                </li>
                                <li class="nav-item">
                                    <a href="clients.html" class="nav-link ${currentPage === 'clients.html' ? 'active' : ''}"> Client List </a>
                                </li>
                                <li class="nav-item">
                                    <a href="agents.html" class="nav-link ${currentPage === 'agents.html' ? 'active' : ''}"> Agent List </a>
                                </li>
                            </ul>
                        </div>
                    </li>

                    <li class="nav-item">
                        <a class="nav-link menu-link ${['withdrawals.html', 'finance-history.html'].includes(currentPage) ? 'active' : ''}" href="#finance" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="finance">
                            <i class="ri-money-dollar-circle-line"></i> <span data-key="t-finance"> Finance </span>
                        </a>
                        <div class="collapse menu-dropdown ${['withdrawals.html', 'finance-history.html'].includes(currentPage) ? 'show' : ''}" id="finance">
                            <ul class="nav nav-sm flex-column">
                                <li class="nav-item">
                                    <a href="withdrawals.html" class="nav-link ${currentPage === 'withdrawals.html' ? 'active' : ''}"> Withdrawal Requests </a>
                                </li>
                                <li class="nav-item">
                                    <a href="finance-history.html" class="nav-link ${currentPage === 'finance-history.html' ? 'active' : ''}"> Transaction History </a>
                                </li>
                            </ul>
                        </div>
                    </li>

                    <li class="nav-item">
                        <a class="nav-link menu-link ${currentPage === 'admins.html' ? 'active' : ''}" href="admins.html">
                            <i class="ri-file-user-line"></i> <span data-key="t-application-manager"> Admin Management </span>
                        </a>
                    </li>

                    <li class="menu-title"><span data-key="t-marketing">Marketing</span></li>
                    <li class="nav-item">
                        <a class="nav-link menu-link ${currentPage === 'email-campaigns.html' ? 'active' : ''}" href="email-campaigns.html">
                            <i class="ri-mail-line"></i> <span data-key="t-campaigns"> Email Campaigns </span>
                        </a>
                    </li>

                    <li class="menu-title"><span data-key="t-configurations">Configurations</span></li>
                    <li class="nav-item">
                        <a class="nav-link menu-link ${currentPage === 'roles.html' ? 'active' : ''}" href="roles.html">
                            <i class="ri-user-settings-line"></i> <span data-key="t-roles"> Role & privileges </span>
                        </a>
                    </li>

                    <li class="nav-item">
                        <a class="nav-link menu-link ${currentPage === 'settings.html' ? 'active' : ''}" href="settings.html">
                            <i class="ri-settings-2-line"></i> <span data-key="t-profile-manager"> Settings </span>
                        </a>
                    </li>
                </ul>
            </div>
        </div>

        <div class="sidebar-background"></div>
    `;

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
    $('.logout-admin').on('click', function(e) {
        e.preventDefault();
        localStorage.removeItem('token');
        localStorage.removeItem('userRole');
        window.location.href = 'index.html';
    });
}

// Ensure it runs after DOM is ready
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', renderNavBar);
} else {
    renderNavBar();
}
