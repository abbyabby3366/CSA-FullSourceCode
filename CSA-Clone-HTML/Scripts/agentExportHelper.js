/**
 * Agent Export Helper
 * Manages the column selection modal and CSV download for Agent List
 */
const AgentExportHelper = {
  modalId: "exportAgentColumnsModal",

  renderModal: function () {
    if (document.getElementById(this.modalId)) return;

    const modalHtml = `
    <div class="modal fade" id="${this.modalId}" tabindex="-1" aria-labelledby="${this.modalId}Label" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered modal-lg">
        <div class="modal-content">
          <div class="modal-header bg-light">
            <h5 class="modal-title" id="${this.modalId}Label">
              <i class="ri-download-2-line align-middle me-1"></i> Select Columns to Export
            </h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <div class="d-flex justify-content-between align-items-center mb-3">
              <p class="text-muted mb-0">Choose which data fields to include in your Agent CSV export:</p>
              <div>
                <button type="button" class="btn btn-sm btn-outline-primary me-1" id="btnAgentExportSelectAll">Select All</button>
                <button type="button" class="btn btn-sm btn-outline-secondary" id="btnAgentExportDeselectAll">Deselect All</button>
              </div>
            </div>

            <!-- 1. Agent Info -->
            <div class="mb-3">
              <h6 class="fs-12 text-primary text-uppercase fw-semibold mb-2">
                <i class="ri-user-3-line me-1 align-middle"></i> 1. Agent Info
              </h6>
              <div class="row g-2 text-start ms-1">
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="full_name" id="chk_agent_name" checked />
                    <label class="form-check-label fw-medium" for="chk_agent_name">Full Name Agent</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="member_code" id="chk_agent_code" checked />
                    <label class="form-check-label fw-medium" for="chk_agent_code">Agent ID</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="ic" id="chk_agent_ic" />
                    <label class="form-check-label" for="chk_agent_ic">Agent IC Number</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="contact_no" id="chk_agent_phone" />
                    <label class="form-check-label" for="chk_agent_phone">Agent Contact No</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="subadmin" id="chk_agent_subadmin" />
                    <label class="form-check-label" for="chk_agent_subadmin">Assigned Subadmin</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="num_referrals" id="chk_agent_num_ref" />
                    <label class="form-check-label" for="chk_agent_num_ref">Total Referrals Count</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="status" id="chk_agent_status" />
                    <label class="form-check-label" for="chk_agent_status">Account Status</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="join_date" id="chk_agent_join_date" />
                    <label class="form-check-label" for="chk_agent_join_date">Joined Date</label>
                  </div>
                </div>
              </div>
            </div>

            <!-- 2. Referral Details -->
            <div class="mb-3">
              <h6 class="fs-12 text-primary text-uppercase fw-semibold mb-2">
                <i class="ri-team-line me-1 align-middle"></i> 2. Referral Details
              </h6>
              <div class="row g-2 text-start ms-1">
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="referral_member_name" id="chk_ref_name" checked />
                    <label class="form-check-label fw-medium" for="chk_ref_name">Referral List (Name)</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="referral_ic" id="chk_ref_ic" checked />
                    <label class="form-check-label fw-medium" for="chk_ref_ic">Referral IC Number</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="referral_contact_no" id="chk_ref_phone" checked />
                    <label class="form-check-label fw-medium" for="chk_ref_phone">Referral Contact Number</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="referral_company_name" id="chk_ref_company" checked />
                    <label class="form-check-label fw-medium" for="chk_ref_company">Referral Working Place</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="referral_member_code" id="chk_ref_code" />
                    <label class="form-check-label" for="chk_ref_code">Referral Member ID</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="referral_join_date" id="chk_ref_join" />
                    <label class="form-check-label" for="chk_ref_join">Referral Joined Date</label>
                  </div>
                </div>
              </div>
            </div>

            <!-- 3. Career & Employment -->
            <div class="mb-3">
              <h6 class="fs-12 text-primary text-uppercase fw-semibold mb-2">
                <i class="ri-briefcase-4-line me-1 align-middle"></i> 3. Agent Career & Employment Details
              </h6>
              <div class="row g-2 text-start ms-1">
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="employment_status" id="chk_agent_emp_status" />
                    <label class="form-check-label" for="chk_agent_emp_status">Employment Status</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="company_name" id="chk_agent_company" />
                    <label class="form-check-label" for="chk_agent_company">Company Name</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="job_title" id="chk_agent_job" />
                    <label class="form-check-label" for="chk_agent_job">Occupation / Job Title</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="state_of_employment" id="chk_agent_emp_state" />
                    <label class="form-check-label" for="chk_agent_emp_state">State of Employment</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="salary" id="chk_agent_salary" />
                    <label class="form-check-label" for="chk_agent_salary">Salary</label>
                  </div>
                </div>
              </div>
            </div>

            <!-- 4. Address Details -->
            <div class="mb-3">
              <h6 class="fs-12 text-primary text-uppercase fw-semibold mb-2">
                <i class="ri-map-pin-line me-1 align-middle"></i> 4. Agent Address Details
              </h6>
              <div class="row g-2 text-start ms-1">
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="address" id="chk_agent_address" />
                    <label class="form-check-label" for="chk_agent_address">Street Address</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="city" id="chk_agent_city" />
                    <label class="form-check-label" for="chk_agent_city">City</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="state" id="chk_agent_state" />
                    <label class="form-check-label" for="chk_agent_state">State</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="postcode" id="chk_agent_postcode" />
                    <label class="form-check-label" for="chk_agent_postcode">Postcode</label>
                  </div>
                </div>
              </div>
            </div>

            <!-- 5. Bank & Wallet Details -->
            <div class="mb-0">
              <h6 class="fs-12 text-primary text-uppercase fw-semibold mb-2">
                <i class="ri-bank-card-line me-1 align-middle"></i> 5. Agent Bank & Wallet Details
              </h6>
              <div class="row g-2 text-start ms-1">
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="bank_name" id="chk_agent_bank_name" />
                    <label class="form-check-label" for="chk_agent_bank_name">Bank Name</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="bank_account_name" id="chk_agent_bank_acc_name" />
                    <label class="form-check-label" for="chk_agent_bank_acc_name">Bank Account Name</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="bank_account_number" id="chk_agent_bank_acc_num" />
                    <label class="form-check-label" for="chk_agent_bank_acc_num">Bank Account Number</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="wallet_cash" id="chk_agent_wallet_cash" />
                    <label class="form-check-label" for="chk_agent_wallet_cash">Wallet Cash (RM)</label>
                  </div>
                </div>
                <div class="col-md-6 col-lg-4">
                  <div class="form-check">
                    <input class="form-check-input agent-export-col-chk" type="checkbox" value="wallet_point" id="chk_agent_wallet_point" />
                    <label class="form-check-label" for="chk_agent_wallet_point">Wallet Point</label>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-light" data-bs-dismiss="modal">Cancel</button>
            <button type="button" class="btn btn-primary" id="btnConfirmAgentExport">
              <i class="ri-download-2-line align-middle me-1"></i> Download CSV
            </button>
          </div>
        </div>
      </div>
    </div>
    `;

    $("body").append(modalHtml);
  },

  init: function () {
    this.renderModal();

    const self = this;

    // Open export modal
    $("#btnExportAgentCSV").on("click", function () {
      $(`#${self.modalId}`).modal("show");
    });

    // Select all / Deselect all
    $(document).on("click", "#btnAgentExportSelectAll", function () {
      $(".agent-export-col-chk").prop("checked", true);
    });

    $(document).on("click", "#btnAgentExportDeselectAll", function () {
      $(".agent-export-col-chk").prop("checked", false);
    });

    // Confirm & Download
    $(document).on("click", "#btnConfirmAgentExport", async function () {
      const selectedCols = [];
      $(".agent-export-col-chk:checked").each(function () {
        selectedCols.push($(this).val());
      });

      if (selectedCols.length === 0) {
        alert("Please select at least one column to export.");
        return;
      }

      const btn = $(this);
      const originalText = btn.html();
      btn.prop("disabled", true).html('<i class="ri-loader-4-line align-middle me-1 spin"></i> Exporting...');

      try {
        const token = localStorage.getItem("token");
        const exportUrl = `${window.API_BASE_URL}/api/admin/agents/export?columns=${encodeURIComponent(selectedCols.join(","))}`;

        const response = await fetch(exportUrl, {
          method: "GET",
          headers: {
            "x-auth-token": token,
          },
        });

        if (!response.ok) {
          throw new Error("Failed to export agent data");
        }

        const blob = await response.blob();
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement("a");
        a.href = url;
        a.download = "agents_export.csv";
        document.body.appendChild(a);
        a.click();
        a.remove();
        window.URL.revokeObjectURL(url);

        $(`#${self.modalId}`).modal("hide");
      } catch (err) {
        console.error("Agent Export Error:", err);
        alert("Error exporting agent details: " + err.message);
      } finally {
        btn.prop("disabled", false).html(originalText);
      }
    });
  },
};
