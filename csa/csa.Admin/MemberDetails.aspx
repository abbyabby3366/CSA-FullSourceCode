<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MemberDetails.aspx.cs" Inherits="csa.Admin.MemberDetails" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
    <!-- select2 -->
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2.min.css") %>" />
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2-bootstrap-5-theme.min.css") %>" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css">
     <style>
        .no-border {
            border: none;        /* Menghapus border */
            background: none;   /* Menghapus background (opsional) */
            outline: none;      /* Menghapus outline saat difokuskan */
            /* Tambahkan gaya lain sesuai kebutuhan */
        }

        .readonly-select {
            pointer-events: none;       /* Prevents clicking */
        }

        .readonly-select {
            pointer-events: none;       /* Prevents clicking */
        }

        #survey-yabam .select2-selection {
            border: none !important;
            background-color: white !important;
        }
        #survey-yabam .select2-selection .select2-selection--multiple {
            border: none !important;
        }

        #survey-yabam .select2-container .select2-selection--multiple .select2-selection__choice {
            background-color: unset;
        }

        #survey-yabam .select2-container--bootstrap-5 .select2-selection--multiple .select2-selection__rendered .select2-selection__choice {
            font-size: unset;
        }
    </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfModelView" runat="server" />
    <div class="page-content">
        <div class="container-fluid">
            <!-- page header -->
            <div class="row mb-3 pb-1">
                <div class="col-12">
                    <div class="d-flex align-items-lg-center flex-lg-row flex-column">
                        <div class="flex-grow-1">
                            <h4 class="fs-16 mb-1">All Member Details</h4>
                            <%--<p class="text-muted mb-0">Here's what's happening with your store today.</p>--%>
                        </div>
                        <div class="mt-3 mt-lg-0">
                            <div class="form">
                                <div class="row g-3 mb-0 align-items-center">
                                    <%--<div class="col-sm-auto">
                                        <div class="input-group">
                                            <input type="text" class="form-control border-0 dash-filter-picker shadow" data-provider="flatpickr" data-range-date="true" data-date-format="d M, Y" data-deafult-date="01 Jan 2022 to 31 Jan 2022">
                                            <div class="input-group-text bg-primary border-primary text-white">
                                                <i class="ri-calendar-2-line"></i>
                                            </div>
                                        </div>
                                    </div>--%>

                                    <%--<div class="col-auto">
                                        <button type="button" class="btn btn-soft-success"><i class="ri-add-circle-line align-middle me-1"></i> Add Product</button>
                                    </div>

                                    <div class="col-auto">
                                        <button type="button" class="btn btn-soft-info btn-icon waves-effect waves-light layout-rightside-btn"><i class="ri-pulse-line"></i></button>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <!-- left panel -->
                <div class="col-12 col-xxl-3 col-xl-3 col-lg-4 col-md-5 col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="profile-bg-cover text-center px-4 py-4">
                                <div class="profile-user position-relative d-inline-block mx-auto">
                                    <img id="imgProfile" src="assets/images/users/user-dummy-img.jpg" onerror="this.onerror=null; this.src='assets/images/users/user-dummy-img.jpg';" class="rounded-circle avatar-xl img-thumbnail user-profile-image" alt="user-profile-image">
                                    <div class="avatar-xs p-0 rounded-circle profile-photo-edit">
                                        <input id="profile-img-file-input" type="file" class="profile-img-file-input">
                                        <label for="profile-img-file-input" class="profile-photo-edit avatar-xs">
                                            <span class="avatar-title rounded-circle bg-light text-body">
                                                <i class="ri-camera-fill"></i>
                                            </span>
                                        </label>
                                    </div>
                                </div>
                            </div>

                            <div class="row g-2 mt-2">
                                <div class="col-mx" role="tablist">
                                    <button type="button" data-bs-toggle="tab" href="#user-details" role="tab" aria-selected="false" class="btn btn-primary bg-gradient waves-effect waves-light text-start w-100 mb-2" id="tabUserDetails"><i class="ri-file-user-line"></i>User Details</button>
                                    <button type="button" data-bs-toggle="tab" href="#bank-details" role="tab" aria-selected="false" class="btn btn-soft-primary waves-effect waves-light text-start w-100 mb-2" id="tabBankDetails"><i class="ri-bank-line"></i>User Bank Details</button>
                                    <button type="button" data-bs-toggle="tab" href="#change-password" role="tab" aria-selected="false" class="btn btn-soft-primary waves-effect waves-light text-start w-100 mb-2" id="tabChangePass"><i class="ri-lock-line"></i>Change Password</button>
                                    <button type="button" data-bs-toggle="tab" href="#survey-yabam" role="tab" aria-selected="false" class="btn btn-soft-primary waves-effect waves-light text-start w-100 mb-2" id="tabSurveyYabam"><i class="ri-lock-line"></i>YABAM Survey Result</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--<div class="card">
                        <div class="card-body">
                            <div class="d-flex align-items-center mb-5">
                                <div class="flex-grow-1">
                                    <h5 class="card-title mb-0">Complete Your Profile</h5>
                                </div>
                                <div class="flex-shrink-0">
                                    <a href="javascript:void(0);" class="badge bg-light text-primary fs-12"><i class="ri-edit-box-line align-bottom me-1"></i>Edit</a>
                                </div>
                            </div>
                            <div class="progress animated-progress custom-progress progress-label">
                                <div class="progress-bar bg-danger" role="progressbar" style="width: 30%" aria-valuenow="30" aria-valuemin="0" aria-valuemax="100">
                                    <div class="label">30%</div>
                                </div>
                            </div>
                        </div>
                    </div>--%>

                     <div class="card">
                        <div class="card-body">
                            <div class="d-flex align-items-center mb-2">
                                <div class="flex-grow-1">
                                    <h5 class="card-title mb-0">Referral Survey Sign Up Link</h5>
                                </div>
                                <div class="flex-shrink-0">
                                    <a href="#" id="btnCopyRefLink" class="badge bg-light text-primary fs-12"><i class="ri-edit-box-line align-bottom me-1"></i>Copy</a>
                                </div>
                            </div>
                            <input type="hidden" id="txtRefLink" />
                            <a href="#" id="anchorReferralLink" target="_blank"></a>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="d-flex align-items-center mb-2">
                                <div class="flex-grow-1">
                                    <h5 class="card-title mb-0">Member Information</h5>
                                </div>
                            </div>
                            <div class="d-flex justify-content-between">
                                <span>Create Date</span>
                                <span id="spanCreateDate"></span>
                            </div>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="d-flex align-items-center mb-2">
                                <div class="flex-grow-1">
                                    <h5 class="card-title mb-0">Wallet</h5>
                                </div>
                            </div>
                            <div class="row ">
                                <div class="col-12">
                                    <input type="text" class="form-control decimal-input" id="txtCashWallet">
                                </div>
                            </div>
                            <button type="button" class="btn btn-primary mt-2 card-action" id="btnSaveWallet">Save Changes</button>
                                
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="d-flex align-items-center mb-2">
                                <div class="flex-grow-1">
                                    <h5 class="card-title mb-0">Savings</h5>
                                </div>
                            </div>
                            <div class="row ">
                                <div class="col-12">
                                    <input type="text" class="form-control decimal-input" id="txtWalletSavings">
                                </div>
                            </div>
                            <button type="button" class="btn btn-primary mt-2 card-action" id="btnSaveWalletSavings">Save Changes</button>
                                
                        </div>
                    </div>
                </div>

                <!-- right panel -->
                <div class="col-12 col-xxl-9 col-xl-9 col-lg-8 col-md-7 col-sm-12">
                    <!-- tab content -->
                    <div class="tab-content">
                        <div class="tab-pane" id="user-details" role="tabpanel">
                            <div class="card">
                                <div class="card-header align-items-center d-flex">
                                    <h5 class="card-title mb-0 flex-grow-1"><i class="ri-file-user-line"></i>User Details</h5>
                                </div>

                                <div class="card-body" id="card-content">
                                    <div class="row g-3 mb-2">
                                        <div class="col-mx">
                                            <div class="form">
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtMemberId" class="form-label">Member ID</label>
                                                            <input type="text" class="form-control" id="txtMemberId" placeholder="Member ID" disabled>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="ddlAccountStatus" class="form-label">Account Status</label>
                                                            <select class="form-control" id="ddlAccountStatus">
                                                                <option value="1">Active</option>
                                                                <option value="2">Inactive</option>
                                                                <option value="3">Unverified</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="ddlReferral" class="form-label">Referral</label>
                                                            <select id="ddlReferral"></select>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtPhoneNo" class="form-label">Phone Number</label>
                                                            <div class="d-flex align-items-center gap-3">
                                            <span>+60</span>
                                                            <input type="text" class="form-control" id="txtPhoneNo" placeholder="Phone No" disabled/>
                                                                </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtFirstName" class="form-label">First Name</label>
                                                            <input type="text" class="form-control" id="txtFirstName" placeholder="First Name" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtLastName" class="form-label">Last Name</label>
                                                            <input type="text" class="form-control" id="txtLastName" placeholder="Last Name" />
                                                        </div>
                                                    </div>                                                    
                                                    <%--<div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtEmail" class="form-label">Email Address</label>
                                                            <input type="email" class="form-control" id="txtEmail" placeholder="Email Address">
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtIdentificationNumber" class="form-label">Identification Number</label>
                                                            <input type="text" class="form-control" id="txtIdentificationNumber" placeholder="Identification Number" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="upldIC" class="form-label ">Upload IC</label>
                                                            <input class="form-control" type="file" id="upldIC">
                                                            <a id="anchorIc" href="#" target="_blank" class="d-none"></a>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="upldpayslip" class="form-label ">Upload Payslip</label>
                                                            <input class="form-control" type="file" id="upldpayslip">
                                                            <a id="anchorpayslip" href="#" target="_blank" class="d-none"></a>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6 d-none">
                                                        <div class="mb-3">
                                                            <label for="txtSalary" class="form-label">Salary</label>
                                                            <input type="text" class="form-control" id="txtSalary" placeholder="Salary" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label>Salary Range</label>
                                                            <select class="form-select" id="ddlSalaryRange">
                                                                <option value="0">Select an option</option>
                                                                <option value="1">Kurang RM2,500</option>
                                                                <option value="2">RM2,501 - RM3,170</option>
                                                                <option value="3">RM3,171 - RM3,970</option>
                                                                <option value="4">RM3,971 - RM4,850</option>
                                                                <option value="5">RM4,851 - RM5,880</option>
                                                                <option value="6">RM5,881 - RM7,100</option>
                                                                <option value="7">RM7,101 - RM8,700</option>
                                                                <option value="8">RM8,701 - RM10,970</option>
                                                                <option value="9">RM10,971 - RM15,040</option>
                                                                <option value="10">RM15,041 dan lebih</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="ddlGender" class="form-label">Gender</label>
                                                            <select class="form-control" id="ddlGender">
                                                                <option value="1">MALE</option>
                                                                <option value="2">FEMALE</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtAddress" class="form-label">Address</label>
                                                            <textarea class="form-control" id="txtAddress" rows="3" placeholder="Address"></textarea>
                                                        </div>
                                                    </div>--%>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="ddlState" class="form-label">State</label>
                                                            <select class="form-control" id="ddlState">
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="ddlCountry" class="form-label">Country</label>
                                                            <select class="form-control" id="ddlCountry">
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <%--<div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtCompanyName" class="form-label">Company / Employer Name</label>
                                                            <input type="text" class="form-control" id="txtCompanyName" placeholder="Company / Employer Name" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="ddlCompanyType" class="form-label">Company Type</label>
                                                            <select class="form-control" id="ddlCompanyType">
                                                                <option value="1">Goverment</option>
                                                                <option value="2">Private</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtDesignation" class="form-label">Designation</label>
                                                            <input type="text" class="form-control" id="txtDesignation" placeholder="Designation" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="ddlSector" class="form-label">Detailed sector</label>
                                                            <select class="form-control" id="ddlSector">
                                                                <option value="0">Select an option</option>
                                                                <option value="1">Automotive</option>
                                                                <option value="2">Banking</option>
                                                                <option value="3">Education</option>
                                                                <option value="4">Electric Power</option>
                                                                <option value="5">Insurance</option>
                                                                <option value="6">Medical</option>
                                                                <option value="7">Others (Government)</option>
                                                                <option value="8">Others (Private)</option>
                                                                <option value="9">Religious</option>
                                                                <option value="10">Telecommunication</option>
                                                                <option value="11">Uniform Body</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="ddlEmploymentStatus" class="form-label">Employment Status</label>
                                                            <select class="form-control" id="ddlEmploymentStatus">
                                                                <option value="0">Select an option</option>
                                                                <option value="1">Permanent</option>
                                                                <option value="2">Contract</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtRetireAge" class="form-label">Retire Age</label>
                                                            <input type="number" class="form-control" id="txtRetireAge" placeholder="Retirement Age" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtYearOfService" class="form-label">Year of Service</label>
                                                            <input type="number" class="form-control" id="txtYearOfService" placeholder="Year of Service" />
                                                        </div>
                                                    </div>--%>


                                                    <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Program/Event Name</label>
                                                        <select class="form-select" name="programEvent">
                                                            <option value="0">Select an option</option>
                                                            <option value="1" selected>YABAM</option>
                                                            <%--<option value="2">PFC</option>
                                                            <option value="3">Siri jelajah etc</option>--%>
                                                        </select>
                                                    </div>
                                                </div>
                                                <%--<div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Full Name as per IC</label>
                                                        <input type="text" class="form-control" name="fullName"/>
                                                    </div>
                                                </div>--%>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Date of Birth</label>
                                                        <input type="text" class="form-control date-input" name="dateOfBirth" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Gender</label>
                                                        <select class="form-select" name="gender">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">Male</option>
                                                            <option value="2">Female</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Identification Number</label>
                                                        <input type="text" class="form-control" name="icNumber"/>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Address (Home/Current)</label>
                                                        <input type="text" class="form-control" name="address" />
                                                    </div>
                                                </div>
                                                <%--<div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Phone Number</label>
                                                        <input type="text" class="form-control" name="phoneNumber" disabled />
                                                    </div>
                                                </div>--%>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Email Address</label>
                                                        <input type="email" class="form-control" name="email" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Tax Number</label>
                                                        <input type="text" class="form-control" name="taxNumber" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Race</label>
                                                        <select class="form-select" name="race">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">Malay</option>
                                                            <option value="2">Chinese</option>
                                                            <option value="3">Indian</option>
                                                            <option value="4">Sabahan</option>
                                                            <option value="5">Sarawakian</option>
                                                            <option value="6">Other</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Religion</label>
                                                        <select class="form-select" name="religion">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">Islam</option>
                                                            <option value="2">Hindu</option>
                                                            <option value="3">Buddhist</option>
                                                            <option value="4">Christian</option>
                                                            <option value="6">Others</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Highest Level of Education</label>
                                                        <select class="form-select" name="education">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">Diploma</option>
                                                            <option value="2">Bachelor</option>
                                                            <option value="3">Master</option>
                                                            <option value="4">PhD</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Marital Status</label>
                                                        <select class="form-select" name="maritalStatus">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">Married</option>
                                                            <option value="2">Single</option>
                                                            <option value="3">Divorce</option>
                                                            <option value="4">Polygamy</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <h5 class="fs-15 col-12 mt-3">Spouse Details</h5>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Spouse's Full Name</label>
                                                        <input type="text" class="form-control" name="spouseName" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Spouse's Identification Number</label>
                                                        <input type="text" class="form-control" name="spouseIC" />
                                                    </div>
                                                </div> 
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Spouse's Contact Information</label>
                                                        <input type="text" class="form-control" name="spouseContactInformation" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Spouse's Occupation</label>
                                                        <input type="text" class="form-control" name="spouseOccupation" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Spouse's Company Address</label>
                                                        <input type="text" class="form-control" name="spouseCompanyAddress" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Spouse's Salary</label>
                                                        <input type="text" class="form-control decimal-input" name="spouseSalary" />
                                                    </div>
                                                </div>

                                                <h5 class="fs-15 col-12 mt-3">Family Details</h5>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Number of Dependent</label>
                                                        <input type="number" class="form-control" name="familyNumberOfDependent" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Do you have any OKU family members</label>
                                                        <select class="form-select" name="familyHasOKU">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">Yes</option>
                                                            <option value="2">No</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Father's Name</label>
                                                        <input type="text" class="form-control" name="familyFatherName" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Father's Contact Number</label>
                                                        <input type="text" class="form-control" name="familyFatherContactNumber" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Father's Address</label>
                                                        <input type="text" class="form-control" name="familyFatherAddress" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Mother's Name</label>
                                                        <input type="text" class="form-control" name="familyMotherName" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Mother's Contact Number</label>
                                                        <input type="text" class="form-control" name="familyMotherContactNumber" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Mother's Address</label>
                                                        <input type="text" class="form-control" name="familyMotherAddress" />
                                                        <div class="mt-2">
                                                            <label>same with father's address</label>
                                                            <input type="checkbox" id="cbCopyFatherAddress" />
                                                        </div>
                                                    </div>
                                                </div>


                                                <h5 class="fs-15 col-12 mt-3">Company/Employment Details</h5>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Employer Type</label>
                                                        <select class="form-select nyatakan" name="companyEmpoyerTypeId" data-nyatakan="6">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">GOV/AN</option>
                                                            <option value="2">GLC</option>
                                                            <option value="3">MNC</option>
                                                            <option value="4">Private</option>
                                                            <option value="5">Professional Certs</option>
                                                            <option value="6">Others</option>
                                                        </select>
                                                         <div id="nyatakan_companyEmpoyerTypeId" class="d-none">
                                                            <label class="mt-2">Sila nyatakan</label>
                                                            <input type="text" class="form-control" name="companyEmpoyerTypeOther" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Company Name</label>
                                                        <input type="text" class="form-control" name="companyName" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Job Title</label>
                                                        <input type="text" class="form-control" name="companyJobTitle" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Detailed sector</label>
                                                        <select class="form-select nyatakan" name="companySectorId" data-nyatakan="<%= csa.Library.Constant.OTHER_NUMBER %>">
                                                            
                                                        </select>
                                                        <div id="nyatakan_companySectorId" class="d-none">
                                                            <label class="mt-2">Sila nyatakan</label>
                                                            <input type="text" class="form-control" name="companySectorOther" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Department</label>
                                                        <select class="form-select nyatakan" name="companyDepartmentId" data-nyatakan="<%= csa.Library.Constant.OTHER_NUMBER %>">
                                                        </select>
                                                        <div id="nyatakan_companyDepartmentId" class="d-none">
                                                            <label class="mt-2">Sila nyatakan</label>
                                                            <input type="text" class="form-control" name="companyDepartmentOther" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Company Address</label>
                                                        <input type="text" class="form-control" name="companyAddress" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Office Contact Number/HR</label>
                                                        <input type="text" class="form-control" name="companyOfficeContactNumber" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Employer Name</label>
                                                        <input type="text" class="form-control" name="companyEmployerName" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Employment Status</label>
                                                        <select class="form-select nyatakan" name="companyEmploymentStatusId" data-nyatakan="4">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">Fixed</option>
                                                            <option value="2">Contract</option>
                                                            <option value="3">Self Employed</option>
                                                            <option value="4">Other</option>
                                                        </select>
                                                        <div id="nyatakan_companyEmploymentStatusId" class="d-none">
                                                            <label class="mt-2">Sila nyatakan</label>
                                                            <input type="text" class="form-control" name="companyEmploymentStatusOther" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Retirement Age</label>
                                                        <input type="number" class="form-control" name="companyRetirementAge" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Year of Service</label>
                                                        <select class="form-select" name="companyYearOfService">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">Kurang dari 1</option>
                                                            <option value="2">1-2</option>
                                                            <option value="3">3-5</option>
                                                            <option value="4">Lebih dari 5</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Salary Range</label>
                                                        <select class="form-select" name="salaryRange">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">Kurang RM2,500</option>
                                                            <option value="2">RM2,501 - RM3,170</option>
                                                            <option value="3">RM3,171 - RM3,970</option>
                                                            <option value="4">RM3,971 - RM4,850</option>
                                                            <option value="5">RM4,851 - RM5,880</option>
                                                            <option value="6">RM5,881 - RM7,100</option>
                                                            <option value="7">RM7,101 - RM8,700</option>
                                                            <option value="8">RM8,701 - RM10,970</option>
                                                            <option value="9">RM10,971 - RM15,040</option>
                                                            <option value="10">RM15,041 dan lebih</option>
                                                        </select>
                                                    </div>
                                                </div>



                                                <h5 class="fs-15 col-12 mt-3">Emergency Contact (not living together)</h5>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Emergency Contact Person</label>
                                                        <input type="text" class="form-control" name="emergencyContactPerson" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Relationship to Emergency Contact</label>
                                                        <select class="form-select" name="emergencyRelationShipId">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">Father</option>
                                                            <option value="2">Mother</option>
                                                            <option value="3">Sibling</option>
                                                            <option value="4">Wife</option>
                                                            <option value="5">Husband</option>
                                                            <option value="6">Others</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Emergency Contact Number</label>
                                                        <input type="text" class="form-control" name="emergencyContactNumber" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Emergency Contact ICNumber</label>
                                                        <input type="text" class="form-control" name="emergencyIC" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Emergency Contact Occupation</label>
                                                        <input type="text" class="form-control" name="emergencyOccupation" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Emergency Contact Address</label>
                                                        <input type="text" class="form-control" name="emergencyAddress" />
                                                    </div>
                                                </div>



                                                <%--<h5 class="fs-15 col-12 mt-3">Bank Details</h5>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Bank Name</label>
                                                        <select class="form-select" name="bankId"></select>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Account Number</label>
                                                        <input type="text" class="form-control" name="bankAccountNumber" />
                                                    </div>
                                                </div>--%>

                                                <h5 class="fs-15 col-12 mt-3">Upload Documents</h5>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label class="form-label">IC</label>
                                                        <%--<input type="file" class="form-control" name="upldIc" accept=".png, .jpg, .jpeg, .pdf"/>
                                                        <a id="anchorIc" href="#" target="_blank" class="d-none"></a>--%>
                                                        <span class="float-end d-none" id="icAction">
                                                                <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                                <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                            </span>
                                                            <div class="input-group">
                                                                <label class="btn btn-primary input-group-text btn-upload" for="upldic">Upload File</label>
                                                                <input class="form-control" id="txtic" readonly onclick="openInputFile('upldic')"/>
                                                            </div>
                                                            <input class="form-control d-none" type="file" id="upldic" onchange="onChangeUpload(event,'txtic')">
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label class="form-label">Payslip</label>
                                                        <%--<input type="file" class="form-control" name="upldPayslip" accept=".png, .jpg, .jpeg, .pdf"/>
                                                        <a id="anchorPayslip" href="#" target="_blank" class="d-none"></a>--%>
                                                        <span class="float-end d-none" id="payslipAction">
                                                                <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                                <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                            </span>
                                                            <div class="input-group">
                                                                <label class="btn btn-primary input-group-text btn-upload" for="upldpayslip">Upload File</label>
                                                                <input class="form-control" id="txtpayslip" readonly onclick="openInputFile('upldpayslip')"/>
                                                            </div>
                                                            <input class="form-control d-none" type="file" id="upldpayslip" onchange="onChangeUpload(event,'txtpayslip')">
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label class="form-label ">Offer letter / Confirmation letter (KIV)</label>
                                                        <%--<input type="file" class="form-control" name="upldOfferLetter" accept=".png, .jpg, .jpeg, .pdf"/>
                                                        <a id="anchorOfferLetter" href="#" target="_blank" class="d-none"></a>--%>
                                                         <span class="float-end d-none" id="offerletterAction">
                                                                <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                                <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                            </span>
                                                            <div class="input-group">
                                                                <label class="btn btn-primary input-group-text btn-upload" for="upldofferletter">Upload File</label>
                                                                <input class="form-control" id="txtofferletter" readonly onclick="openInputFile('upldofferletter')"/>
                                                            </div>
                                                            <input class="form-control d-none" type="file" id="upldofferletter" onchange="onChangeUpload(event,'txtofferletter')">
                                                    </div>
                                                </div>



                                                <h5 class="fs-15 col-12 mt-3">Other Information (KIV)</h5>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Preferred Language</label>
                                                        <input type="text" class="form-control" name="otherLanguage" />
                                                    </div>
                                                </div> 
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Hobbies/Interest</label>
                                                        <input type="text" class="form-control" name="otherHobbies" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Social Media Handles</label>
                                                        <input type="text" class="form-control" name="otherSocialMediaHandles" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>FB Name</label>
                                                        <input type="text" class="form-control" name="otherFbName" />
                                                    </div>
                                                </div>

                                                    <div class="col-lg-12">
                                                    <div class="mb-3">
                                                        <label>Remark</label>
                                                        <input type="text" class="form-control" name="adminRemark" disabled/>
                                                    </div>
                                                </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="card-footer card-action">
                                    <div class="col-lg-12">
                                        <div class="hstack gap-2 justify-content-end">
                                            <button type="button" class="btn btn-primary" id="btnSaveDetails">Save</button>
                                            <a href="<%= Page.ResolveUrl("~/Members")%>" class="btn btn-soft-success">Cancel</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="tab-pane" id="bank-details" role="tabpanel">
                            <div class="card">
                                <div class="card-header align-items-center d-flex">
                                    <h5 class="card-title mb-0 flex-grow-1"><i class="ri-bank-line"></i>Bank Details</h5>
                                </div>

                                <div class="card-body">
                                    <div class="row g-3 mb-2">
                                        <div class="col-mx">
                                            <div class="form">
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="ddlBankName" class="form-label">Bank Name</label>
                                                            <select class="form-control nyatakan" id="ddlBankName" name="ddlBankName" data-nyatakan="<%= csa.Library.Constant.OTHER_NUMBER %>">
                                                                
                                                            </select>
                                                            <div id="nyatakan_ddlBankName" class="d-none">
                                                                <label class="mt-2">Sila nyatakan</label>
                                                                <input type="text" class="form-control" id="txtBankOther" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtBankAccHolder" class="form-label">Bank Account Name</label>
                                                            <input type="text" class="form-control" id="txtBankAccHolder" placeholder="Bank Account Name">
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtBankAccNo" class="form-label">Bank Account Number</label>
                                                            <input type="text" class="form-control" id="txtBankAccNo" placeholder="Bank Account Number">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="card-footer card-action">
                                    <div class="col-lg-12">
                                        <div class="hstack gap-2 justify-content-end">
                                            <button type="button" class="btn btn-primary" id="btnSaveBank">Save</button>
                                            <a href="<%= Page.ResolveUrl("~/Members")%>" class="btn btn-soft-success">Cancel</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="tab-pane" id="change-password" role="tabpanel">
                            <div class="card">
                                <div class="card-header align-items-center d-flex">
                                    <h5 class="card-title mb-0 flex-grow-1"><i class="ri-lock-line"></i>Change Password</h5>
                                </div>

                                <div class="card-body">
                                    <div class="row g-3 mb-2">
                                        <div class="col-mx">
                                            <div class="form">
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtCurrentPass" class="form-label">Current Admin Password</label>
                                                            <input type="password" class="form-control" id="txtCurrentPass" placeholder="Current Admin Password">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtNewPassword" class="form-label">New Password</label>
                                                            <input type="password" class="form-control" id="txtNewPassword" placeholder="New Password">
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtConfirmNewPass" class="form-label">Confirm New Password</label>
                                                            <input type="password" class="form-control" id="txtConfirmNewPass" placeholder="Confirm New Password">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="card-footer card-action">
                                    <div class="col-lg-12">
                                        <div class="hstack gap-2 justify-content-end">
                                            <button type="button" class="btn btn-primary" id="btnSavePassword">Save</button>
                                            <a href="<%= Page.ResolveUrl("~/Members")%>" class="btn btn-soft-success">Cancel</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="tab-pane" id="survey-yabam" role="tabpanel">
                             <%@ Register TagPrefix="uc" TagName="SurveyYabamResult" Src="~/Uc/SurveyYabamResult.ascx" %>
                                <uc:SurveyYabamResult ID="SurveyYabamResult" runat="server" Visible="false"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent" runat="server">
    <!-- moment js -->
    <script src="<%=Page.ResolveUrl("~/assets/libs/moment/min/moment.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/assets/libs/moment/min/moment-with-locales.min.js") %>"></script>
    <!-- datatables js -->
    <!-- select2 js -->
    <script type='text/javascript' src="<%=Page.ResolveUrl("~/assets/libs/select2/js/select2.min.js") %>"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>    
    <script type="text/javascript">
        $("form").submit(function (e) {
            e.preventDefault();
        });

        $(".date-input").datepicker({
            format: 'dd-mm-yyyy',
            autoclose: true
        });

        var vm,vmSurvey
        var txtMemberId = $('#txtMemberId')
        var ddlAccountStatus = $('#ddlAccountStatus')
        var txtFirstName = $('#txtFirstName')
        var txtLastName = $('#txtLastName')
        var txtPhoneNo = $('#txtPhoneNo')
        //var txtEmail = $('#txtEmail')
        //var upldIC = $('#upldIC')
        var upldProfile = $('#profile-img-file-input')
        //var txtSalary = $('#txtSalary')
        //var ddlGender = $('#ddlGender')
        //var txtAddress = $('#txtAddress')
        var ddlState = $('#ddlState')
        var ddlCountry = $('#ddlCountry')
        //var txtCompanyName = $('#txtCompanyName')
        //var txtDesignation = $('#txtDesignation')
        var txtNewPassword = $('#txtNewPassword')
        var txtConfirmNewPass = $('#txtConfirmNewPass')
        var ddlBankName = $('#ddlBankName')
        var txtBankAccHolder = $('#txtBankAccHolder')
        var txtBankAccNo = $('#txtBankAccNo')
        var btnSaveDetails = $('#btnSaveDetails')
        var btnSaveBank = $('#btnSaveBank')
        var btnSavePassword = $('#btnSavePassword')
        var txtCurrentPass = $('#txtCurrentPass')
        var ddlReferral = $('#ddlReferral')
        //var txtIdentificationNumber = $('#txtIdentificationNumber')
        //var upldpayslip = $('#upldpayslip')
        //var ddlCompanyType = $('#ddlCompanyType')
        //var ddlSector = $('#ddlSector')
        //var ddlEmploymentStatus = $('#ddlEmploymentStatus')
        //var txtRetireAge = $('#txtRetireAge')
        //var txtYearOfService = $('#txtYearOfService')
        //var ddlSalaryRange = $('#ddlSalaryRange')

        var programEvent = $('[name="programEvent"]')
        var fullName = $('[name="fullName"]')
        var dateOfBirth = $('[name="dateOfBirth"]')
        var gender = $('[name="gender"]')
        var icNumber = $('[name="icNumber"]')
        var address = $('[name="address"]')
        var phoneNumber = $('[name="phoneNumber"]')
        var email = $('[name="email"]')
        var taxNumber = $('[name="taxNumber"]')
        var race = $('[name="race"]')
        var religion = $('[name="religion"]')
        var education = $('[name="education"]')
        var maritalStatus = $('[name="maritalStatus"]')
        var referralName = $('[name="referralName"]')
        var spouseName = $('[name="spouseName"]')
        var spouseIC = $('[name="spouseIC"]')
        var spouseContactInformation = $('[name="spouseContactInformation"]')
        var spouseOccupation = $('[name="spouseOccupation"]')
        var spouseCompanyAddress = $('[name="spouseCompanyAddress"]')
        var spouseSalary = $('[name="spouseSalary"]')
        var familyNumberOfDependent = $('[name="familyNumberOfDependent"]')
        var familyHasOKU = $('[name="familyHasOKU"]')
        var familyFatherName = $('[name="familyFatherName"]')
        var familyFatherContactNumber = $('[name="familyFatherContactNumber"]')
        var familyFatherAddress = $('[name="familyFatherAddress"]')
        var familyMotherName = $('[name="familyMotherName"]')
        var familyMotherContactNumber = $('[name="familyMotherContactNumber"]')
        var familyMotherAddress = $('[name="familyMotherAddress"]')
        var companyEmpoyerTypeId = $('[name="companyEmpoyerTypeId"]')
        var companyName = $('[name="companyName"]')
        var companyJobTitle = $('[name="companyJobTitle"]')
        var companySectorId = $('[name="companySectorId"]')
        var companySectorOther = $('[name="companySectorOther"]')
        var companyDepartmentId = $('[name="companyDepartmentId"]')
        var companyDepartmentOther = $('[name="companyDepartmentOther"]')
        var companyAddress = $('[name="companyAddress"]')
        var companyOfficeContactNumber = $('[name="companyOfficeContactNumber"]')
        var companyEmploymentStatusId = $('[name="companyEmploymentStatusId"]')
        var companyRetirementAge = $('[name="companyRetirementAge"]')
        var companyYearOfService = $('[name="companyYearOfService"]')
        var salaryRange = $('[name="salaryRange"]')
        var emergencyContactPerson = $('[name="emergencyContactPerson"]')
        var emergencyRelationShipId = $('[name="emergencyRelationShipId"]')
        var emergencyContactNumber = $('[name="emergencyContactNumber"]')
        var emergencyIC = $('[name="emergencyIC"]')
        var emergencyOccupation = $('[name="emergencyOccupation"]')
        var emergencyAddress = $('[name="emergencyAddress"]')
        //var bankId = $('[name="bankId"]')
        //var bankAccountNumber = $('[name="bankAccountNumber"]')
        //var upldIc = $('[name="upldIc"]')
        //var upldPayslip = $('[name="upldPayslip"]')
        //var upldOfferLetter = $('[name="upldOfferLetter"]')
        var otherLanguage = $('[name="otherLanguage"]')
        var otherHobbies = $('[name="otherHobbies"]')
        var otherSocialMediaHandles = $('[name="otherSocialMediaHandles"]')
        var otherFbName = $('[name="otherFbName"]')
        var companyEmploymentStatusOther = $('[name="companyEmploymentStatusOther"]')
        var companyEmpoyerTypeOther = $('[name="companyEmpoyerTypeOther"]')
        var companyEmployerName = $('[name="companyEmployerName"]')
        var txtBankOther = $('#txtBankOther')

        //wallet
        var txtCashWallet = $('#txtCashWallet')
        var txtWalletSavings = $('#txtWalletSavings')

        $('[multiple="multiple"]').select2({ placeholder: 'Select an option', theme: 'bootstrap-5' })

        ddlReferral.select2({
            placeholder: 'Select an option',
            theme: 'bootstrap-5',
            minimumInputLength: 1,
            ajax: {
                url: '<%=Page.ResolveUrl("~/Member/GetMemberVts")%>', // Replace with your API endpoint
                type: 'POST',
                dataType: 'json',
                data: function (params) {
                    return {
                        q: params.term, // search term
                        exceptMemberId: vm.Member.MemberId
                    };
                },
                processResults: function (data) {
                    // Process the results into the format expected by Select2
                    return {
                        results: data.map(item => ({
                            id: item.Value,         // Assuming the API returns an id field
                            text: item.Text      // Assuming the API returns a name field
                        }))
                    };
                },
                cache: true
            }
        });

        function tabUIControl() {
            $('button[data-bs-toggle="tab"]').on('shown.bs.tab', function (e) {
                let target = $(e.target).attr("href");
                let tabUser = $('#tabUserDetails');
                let tabBank = $('#tabBankDetails');
                let tabChangePass = $('#tabChangePass');
                let tabSurveyYabam = $('#tabSurveyYabam');

                if (target == null) { return; }
                if (tabUser.length == 0 || tabBank.length == 0) { return; }

                switch (target) {
                    case '#user-details':
                        tabUser.removeClass('btn-soft-primary').addClass('btn-primary');
                        tabBank.removeClass('btn-primary').addClass('btn-soft-primary');
                        tabChangePass.removeClass('btn-primary').addClass('btn-soft-primary');
                        tabSurveyYabam.removeClass('btn-primary').addClass('btn-soft-primary');
                        break;
                    case '#bank-details':
                        tabUser.removeClass('btn-primary').addClass('btn-soft-primary');
                        tabBank.removeClass('btn-soft-primary').addClass('btn-primary');
                        tabChangePass.removeClass('btn-primary').addClass('btn-soft-primary');
                        tabSurveyYabam.removeClass('btn-primary').addClass('btn-soft-primary');
                        break;
                    case '#change-password':
                        tabUser.removeClass('btn-primary').addClass('btn-soft-primary');
                        tabBank.removeClass('btn-primary').addClass('btn-soft-primary');
                        tabChangePass.removeClass('btn-soft-primary').addClass('btn-primary');
                        tabSurveyYabam.removeClass('btn-primary').addClass('btn-soft-primary');
                        break;
                    case '#survey-yabam':
                        tabUser.removeClass('btn-primary').addClass('btn-soft-primary');
                        tabBank.removeClass('btn-primary').addClass('btn-soft-primary');
                        tabChangePass.removeClass('btn-primary').addClass('btn-soft-primary');
                        tabSurveyYabam.removeClass('btn-soft-primary').addClass('btn-primary');
                        break;
                }
            });

            $('#tabUserDetails').click();
        }

        $('.nyatakan').on('change', function (e) {
            $('#nyatakan_' + $(this).attr('name')).addClass('d-none')
            let showNyatakan = false
            const dataNyatakan = String($(this).data('nyatakan'))
            if ($(this).data('select2') !== undefined) {
                showNyatakan = $(this).val().includes(dataNyatakan)
            }
            else {
                showNyatakan = dataNyatakan == $(this).val()
            }
            if (showNyatakan) {
                $('#nyatakan_' + $(this).attr('name')).removeClass('d-none')
            }
        })

        $('#cbCopyFatherAddress').on('change', function () {
            if ($(this).is(':checked')) {
                $('[name="familyMotherAddress"]').val($('[name="familyFatherAddress"]').val())
            }
            else {
                $('[name="familyMotherAddress"]').val('')
            }
        })

        ddlCountry.change(function (e) {
            ddlState.empty()
            ddlState.append(`<option value='${0}'>Choose a state</option>`)
            vm.States.filter(x => x.CountryId == ddlCountry.val()).forEach(function (item) {
                ddlState.append(`<option value='${item.StateId}'>${item.Name}</option>`)
            })
        })

        function prepare() {
            vm.Banks.forEach(function (item) {
                ddlBankName.append(`<option value='${item.Key}'>${item.Text}</option>`)
            })

            vm.Countries.forEach(function (item) {
                ddlCountry.append(`<option value='${item.CountryId}'>${item.Name}</option>`)
            })
            ddlCountry.trigger('change')

            vm.Member.Sectors.splice(0, 0, { Key: 0, Text: 'Select an option' })
            vm.Member.Sectors.forEach(function (item) {
                $(companySectorId).append(`<option value='${item.Key}'>${item.Text}</option>`)
            })
            $(companySectorId).append(`<option value='<%= csa.Library.Constant.OTHER_NUMBER %>'>Lain-lain</option>`)
        }

        $(companySectorId).on('change', function () {
            $(companyDepartmentId).empty()
            $(companyDepartmentId).append(`<option value='0'>Select an option</option>`)
            vm.Member.JobPositions.filter(x => x.RefValue == $(this).val()).forEach(function (item) {
                $(companyDepartmentId).append(`<option value='${item.Value}'>${item.Text}</option>`)
            })
            if ($(this).val() != 0) {
                $(companyDepartmentId).append(`<option value='<%= csa.Library.Constant.OTHER_NUMBER %>'>Lain-lain</option>`)
            }
        })

        async function onSave(e) {
            //$(this).prop('disabled', true);

            //const payload = {
            //    MemberId: vm.Member.MemberId,
            //    MemberCode: txtMemberId.val(),
            //    FirstName: txtFirstName.val(),
            //    LastName: txtLastName.val(),
            //    PhoneNumber: txtPhoneNo.val(),
            //    Email: txtEmail.val(),
            //    StatusId: ddlAccountStatus.val(),
            //    Salary: txtSalary.val(),
            //    GenderId: ddlGender.val(),
            //    Address: txtAddress.val(),
            //    CountryId: ddlCountry.val(),
            //    StateId: ddlState.val(),
            //    CompanyName: txtCompanyName.val(),
            //    Occupation: txtDesignation.val(),
            //    IcFile: upldIC[0].files[0],
            //    PayslipFile: upldpayslip[0].files[0],
            //    ReferrerMemberId: ddlReferral.val(),
            //    ICNumber: txtIdentificationNumber.val(),
            //    CompanySectorId: ddlSector.val(),
            //    CompanyEmploymentStatusId: ddlEmploymentStatus.val(),
            //    RetirementAge: txtRetireAge.val(),
            //    YearOfService: txtYearOfService.val(),
            //    CompanyTypeId: ddlCompanyType.val(),
            //    SalaryRangeId: ddlSalaryRange.val()
            //}

            //const res = await ApiHelper.postFormData(window.location.origin + '/member/Update', payload)
            //if (!res.data.Error) {
            //    dialogHelper.success('Member updated')
            //} else {
            //    dialogHelper.error(res.data.Message)
            //}

            //$(this).prop('disabled', false);
            e.preventDefault()
            $(this).prop("disabled", true)

            try {
                const formData = new FormData()
                formData.append('IcFile', $('#upldic')[0].files[0])
                formData.append('PayslipFile', $('#upldpayslip')[0].files[0])
                formData.append('OfferLetterFile', $('#upldofferletter')[0].files[0])


                const data = {
                    MemberId:vm.Member.MemberId,
                    FirstName: txtFirstName.val(),
                    LastName: txtLastName.val(),
                    StatusId: ddlAccountStatus.val(),
                    ReferrerMemberId: ddlReferral.val(),
                    CountryId: ddlCountry.val(),
                    StateId: ddlState.val(),

                    ProgramEventId: programEvent.val(),
                    DateOfBirth: appHelper.convertToApiDate(dateOfBirth.val()),
                    Gender: gender.val(),
                    ICNumber: icNumber.val(),
                    Address: address.val(),
                    PhoneNumber: phoneNumber.val(),
                    Email: email.val(),
                    TaxNumber: taxNumber.val(),
                    RaceId: race.val(),
                    ReligionId: religion.val(),
                    HighestLevelOfEducationId: education.val(),
                    MaritalStatusId: maritalStatus.val(),
                    Spouse: {
                        FullName: spouseName.val(),
                        ICNumber: spouseIC.val(),
                        ContactInformation: spouseContactInformation.val(),
                        Occupation: spouseOccupation.val(),
                        CompanyAddress: spouseCompanyAddress.val(),
                        Salary: spouseSalary.val(),
                    },
                    Family: {
                        NumberOfDependent: familyNumberOfDependent.val(),
                        IsHasOKU: familyHasOKU.val(),
                        FatherName: familyFatherName.val(),
                        FatherContactNumber: familyFatherContactNumber.val(),
                        FatherAddress: familyFatherAddress.val(),
                        MotherName: familyMotherName.val(),
                        MotherContactNumber: familyMotherContactNumber.val(),
                        MotherAddress: familyMotherAddress.val(),
                    },
                    Company: {
                        EmployerTypeId: companyEmpoyerTypeId.val(),
                        CompanyName: companyName.val(),
                        JobTitle: companyJobTitle.val(),
                        SectorId: companySectorId.val() === 'null' ? null : companySectorId.val(),
                        SectorOther: companySectorOther.val(),
                        DepartmentId: companyDepartmentId.val() === 'null' ? null : companyDepartmentId.val(),
                        DepartmentOther: companyDepartmentOther.val(),
                        CompanyAddress: companyAddress.val(),
                        OfficeContactNumber: companyOfficeContactNumber.val(),
                        EmploymentStatusId: companyEmploymentStatusId.val(),
                        RetirementAge: companyRetirementAge.val(),
                        YearOfService: companyYearOfService.val(),
                        EmployerTypeOther: companyEmpoyerTypeOther.val(),
                        EmploymentStatusOther: companyEmploymentStatusOther.val(),
                        EmployerName: companyEmployerName.val()
                    },
                    Emergency: {
                        ContactPerson: emergencyContactPerson.val(),
                        RelationShipId: emergencyRelationShipId.val(),
                        ContactNumber: emergencyContactNumber.val(),
                        ICNumber: emergencyIC.val(),
                        Occupation: emergencyOccupation.val(),
                        Address: emergencyAddress.val(),
                    },
                    Bank: {
                        SalaryRangeId: salaryRange.val(),
                    },
                    Other: {
                        PreferredLanguage: otherLanguage.val(),
                        Hobbies: otherHobbies.val(),
                        SocialMediaHandles: otherSocialMediaHandles.val(),
                        FBName: otherFbName.val(),
                    }
                }
                formData.append('Json', JSON.stringify(data))
                const res = await ApiHelper.post(window.location.origin + '/Member/Update', formData)
                if (!res.data.Error) {
                    dialogHelper.successAutoRedirect('Member updated',location.href)
                } else {
                    dialogHelper.errorHTML(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }

            $(this).prop("disabled", false)
        }

        //txtSalary.on('change', appHelper.onCurrencyInput)
        upldProfile.change(async function (e) {
            const payload = {
                memberId: vm.Member.MemberId,
                imageFile: $(this)[0].files[0]
            }

            $('#imgProfile').attr('src', URL.createObjectURL(payload.imageFile))

            const res = await ApiHelper.postFormData(window.location.origin + '/member/ChangeMemberPicture', payload)
            if (res.data.Error) {
                dialogHelper.error(res.data.Message)
            }
        })

        btnSaveDetails.click(onSave)

        btnSavePassword.click(async function (e) {
            $(this).prop('disabled', true);

            const payload = {
                AdminId: <%= CurrentLoginAdmin.AdminId%>,
                MemberId: vm.Member.MemberId,
                CurrentPassword: txtCurrentPass.val(),
                NewPassword: txtNewPassword.val(),
                ConfirmNewPassword: txtConfirmNewPass.val(),
            }
            const res = await ApiHelper.post(window.location.origin + '/member/ChangePassword', payload)
            if (!res.data.Error) {
                dialogHelper.success('Password changed')
            } else {
                dialogHelper.error(res.data.Message)
            }

            $(this).prop('disabled', false);
        })

        btnSaveBank.click(async function (e) {
            $(this).prop('disabled', true);

            const payload = {
                MemberId: vm.Member.MemberId,
                BankId: ddlBankName.val(),
                BankOther: txtBankOther.val(),
                BankAccountNumber: txtBankAccNo.val(),
                BankAccountName: txtBankAccHolder.val(),
            }
            const res = await ApiHelper.post(window.location.origin + '/member/ChangeBankDetails', payload)
            if (!res.data.Error) {
                dialogHelper.success('Bank changed')
            } else {
                dialogHelper.error(res.data.Message)
            }

            $(this).prop('disabled', false);
        })

        function loadDetails() {
            txtMemberId.val(vm.Member.FileNumber)
            txtFirstName.val(vm.Member.FirstName)
            txtLastName.val(vm.Member.LastName)
            txtPhoneNo.val(vm.Member.PhoneNumber)
            //txtEmail.val(vm.Member.Email)
            ddlAccountStatus.val(vm.Member.StatusId).trigger('change')
            //txtSalary.val(vm.Member.Salary)
            //ddlGender.val(vm.Member.GenderId).trigger('change')
            //txtAddress.val(vm.Member.Address)
            ddlCountry.val(vm.Member.CountryId).trigger('change')
            ddlState.val(vm.Member.StateId).trigger('change')
            //txtCompanyName.val(vm.Member.CompanyName)
            //txtDesignation.val(vm.Member.Occupation)
            //txtIdentificationNumber.val(vm.Member.ICNumber)
            //ddlCompanyType.val(vm.Member.CompanyTypeId).trigger('change')
            //ddlSector.val(vm.Member.CompanySectorId).trigger('change')
            //ddlEmploymentStatus.val(vm.Member.CompanyEmploymentStatusId).trigger('change')
            //txtRetireAge.val(vm.Member.RetirementAge)
            //txtYearOfService.val(vm.Member.CompanyYearOfService)
            //ddlSalaryRange.val(vm.Member.SalaryRangeId)

            //if (ddlEmploymentStatus.val() == null) {
            //    ddlEmploymentStatus.prop('selectedIndex',0)
            //}
            //if (ddlSector.val() == null) {
            //    ddlSector.prop('selectedIndex', 0)
            //}
            //if (ddlSalaryRange.val() == null) {
            //    ddlSalaryRange.prop('selectedIndex', 0)
            //}

            ddlBankName.val(vm.Member.Bank.BankId)
            txtBankAccHolder.val(vm.Member.Bank.AccountName)
            txtBankAccNo.val(vm.Member.Bank.AccountNumber)
            txtBankOther.val(vm.Member.Bank.BankOther)

            updateSelect(ddlBankName)

            //if (vm.Member.PayslipFile != null) {
            //    $('#anchorpayslip').removeClass('d-none').attr('href', vm.Member.PayslipFile.FilePath).text(vm.Member.PayslipFile.FileName)
            //}

            //if (vm.Member.IcFile != null) {
            //    $('#anchorIc').removeClass('d-none').attr('href', vm.Member.IcFile.FilePath).text(vm.Member.IcFile.FileName)
            //}

            programEvent.val(vm.Member.ProgramEventId).trigger('change')
            if (vm.Member.DateOfBirth != null) {
                dateOfBirth.data('datepicker').setDate(new Date(vm.Member.DateOfBirth))
            }
            gender.val(vm.Member.Gender).trigger('change')
            icNumber.val(vm.Member.ICNumber)
            address.val(vm.Member.Address)
            phoneNumber.val(vm.Member.PhoneNumber)
            email.val(vm.Member.Email)
            taxNumber.val(vm.Member.TaxNumber)
            race.val(vm.Member.RaceId).trigger('change')
            religion.val(vm.Member.ReligionId).trigger('change')
            education.val(vm.Member.HighestLevelOfEducationId).trigger('change')
            maritalStatus.val(vm.Member.MaritalStatusId).trigger('change')
            
            spouseName.val(vm.Member.Spouse.FullName)
            spouseIC.val(vm.Member.Spouse.ICNumber)
            spouseContactInformation.val(vm.Member.Spouse.ContactInformation)
            spouseOccupation.val(vm.Member.Spouse.Occupation)
            spouseCompanyAddress.val(vm.Member.Spouse.CompanyAddress)
            spouseSalary.val(vm.Member.Spouse.Salary)

            familyNumberOfDependent.val(vm.Member.Family.NumberOfDependent)
            familyHasOKU.val(vm.Member.Family.IsHasOKU).trigger('change')
            familyFatherName.val(vm.Member.Family.FatherName)
            familyFatherContactNumber.val(vm.Member.Family.FatherContactNumber)
            familyFatherAddress.val(vm.Member.Family.FatherAddress)
            familyMotherName.val(vm.Member.Family.MotherName)
            familyMotherContactNumber.val(vm.Member.Family.MotherContactNumber)
            familyMotherAddress.val(vm.Member.Family.MotherAddress)
            companyEmpoyerTypeId.val(vm.Member.Company.EmployerTypeId).trigger('change')
            companyName.val(vm.Member.Company.CompanyName)
            companyJobTitle.val(vm.Member.Company.JobTitle)
            let sector = vm.Member.Company.SectorId
            if (sector == null) {
                sector = "null"
            }
            companySectorId.val(sector).trigger('change')
            companySectorOther.val(vm.Member.Company.SectorOther)
            let departement = vm.Member.Company.DepartmentId
            if (departement == null) {
                departement = "null"
            }
            companyDepartmentId.val(departement).trigger('change')
            companyDepartmentOther.val(vm.Member.Company.DepartmentOther)
            companyAddress.val(vm.Member.Company.CompanyAddress)
            companyOfficeContactNumber.val(vm.Member.Company.OfficeContactNumber)
            companyEmploymentStatusId.val(vm.Member.Company.EmploymentStatusId).trigger('change')
            companyRetirementAge.val(vm.Member.Company.RetirementAge)
            companyYearOfService.val(vm.Member.Company.YearOfService).trigger('change');

            emergencyContactPerson.val(vm.Member.Emergency.ContactPerson)
            emergencyRelationShipId.val(vm.Member.Emergency.RelationShipId).trigger('change')
            emergencyContactNumber.val(vm.Member.Emergency.ContactNumber)
            emergencyIC.val(vm.Member.Emergency.ICNumber)
            emergencyOccupation.val(vm.Member.Emergency.Occupation)
            emergencyAddress.val(vm.Member.Emergency.Address)

            //bankId.val(vm.Member.Bank.BankId).trigger('change')
            //bankAccountNumber.val(vm.Member.Bank.AccountNumber)
            salaryRange.val(vm.Member.Bank.SalaryRangeId).trigger('change')

            otherLanguage.val(vm.Member.Other.PreferredLanguage)
            otherHobbies.val(vm.Member.Other.Hobbies)
            otherSocialMediaHandles.val(vm.Member.Other.SocialMediaHandles)
            otherFbName.val(vm.Member.Other.FBName)

            $('[name="adminRemark"]').val(vm.Member.AdminRemark)

            companyEmploymentStatusOther.val(vm.Member.Company.EmploymentStatusOther)
            companyEmpoyerTypeOther.val(vm.Member.Company.EmployerTypeOther)
            companyEmployerName.val(vm.Member.Company.EmployerName)

            //if (vm.Member.ICFile != null) {
            //    $('#anchorIc').removeClass('d-none').attr('href', vm.Member.ICFile.FilePath).text(vm.Member.ICFile.FileName)
            //}

            //if (vm.Member.PayslipFile != null) {
            //    $('#anchorPayslip').removeClass('d-none').attr('href', vm.Member.PayslipFile.FilePath).text(vm.Member.PayslipFile.FileName)
            //}

            //if (vm.Member.OfferLetterFile != null) {
            //    $('#anchorOfferLetter').removeClass('d-none').attr('href', vm.Member.OfferLetterFile.FilePath).text(vm.Member.OfferLetterFile.FileName)
            //}

            if (vm.Member.ICFile) {
                showFile(vm.Member.ICFile, "ic", "icFile")
            }

            if (vm.Member.PayslipFile) {
                showFile(vm.Member.PayslipFile, "payslip", "member")
            }

            if (vm.Member.OfferLetterFile) {
                showFile(vm.Member.OfferLetterFile, "offerletter", "member")
            }

            if (vm.Member.ProfileFile != null) {
                $('#imgProfile').attr('src', vm.Member.ProfileFile.FileThumbnailPath)
            }

            if (vm.Member.ReferrerMember != null) {
                var newOption = new Option(vm.Member.ReferrerMember.Text, vm.Member.ReferrerMember.Value, false, false);
                ddlReferral.append(newOption).trigger('change')
            }

            $('#txtRefLink').val(vm.ReferralLink)
            $('#anchorReferralLink').attr('href', vm.ReferralLink).text(vm.ReferralLink)

            //check element if null value
            $('#card-content select').each(function () {
                var selectElement = $(this);
                if (selectElement.val() == null) {
                    selectElement.prop('selectedIndex', 0)
                }
            });

            //wallet
            txtCashWallet.val(vm.Member.WalletCash)
            $('#spanCreateDate').text(appHelper.dateToFormat(vm.Member.CreateDate, 'DD MMM YYYY HH:mm'))

            txtWalletSavings.val(vm.Member.WalletSavings)

            //disable save if unverify
            if (vm.Member.StatusId == 3) {
                $('.card-action').each(function () {
                    $(this).addClass('d-none')
                })
            }
        }
        $('#btnCopyRefLink').on('click', function () {
            navigator.clipboard.writeText($('#txtRefLink').val());
            $(this).removeClass('text-primary').addClass('text-success')
            $(this).html(`<i class="ri-edit-box-line align-bottom me-1"></i>Copied`)
            setTimeout(() => {
                $(this).removeClass('text-success').addClass('text-primary')
                $(this).html(`<i class="ri-edit-box-line align-bottom me-1"></i>Copy`)
            },2000)
        })

        $('.decimal-input').on('input', function () {
            // Get the current value
            let value = $(this).val();

            // Use regex to allow only numbers and one decimal point
            value = value.replace(/[^0-9.]/g, ''); // Remove non-numeric characters

            // Check for multiple decimal points
            const decimalCount = (value.match(/\./g) || []).length;
            if (decimalCount > 1) {
                value = value.replace(/\.+$/, ''); // Remove the last decimal point
            }

            // Check if there are more than 2 decimal places
            const decimalPart = value.split('.')[1];
            if (decimalPart && decimalPart.length > 2) {
                value = value.substring(0, value.indexOf('.') + 3); // Limit to 2 decimal places
            }

            // Update the input value
            $(this).val(value);
        });

        $('#btnSaveWallet').on('click', async function () {
            $(this).prop('disabled', true)

            try {
                const payload = {
                    MemberId: vm.Member.MemberId,
                    AdminId: <%= CurrentLoginAdmin.AdminId %>,
                    Amount: txtCashWallet.val(),
                }
                const res = await ApiHelper.post(window.location.origin + '/member/WalletChangesByAdmin', payload)
                if (!res.data.Error) {
                    dialogHelper.success('Wallet changed')
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }

            $(this).prop('disabled', false)
        })

        $('#btnSaveWalletSavings').on('click', async function () {
            $(this).prop('disabled', true)

            try {
                const payload = {
                    MemberId: vm.Member.MemberId,
                    AdminId: <%= CurrentLoginAdmin.AdminId %>,
                    Amount: txtWalletSavings.val(),
                }
                const res = await ApiHelper.post(window.location.origin + '/member/WalletSavingsChangesByAdmin', payload)
                if (!res.data.Error) {
                    dialogHelper.success('Savings changed')
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }

            $(this).prop('disabled', false)
        })

        function updateSelect(el) {
            if (el.val() == null) {
                el.prop('selectedIndex', 0)
            }

            el.trigger('change')
        }

        function showFile(objFile, actionName, type) {
            let action = $('#' + actionName + "Action")
            action.removeClass("d-none")
            $('#txt' + actionName).val(objFile.Value.Text)
            $(action.children().eq(0)).data('download', `${window.location.origin}/DownloadFile.aspx?f=${objFile.Value.Value}&n=${objFile.Value.Text}&t=${type}`)
            $(action.children().eq(1)).attr('href', `${window.location.origin}/ViewFile.aspx?f=${objFile.Value.Value}&n=${objFile.Value.Text}&t=${type}`)
        }

        function openInputFile(inputId) {
            $('#' + inputId).trigger('click')
        }

        function onChangeUpload(e, inputId) {
            $('#' + inputId).val(event.target.files[0].name)
        }

        $(document).on('click', '.btn-download-file', function () {
            // Create an anchor element and trigger the download
            const a = document.createElement('a');
            a.href = $(this).data('download');
            //a.download = ; // Specify the file name
            document.body.appendChild(a); // Append to body (needed for Firefox)
            a.click(); // Trigger the download
            document.body.removeChild(a); // Clean up
            URL.revokeObjectURL(a.href); // Free up memory
        })

        $('.nyatakan').on('change', function (e) {
            $('#nyatakan_' + $(this).attr('name')).addClass('d-none')
            let showNyatakan = false
            const dataNyatakan = String($(this).data('nyatakan'))
            if ($(this).data('select2') !== undefined) {
                showNyatakan = $(this).val().includes(dataNyatakan)
            }
            else {
                showNyatakan = dataNyatakan == $(this).val()
            }
            if (showNyatakan) {
                $('#nyatakan_' + $(this).attr('name')).removeClass('d-none')
            }
        })

        $(document).ready(function () {

            //nav-tab ui control
            tabUIControl();

            $('#ddlAccountStatus').select2({ 'theme': 'bootstrap-5', 'minimumResultsForSearch': -1 });
            $('#ddlGender').select2({ 'theme': 'bootstrap-5', 'minimumResultsForSearch': -1 });
            $('#ddlState').select2({ 'theme': 'bootstrap-5', 'minimumResultsForSearch': -1 });
            $('#ddlCountry').select2({ 'theme': 'bootstrap-5', 'minimumResultsForSearch': -1 });
            $('#ddlBankName').select2({ 'theme': 'bootstrap-5', 'minimumResultsForSearch': -1 });
            vm = JSON.parse($('#<%= hfModelView.ClientID %>').val())
            prepare()
            loadDetails()

            if (typeof loadYabam === 'function') {
                loadYabam()
            }

            if (vmSurvey == null) {
                $('#tabSurveyYabam').addClass('d-none')
            }
        });
    </script>
</asp:Content>
