/**
 * Application Components - Shared module for application form fields,
 * view-details modal HTML, salary mapping, validation, and doc history.
 * 
 * Usage: Include via <script src="../Scripts/application-components.js"> 
 * AFTER jQuery and BEFORE inline page scripts.
 */
const AppComponents = (function () {

  // ── Salary Range Map ──
  const salaryRangeMap = {
    "1": "Below 3k",
    "2": "3001 - 5k",
    "3": "5k and Above"
  };

  function getSalaryLabel(value) {
    return salaryRangeMap[value] || "N/A";
  }

  // ── Employment Form HTML ──
  function renderEmploymentFormHTML() {
    return `
      <div class="d-flex align-items-center mt-4 mb-3">
        <div class="avatar-xs me-2">
          <div class="avatar-title bg-primary-subtle text-primary rounded-circle fs-16">
            <i class="ri-briefcase-4-line"></i>
          </div>
        </div>
        <div>
          <h5 class="fs-15 mb-0 text-primary fw-semibold">Career & Employment Information</h5>
          <p class="text-muted fs-12 mb-0">Sila isi maklumat pekerjaan anda dengan tepat dan lengkap.</p>
        </div>
      </div>
      <div class="row">
        <div class="col-lg-6 mb-3">
          <label class="form-label">EMPLOYMENT STATUS <span class="text-danger">*</span></label>
          <select class="form-select" id="employmentStatus" required>
            <option value="">Sila pilih</option>
            <option value="GOVERNMENT">GOVERNMENT</option>
            <option value="GLC">GLC</option>
            <option value="SWASTA/ PRIVATE">SWASTA/ PRIVATE</option>
            <option value="OWN BUSINESS">OWN BUSINESS</option>
            <option value="OTHERS">OTHERS</option>
          </select>
        </div>
        <div class="col-lg-6 mb-3">
          <label class="form-label">COMPANY NAME <span class="text-danger">*</span></label>
          <input type="text" class="form-control" id="employerName" placeholder="Contoh: Hospital UKM" required />
        </div>
        <div class="col-lg-6 mb-3">
          <label class="form-label">OCCUPATION / JOB TITLE <span class="text-danger">*</span></label>
          <input type="text" class="form-control" id="jobTitle" placeholder="Contoh: Juru Xray" required />
        </div>
        <div class="col-lg-6 mb-3">
          <label class="form-label">STATE OF EMPLOYMENT (eg: Selangor) <span class="text-danger">*</span></label>
          <select class="form-select" id="employmentState" required>
            <option value="">Sila pilih negeri</option>
            <option value="Johor">Johor</option>
            <option value="Kedah">Kedah</option>
            <option value="Kelantan">Kelantan</option>
            <option value="Melaka">Melaka</option>
            <option value="Negeri Sembilan">Negeri Sembilan</option>
            <option value="Pahang">Pahang</option>
            <option value="Perak">Perak</option>
            <option value="Perlis">Perlis</option>
            <option value="Pulau Pinang">Pulau Pinang</option>
            <option value="Sabah">Sabah</option>
            <option value="Sarawak">Sarawak</option>
            <option value="Selangor">Selangor</option>
            <option value="Terengganu">Terengganu</option>
            <option value="Wilayah Persekutuan Kuala Lumpur">Wilayah Persekutuan Kuala Lumpur</option>
            <option value="Wilayah Persekutuan Labuan">Wilayah Persekutuan Labuan</option>
            <option value="Wilayah Persekutuan Putrajaya">Wilayah Persekutuan Putrajaya</option>
          </select>
        </div>
        <div class="col-lg-6 mb-3">
          <label class="form-label">MONTHLY SALARY <span class="text-danger">*</span></label>
          <select class="form-select" id="salaryRange" required>
            <option value="">Sila pilih</option>
            <option value="1">Below 3k</option>
            <option value="2">3001 - 5k</option>
            <option value="3">5k and Above</option>
          </select>
        </div>
        <div class="col-lg-6 mb-3">
          <label class="form-label">RETIREMENT AGE <span class="text-danger">*</span></label>
          <input type="text" class="form-control" id="retirementAge" placeholder="Contoh: 60" maxlength="2" oninput="this.value = this.value.replace(/[^0-9]/g, '')" required />
        </div>
        <div class="col-lg-12 mb-3">
          <div class="alert alert-warning border-0 bg-warning-subtle d-flex align-items-center justify-content-between p-3 rounded-3 mb-0" role="alert">
            <div class="d-flex align-items-center">
              <div class="flex-shrink-0 me-2">
                <span class="badge bg-primary rounded-circle p-1 d-inline-flex align-items-center justify-content-center" style="width:24px; height:24px;">
                  <i class="ri-check-line text-white fs-14"></i>
                </span>
              </div>
              <div class="flex-grow-1 fs-13 text-dark fw-medium">
                Maklumat yang diberikan adalah sulit dan hanya digunakan untuk tujuan penilaian.
              </div>
            </div>
            <div class="flex-shrink-0 ms-3">
              <i class="ri-lock-2-line fs-20 text-primary"></i>
            </div>
          </div>
        </div>
      </div>
    `;
  }

  // ── Survey & Consent Form HTML ──
  function renderSurveyQuestionsHTML() {
    return `
      <div class="d-flex align-items-center mt-4 mb-3">
        <div class="avatar-xs me-2">
          <div class="avatar-title bg-primary-subtle text-primary rounded-circle fs-16">
            <i class="ri-questionnaire-line"></i>
          </div>
        </div>
        <div>
          <h5 class="fs-15 mb-0 text-primary fw-semibold">Maklumat Tambahan & Pengesahan</h5>
          <p class="text-muted fs-12 mb-0">Sila jawab soalan di bawah dan tandakan persetujuan anda.</p>
        </div>
      </div>
      <div class="row">
        <div class="col-lg-6 mb-3">
          <label class="form-label">STATUS PERKAHWINAN <span class="text-danger">*</span></label>
          <select class="form-select" id="maritalStatus" required>
            <option value="">Sila pilih</option>
            <option value="Bujang">Bujang (Single)</option>
            <option value="Berkahwin">Berkahwin (Married)</option>
            <option value="Duda/Janda">Duda/Janda (Widow/Divorced)</option>
            <option value="Lain-lain">Lain-lain (Others)</option>
          </select>
        </div>
        <div class="col-lg-6 mb-3">
          <label class="form-label">PEKERJAAN PASANGAN <span class="text-danger">*</span></label>
          <select class="form-select" id="partnerOccupation" required>
            <option value="">Sila pilih</option>
            <option value="Penjawat Awam">Penjawat Awam</option>
            <option value="GLC">GLC</option>
            <option value="Berhad">Berhad</option>
            <option value="Swasta">Swasta</option>
            <option value="Biz Owner">Biz Owner</option>
            <option value="Lain-lain">Lain-lain</option>
            <option value="Tiada Pasangan">Tiada Pasangan</option>
          </select>
        </div>
        <div class="col-lg-12 mb-3">
          <label class="form-label">RESTORAN / CAFE KEGEMARAN ANDA? (Selain Fast Food) <span class="text-danger">*</span></label>
          <input type="text" class="form-control" id="favoriteRestaurant" placeholder="Contoh: Restoran Nasi Kandar Pelita, Secret Recipe" required />
        </div>
        <div class="col-lg-12 mb-3">
          <label class="form-label">JIKA ANDA BERPELUANG MEMBUKA PERNIAGAAN FRANCHISE F&B DENGAN BERMODALKAN RM5,000-30,000, ADAKAH ANDA BERMINAT? <span class="text-danger">*</span></label>
          <select class="form-select" id="interestFbFranchise" required>
            <option value="">Sila pilih</option>
            <option value="YA">YA</option>
            <option value="TIDAK">TIDAK</option>
            <option value="Saya tidak suka berniaga">Saya tidak suka berniaga</option>
          </select>
        </div>
        <div class="col-lg-12 mb-3">
          <div class="card border shadow-none mb-0">
            <div class="card-body bg-light rounded-3 p-3">
              <h6 class="fs-14 text-dark fw-semibold mb-3"><i class="ri-shield-check-line text-success me-1"></i> Pengesahan & Kebenaran (Consent)</h6>
              <div class="form-check mb-3">
                <input class="form-check-input" type="checkbox" id="declarationConsent" required />
                <label class="form-check-label fs-13 text-dark" for="declarationConsent">
                  <strong>PENGESAHAN & PERSETUJUAN (TICK BOX):</strong> Saya mengesahkan bahawa semua maklumat yang diberikan adalah benar, tepat dan lengkap. Saya juga memberi kebenaran kepada pihak iBELANJA untuk menggunakan maklumat ini bagi tujuan naik taraf khidmat, termasuk mendapatkan laporan CTOS percuma serta menghubungi saya berkaitan maklumat lanjut kaji selidik ini.
                </label>
              </div>
              <div class="form-check mb-0">
                <input class="form-check-input" type="checkbox" id="pdpaConsent" required />
                <label class="form-check-label fs-13 text-dark" for="pdpaConsent">
                  <strong>PDPA (TICK BOX):</strong> Saya memahami bahawa semakan CTOS hanya akan dibuat dengan kebenaran saya dan maklumat peribadi saya akan dikendalikan mengikut Akta Perlindungan Data Peribadi 2010 (PDPA).
                </label>
              </div>
            </div>
          </div>
        </div>
      </div>
    `;
  }

  // ── Upload Documents Form HTML ──
  function renderUploadDocsFormHTML() {
    return `
      <h5 class="fs-15 col-12 mt-4 text-primary">Upload Documents</h5>
      <div class="row">
        <div class="col-lg-4 mb-3">
          <label class="form-label">IC (Front) <span class="text-danger">*</span></label>
          <input type="file" class="form-control" id="icFront" accept="image/*,.pdf,.doc,.docx,.heic,.heif" required />
        </div>
        <div class="col-lg-4 mb-3">
          <label class="form-label">IC (Back) <span class="text-danger">*</span></label>
          <input type="file" class="form-control" id="icBack" accept="image/*,.pdf,.doc,.docx,.heic,.heif" required />
        </div>
        <div class="col-lg-4 mb-3">
          <label class="form-label">Latest Payslip <span class="text-danger">*</span></label>
          <input type="file" class="form-control" id="payslip" accept="image/*,.pdf,.doc,.docx,.heic,.heif" required />
        </div>
      </div>
    `;
  }

  // ── Validate Application Form ──
  function validateApplicationForm() {
    var hasMaritalStatus = $('#maritalStatus').length ? $('#maritalStatus').val() : true;
    var hasPartnerOccupation = $('#partnerOccupation').length ? $('#partnerOccupation').val() : true;
    var hasFavoriteRestaurant = $('#favoriteRestaurant').length ? $('#favoriteRestaurant').val() : true;
    var hasInterestFbFranchise = $('#interestFbFranchise').length ? $('#interestFbFranchise').val() : true;
    var hasDeclarationConsent = $('#declarationConsent').length ? $('#declarationConsent').is(':checked') : true;
    var hasPdpaConsent = $('#pdpaConsent').length ? $('#pdpaConsent').is(':checked') : true;

    if (
      !$('#fullName').val() ||
      !$('#icNumber').val() ||
      !$('#phoneNumber').val() ||
      !$('#email').val() ||
      !$('#employmentStatus').val() ||
      !$('#employerName').val() ||
      !$('#jobTitle').val() ||
      !$('#employmentState').val() ||
      !$('#salaryRange').val() ||
      $('#salaryRange').val() === '0' ||
      !$('#retirementAge').val() ||
      !hasMaritalStatus ||
      !hasPartnerOccupation ||
      !hasFavoriteRestaurant ||
      !hasInterestFbFranchise ||
      !hasDeclarationConsent ||
      !hasPdpaConsent ||
      !$('#icFront')[0].files[0] ||
      !$('#icBack')[0].files[0] ||
      !$('#payslip')[0].files[0]
    ) {
      if ($('#declarationConsent').length && !$('#declarationConsent').is(':checked')) {
        return {
          valid: false,
          message: 'Sila tandakan Pengesahan & Persetujuan sebelum menghantar.'
        };
      }
      if ($('#pdpaConsent').length && !$('#pdpaConsent').is(':checked')) {
        return {
          valid: false,
          message: 'Sila tandakan pengakuan PDPA & semakan CTOS sebelum menghantar.'
        };
      }
      return {
        valid: false,
        message: 'Please fill in all required fields and upload all required documents.'
      };
    }
    return { valid: true, message: '' };
  }

  // ── Collect Application Details from Form ──
  function collectApplicationDetails() {
    var detailsObj = {
      fullName: $('#fullName').val(),
      icNumber: $('#icNumber').val().replace(/-/g, '').trim(),
      phoneNumber: $('#phoneNumber').val(),
      email: $('#email').val(),
      employmentDetails: {
        employmentStatus: $('#employmentStatus').val(),
        employerName: $('#employerName').val(),
        jobTitle: $('#jobTitle').val(),
        employmentState: $('#employmentState').val(),
        salaryRange: $('#salaryRange').val(),
        retirementAge: $('#retirementAge').val()
      }
    };

    if ($('#maritalStatus').length) detailsObj.maritalStatus = $('#maritalStatus').val();
    if ($('#partnerOccupation').length) detailsObj.partnerOccupation = $('#partnerOccupation').val();
    if ($('#favoriteRestaurant').length) detailsObj.favoriteRestaurant = $('#favoriteRestaurant').val();
    if ($('#interestFbFranchise').length) detailsObj.interestFbFranchise = $('#interestFbFranchise').val();
    if ($('#declarationConsent').length) detailsObj.declarationConsent = $('#declarationConsent').is(':checked');
    if ($('#pdpaConsent').length) detailsObj.pdpaConsent = $('#pdpaConsent').is(':checked');

    return detailsObj;
  }

  // ── Document Upload History HTML ──
  function renderDocHistoryHTML(historyList, collapseId) {
    if (!historyList || historyList.length === 0) return "";
    var historyItems = historyList.slice().reverse().map(function (item, idx) {
      var itemDate = item.uploadedAt
        ? new Date(item.uploadedAt).toLocaleString('en-GB')
        : "N/A";
      var isLatest = idx === 0;
      return `
        <li class="list-group-item d-flex justify-content-between align-items-center py-2 fs-12">
          <div>
            <span class="badge ${isLatest ? 'bg-success' : 'bg-secondary'} me-1">${isLatest ? 'Current' : 'History'}</span>
            <small class="text-muted">${itemDate}</small>
            ${item.note ? '<br><small class="fst-italic text-dark">' + item.note + '</small>' : ''}
          </div>
          <a href="https://x.neuronwww.com/${item.file}" target="_blank" class="btn btn-xs btn-outline-primary ms-2"><i class="ri-file-search-line"></i> View</a>
        </li>
      `;
    }).join('');

    return `
      <div class="mt-2">
        <button class="btn btn-sm btn-link text-decoration-none p-0 fs-12" type="button" data-bs-toggle="collapse" data-bs-target="#${collapseId}">
          <i class="ri-history-line"></i> View Upload History (${historyList.length})
        </button>
        <div class="collapse mt-2" id="${collapseId}">
          <ul class="list-group">
            ${historyItems}
          </ul>
        </div>
      </div>
    `;
  }

  // ── Format Date Helper ──
  function formatDate(dateStr) {
    var d = new Date(dateStr);
    var day = String(d.getDate()).padStart(2, '0');
    var month = String(d.getMonth() + 1).padStart(2, '0');
    var year = d.getFullYear();
    var hours = String(d.getHours()).padStart(2, '0');
    var minutes = String(d.getMinutes()).padStart(2, '0');
    return day + '/' + month + '/' + year + ' ' + hours + ':' + minutes;
  }

  // ── Render Document Cell (for view details) ──
  function _renderDocCell(label, fileUrl, reuploadAppId, reuploadType, historyHtml, options) {
    var showReupload = options && options.showReuploadButtons;
    var viewBtn = fileUrl
      ? '<a href="https://x.neuronwww.com/' + fileUrl + '" target="_blank" class="btn btn-sm btn-soft-info mt-1"><i class="ri-file-search-line"></i> View ' + label + '</a> <a href="javascript:void(0);" onclick="window.downloadFile(\'https://x.neuronwww.com/' + fileUrl + '\')" class="btn btn-sm btn-soft-primary mt-1"><i class="ri-download-line"></i> Download</a>'
      : 'Not uploaded';
    var reuploadBtn = showReupload
      ? '<br><button class="btn btn-sm btn-soft-warning mt-2 open-reupload-btn" data-id="' + reuploadAppId + '" data-type="' + reuploadType + '"><i class="ri-upload-cloud-line"></i> Update ' + label + '</button>'
      : '';
    return '<div class="col-md-4 mb-2"><strong>' + label + ':</strong><br>' + viewBtn + reuploadBtn + (historyHtml || '') + '</div>';
  }

  // ── View Details Modal HTML ──
  // options: { showReferrer, referrerInfo, showApprovedBy, approvedByHtml,
  //            showReuploadButtons, historyPrefix }
  function renderViewDetailsHTML(app, details, options) {
    options = options || {};
    var employment = details.employmentDetails || {};
    var salaryLabel = getSalaryLabel(employment.salaryRange);
    var formattedDate = formatDate(app.createDate);
    var prefix = options.historyPrefix || '';

    var icFrontHist = renderDocHistoryHTML(details.icFrontHistory, prefix + 'IcFrontHistoryCollapse');
    var icBackHist = renderDocHistoryHTML(details.icBackHistory, prefix + 'IcBackHistoryCollapse');
    var payslipHist = renderDocHistoryHTML(details.payslipHistory, prefix + 'PayslipHistoryCollapse');

    var html = '<div class="row">';

    // ── Header ──
    html += '<div class="col-md-6 mb-3"><strong>Application ID:</strong><br>' + app._id + '</div>';
    html += '<div class="col-md-6 mb-3"><strong>Date Submitted:</strong><br>' + formattedDate + '</div>';
    html += '<div class="col-md-12"><hr></div>';

    // ── Personal Details ──
    html += '<h6 class="mb-3 text-primary">Personal Details</h6>';
    html += '<div class="col-md-6 mb-2"><strong>Full Name:</strong> ' + (details.fullName || 'N/A') + '</div>';
    html += '<div class="col-md-6 mb-2"><strong>Phone Number:</strong> ' + (details.phoneNumber || 'N/A') + '</div>';
    html += '<div class="col-md-6 mb-2"><strong>IC Number:</strong> ' + (details.icNumber || 'N/A').toString().replace(/-/g, '') + '</div>';
    html += '<div class="col-md-6 mb-2"><strong>Email Address:</strong> ' + (details.email || 'N/A') + '</div>';

    // ── Referrer (admin only) ──
    if (options.showReferrer) {
      html += '<div class="col-md-6 mb-2"><strong>Referrer:</strong> ' + (options.referrerInfo || 'N/A') + '</div>';
    }

    html += '<div class="col-md-12"><hr></div>';

    // ── Employment Details ──
    html += '<h6 class="mb-3 text-primary">Employment Details</h6>';
    html += '<div class="col-md-6 mb-2"><strong>Employment Status:</strong> ' + (employment.employmentStatus || 'N/A') + '</div>';
    html += '<div class="col-md-6 mb-2"><strong>Employer:</strong> ' + (employment.employerName || 'N/A') + '</div>';
    html += '<div class="col-md-6 mb-2"><strong>Job Title:</strong> ' + (employment.jobTitle || 'N/A') + '</div>';
    html += '<div class="col-md-6 mb-2"><strong>State of Employment:</strong> ' + (employment.employmentState || 'N/A') + '</div>';
    html += '<div class="col-md-6 mb-2"><strong>Retirement Age:</strong> ' + (employment.retirementAge ? employment.retirementAge + ' Years Old' : 'N/A') + '</div>';
    html += '<div class="col-md-6 mb-3"><strong>Salary Range:</strong> ' + salaryLabel + '</div>';

    // ── Survey & Consent Responses ──
    if (details.maritalStatus || details.partnerOccupation || details.favoriteRestaurant || details.interestFbFranchise || details.pdpaConsent !== undefined) {
      html += '<div class="col-md-12"><hr></div>';
      html += '<h6 class="mb-3 text-primary">Survey & Consent Responses</h6>';
      if (details.maritalStatus) {
        html += '<div class="col-md-6 mb-2"><strong>Status Perkahwinan:</strong> ' + details.maritalStatus + '</div>';
      }
      if (details.partnerOccupation) {
        html += '<div class="col-md-6 mb-2"><strong>Pekerjaan Pasangan:</strong> ' + details.partnerOccupation + '</div>';
      }
      if (details.favoriteRestaurant) {
        html += '<div class="col-md-6 mb-2"><strong>Restoran/Cafe Kegemaran:</strong> ' + details.favoriteRestaurant + '</div>';
      }
      if (details.interestFbFranchise) {
        html += '<div class="col-md-6 mb-2"><strong>Minat Franchise F&B (RM5k-30k):</strong> ' + details.interestFbFranchise + '</div>';
      }
      if (details.declarationConsent !== undefined) {
        html += '<div class="col-md-6 mb-2"><strong>Pengesahan & Persetujuan:</strong> ' + (details.declarationConsent ? '<span class="badge bg-success">Disetujui</span>' : '<span class="badge bg-secondary">N/A</span>') + '</div>';
      }
      if (details.pdpaConsent !== undefined) {
        html += '<div class="col-md-6 mb-2"><strong>Pengakuan PDPA & CTOS:</strong> ' + (details.pdpaConsent ? '<span class="badge bg-success">Disetujui</span>' : '<span class="badge bg-secondary">N/A</span>') + '</div>';
      }
    }

    html += '<div class="col-md-12"><hr></div>';

    // ── Submitted Documents ──
    html += '<h6 class="mb-3 text-primary">Submitted Documents</h6>';
    html += _renderDocCell('IC Front', details.icFrontFile, app._id, 'icFront', icFrontHist, options);
    html += _renderDocCell('IC Back', details.icBackFile, app._id, 'icBack', icBackHist, options);
    html += _renderDocCell('Payslip', details.payslipFile, app._id, 'payslip', payslipHist, options);

    // ── Approved By (admin only) ──
    if (options.showApprovedBy && options.approvedByHtml) {
      html += options.approvedByHtml;
    }

    html += '</div>';
    return html;
  }

  // ── Public API ──
  return {
    salaryRangeMap: salaryRangeMap,
    getSalaryLabel: getSalaryLabel,
    renderEmploymentFormHTML: renderEmploymentFormHTML,
    renderSurveyQuestionsHTML: renderSurveyQuestionsHTML,
    renderUploadDocsFormHTML: renderUploadDocsFormHTML,
    validateApplicationForm: validateApplicationForm,
    collectApplicationDetails: collectApplicationDetails,
    renderDocHistoryHTML: renderDocHistoryHTML,
    formatDate: formatDate,
    renderViewDetailsHTML: renderViewDetailsHTML
  };

})();
