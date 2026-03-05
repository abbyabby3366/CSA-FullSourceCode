let initPromise = null;

async function initMemberPortal() {
  if (initPromise) return initPromise;

  initPromise = (async () => {
    const token = localStorage.getItem("token");
    if (!token) {
      window.location.href = "index.html";
      return;
    }

    try {
      const response = await fetch(`${window.API_BASE_URL}/api/members/me`, {
        headers: {
          "x-auth-token": token,
        },
      });

      if (response.status === 401) {
        localStorage.removeItem("token");
        window.location.href = "index.html";
        return;
      }

      const member = await response.json();
      if (!member) {
        console.error("Member data not found");
        localStorage.removeItem("token");
        window.location.href = "index.html";
        return;
      }
      window.currentMember = member;

      // Synchronize role for navbar consistency
      const roleStr = member.memberType === 2 ? "agent" : "member";
      localStorage.setItem("userRole", roleStr);

      // Apply role permissions
      applyRolePermissions(member.memberType);

      // Update Header UI
      $(".user-name-text").text(member.fullName);
      $(".user-name-sub-text").text(
        member.memberType === 2 ? "Agent" : "Member",
      );
      if (member.memberType !== 2) {
        alert("Access Denied: This portal is for Agents only.");
        window.location.href = "index.html";
        return;
      }
      $(".dropdown-header").text("Welcome " + member.fullName + "!");

      if (member.profileImage) {
        $(".header-profile-user").attr(
          "src",
          window.API_BASE_URL + "/" + member.profileImage,
        );
      }

      // Handle Logout
      $('.dropdown-item:contains("Logout")').on("click", function (e) {
        e.preventDefault();
        localStorage.removeItem("token");
        localStorage.removeItem("userRole");
        window.location.href = "index.html";
      });

      // Global Profile Completion Check
      const currentPage = window.location.pathname.split("/").pop();
      const isAuthPage = [
        "index.html",
        "register.html",
        "forgot-password.html",
      ].includes(currentPage);
      const isProfilePage =
        currentPage === "profile.html" ||
        currentPage === "profile-management.html";

      if (!isAuthPage && !isProfilePage) {
        const isIncomplete = !member.icNumber || !member.bankAccountNumber;
        if (isIncomplete) {
          showProfileCompletionModal(member);
          return;
        }
      }

      return member;
    } catch (err) {
      console.error("Error initializing portal:", err);
      initPromise = null; // Allow retry on error
      return null;
    }
  })();

  return initPromise;
}

