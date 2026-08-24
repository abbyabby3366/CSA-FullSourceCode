/**
 * Application Components - Shared module for application form fields,
 * view-details modal HTML, salary mapping, validation, and doc history.
 * Includes BM / ENG language translation toggle support (default: BM).
 */
const AppComponents = (function () {

  const salaryRangeMap = { "1": "Below 3k", "2": "3001 - 5k", "3": "5k and Above" };
  function getSalaryLabel(value) { return salaryRangeMap[value] || "N/A"; }

  let currentLang = 'bm';
  const i18n = {
    bm: {
      hdrApplyNow: '<i class="mdi mdi-file-document-edit-outline me-1"></i> Mohon Sekarang',
      surveyTitle: "SURVEY FNB MARKET by iBELANJA",
      surveyP1: "Terima kasih atas minat anda terhadap kaji selidik program <strong>iBELANJA</strong>.",
      surveyP2: "Kaji selidik ini adalah bertujuan bagi mengkaji rutin dan tabiat perbelanjaan golongan professional dan berpendidikan terhadap makanan dan gaya hidup.",
      surveyP3: "Sila lengkapkan borang ini dengan maklumat yang tepat. Jawapan anda bakal membantu kami untuk menaik taraf servis kami.",
      surveyP4: "Kerjasama anda untuk menjawab dengan jujur dan telus amatlah kami hargai.",
      surveyP5: "Anda akan memperolehi token penghargaan sehingga RM350 (TnC applied) dan laporan CTOS percuma.",
      lblFullName: "Nama Penuh (mengikut KP / MyKad)",
      phFullName: "Masukkan nama penuh seperti dalam KP (cth: termasuk BIN / BINTI / BTE / A/L)",
      subFullName: "Sila masukkan nama PENUH anda seperti dalam KP (termasuk BIN / BINTI / BTE / A/L).",
      lblIcNumber: "Nombor Kad Pengenalan", lblPhoneNumber: "Nombor Telefon", lblEmail: "Alamat E-mel",
      secEmploymentTitle: "Maklumat Kerjaya & Pekerjaan",
      secEmploymentSub: "Sila isi maklumat pekerjaan anda dengan tepat dan lengkap.",
      lblEmploymentStatus: "STATUS PEKERJAAN", optSelect: "Sila pilih",
      optGov: "GOVERNMENT / KERAJAAN", optGlc: "GLC", optPrivate: "SWASTA / PRIVATE",
      optOwnBiz: "OWN BUSINESS / PERNIAGAAN SENDIRI", optOthers: "OTHERS / LAIN-LAIN",
      lblCompanyName: "NAMA SYARIKAT", phCompanyName: "Contoh: Hospital UKM",
      lblJobTitle: "JAWATAN / PEKERJAAN", phJobTitle: "Contoh: Juru Xray",
      lblEmploymentState: "NEGERI TEMPAT BEKERJA (cth: Selangor)", optSelectState: "Sila pilih negeri",
      lblSalaryRange: "GAJI BULANAN", optSalary1: "Bawah 3k", optSalary2: "3001 - 5k", optSalary3: "5k dan Ke Atas",
      lblRetirementAge: "UMUR PERSARAAN", phRetirementAge: "Contoh: 60",
      msgConfidential: "Maklumat yang diberikan adalah sulit dan hanya digunakan untuk tujuan penilaian.",
      secSurveyTitle: "Maklumat Tambahan & Pengesahan",
      secSurveySub: "Sila jawab soalan di bawah dan tandakan persetujuan anda.",
      lblMaritalStatus: "STATUS PERKAHWINAN", optMaritalSingle: "Bujang (Single)", optMaritalMarried: "Berkahwin (Married)", optMaritalDivorced: "Duda/Janda (Widow/Divorced)", optMaritalOthers: "Lain-lain (Others)",
      lblPartnerOccupation: "PEKERJAAN PASANGAN", optPartnerCivil: "Penjawat Awam", optPartnerGlc: "GLC", optPartnerBerhad: "Berhad", optPartnerPrivate: "Swasta", optPartnerBiz: "Biz Owner", optPartnerOthers: "Lain-lain", optPartnerNone: "Tiada Pasangan",
      lblFavRestaurant: "APAKAH JENAMA RESTAURANT/ KAFE KEGEMARAN ANDA? (SELAIN FAST FOOD)", phFavRestaurant: "Contoh: Restoran Nasi Kandar Pelita, Secret Recipe",
      lblMonthlyFoodSpend: "BERAPA ANGGARAN ANDA BERBELANJA UNTUK MAKANAN SETIAP BULAN?",
      optSpend1: "Bawah RM300", optSpend2: "RM301 – RM500", optSpend3: "RM501 – RM1,000", optSpend4: "RM1,001 – RM1,500",
      optSpend5: "RM1,501 – RM2,000", optSpend6: "RM2,001 – RM3,000", optSpend7: "RM3,001 – RM5,000", optSpend8: "Lebih RM5,000",
      lblInterestFranchise: "JIKA ANDA BERPELUANG MEMBUKA PERNIAGAAN FRANCHISE F&B DENGAN BERMODALKAN RM5,000-30,000, ADAKAH ANDA BERMINAT?",
      optFranchiseYes: "YA", optFranchiseNo: "TIDAK", optFranchiseNoBiz: "Saya tidak suka berniaga",
      secConsentTitle: "Pengesahan & Kebenaran (Consent)",
      lblDeclarationTitle: "PENGESAHAN & PERSETUJUAN:",
      lblDeclarationText: "Saya mengesahkan bahawa semua maklumat yang diberikan adalah benar, tepat dan lengkap. Saya juga memberi kebenaran kepada pihak iBELANJA untuk menggunakan maklumat ini bagi tujuan naik taraf khidmat, termasuk mendapatkan laporan CTOS percuma serta menghubungi saya berkaitan maklumat lanjut kaji selidik ini.",
      lblPdpaTitle: "PDPA:",
      lblPdpaText: "Saya memahami bahawa semakan CTOS hanya akan dibuat dengan kebenaran saya dan maklumat peribadi saya akan dikendalikan mengikut Akta Perlindungan Data Peribadi 2010 (PDPA).",
      secDownloadDocsTitle: "Muat turun dokumen dibawah",
      lblCtosConsentForm: "Borang Kebenaran dan Persetujuan CTOS",
      lblCtosFormInstruction: "• Sila muat turun, lengkapkan tarikh, tandatangan dan isi maklumat lengkap di sebelah kiri.",
      lblCtosPdfFileName: "Borang Kebenaran & Persetujuan CTOS.pdf",
      btnDownloadForm: "Muat Turun",
      secUploadDocsTitle: "Muat Naik Dokumen",
      lblIcFront: "Kad Pengenalan (Depan)",
      subIcFront: "Bagi memperoleh report *CTOS percuma*",
      lblIcBack: "Kad Pengenalan (Belakang)",
      subIcBack: "Bagi memperoleh report *CTOS percuma*",
      lblPayslip: "Penyata Gaji Terkini",
      subPayslip: "Bagi validasi tempat berkhidmat & jenis pekerjaan",
      lblCtosConsent: "CTOS Consent Form",
      subCtosConsent: "Kebenaran untuk mendapatkan report CTOS percuma",
      msgUploadNote: "<strong>Nota:</strong> Sila pastikan dokumen jelas, lengkap dan dalam format <strong>JPG, PNG atau PDF</strong>. Saiz maksimum setiap fail adalah <strong>10MB</strong>.",
      btnSubmit: "Hantar", btnCancel: "Batal",
      errDeclaration: "Sila tandakan Pengesahan & Persetujuan sebelum menghantar.",
      errPdpa: "Sila tandakan pengakuan PDPA & semakan CTOS sebelum menghantar.",
      errRequiredFields: "Sila isi semua medan yang diperlukan dan muat naik semua dokumen yang diperlukan."
    },
    eng: {
      hdrApplyNow: '<i class="mdi mdi-file-document-edit-outline me-1"></i> Apply Now',
      surveyTitle: "SURVEY FNB MARKET by iBELANJA",
      surveyP1: "Thank you for your interest in the <strong>iBELANJA</strong> program survey.",
      surveyP2: "This survey aims to study the spending habits and routines of educated professionals regarding food and lifestyle.",
      surveyP3: "Please complete this form with accurate information. Your response will help us upgrade our services.",
      surveyP4: "Your cooperation in answering honestly and transparently is greatly appreciated.",
      surveyP5: "You will receive an appreciation token of up to RM350 (TnC applied) and a free CTOS report.",
      lblFullName: "Full Name (as per IC / MyKad)",
      phFullName: "Enter full name as per IC (e.g. including BIN / BINTI / BTE / A/L)",
      subFullName: "Please enter your FULL name as per IC (including BIN / BINTI / BTE / A/L).",
      lblIcNumber: "Identification Number", lblPhoneNumber: "Phone Number", lblEmail: "Email Address",
      secEmploymentTitle: "Career & Employment Information",
      secEmploymentSub: "Please fill in your employment information accurately and completely.",
      lblEmploymentStatus: "EMPLOYMENT STATUS", optSelect: "Please select",
      optGov: "GOVERNMENT", optGlc: "GLC", optPrivate: "SWASTA / PRIVATE",
      optOwnBiz: "OWN BUSINESS", optOthers: "OTHERS",
      lblCompanyName: "COMPANY NAME", phCompanyName: "Example: Hospital UKM",
      lblJobTitle: "OCCUPATION / JOB TITLE", phJobTitle: "Example: Radiographer",
      lblEmploymentState: "STATE OF EMPLOYMENT (eg: Selangor)", optSelectState: "Please select state",
      lblSalaryRange: "MONTHLY SALARY", optSalary1: "Below 3k", optSalary2: "3001 - 5k", optSalary3: "5k and Above",
      lblRetirementAge: "RETIREMENT AGE", phRetirementAge: "Example: 60",
      msgConfidential: "Information provided is confidential and used solely for evaluation purposes.",
      secSurveyTitle: "Additional Information & Verification",
      secSurveySub: "Please answer the questions below and tick your consent.",
      lblMaritalStatus: "MARITAL STATUS", optMaritalSingle: "Single", optMaritalMarried: "Married", optMaritalDivorced: "Widow / Divorced", optMaritalOthers: "Others",
      lblPartnerOccupation: "SPOUSE OCCUPATION", optPartnerCivil: "Civil Servant", optPartnerGlc: "GLC", optPartnerBerhad: "Berhad", optPartnerPrivate: "Private Sector", optPartnerBiz: "Business Owner", optPartnerOthers: "Others", optPartnerNone: "No Spouse",
      lblFavRestaurant: "WHAT IS YOUR FAVORITE RESTAURANT/ CAFE BRAND? (EXCEPT FAST FOOD)", phFavRestaurant: "Example: Nasi Kandar Pelita Restaurant, Secret Recipe",
      lblMonthlyFoodSpend: "WHAT IS YOUR ESTIMATED MONTHLY FOOD SPENDING?",
      optSpend1: "Below RM300", optSpend2: "RM301 – RM500", optSpend3: "RM501 – RM1,000", optSpend4: "RM1,001 – RM1,500",
      optSpend5: "RM1,501 – RM2,000", optSpend6: "RM2,001 – RM3,000", optSpend7: "RM3,001 – RM5,000", optSpend8: "Above RM5,000",
      lblInterestFranchise: "IF YOU HAD THE OPPORTUNITY TO OPEN AN F&B FRANCHISE BUSINESS WITH A CAPITAL OF RM5,000-30,000, WOULD YOU BE INTERESTED?",
      optFranchiseYes: "YES", optFranchiseNo: "NO", optFranchiseNoBiz: "I do not like doing business",
      secConsentTitle: "Confirmation & Consent",
      lblDeclarationTitle: "CONFIRMATION & AGREEMENT:",
      lblDeclarationText: "I confirm that all information provided is true, accurate and complete. I also give permission to iBELANJA to use this information for service upgrade purposes, including obtaining a free CTOS report and contacting me regarding further details of this survey.",
      lblPdpaTitle: "PDPA:",
      lblPdpaText: "I understand that CTOS checks will only be conducted with my consent and my personal data will be managed in accordance with the Personal Data Protection Act 2010 (PDPA).",
      secDownloadDocsTitle: "Download document below",
      lblCtosConsentForm: "CTOS Consent and Authorization Form",
      lblCtosFormInstruction: "• Please download, complete the date, sign and fill in complete details on the left.",
      lblCtosPdfFileName: "Borang Kebenaran & Persetujuan CTOS.pdf",
      btnDownloadForm: "Download",
      secUploadDocsTitle: "Upload Documents",
      lblIcFront: "Identification Card (Front)",
      subIcFront: "To obtain a *free CTOS report*",
      lblIcBack: "Identification Card (Back)",
      subIcBack: "To obtain a *free CTOS report*",
      lblPayslip: "Latest Payslip",
      subPayslip: "For workplace & job verification",
      lblCtosConsent: "CTOS Consent Form",
      subCtosConsent: "Consent to obtain a free CTOS report",
      msgUploadNote: "<strong>Note:</strong> Please ensure documents are clear, complete and in <strong>JPG, PNG or PDF</strong> format. Maximum size per file is <strong>10MB</strong>.",
      btnSubmit: "Submit", btnCancel: "Cancel",
      errDeclaration: "Please check the Confirmation & Agreement box before submitting.",
      errPdpa: "Please check the PDPA & CTOS consent box before submitting.",
      errRequiredFields: "Please fill in all required fields and upload all required documents."
    }
  };

  function setLanguage(lang) {
    if (!i18n[lang]) return;
    currentLang = lang;

    if (lang === 'bm') {
      $('#btnLangBM').addClass('active btn-primary').removeClass('btn-outline-primary');
      $('#btnLangENG').removeClass('active btn-primary').addClass('btn-outline-primary');
    } else {
      $('#btnLangENG').addClass('active btn-primary').removeClass('btn-outline-primary');
      $('#btnLangBM').removeClass('active btn-primary').addClass('btn-outline-primary');
    }

    $('[data-i18n]').each(function () {
      const key = $(this).attr('data-i18n');
      if (i18n[lang][key]) {
        $(this).html(i18n[lang][key]);
      }
    });

    $('[data-i18n-ph]').each(function () {
      const key = $(this).attr('data-i18n-ph');
      if (i18n[lang][key]) {
        $(this).attr('placeholder', i18n[lang][key]);
      }
    });
  }

  function getCurrentLanguage() { return currentLang; }

  function renderEmploymentFormHTML() {
    return `
      <div class="d-flex align-items-center mt-4 mb-3">
        <div class="avatar-xs me-2">
          <div class="avatar-title bg-primary-subtle text-primary rounded-circle fs-16"><i class="ri-briefcase-4-line"></i></div>
        </div>
        <div>
          <h5 class="fs-15 mb-0 text-primary fw-semibold" data-i18n="secEmploymentTitle">Maklumat Kerjaya & Pekerjaan</h5>
          <p class="text-muted fs-12 mb-0" data-i18n="secEmploymentSub">Sila isi maklumat pekerjaan anda dengan tepat dan lengkap.</p>
        </div>
      </div>
      <div class="row">
        <div class="col-lg-6 mb-3">
          <label class="form-label"><span data-i18n="lblEmploymentStatus">STATUS PEKERJAAN</span> <span class="text-danger">*</span></label>
          <select class="form-select" id="employmentStatus" required>
            <option value="" data-i18n="optSelect">Sila pilih</option>
            <option value="GOVERNMENT" data-i18n="optGov">GOVERNMENT / KERAJAAN</option>
            <option value="GLC">GLC</option>
            <option value="SWASTA/ PRIVATE" data-i18n="optPrivate">SWASTA / PRIVATE</option>
            <option value="OWN BUSINESS" data-i18n="optOwnBiz">OWN BUSINESS / PERNIAGAAN SENDIRI</option>
            <option value="OTHERS" data-i18n="optOthers">OTHERS / LAIN-LAIN</option>
          </select>
        </div>
        <div class="col-lg-6 mb-3">
          <label class="form-label"><span data-i18n="lblCompanyName">NAMA SYARIKAT</span> <span class="text-danger">*</span></label>
          <input type="text" class="form-control" id="employerName" placeholder="Contoh: Hospital UKM" data-i18n-ph="phCompanyName" required />
        </div>
        <div class="col-lg-6 mb-3">
          <label class="form-label"><span data-i18n="lblJobTitle">JAWATAN / PEKERJAAN</span> <span class="text-danger">*</span></label>
          <input type="text" class="form-control" id="jobTitle" placeholder="Contoh: Juru Xray" data-i18n-ph="phJobTitle" required />
        </div>
        <div class="col-lg-6 mb-3">
          <label class="form-label"><span data-i18n="lblEmploymentState">NEGERI TEMPAT BEKERJA (cth: Selangor)</span> <span class="text-danger">*</span></label>
          <select class="form-select" id="employmentState" required>
            <option value="" data-i18n="optSelectState">Sila pilih negeri</option>
            <option value="Johor">Johor</option><option value="Kedah">Kedah</option><option value="Kelantan">Kelantan</option><option value="Melaka">Melaka</option>
            <option value="Negeri Sembilan">Negeri Sembilan</option><option value="Pahang">Pahang</option><option value="Perak">Perak</option><option value="Perlis">Perlis</option>
            <option value="Pulau Pinang">Pulau Pinang</option><option value="Sabah">Sabah</option><option value="Sarawak">Sarawak</option><option value="Selangor">Selangor</option>
            <option value="Terengganu">Terengganu</option><option value="Wilayah Persekutuan Kuala Lumpur">Wilayah Persekutuan Kuala Lumpur</option>
            <option value="Wilayah Persekutuan Labuan">Wilayah Persekutuan Labuan</option><option value="Wilayah Persekutuan Putrajaya">Wilayah Persekutuan Putrajaya</option>
          </select>
        </div>
        <div class="col-lg-6 mb-3">
          <label class="form-label"><span data-i18n="lblSalaryRange">GAJI BULANAN</span> <span class="text-danger">*</span></label>
          <select class="form-select" id="salaryRange" required>
            <option value="" data-i18n="optSelect">Sila pilih</option>
            <option value="1" data-i18n="optSalary1">Bawah 3k</option>
            <option value="2" data-i18n="optSalary2">3001 - 5k</option>
            <option value="3" data-i18n="optSalary3">5k dan Ke Atas</option>
          </select>
        </div>
        <div class="col-lg-6 mb-3">
          <label class="form-label"><span data-i18n="lblRetirementAge">UMUR PERSARAAN</span> <span class="text-danger">*</span></label>
          <input type="text" class="form-control" id="retirementAge" placeholder="Contoh: 60" data-i18n-ph="phRetirementAge" maxlength="2" oninput="this.value = this.value.replace(/[^0-9]/g, '')" required />
        </div>
        <div class="col-lg-12 mb-3">
          <div class="alert alert-warning border-0 bg-warning-subtle d-flex align-items-center justify-content-between p-3 rounded-3 mb-0" role="alert">
            <div class="d-flex align-items-center">
              <div class="flex-shrink-0 me-2"><span class="badge bg-primary rounded-circle p-1 d-inline-flex align-items-center justify-content-center" style="width:24px; height:24px;"><i class="ri-check-line text-white fs-14"></i></span></div>
              <div class="flex-grow-1 fs-13 text-dark fw-medium" data-i18n="msgConfidential">Maklumat yang diberikan adalah sulit dan hanya digunakan untuk tujuan penilaian.</div>
            </div>
            <div class="flex-shrink-0 ms-3"><i class="ri-lock-2-line fs-20 text-primary"></i></div>
          </div>
        </div>
      </div>
    `;
  }

  function renderSurveyQuestionsHTML() {
    return `
      <div class="d-flex align-items-center mt-4 mb-3">
        <div class="avatar-xs me-2">
          <div class="avatar-title bg-primary-subtle text-primary rounded-circle fs-16"><i class="ri-questionnaire-line"></i></div>
        </div>
        <div>
          <h5 class="fs-15 mb-0 text-primary fw-semibold" data-i18n="secSurveyTitle">Maklumat Tambahan & Pengesahan</h5>
          <p class="text-muted fs-12 mb-0" data-i18n="secSurveySub">Sila jawab soalan di bawah dan tandakan persetujuan anda.</p>
        </div>
      </div>
      <div class="row">
        <div class="col-lg-6 mb-3">
          <label class="form-label"><span data-i18n="lblMaritalStatus">STATUS PERKAHWINAN</span> <span class="text-danger">*</span></label>
          <select class="form-select" id="maritalStatus" required>
            <option value="" data-i18n="optSelect">Sila pilih</option>
            <option value="Bujang" data-i18n="optMaritalSingle">Bujang (Single)</option>
            <option value="Berkahwin" data-i18n="optMaritalMarried">Berkahwin (Married)</option>
            <option value="Duda/Janda" data-i18n="optMaritalDivorced">Duda/Janda (Widow/Divorced)</option>
            <option value="Lain-lain" data-i18n="optMaritalOthers">Lain-lain (Others)</option>
          </select>
        </div>
        <div class="col-lg-6 mb-3">
          <label class="form-label"><span data-i18n="lblPartnerOccupation">PEKERJAAN PASANGAN</span> <span class="text-danger">*</span></label>
          <select class="form-select" id="partnerOccupation" required>
            <option value="" data-i18n="optSelect">Sila pilih</option>
            <option value="Penjawat Awam" data-i18n="optPartnerCivil">Penjawat Awam</option>
            <option value="GLC" data-i18n="optPartnerGlc">GLC</option>
            <option value="Berhad" data-i18n="optPartnerBerhad">Berhad</option>
            <option value="Swasta" data-i18n="optPartnerPrivate">Swasta</option>
            <option value="Biz Owner" data-i18n="optPartnerBiz">Biz Owner</option>
            <option value="Lain-lain" data-i18n="optPartnerOthers">Lain-lain</option>
            <option value="Tiada Pasangan" data-i18n="optPartnerNone">Tiada Pasangan</option>
          </select>
        </div>
        <div class="col-lg-12 mb-3">
          <label class="form-label"><span data-i18n="lblFavRestaurant">APAKAH JENAMA RESTAURANT/ KAFE KEGEMARAN ANDA? (SELAIN FAST FOOD)</span> <span class="text-danger">*</span></label>
          <input type="text" class="form-control" id="favoriteRestaurant" placeholder="Contoh: Restoran Nasi Kandar Pelita, Secret Recipe" data-i18n-ph="phFavRestaurant" required />
        </div>
        <div class="col-lg-12 mb-3">
          <label class="form-label"><span data-i18n="lblMonthlyFoodSpend">BERAPA ANGGARAN ANDA BERBELANJA UNTUK MAKANAN SETIAP BULAN?</span> <span class="text-danger">*</span></label>
          <select class="form-select" id="monthlyFoodSpend" required>
            <option value="" data-i18n="optSelect">Sila pilih</option>
            <option value="Bawah RM300" data-i18n="optSpend1">Bawah RM300</option>
            <option value="RM301 – RM500" data-i18n="optSpend2">RM301 – RM500</option>
            <option value="RM501 – RM1,000" data-i18n="optSpend3">RM501 – RM1,000</option>
            <option value="RM1,001 – RM1,500" data-i18n="optSpend4">RM1,001 – RM1,500</option>
            <option value="RM1,501 – RM2,000" data-i18n="optSpend5">RM1,501 – RM2,000</option>
            <option value="RM2,001 – RM3,000" data-i18n="optSpend6">RM2,001 – RM3,000</option>
            <option value="RM3,001 – RM5,000" data-i18n="optSpend7">RM3,001 – RM5,000</option>
            <option value="Lebih RM5,000" data-i18n="optSpend8">Lebih RM5,000</option>
          </select>
        </div>
        <div class="col-lg-12 mb-3">
          <label class="form-label"><span data-i18n="lblInterestFranchise">JIKA ANDA BERPELUANG MEMBUKA PERNIAGAAN FRANCHISE F&B DENGAN BERMODALKAN RM5,000-30,000, ADAKAH ANDA BERMINAT?</span> <span class="text-danger">*</span></label>
          <select class="form-select" id="interestFbFranchise" required>
            <option value="" data-i18n="optSelect">Sila pilih</option>
            <option value="YA" data-i18n="optFranchiseYes">YA</option>
            <option value="TIDAK" data-i18n="optFranchiseNo">TIDAK</option>
            <option value="Saya tidak suka berniaga" data-i18n="optFranchiseNoBiz">Saya tidak suka berniaga</option>
          </select>
        </div>
        <div class="col-lg-12 mb-3">
          <div class="card border shadow-none mb-0">
            <div class="card-body bg-light rounded-3 p-3">
              <h6 class="fs-14 text-dark fw-semibold mb-3"><i class="ri-shield-check-line text-success me-1"></i> <span data-i18n="secConsentTitle">Pengesahan & Kebenaran (Consent)</span></h6>
              <div class="form-check mb-3">
                <input class="form-check-input" type="checkbox" id="declarationConsent" required />
                <label class="form-check-label fs-13 text-dark" for="declarationConsent">
                  <strong data-i18n="lblDeclarationTitle">PENGESAHAN & PERSETUJUAN:</strong> <span data-i18n="lblDeclarationText">Saya mengesahkan bahawa semua maklumat yang diberikan adalah benar, tepat dan lengkap. Saya juga memberi kebenaran kepada pihak iBELANJA untuk menggunakan maklumat ini bagi tujuan naik taraf khidmat, termasuk mendapatkan laporan CTOS percuma serta menghubungi saya berkaitan maklumat lanjut kaji selidik ini.</span>
                </label>
              </div>
              <div class="form-check mb-0">
                <input class="form-check-input" type="checkbox" id="pdpaConsent" required />
                <label class="form-check-label fs-13 text-dark" for="pdpaConsent">
                  <strong data-i18n="lblPdpaTitle">PDPA:</strong> <span data-i18n="lblPdpaText">Saya memahami bahawa semakan CTOS hanya akan dibuat dengan kebenaran saya dan maklumat peribadi saya akan dikendalikan mengikut Akta Perlindungan Data Peribadi 2010 (PDPA).</span>
                </label>
              </div>
            </div>
          </div>
        </div>
      </div>
    `;
  }

  function renderDownloadDocsFormHTML() {
    return `
      <div class="card border shadow-none rounded-3 mt-4 mb-4" style="background-color: #f7f9fc !important;">
        <div class="card-body p-3 p-sm-4">
          <div class="mb-3">
            <h5 class="fs-16 text-primary fw-bold mb-1" data-i18n="secDownloadDocsTitle">Muat turun dokumen dibawah</h5>
            <h6 class="fs-14 text-dark fw-bold mb-1" data-i18n="lblCtosConsentForm">Borang Kebenaran dan Persetujuan CTOS</h6>
            <p class="text-muted fs-13 mb-0" data-i18n="lblCtosFormInstruction">
              • Sila muat turun, lengkapkan tarikh, tandatangan dan isi maklumat lengkap di sebelah kiri.
            </p>
          </div>
          <div>
            <a href="../assets/docs/borang_kebenaran_ctos.pdf" download="Borang_Kebenaran_Persetujuan_CTOS.pdf" target="_blank" class="d-flex align-items-center justify-content-between p-2 px-3 bg-white border rounded-3 text-dark text-decoration-none shadow-sm text-reset">
              <div class="d-flex align-items-center">
                <div class="badge bg-danger p-2 me-2 rounded-2 d-flex flex-column align-items-center justify-content-center" style="width: 32px; height: 34px;">
                  <i class="ri-file-pdf-fill fs-16 text-white"></i>
                  <span style="font-size: 8px; line-height: 1; margin-top: 1px;" class="fw-bold text-white">PDF</span>
                </div>
                <div>
                  <div class="fw-bold fs-13 text-dark" data-i18n="lblCtosPdfFileName">Borang Kebenaran & Persetujuan CTOS.pdf</div>
                  <div class="text-muted fs-11">PDF • 550 KB</div>
                </div>
              </div>
              <i class="ri-download-2-line text-primary fs-20 me-1"></i>
            </a>
          </div>
        </div>
      </div>
    `;
  }

  function renderUploadDocsFormHTML() {
    return `
      <h5 class="fs-16 col-12 mt-4 text-primary fw-bold mb-3" data-i18n="secUploadDocsTitle">2. Muat Naik Dokumen</h5>
      <div class="row">
        <div class="col-lg-12 mb-3">
          <label class="form-label fw-bold text-dark mb-1"><span data-i18n="lblIcFront">Kad Pengenalan (Depan)</span> <span class="text-danger">*</span></label>
          <input type="file" class="form-control" id="icFront" accept="image/*,.pdf,.doc,.docx,.heic,.heif" required />
          <div class="form-text text-primary mt-1 fs-12 d-flex align-items-center">
            <i class="ri-upload-cloud-line me-1 fs-14"></i>
            <strong class="me-1">Upload IC:</strong>
            <span class="text-dark" data-i18n="subIcFront">Bagi memperoleh report <strong>*CTOS percuma*</strong></span>
          </div>
        </div>
        <div class="col-lg-12 mb-3">
          <label class="form-label fw-bold text-dark mb-1"><span data-i18n="lblIcBack">Kad Pengenalan (Belakang)</span> <span class="text-danger">*</span></label>
          <input type="file" class="form-control" id="icBack" accept="image/*,.pdf,.doc,.docx,.heic,.heif" required />
          <div class="form-text text-primary mt-1 fs-12 d-flex align-items-center">
            <i class="ri-upload-cloud-line me-1 fs-14"></i>
            <strong class="me-1">Upload IC:</strong>
            <span class="text-dark" data-i18n="subIcBack">Bagi memperoleh report <strong>*CTOS percuma*</strong></span>
          </div>
        </div>
        <div class="col-lg-12 mb-3">
          <label class="form-label fw-bold text-dark mb-1"><span data-i18n="lblPayslip">Penyata Gaji Terkini</span> <span class="text-danger">*</span></label>
          <input type="file" class="form-control" id="payslip" accept="image/*,.pdf,.doc,.docx,.heic,.heif" required />
          <div class="form-text text-primary mt-1 fs-12 d-flex align-items-center">
            <i class="ri-upload-cloud-line me-1 fs-14"></i>
            <strong class="me-1">Upload payslip:</strong>
            <span class="text-dark" data-i18n="subPayslip">Bagi validasi tempat berkhidmat & jenis pekerjaan</span>
          </div>
        </div>
        <div class="col-lg-12 mb-3">
          <label class="form-label fw-bold text-dark mb-1"><span data-i18n="lblCtosConsent">CTOS Consent Form</span> <span class="text-danger">*</span></label>
          <input type="file" class="form-control" id="ctosConsent" accept="image/*,.pdf,.doc,.docx,.heic,.heif" required />
          <div class="form-text text-primary mt-1 fs-12 d-flex align-items-center">
            <i class="ri-upload-cloud-line me-1 fs-14"></i>
            <strong class="me-1">Upload consent:</strong>
            <span class="text-dark" data-i18n="subCtosConsent">Kebenaran untuk mendapatkan report CTOS percuma</span>
          </div>
        </div>
        <div class="col-lg-12 mb-2">
          <div class="alert alert-info border-0 bg-info-subtle p-3 rounded-3 mb-0">
            <i class="ri-information-line me-1 align-middle fs-15 text-info"></i>
            <span class="fs-13 text-dark" data-i18n="msgUploadNote"><strong>Nota:</strong> Sila pastikan dokumen jelas, lengkap dan dalam format <strong>JPG, PNG atau PDF</strong>. Saiz maksimum setiap fail adalah <strong>10MB</strong>.</span>
          </div>
        </div>
      </div>
    `;
  }

  function validateApplicationForm() {
    var hasMaritalStatus = $('#maritalStatus').length ? $('#maritalStatus').val() : true;
    var hasPartnerOccupation = $('#partnerOccupation').length ? $('#partnerOccupation').val() : true;
    var hasFavoriteRestaurant = $('#favoriteRestaurant').length ? $('#favoriteRestaurant').val() : true;
    var hasMonthlyFoodSpend = $('#monthlyFoodSpend').length ? $('#monthlyFoodSpend').val() : true;
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
      !hasMonthlyFoodSpend ||
      !hasInterestFbFranchise ||
      !hasDeclarationConsent ||
      !hasPdpaConsent ||
      !$('#icFront')[0].files[0] ||
      !$('#icBack')[0].files[0] ||
      !$('#payslip')[0].files[0] ||
      ($('#ctosConsent').length && !$('#ctosConsent')[0].files[0])
    ) {
      if ($('#declarationConsent').length && !$('#declarationConsent').is(':checked')) {
        return { valid: false, message: i18n[currentLang] ? i18n[currentLang].errDeclaration : 'Sila tandakan Pengesahan & Persetujuan sebelum menghantar.' };
      }
      if ($('#pdpaConsent').length && !$('#pdpaConsent').is(':checked')) {
        return { valid: false, message: i18n[currentLang] ? i18n[currentLang].errPdpa : 'Sila tandakan pengakuan PDPA & semakan CTOS sebelum menghantar.' };
      }
      return { valid: false, message: i18n[currentLang] ? i18n[currentLang].errRequiredFields : 'Sila isi semua medan yang diperlukan dan muat naik semua dokumen yang diperlukan.' };
    }

    // Check file sizes (Max 10MB per file)
    var MAX_FILE_SIZE = 10 * 1024 * 1024;
    var fileInputs = [
      { id: '#icFront', labelBM: 'Kad Pengenalan (Depan)', labelENG: 'IC Front' },
      { id: '#icBack', labelBM: 'Kad Pengenalan (Belakang)', labelENG: 'IC Back' },
      { id: '#payslip', labelBM: 'Penyata Gaji Terkini', labelENG: 'Latest Payslip' },
      { id: '#ctosConsent', labelBM: 'CTOS Consent Form', labelENG: 'CTOS Consent Form' },
      { id: '#offerLetter', labelBM: 'Surat Tawaran', labelENG: 'Offer Letter' }
    ];

    for (var i = 0; i < fileInputs.length; i++) {
      var item = fileInputs[i];
      var el = $(item.id)[0];
      if (el && el.files && el.files[0]) {
        var file = el.files[0];
        if (file.size > MAX_FILE_SIZE) {
          var sizeMB = (file.size / (1024 * 1024)).toFixed(1);
          var docLabel = currentLang === 'bm' ? item.labelBM : item.labelENG;
          var msg = currentLang === 'bm'
            ? `Fail "${docLabel}" melebihi had saiz (${sizeMB}MB). Had maksimum setiap fail adalah 10MB. Sila kurangkan resolusi gambar atau ambil tangkap layar (screenshot) dokumen.`
            : `File "${docLabel}" exceeds the maximum size (${sizeMB}MB). Maximum allowed size is 10MB per file. Please reduce file resolution or take a screenshot.`;
          return { valid: false, message: msg };
        }
      }
    }

    return { valid: true, message: '' };
  }

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
    if ($('#monthlyFoodSpend').length) detailsObj.monthlyFoodSpend = $('#monthlyFoodSpend').val();
    if ($('#interestFbFranchise').length) detailsObj.interestFbFranchise = $('#interestFbFranchise').val();
    if ($('#declarationConsent').length) detailsObj.declarationConsent = $('#declarationConsent').is(':checked');
    if ($('#pdpaConsent').length) detailsObj.pdpaConsent = $('#pdpaConsent').is(':checked');
    return detailsObj;
  }

  function renderDocHistoryHTML(historyList, collapseId) {
    if (!historyList || historyList.length === 0) return "";
    var historyItems = historyList.slice().reverse().map(function (item, idx) {
      var itemDate = item.uploadedAt ? new Date(item.uploadedAt).toLocaleString('en-GB') : "N/A";
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
          <ul class="list-group">${historyItems}</ul>
        </div>
      </div>
    `;
  }

  function formatDate(dateStr) {
    var d = new Date(dateStr);
    var day = String(d.getDate()).padStart(2, '0');
    var month = String(d.getMonth() + 1).padStart(2, '0');
    var year = d.getFullYear();
    var hours = String(d.getHours()).padStart(2, '0');
    var minutes = String(d.getMinutes()).padStart(2, '0');
    return day + '/' + month + '/' + year + ' ' + hours + ':' + minutes;
  }

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
    html += '<div class="col-md-6 mb-3"><strong>Application ID:</strong><br>' + app._id + '</div>';
    html += '<div class="col-md-6 mb-3"><strong>Date Submitted:</strong><br>' + formattedDate + '</div>';
    html += '<div class="col-md-12"><hr></div>';
    html += '<h6 class="mb-3 text-primary">Personal Details</h6>';
    html += '<div class="col-md-6 mb-2"><strong>Full Name:</strong> ' + (details.fullName || 'N/A') + '</div>';
    html += '<div class="col-md-6 mb-2"><strong>Phone Number:</strong> ' + (details.phoneNumber || 'N/A') + '</div>';
    html += '<div class="col-md-6 mb-2"><strong>IC Number:</strong> ' + (details.icNumber || 'N/A').toString().replace(/-/g, '') + '</div>';
    html += '<div class="col-md-6 mb-2"><strong>Email Address:</strong> ' + (details.email || 'N/A') + '</div>';

    if (options.showReferrer) {
      html += '<div class="col-md-6 mb-2"><strong>Referrer:</strong> ' + (options.referrerInfo || 'N/A') + '</div>';
    }
    html += '<div class="col-md-12"><hr></div>';
    html += '<h6 class="mb-3 text-primary">Employment Details</h6>';
    html += '<div class="col-md-6 mb-2"><strong>Employment Status:</strong> ' + (employment.employmentStatus || 'N/A') + '</div>';
    html += '<div class="col-md-6 mb-2"><strong>Employer:</strong> ' + (employment.employerName || 'N/A') + '</div>';
    html += '<div class="col-md-6 mb-2"><strong>Job Title:</strong> ' + (employment.jobTitle || 'N/A') + '</div>';
    html += '<div class="col-md-6 mb-2"><strong>State of Employment:</strong> ' + (employment.employmentState || 'N/A') + '</div>';
    html += '<div class="col-md-6 mb-2"><strong>Retirement Age:</strong> ' + (employment.retirementAge ? employment.retirementAge + ' Years Old' : 'N/A') + '</div>';
    html += '<div class="col-md-6 mb-3"><strong>Salary Range:</strong> ' + salaryLabel + '</div>';

    if (details.maritalStatus || details.partnerOccupation || details.favoriteRestaurant || details.monthlyFoodSpend || details.interestFbFranchise || details.pdpaConsent !== undefined) {
      html += '<div class="col-md-12"><hr></div>';
      html += '<h6 class="mb-3 text-primary">Survey & Consent Responses</h6>';
      if (details.maritalStatus) html += '<div class="col-md-6 mb-2"><strong>Status Perkahwinan:</strong> ' + details.maritalStatus + '</div>';
      if (details.partnerOccupation) html += '<div class="col-md-6 mb-2"><strong>Pekerjaan Pasangan:</strong> ' + details.partnerOccupation + '</div>';
      if (details.favoriteRestaurant) html += '<div class="col-md-6 mb-2"><strong>Restoran/Cafe Kegemaran:</strong> ' + details.favoriteRestaurant + '</div>';
      if (details.monthlyFoodSpend) html += '<div class="col-md-6 mb-2"><strong>Anggaran Perbelanjaan Makanan/Bulan:</strong> ' + details.monthlyFoodSpend + '</div>';
      if (details.interestFbFranchise) html += '<div class="col-md-6 mb-2"><strong>Minat Franchise F&B (RM5k-30k):</strong> ' + details.interestFbFranchise + '</div>';
      if (details.declarationConsent !== undefined) html += '<div class="col-md-6 mb-2"><strong>Pengesahan & Persetujuan:</strong> ' + (details.declarationConsent ? '<span class="badge bg-success">Disetujui</span>' : '<span class="badge bg-secondary">N/A</span>') + '</div>';
      if (details.pdpaConsent !== undefined) html += '<div class="col-md-6 mb-2"><strong>Pengakuan PDPA & CTOS:</strong> ' + (details.pdpaConsent ? '<span class="badge bg-success">Disetujui</span>' : '<span class="badge bg-secondary">N/A</span>') + '</div>';
    }

    html += '<div class="col-md-12"><hr></div>';
    html += '<h6 class="mb-3 text-primary">Submitted Documents</h6>';
    html += _renderDocCell('IC Front', details.icFrontFile, app._id, 'icFront', icFrontHist, options);
    html += _renderDocCell('IC Back', details.icBackFile, app._id, 'icBack', icBackHist, options);
    html += _renderDocCell('Payslip', details.payslipFile, app._id, 'payslip', payslipHist, options);
    if (details.ctosConsentFile) {
      var ctosHist = renderDocHistoryHTML(details.ctosConsentHistory, prefix + 'CtosHistoryCollapse');
      html += _renderDocCell('CTOS Consent Form', details.ctosConsentFile, app._id, 'ctosConsent', ctosHist, options);
    }

    if (options.showApprovedBy && options.approvedByHtml) {
      html += options.approvedByHtml;
    }
    html += '</div>';
    return html;
  }

  return {
    salaryRangeMap: salaryRangeMap,
    getSalaryLabel: getSalaryLabel,
    setLanguage: setLanguage,
    getCurrentLanguage: getCurrentLanguage,
    renderEmploymentFormHTML: renderEmploymentFormHTML,
    renderSurveyQuestionsHTML: renderSurveyQuestionsHTML,
    renderDownloadDocsFormHTML: renderDownloadDocsFormHTML,
    renderUploadDocsFormHTML: renderUploadDocsFormHTML,
    validateApplicationForm: validateApplicationForm,
    collectApplicationDetails: collectApplicationDetails,
    renderDocHistoryHTML: renderDocHistoryHTML,
    formatDate: formatDate,
    renderViewDetailsHTML: renderViewDetailsHTML
  };

})();
