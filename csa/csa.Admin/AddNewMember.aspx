<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewMember.aspx.cs" Inherits="csa.Admin.AddNewMember" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
    <!-- select2 -->
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2.min.css") %>" />
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2-bootstrap-5-theme.min.css") %>" />
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
                            <h4 class="fs-16 mb-1">Add New Member</h4>
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
                                    <img id="imgProfile" src="assets/images/users/user-dummy-img.jpg" class="rounded-circle avatar-xl img-thumbnail user-profile-image" alt="user-profile-image">
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
                                    <button type="button" data-bs-toggle="tab" href="#user-details" role="tab" aria-selected="false" class="btn btn-primary bg-gradient waves-effect waves-light text-start w-100 mb-2" id="tabUserDetails"><i class="ri-file-user-line"></i> User Details</button>
                                    <button type="button" data-bs-toggle="tab" href="#bank-details" role="tab" aria-selected="false" class="btn btn-soft-primary waves-effect waves-light text-start w-100 mb-2" id="tabBankDetails"><i class="ri-bank-line"></i> User Bank Details</button>
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
                </div>

                <!-- right panel -->
                <div class="col-12 col-xxl-9 col-xl-9 col-lg-8 col-md-7 col-sm-12">
                    <!-- tab content -->
                    <div class="tab-content">
                        <div class="tab-pane" id="user-details" role="tabpanel">
                            <div class="card">
                                <div class="card-header align-items-center d-flex">
                                    <h5 class="card-title mb-0 flex-grow-1"><i class="ri-file-user-line"></i> User Details</h5>
                                </div>

                                <div class="card-body">
                                    <div class="row g-3 mb-2">
                                        <div class="col-mx">
                                            <div class="form">
                                                <div class="row">
                                                    <div class="col-lg-6 d-none">
                                                        <div class="mb-3">
                                                            <label for="ddlAccountStatus" class="form-label">Account Status</label>
                                                            <select class="form-control" id="ddlAccountStatus">
                                                                <option value="1" selected="selected">Active</option>
                                                                <option value="2">Inactive</option>
                                                                <option value="5">Unverified</option>
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
                                                            <input type="text" class="form-control" id="txtPhoneNo" placeholder="Phone Number"  />
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
                                                    
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtEmail" class="form-label">Email Address</label>
                                                            <input type="email" class="form-control" id="txtEmail" placeholder="Email Address" >
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
                                                            <label for="upldIC" class="form-label">Upload IC</label>
                                                            <input class="form-control" type="file" id="upldIC">
                                                        </div>
                                                    </div>
                                                     <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="upldpayslip" class="form-label ">Upload Payslip</label>
                                                            <input class="form-control" type="file" id="upldpayslip">
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6 d-none">
                                                        <div class="mb-3">
                                                            <label for="txtSalary" class="form-label">Salary</label>
                                                            <input type="text" class="form-control" id="txtSalary" placeholder="Salary"  />
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
                                                            <textarea class="form-control" id="txtAddress" rows="3" placeholder="Address" ></textarea>
                                                        </div>
                                                    </div>
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
                                                    <div class="col-lg-6">
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
                                                    </div>
                                                </div>
                                            </div>

                                            <hr class="hr" />

                                            <div class="form">
                                                <div class="row g-2">
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtPassword" class="form-label">Password</label>
                                                            <input type="password" class="form-control" id="txtPassword" placeholder="Enter new password"/>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtConfirmPass" class="form-label">Confirm Password</label>
                                                            <input type="password" class="form-control" id="txtConfirmPass" placeholder="Confirm password"/>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="card-footer">
                                    <div class="col-lg-12">
                                        <div class="hstack gap-2 justify-content-end">
                                            <button type="submit" class="btn btn-primary" id="btnSave1">Submit</button>
                                            <button type="button" class="btn btn-soft-success">Cancel</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="tab-pane" id="bank-details" role="tabpanel">
                            <div class="card">
                                <div class="card-header align-items-center d-flex">
                                    <h5 class="card-title mb-0 flex-grow-1"><i class="ri-bank-line"></i> Bank Details</h5>
                                </div>

                                <div class="card-body">
                                    <div class="row g-3 mb-2">
                                        <div class="col-mx">
                                            <div class="form">
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="ddlBankName" class="form-label">Bank Name</label>
                                                            <select class="form-control" id="ddlBankName">
                                                               
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtBankAccHolder" class="form-label">Bank Account Name</label>
                                                            <input type="text" class="form-control" id="txtBankAccHolder" placeholder="Bank Account Name" >
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtBankAccNo" class="form-label">Bank Account Number</label>
                                                            <input type="text" class="form-control" id="txtBankAccNo" placeholder="Bank Account Number" >
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="card-footer">
                                    <div class="col-lg-12">
                                        <div class="hstack gap-2 justify-content-end">
                                            <button type="submit" class="btn btn-primary" id="btnSave2">Submit</button>
                                            <button type="button" class="btn btn-soft-success">Cancel</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent" runat="server">
    <!-- select2 js -->
    <script  type='text/javascript' src="<%=Page.ResolveUrl("~/assets/libs/select2/js/select2.min.js") %>"></script>

    <script type="text/javascript">
        var vm
        var ddlAccountStatus = $('#ddlAccountStatus')
        var txtFirstName = $('#txtFirstName')
        var txtLastName = $('#txtLastName')
        var txtPhoneNo = $('#txtPhoneNo')
        var txtEmail = $('#txtEmail')
        var upldIC = $('#upldIC')
        var upldProfile = $('#profile-img-file-input')
        var txtSalary = $('#txtSalary')
        var ddlGender = $('#ddlGender')
        var txtAddress = $('#txtAddress')
        var ddlState = $('#ddlState')
        var ddlCountry = $('#ddlCountry')
        var txtCompanyName = $('#txtCompanyName')
        var txtDesignation = $('#txtDesignation')
        var txtPassword = $('#txtPassword')
        var txtConfirmPass = $('#txtConfirmPass')
        var ddlBankName = $('#ddlBankName')
        var txtBankAccHolder = $('#txtBankAccHolder')
        var txtBankAccNo = $('#txtBankAccNo')
        var btnSave1 = $('#btnSave1')
        var btnSave2 = $('#btnSave2')
        var ddlReferral = $('#ddlReferral')
        var txtIdentificationNumber = $('#txtIdentificationNumber')
        var upldpayslip = $('#upldpayslip')
        var ddlCompanyType = $('#ddlCompanyType')
        var ddlSector = $('#ddlSector')
        var ddlEmploymentStatus = $('#ddlEmploymentStatus')
        var txtRetireAge = $('#txtRetireAge')
        var txtYearOfService = $('#txtYearOfService')

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
                        exceptMemberId: 0
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

                if (target == null) { return; }
                if (tabUser.length == 0 || tabBank.length == 0) { return; }

                switch (target) {
                    case '#user-details':
                        tabUser.removeClass('btn-soft-primary').addClass('btn-primary');
                        tabBank.removeClass('btn-primary').addClass('btn-soft-primary');
                        break;
                    case '#bank-details':
                        tabUser.removeClass('btn-primary').addClass('btn-soft-primary');
                        tabBank.removeClass('btn-soft-primary').addClass('btn-primary');
                        break;
                }
            });

            $('#tabUserDetails').click();
        }

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
        }

        async function onSave(e) {
            $(this).prop('disabled', true);

            const payload = {
                FirstName: txtFirstName.val(),
                LastName: txtLastName.val(),
                PhoneNumber: txtPhoneNo.val(),
                Email: txtEmail.val(),
                StatusId: ddlAccountStatus.val(),
                Salary: txtSalary.val(),
                GenderId: ddlGender.val(),
                Address: txtAddress.val(),
                CountryId: ddlCountry.val(),
                StateId: ddlState.val(),
                CompanyName: txtCompanyName.val(),
                Occupation: txtDesignation.val(),
                Password: txtPassword.val(),
                ConfirmPassword: txtConfirmPass.val(),
                Bank: {
                    BankId: ddlBankName.val(),
                    BankAccountName: txtBankAccHolder.val(),
                    BankAccountNumber: txtBankAccNo.val()
                },
                IcFile: upldIC[0].files[0],
                ProfileFile: upldProfile[0].files[0],
                ReferrerMemberId: ddlReferral.val(),
                PayslipFile: upldpayslip[0].files[0],
                ICNumber: txtIdentificationNumber.val(),
                CompanySectorId: ddlSector.val(),
                CompanyEmploymentStatusId: ddlEmploymentStatus.val(),
                RetirementAge: txtRetireAge.val(),
                YearOfService: txtYearOfService.val(),
                CompanyTypeId: ddlCompanyType.val(),
                SalaryRangeId: $('[name="salaryRange"]').val()
            }

            const res = await ApiHelper.postFormData(window.location.origin + '/member/Create', payload)
            if (!res.data.Error) {
                dialogHelper.successAutoRedirect('Member created','<%= Page.ResolveUrl("~/Members")%>')
            } else {
                dialogHelper.error(res.data.Message)
            }

            $(this).prop('disabled', false);
        }

        txtSalary.on('change',appHelper.onCurrencyInput)

        btnSave1.click(onSave)
        btnSave2.click(onSave)

        upldProfile.change(async function (e) {
            $('#imgProfile').attr('src', URL.createObjectURL($(this)[0].files[0]))
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

        });
    </script>
</asp:Content>