function showProfileCompletionModal(member) {
  const modalHTML = `
    <div class="modal fade" id="completeProfileModal" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-hidden="true" style="z-index: 1060;">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content border-0">
                <div class="modal-header bg-soft-primary p-3">
                    <h5 class="modal-title">Complete Your Profile</h5>
                </div>
                <div class="modal-body p-4">
                    <p class="text-muted mb-4">
                      Welcome! To ensure you can participate in surveys and receive
                      rewards, please complete your profile details below.
                    </p>
                    <form id="completeProfileForm">
                      <div class="row">
                          <div class="col-lg-12 mb-3">
                            <label class="form-label">Full Name (as per NRIC)</label>
                            <input type="text" class="form-control" id="compFullName" value="${member.fullName || ""}" required>
                          </div>
                        <div class="col-md-6 mb-3">
                          <label class="form-label">Gender</label>
                          <select class="form-select" id="compGender" required>
                            <option value="" selected disabled>Select Gender</option>
                            <option value="Male" ${member.gender === "Male" ? "selected" : ""}>Male</option>
                            <option value="Female" ${member.gender === "Female" ? "selected" : ""}>Female</option>
                          </select>
                        </div>
                        <div class="col-md-6 mb-3">
                          <label class="form-label">NRIC (IC Number)</label>
                          <input type="text" class="form-control" id="compIcNumber" placeholder="900101-10-1234" value="${member.icNumber || ""}" required>
                        </div>
                        <div class="col-md-12">
                          <hr class="my-4">
                          <h6 class="mb-3">Bank Details</h6>
                        </div>
                        <div class="col-md-6 mb-3">
                          <label class="form-label">Bank Name</label>
                          <select class="form-select" id="compBankName" required>
                            <option value="" selected disabled>Select Bank</option>
                            <option value="Affin Bank">Affin Bank</option>
                            <option value="Agrobank">Agrobank</option>
                            <option value="Al Rajhi Bank Malaysia">Al Rajhi Bank Malaysia</option>
                            <option value="Alliance Bank">Alliance Bank</option>
                            <option value="AmBank">AmBank</option>
                            <option value="Bank Islam">Bank Islam</option>
                            <option value="Bank Muamalat">Bank Muamalat</option>
                            <option value="Bank Rakyat">Bank Rakyat</option>
                            <option value="BSN">BSN</option>
                            <option value="CIMB">CIMB</option>
                            <option value="Citibank Malaysia">Citibank Malaysia</option>
                            <option value="GX Bank">GX Bank</option>
                            <option value="Hong Leong Bank">Hong Leong Bank</option>
                            <option value="HSBC Malaysia">HSBC Malaysia</option>
                            <option value="Maybank">Maybank</option>
                            <option value="MBSB Bank">MBSB Bank</option>
                            <option value="OCBC Malaysia">OCBC Malaysia</option>
                            <option value="Public Bank">Public Bank</option>
                            <option value="RHB">RHB</option>
                            <option value="Standard Chartered Malaysia">Standard Chartered Malaysia</option>
                            <option value="Touch and Go">Touch and Go</option>
                            <option value="UOB Malaysia">UOB Malaysia</option>
                          </select>
                        </div>
                        <div class="col-md-6 mb-3">
                          <label class="form-label">Bank Account Name</label>
                          <input type="text" class="form-control" id="compBankAccountName" placeholder="Enter Account Name" required>
                        </div>
                        <div class="col-md-12 mb-3">
                          <label class="form-label">Bank Account Number</label>
                          <input type="text" class="form-control numeric-input" id="compBankAccountNumber" inputmode="numeric" pattern="[0-9]*" placeholder="Enter Account Number" required>
                        </div>
                      </div>
                      <div class="text-end mt-4">
                        <button type="submit" class="btn btn-primary btn-lg px-4" id="saveProfileBtn">
                          Save & Complete Profile
                        </button>
                      </div>
                    </form>
                </div>
            </div>
        </div>
    </div>`;

  $("body").append(modalHTML);
  const modal = new bootstrap.Modal(
    document.getElementById("completeProfileModal"),
  );
  modal.show();

  // Handle Form Submission
  $("#completeProfileForm").on("submit", async function (e) {
    e.preventDefault();
    const btn = $("#saveProfileBtn");
    const originalBtnText = btn.html();
    btn
      .prop("disabled", true)
      .html(
        '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Saving...',
      );

    const updateData = {
      fullName: $("#compFullName").val(),
      gender: $("#compGender").val(),
      icNumber: $("#compIcNumber").val(),
      bankName: $("#compBankName").val(),
      bankAccountName: $("#compBankAccountName").val(),
      bankAccountNumber: $("#compBankAccountNumber").val(),
    };

    try {
      const response = await fetch(
        `${window.API_BASE_URL}/api/members/update`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            "x-auth-token": localStorage.getItem("token"),
          },
          body: JSON.stringify(updateData),
        },
      );

      if (response.ok) {
        modal.hide();
        // Optional: Show success message before reload
        location.reload();
      } else {
        const errorData = await response.json();
        alert("Error: " + (errorData.msg || "Failed to update profile"));
        btn.prop("disabled", false).html(originalBtnText);
      }
    } catch (err) {
      console.error("Error updating profile:", err);
      alert("Server error. Please try again later.");
      btn.prop("disabled", false).html(originalBtnText);
    }
  });
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

$(document).ready(function () {
  initMemberPortal();
});
