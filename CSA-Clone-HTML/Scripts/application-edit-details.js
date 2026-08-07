/**
 * Application Edit Details - Handles inline editing of application personal details
 * with audit trail display. Used by admin/applications.html.
 *
 * Depends on: jQuery, adminHelper.js (apiCall), application-components.js (AppComponents)
 */
const AppEditDetails = (function () {

  // Track the current application being edited
  let _currentApp = null;
  let _isEditing = false;

  /**
   * Render the edit history section HTML from an application's editHistory array.
   */
  function renderEditHistoryHTML(editHistory) {
    if (!editHistory || editHistory.length === 0) return "";

    const items = editHistory.slice().reverse().map(function (entry) {
      const dateStr = entry.editedAt
        ? new Date(entry.editedAt).toLocaleString("en-GB")
        : "N/A";
      const editorName = entry.editedByName || "Unknown";
      const editorRole = entry.editedByRole || "";
      const roleBadge = editorRole === "admin"
        ? '<span class="badge bg-danger-subtle text-danger ms-1">Admin</span>'
        : '<span class="badge bg-info-subtle text-info ms-1">Subadmin</span>';

      return `
        <li class="list-group-item py-2 fs-12">
          <div class="d-flex align-items-start">
            <i class="ri-edit-line text-warning me-2 fs-14 mt-1 flex-shrink-0"></i>
            <div>
              <strong>${entry.fieldLabel || entry.field}</strong>
              edited from
              <span class="text-danger fw-medium">"${entry.oldValue || '(empty)'}"</span>
              &rarr;
              <span class="text-success fw-medium">"${entry.newValue || '(empty)'}"</span>
              <br>
              <small class="text-muted">
                by <span class="fw-medium text-primary">${editorName}</span>${roleBadge}
                on ${dateStr}
              </small>
            </div>
          </div>
        </li>
      `;
    }).join("");

    return `
      <div class="col-md-12 mt-2">
        <div class="d-flex align-items-center mb-2">
          <i class="ri-history-line text-warning me-1 fs-16"></i>
          <button class="btn btn-sm btn-link text-decoration-none p-0 fs-12 fw-medium"
                  type="button" data-bs-toggle="collapse" data-bs-target="#editHistoryCollapse">
            Edit History (${editHistory.length})
          </button>
        </div>
        <div class="collapse" id="editHistoryCollapse">
          <ul class="list-group list-group-flush">
            ${items}
          </ul>
        </div>
      </div>
    `;
  }

  /**
   * Build read-only personal details HTML with an "Edit Details" button.
   * Returns just the personal details section (to be inserted in the modal).
   */
  function renderPersonalDetailsHTML(app, details, referrerInfo) {
    const editBtn = `
      <button type="button" class="btn btn-sm btn-soft-warning ms-auto edit-personal-btn"
              data-id="${app._id}" title="Edit Personal Details">
        <i class="ri-edit-line me-1"></i>Edit Details
      </button>
    `;

    let html = `
      <div class="d-flex align-items-center mb-3">
        <h6 class="mb-0 text-primary">Personal Details</h6>
        ${editBtn}
      </div>
      <div id="personalDetailsContent">
        <div class="row">
          <div class="col-md-6 mb-2"><strong>Full Name:</strong> ${details.fullName || "N/A"}</div>
          <div class="col-md-6 mb-2"><strong>Phone Number:</strong> ${details.phoneNumber || "N/A"}</div>
          <div class="col-md-6 mb-2"><strong>IC Number:</strong> ${(details.icNumber || "N/A").toString().replace(/-/g, "")}</div>
          <div class="col-md-6 mb-2"><strong>Email Address:</strong> ${details.email || "N/A"}</div>
          <div class="col-md-6 mb-2"><strong>Referrer:</strong> ${referrerInfo || "N/A"}</div>
        </div>
      </div>
    `;

    // Edit history
    html += renderEditHistoryHTML(app.editHistory);

    return html;
  }

  /**
   * Switch personal details section to inline edit mode.
   */
  function enterEditMode(app) {
    _currentApp = app;
    _isEditing = true;
    const details = app.details || {};

    const formHtml = `
      <div class="row g-2" id="editPersonalForm">
        <div class="col-md-6">
          <label class="form-label fs-12 mb-1">Full Name</label>
          <input type="text" class="form-control form-control-sm" id="editFullName"
                 value="${(details.fullName || '').replace(/"/g, '&quot;')}" />
        </div>
        <div class="col-md-6">
          <label class="form-label fs-12 mb-1">Phone Number</label>
          <input type="text" class="form-control form-control-sm" id="editPhoneNumber"
                 value="${(details.phoneNumber || '').replace(/"/g, '&quot;')}" />
        </div>
        <div class="col-md-6">
          <label class="form-label fs-12 mb-1">IC Number</label>
          <input type="text" class="form-control form-control-sm" id="editIcNumber"
                 value="${(details.icNumber || '').toString().replace(/-/g, '').replace(/"/g, '&quot;')}" />
        </div>
        <div class="col-md-6">
          <label class="form-label fs-12 mb-1">Email Address</label>
          <input type="email" class="form-control form-control-sm" id="editEmail"
                 value="${(details.email || '').replace(/"/g, '&quot;')}" />
        </div>
        <div class="col-12 mt-2">
          <button type="button" class="btn btn-sm btn-primary" id="btnSaveEditDetails">
            <i class="ri-save-line me-1"></i>Save Changes
          </button>
          <button type="button" class="btn btn-sm btn-light ms-1" id="btnCancelEditDetails">
            Cancel
          </button>
        </div>
      </div>
    `;

    $("#personalDetailsContent").html(formHtml);
    $(".edit-personal-btn").hide();
  }

  /**
   * Save edited details via API.
   * Returns a Promise that resolves with the API response.
   */
  async function saveEditDetails() {
    if (!_currentApp) return;

    const payload = {
      fullName: $("#editFullName").val(),
      phoneNumber: $("#editPhoneNumber").val(),
      icNumber: $("#editIcNumber").val(),
      email: $("#editEmail").val(),
    };

    try {
      const response = await fetch(
        `${window.API_BASE_URL}/api/admin/application/${_currentApp._id}/edit-details`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
            "x-auth-token": localStorage.getItem("token"),
          },
          body: JSON.stringify(payload),
        }
      );

      const data = await response.json();

      if (!response.ok) {
        alert(data.msg || "Error updating details.");
        return null;
      }

      _isEditing = false;
      return data;
    } catch (err) {
      console.error("Save edit error:", err);
      alert("Error saving changes.");
      return null;
    }
  }

  /**
   * Cancel edit mode — restores original read-only display.
   */
  function cancelEditMode(app, referrerInfo) {
    _isEditing = false;
    const details = app.details || {};
    const readOnlyHtml = `
      <div class="row">
        <div class="col-md-6 mb-2"><strong>Full Name:</strong> ${details.fullName || "N/A"}</div>
        <div class="col-md-6 mb-2"><strong>Phone Number:</strong> ${details.phoneNumber || "N/A"}</div>
        <div class="col-md-6 mb-2"><strong>IC Number:</strong> ${(details.icNumber || "N/A").toString().replace(/-/g, "")}</div>
        <div class="col-md-6 mb-2"><strong>Email Address:</strong> ${details.email || "N/A"}</div>
        <div class="col-md-6 mb-2"><strong>Referrer:</strong> ${referrerInfo || "N/A"}</div>
      </div>
    `;
    $("#personalDetailsContent").html(readOnlyHtml);
    $(".edit-personal-btn").show();
  }

  return {
    renderEditHistoryHTML: renderEditHistoryHTML,
    renderPersonalDetailsHTML: renderPersonalDetailsHTML,
    enterEditMode: enterEditMode,
    saveEditDetails: saveEditDetails,
    cancelEditMode: cancelEditMode,
    getCurrentApp: function () { return _currentApp; },
    isEditing: function () { return _isEditing; },
  };

})();
