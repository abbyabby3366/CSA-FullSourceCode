function renderMemberNavBar() {
  const currentPage = window.location.pathname.split("/").pop() || "index.html";

  const headerHTML = `
            <div class="layout-width">
                <div class="navbar-header">
                    <div class="d-flex">
                        <!-- logo -->
                        <div class="navbar-brand-box horizontal-logo">
                            <a href="dashboard.html" class="logo logo-dark">
                                <span class="logo-sm">
                                    <img src="../assets/images/logos/yabam-logo-dark.png" alt="" height="40">
                                </span>
                                <span class="logo-lg">
                                    <img src="../assets/images/logos/yabam-logo-dark.png" alt="" height="60">
                                </span>
                            </a>

                            <a href="dashboard.html" class="logo logo-light">
                                <span class="logo-sm">
                                    <img src="../assets/images/logos/logo-main.png" alt="" height="40">
                                </span>
                                <span class="logo-lg">
                                    <img src="../assets/images/logos/logo-light.png" alt="" height="60">
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
                        <img src="../assets/images/logos/logo-main.png" alt="" height="40">
                    </span>
                    <span class="logo-lg">
                        <img src="../assets/images/logos/logo-light.png" alt="" height="60">
                    </span>
                </a>

                <!-- light -->
                <a href="dashboard.html" class="logo logo-light">
                    <span class="logo-sm">
                        <img src="../assets/images/logos/yabam-logo-dark.png" alt="" height="40">
                    </span>
                    <span class="logo-lg">
                        <img src="../assets/images/logos/yabam-logo-dark.png" alt="" height="60">
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
                                <i class="ri-home-8-line"></i><span data-key="t-dashboard">Dashboard </span>
                            </a>
                        </li>

                        <li class="nav-item">
                            <a class="nav-link menu-link ${currentPage === "apply.html" || currentPage === "application-status.html" || currentPage === "application-details.html" ? "active" : ""}" href="#application-manager" data-bs-toggle="collapse" role="button" aria-expanded="${currentPage === "apply.html" || currentPage === "application-status.html" || currentPage === "application-details.html" ? "true" : "false"}" aria-controls="application-manager">
                                <i class="mdi mdi-file-document-edit-outline"></i><span data-key="t-application-manager">Application Manager </span>
                            </a>
                            <div class="collapse menu-dropdown ${currentPage === "apply.html" || currentPage === "application-status.html" || currentPage === "application-details.html" ? "show" : ""}" id="application-manager">
                                <ul class="nav nav-sm flex-column">
                                    <li class="nav-item">
                                        <a href="apply.html" class="nav-link ${currentPage === "apply.html" ? "active" : ""}">Apply Now </a>
                                    </li>
                                    <li class="nav-item">
                                        <a href="application-status.html" class="nav-link ${currentPage === "application-status.html" ? "active" : ""}">Application History </a>
                                    </li>
                                </ul>
                            </div>
                        </li>

                        <li class="nav-item" id="become-agent-nav">
                            <a class="nav-link menu-link ${currentPage === "become-agent.html" ? "active" : ""}" href="become-agent.html">
                                <i class="ri-user-star-line"></i><span>Become an Agent </span>
                            </a>
                        </li>

                        <li class="menu-title"><i class="ri-more-fill"></i><span data-key="t-user">User</span></li>

                        <li class="nav-item">
                            <a class="nav-link menu-link ${currentPage === "profile.html" || currentPage === "profile-management.html" ? "active" : ""}" href="#user-profile" data-bs-toggle="collapse" role="button" aria-expanded="${currentPage === "profile.html" || currentPage === "profile-management.html" ? "true" : "false"}" aria-controls="user-profile">
                                <i class="mdi mdi-card-account-details-outline"></i><span data-key="t-user-profile">My Profile </span>
                            </a>
                            <div class="collapse menu-dropdown ${currentPage === "profile.html" || currentPage === "profile-management.html" ? "show" : ""}" id="user-profile">
                                <ul class="nav nav-sm flex-column">
                                    <li class="nav-item">
                                        <a href="profile.html" class="nav-link ${currentPage === "profile.html" ? "active" : ""}">User Profile </a>
                                    </li>
                                    <li class="nav-item">
                                        <a href="profile-management.html" class="nav-link ${currentPage === "profile-management.html" ? "active" : ""}">Profile Management </a>
                                    </li>
                                </ul>
                            </div>
                        </li>

                        <li class="nav-item">
                            <a class="nav-link menu-link ${currentPage === "wallet.html" ? "active" : ""}" href="wallet.html">
                                <i class="ri-wallet-line"></i><span>Rewards </span>
                            </a>
                        </li>

                        <li class="menu-title"><i class="ri-more-fill"></i><span data-key="t-support">Support</span></li>

                        <li class="nav-item">
                            <a class="nav-link menu-link" href="https://wa.me/60122273341" target="_blank" rel="noopener noreferrer" id="whatsapp-link-nav">
                                <i class="ri-customer-service-2-line"></i><span data-key="t-contact-cs">Contact Customer Service</span>
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="sidebar-background"></div>`;

  // Inject Header
  const topbar = document.getElementById("page-topbar");
  if (topbar) {
    topbar.innerHTML = headerHTML;

    // Fix: Attach click listener to the hamburger button for mobile toggle
    const hamburgerIcon = document.getElementById("topnav-hamburger-icon");
    if (hamburgerIcon) {
      hamburgerIcon.addEventListener("click", function () {
        const windowWidth = document.documentElement.clientWidth;
        if (windowWidth <= 767) {
          document.body.classList.toggle("vertical-sidebar-enable");
        } else {
          const hIcon = document.querySelector(".hamburger-icon");
          if (hIcon) hIcon.classList.toggle("open");
          const layout = document.documentElement.getAttribute("data-layout");
          if (layout === "vertical") {
            if (windowWidth > 1025) {
              const currentSize =
                document.documentElement.getAttribute("data-sidebar-size");
              document.documentElement.setAttribute(
                "data-sidebar-size",
                currentSize === "sm" ? "lg" : "sm",
              );
            } else if (windowWidth > 767) {
              const currentSize =
                document.documentElement.getAttribute("data-sidebar-size");
              document.documentElement.setAttribute(
                "data-sidebar-size",
                currentSize === "sm" ? "" : "sm",
              );
            }
          }
        }
      });
    }

    // Fix: Attach click listener to overlay to collapse sidebar on mobile
    const overlay = document.querySelector(".vertical-overlay");
    if (overlay) {
      overlay.addEventListener("click", function () {
        document.body.classList.remove("vertical-sidebar-enable");
      });
    }
  }

  // Inject Sidebar
  const sideMenu = document.querySelector(".navbar-menu");
  if (sideMenu) {
    sideMenu.innerHTML = sidebarHTML;
  }

  // Handle Logout
  if (window.jQuery) {
    $(".logout-member").on("click", function (e) {
      e.preventDefault();
      localStorage.removeItem("token");
      localStorage.removeItem("userRole");
      window.location.href = "index.html";
    });
  } else {
    document.querySelectorAll(".logout-member").forEach((el) => {
      el.addEventListener("click", function (e) {
        e.preventDefault();
        localStorage.removeItem("token");
        localStorage.removeItem("userRole");
        window.location.href = "index.html";
      });
    });
  }

  // Update Role visibility (based on memberHelper.js logic)
  const updateRoleMenu = () => {
    const userRole = localStorage.getItem("userRole");
    if (userRole === "agent") {
      document
        .querySelectorAll('[data-role="agent-only"]')
        .forEach((el) => (el.style.display = "block"));
      document
        .querySelectorAll('[data-role="member-only"]')
        .forEach((el) => (el.style.display = "none"));
    } else {
      document
        .querySelectorAll('[data-role="agent-only"]')
        .forEach((el) => (el.style.display = "none"));
      document
        .querySelectorAll('[data-role="member-only"]')
        .forEach((el) => (el.style.display = "block"));

      // Always show "Become an Agent" link in sidebar
      // (The logic that used to check for approved application is removed)
    }
  };

  updateRoleMenu();
}

// Run immediately - script is placed at the bottom of the body
renderMemberNavBar();
