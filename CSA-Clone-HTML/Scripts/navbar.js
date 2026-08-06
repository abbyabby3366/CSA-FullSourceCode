function renderNavBar() {
  const currentPage = window.location.pathname.split("/").pop() || "index.html";
  const userRole = localStorage.getItem("userRole") || "admin";
  const userName = localStorage.getItem("userName") || (userRole === "subadmin" ? "Sub Admin" : "Admin User");

  // Route guard for Subadmin
  if (userRole === "subadmin" && currentPage !== "index.html") {
    const allowedPages = ["applications.html", "members.html", "agents.html", "member-details.html"];
    if (!allowedPages.includes(currentPage)) {
      window.location.href = "applications.html";
      return;
    }
  }

  const roleTitle = userRole === "subadmin" ? "Sub Admin" : "Super Admin";

  const headerHTML = `
        <div class="layout-width">
            <div class="navbar-header">
                <div class="d-flex">
                    <!-- logo -->
                    <div class="navbar-brand-box horizontal-logo">
                        <a href="${userRole === "subadmin" ? "applications.html" : "dashboard.html"}" class="logo logo-dark">
                            <span class="logo-sm">
                                <img src="../assets/images/logos/logo-main.png" alt="" height="40">
                            </span>
                            <span class="logo-lg">
                                <img src="../assets/images/logos/logo-dark.png" alt="" height="60">
                            </span>
                        </a>

                        <a href="${userRole === "subadmin" ? "applications.html" : "dashboard.html"}" class="logo logo-light">
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
                                    <span class="d-none d-xl-inline-block ms-1 fw-medium user-name-text">${userName}</span>
                                    <span class="d-none d-xl-block ms-1 fs-12 user-name-sub-text">${roleTitle}</span>
                                </span>
                            </span>
                        </button>

                        <div class="dropdown-menu dropdown-menu-end">
                            <h6 class="dropdown-header">Welcome ${roleTitle}!</h6>
                            <a class="dropdown-item" href="profile.html"><i class="mdi mdi-account-circle text-muted fs-16 align-middle me-1"></i> <span class="align-middle">Profile</span></a>
                            <div class="dropdown-divider"></div>
                            <a class="dropdown-item logout-admin" href="#"><i class="mdi mdi-logout text-muted fs-16 align-middle me-1"></i> <span class="align-middle">Logout</span></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `;

  let navItemsHTML = "";

  if (userRole === "subadmin") {
    // Subadmin view: ONLY Application Management, Member List, Agent List
    navItemsHTML = `
      <li class="nav-item">
          <a class="nav-link menu-link ${currentPage === "applications.html" ? "active" : ""}" href="applications.html">
              <i class="mdi mdi-application-cog-outline"></i> <span data-key="t-application-manager"> Application Management </span>
          </a>
      </li>

      <li class="nav-item">
          <a class="nav-link menu-link ${["members.html", "agents.html"].includes(currentPage) ? "active" : ""}" href="#member-manager" data-bs-toggle="collapse" role="button" aria-expanded="true" aria-controls="member-manager">
              <i class="ri-file-user-line"></i> <span data-key="t-member-manager"> Member Management </span>
          </a>
          <div class="collapse menu-dropdown ${["members.html", "agents.html"].includes(currentPage) ? "show" : ""}" id="member-manager">
              <ul class="nav nav-sm flex-column">
                  <li class="nav-item">
                      <a href="members.html" class="nav-link ${currentPage === "members.html" ? "active" : ""}"> Member List </a>
                  </li>
                  <li class="nav-item">
                      <a href="agents.html" class="nav-link ${currentPage === "agents.html" ? "active" : ""}"> Agent List </a>
                  </li>
              </ul>
          </div>
      </li>
    `;
  } else {
    // Admin view: Full navigation including Subadmin List
    navItemsHTML = `
      <li class="menu-title"><span data-key="t-main">Main</span></li>

      <li class="nav-item">
          <a class="nav-link menu-link ${currentPage === "dashboard.html" ? "active" : ""}" href="dashboard.html">
              <i class="ri-home-8-line"></i> <span data-key="t-dashboard"> Dashboards </span>
          </a>
      </li>

      <li class="nav-item">
          <a class="nav-link menu-link ${currentPage === "applications.html" ? "active" : ""}" href="applications.html">
              <i class="mdi mdi-application-cog-outline"></i> <span data-key="t-application-manager"> Application Management </span>
          </a>
      </li>

      <li class="nav-item">
          <a class="nav-link menu-link ${["members.html", "agents.html"].includes(currentPage) ? "active" : ""}" href="#member-manager" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="member-manager">
              <i class="ri-file-user-line"></i> <span data-key="t-member-manager"> Member Management </span>
          </a>
          <div class="collapse menu-dropdown ${["members.html", "agents.html"].includes(currentPage) ? "show" : ""}" id="member-manager">
              <ul class="nav nav-sm flex-column">
                  <li class="nav-item">
                      <a href="members.html" class="nav-link ${currentPage === "members.html" ? "active" : ""}"> Member List </a>
                  </li>
                  <li class="nav-item">
                      <a href="agents.html" class="nav-link ${currentPage === "agents.html" ? "active" : ""}"> Agent List </a>
                  </li>
              </ul>
          </div>
      </li>

      <li class="nav-item">
          <a class="nav-link menu-link ${["withdrawals.html", "finance-history.html"].includes(currentPage) ? "active" : ""}" href="#finance" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="finance">
              <i class="ri-money-dollar-circle-line"></i> <span data-key="t-finance"> Finance </span>
          </a>
          <div class="collapse menu-dropdown ${["withdrawals.html", "finance-history.html"].includes(currentPage) ? "show" : ""}" id="finance">
              <ul class="nav nav-sm flex-column">
                  <li class="nav-item">
                      <a href="withdrawals.html" class="nav-link ${currentPage === "withdrawals.html" ? "active" : ""}"> Withdrawal Requests </a>
                  </li>
                  <li class="nav-item">
                      <a href="finance-history.html" class="nav-link ${currentPage === "finance-history.html" ? "active" : ""}"> Transaction History </a>
                  </li>
              </ul>
          </div>
      </li>

      <li class="nav-item">
          <a class="nav-link menu-link ${["admins.html", "subadmins.html"].includes(currentPage) ? "active" : ""}" href="#admin-manager" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="admin-manager">
              <i class="ri-user-star-line"></i> <span data-key="t-admin-manager"> Admin Management </span>
          </a>
          <div class="collapse menu-dropdown ${["admins.html", "subadmins.html"].includes(currentPage) ? "show" : ""}" id="admin-manager">
              <ul class="nav nav-sm flex-column">
                  <li class="nav-item">
                      <a href="admins.html" class="nav-link ${currentPage === "admins.html" ? "active" : ""}"> Admin List </a>
                  </li>
                  <li class="nav-item">
                      <a href="subadmins.html" class="nav-link ${currentPage === "subadmins.html" ? "active" : ""}"> Subadmin List </a>
                  </li>
              </ul>
          </div>
      </li>

      <li class="menu-title"><span data-key="t-marketing">Marketing</span></li>
      <li class="nav-item">
          <a class="nav-link menu-link ${currentPage === "email-campaigns.html" ? "active" : ""}" href="email-campaigns.html">
              <i class="ri-mail-line"></i> <span data-key="t-campaigns"> Email Campaigns </span>
          </a>
      </li>

      <li class="menu-title"><span data-key="t-configurations">Configurations</span></li>
      <li class="nav-item">
          <a class="nav-link menu-link ${currentPage === "roles.html" ? "active" : ""}" href="roles.html">
              <i class="ri-user-settings-line"></i> <span data-key="t-roles"> Role & privileges </span>
          </a>
      </li>

      <li class="nav-item">
          <a class="nav-link menu-link ${currentPage === "carousel-mgmt.html" ? "active" : ""}" href="carousel-mgmt.html">
              <i class="ri-palette-line"></i> <span data-key="t-carousel"> Dashboard Images </span>
          </a>
      </li>

      <li class="nav-item">
          <a class="nav-link menu-link ${currentPage === "tac-history.html" ? "active" : ""}" href="tac-history.html">
              <i class="ri-key-2-line"></i> <span data-key="t-tac-history"> TAC History </span>
          </a>
      </li>

      <li class="nav-item">
          <a class="nav-link menu-link ${currentPage === "settings.html" ? "active" : ""}" href="settings.html">
              <i class="ri-settings-2-line"></i> <span data-key="t-profile-manager"> Settings </span>
          </a>
      </li>
    `;
  }

  const sidebarHTML = `
        <!-- logo -->
        <div class="navbar-brand-box">
            <!-- dark -->
            <a href="${userRole === "subadmin" ? "applications.html" : "dashboard.html"}" class="logo logo-dark">
                <span class="logo-sm">
                    <img src="../assets/images/logos/logo-main.png" alt="" height="40">
                </span>
                <span class="logo-lg">
                    <img src="../assets/images/logos/logo-light.png" alt="" height="60">
                </span>
            </a>

            <!-- light -->
            <a href="${userRole === "subadmin" ? "applications.html" : "dashboard.html"}" class="logo logo-light">
                <span class="logo-sm">
                    <img src="../assets/images/logos/logo-main.png" alt="" height="40">
                </span>
                <span class="logo-lg">
                    <img src="../assets/images/logos/logo-dark.png" alt="" height="60">
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
                    ${navItemsHTML}
                </ul>
            </div>
        </div>

        <div class="sidebar-background"></div>
    `;

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

  // Inject Footer
  renderFooter();

  // Handle Logout
  $(".logout-admin").on("click", function (e) {
    e.preventDefault();
    localStorage.removeItem("token");
    localStorage.removeItem("userRole");
    localStorage.removeItem("userName");
    window.location.href = "index.html";
  });
}

function renderFooter() {
  const footerEl = document.querySelector("footer.footer");
  if (footerEl) {
    footerEl.innerHTML = `
      <div class="container-fluid">
        <div class="row">
          <div class="col-sm-6">
            © ${new Date().getFullYear()} iBelanja - All rights reserved.
          </div>
          <div class="col-sm-6">
            <div class="text-sm-end d-none d-sm-block">
              Design & Develop by Neurontech Trading
            </div>
          </div>
        </div>
      </div>
    `;
  }
}

// Ensure it runs after DOM is ready
if (document.readyState === "loading") {
  document.addEventListener("DOMContentLoaded", renderNavBar);
} else {
  renderNavBar();
}
