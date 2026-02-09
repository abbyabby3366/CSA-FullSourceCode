<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfileManagement.aspx.cs" Inherits="csa.Member.ProfileManagement" %>

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
                            <h4 class="fs-16 mb-1">Profile Management</h4>
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
                                        <input id="profile-img-file-input" type="file" class="profile-img-file-input" accept="image/*">
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
                                    <button type="button" data-bs-toggle="tab" href="#personal-details" role="tab" aria-selected="false" class="btn btn-primary bg-gradient waves-effect waves-light text-start w-100 mb-2" id="tabPersonalDetails"><i class="ri-file-user-line"></i> Personal Details</button>
                                    <button type="button" data-bs-toggle="tab" href="#bank-details" role="tab" aria-selected="false" class="btn btn-soft-primary waves-effect waves-light text-start w-100 mb-2" id="tabBankDetails"><i class="ri-bank-line"></i> Bank Details</button>
                                    <button type="button" data-bs-toggle="tab" href="#change-password" role="tab" aria-selected="false" class="btn btn-soft-primary waves-effect waves-light text-start w-100 mb-2" id="tabChangePass"><i class="ri-lock-line"></i> Change Password</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- right panel -->
                <div class="col-12 col-xxl-9 col-xl-9 col-lg-8 col-md-10 col-sm-12">
                    <!-- tab content -->
                    <div class="tab-content">
                        <div class="tab-pane" id="personal-details" role="tabpanel">
                            <div class="card">
                                <div class="card-header align-items-center d-flex">
                                    <h5 class="card-title mb-0 flex-grow-1"><i class="ri-file-user-line"></i> Personal Details</h5>
                                </div>

                                <div class="card-body">
                                    <div class="row g-3 mb-2">
                                        <div class="col-mx">
                                            <div class="form">
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtFullName" class="form-label">First Name</label>
                                                            <input type="text" class="form-control" id="txtFirstName" placeholder="First Name"  />
                                                        </div>
                                                    </div>
                                                     <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtFullName" class="form-label">Last Name</label>
                                                            <input type="text" class="form-control" id="txtLastName" placeholder="Last Name"  />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtPhoneNo" class="form-label">Phone Number</label>
                                                            <div class="d-flex align-items-center gap-3">
                                            <span>+60</span>
                                                            <input type="text" class="form-control" id="txtPhoneNo" placeholder="Phone Number" disabled/>
                                                                </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtEmail" class="form-label">Email Address</label>
                                                            <input type="email" class="form-control" id="txtEmail" name="emailAddress" placeholder="Email Address" >
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="upldIC" class="form-label ">Upload IC</label>
                                                            <%--<a id="anchorIc" href="#" target="_blank" class="d-none"></a>
                                                            <input class="form-control" type="file" id="upldIC">--%>

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
                                                            <label for="upldpayslip" class="form-label ">Upload Payslip</label>
                                                           <%-- <a id="anchorpayslip" href="#" target="_blank" class="d-none"></a>
                                                            <input class="form-control" type="file" id="upldpayslip">--%>

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
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="card-footer">
                                    <div class="col-lg-12">
                                        <div class="hstack gap-2 justify-content-end">
                                            <button class="btn btn-soft-primary" id="btnSavePersonalDetails"><i class="ri-send-plane-2-line me-1 align-middle"></i> Save</button>
                                            <a href="<%=Page.ResolveUrl("~/MyProfile")%>" class="btn btn-soft-secondary"><i class="ri-close-circle-line me-1 align-middle"></i> Cancel</a>
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
                                                            <select class="form-control nyatakan" name="ddlBankName" id="ddlBankName" data-nyatakan="<%= csa.Library.Constant.OTHER_NUMBER %>"></select>
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

                                <div class="card-footer">
                                    <div class="col-lg-12">
                                        <div class="hstack gap-2 justify-content-end">
                                            <button class="btn btn-soft-primary" id="btnSaveBank"><i class="ri-send-plane-2-line me-1 align-middle"></i> Save</button>
                                            <a href="<%=Page.ResolveUrl("~/MyProfile")%>" class="btn btn-soft-secondary"><i class="ri-close-circle-line me-1 align-middle"></i> Cancel</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="tab-pane" id="change-password" role="tabpanel">
                            <div class="card">
                                <div class="card-header align-items-center d-flex">
                                    <h5 class="card-title mb-0 flex-grow-1"><i class="ri-lock-line"></i> Change Password</h5>
                                </div>

                                <div class="card-body">
                                    <div class="row g-3 mb-2">
                                        <div class="col-mx">
                                            <div class="form">
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtCurrentPass" class="form-label">Current Password</label>
                                                            <input type="password" class="form-control" id="txtCurrentPass" placeholder="Current Password" >
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtNewPassword" class="form-label">New Password</label>
                                                            <input type="password" class="form-control" id="txtNewPassword" placeholder="New Password" >
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtConfirmNewPass" class="form-label">Confirm New Password</label>
                                                            <input type="password" class="form-control" id="txtConfirmNewPass" placeholder="Confirm New Password" >
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
                                            <button class="btn btn-soft-primary" id="btnSavePassword"><i class="ri-send-plane-2-line me-1 align-middle"></i> Save</button>
                                            <a href="<%=Page.ResolveUrl("~/MyProfile")%>" class="btn btn-soft-secondary"><i class="ri-close-circle-line me-1 align-middle"></i> Cancel</a>
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
    <script src="<%=Page.ResolveUrl("~/assets/libs/jqueryValidation/dist/jquery.validate.js") %>"></script>
    <!-- select2 js -->
    <script  type='text/javascript' src="<%=Page.ResolveUrl("~/assets/libs/select2/js/select2.min.js") %>"></script>

    <script type="text/javascript">
        var vm
        var txtFirstName = $('#txtFirstName')
        var txtLastName = $('#txtLastName')
        var txtPhoneNo = $('#txtPhoneNo')
        var txtEmail = $('#txtEmail')
        //var upldIC = $('#upldIC')
        var ddlBankName = $('#ddlBankName')
        var txtBankOther = $('#txtBankOther')
        var txtBankAccHolder = $('#txtBankAccHolder')
        var txtBankAccNo = $('#txtBankAccNo')
        var btnSavePassword = $('#btnSavePassword')
        var txtCurrentPass = $('#txtCurrentPass')
        var txtNewPassword = $('#txtNewPassword')
        var txtConfirmNewPass = $('#txtConfirmNewPass')
        var btnSaveBank = $('#btnSaveBank')
        var btnSavePersonalDetails = $('#btnSavePersonalDetails')
        var anchorIc = $('#anchorIc')
        var anchorpayslip = $('#anchorpayslip')
        //var upldpayslip = $('#upldpayslip')

        function tabUIControl() {
            $('button[data-bs-toggle="tab"]').on('shown.bs.tab', function (e) {
                let target = $(e.target).attr("href");
                let tabPrsnl = $('#tabPersonalDetails');
                let tabBank = $('#tabBankDetails');
                let tabChgPwd = $('#tabChangePass');

                if (target == null) { return; }
                if (tabPrsnl.length == 0 || tabBank.length == 0 || tabChgPwd.length == 0) { return; }

                switch (target) {
                    case '#personal-details':
                        tabPrsnl.removeClass('btn-soft-primary').addClass('btn-primary');
                        tabBank.removeClass('btn-primary').addClass('btn-soft-primary');
                        tabChgPwd.removeClass('btn-primary').addClass('btn-soft-primary');
                        break;
                    case '#bank-details':
                        tabPrsnl.removeClass('btn-primary').addClass('btn-soft-primary');
                        tabBank.removeClass('btn-soft-primary').addClass('btn-primary');
                        tabChgPwd.removeClass('btn-primary').addClass('btn-soft-primary');
                        break;
                    case '#change-password':
                        tabPrsnl.removeClass('btn-primary').addClass('btn-soft-primary');
                        tabBank.removeClass('btn-primary').addClass('btn-soft-primary');
                        tabChgPwd.removeClass('btn-soft-primary').addClass('btn-primary');
                        break;
                }
            });

            $('#tabPersonalDetails').click();
        }

        function loadPersonalDetails() {
            txtFirstName.val(vm.ProfileManagementModel.PersonalDetails.FirstName)
            txtLastName.val(vm.ProfileManagementModel.PersonalDetails.LastName)
            txtPhoneNo.val(vm.ProfileManagementModel.PersonalDetails.PhoneNumber)
            txtEmail.val(vm.ProfileManagementModel.PersonalDetails.Email)

            //if (vm.ProfileManagementModel.PersonalDetails.ICFile != null) {
            //    anchorIc.removeClass('d-none').attr('href', vm.ProfileManagementModel.PersonalDetails.ICFile.FilePath).text(vm.ProfileManagementModel.PersonalDetails.ICFile.FileName)
            //    upldIC.hide()
            //}

            //if (vm.ProfileManagementModel.PersonalDetails.PayslipFile != null) {
            //    anchorpayslip.removeClass('d-none').attr('href', vm.ProfileManagementModel.PersonalDetails.PayslipFile.FilePath).text(vm.ProfileManagementModel.PersonalDetails.PayslipFile.FileName)
            //    upldpayslip.hide()
            //}

            if (vm.ProfileManagementModel.PersonalDetails.ICFile) {
                showFile(vm.ProfileManagementModel.PersonalDetails.ICFile, "ic", "icFile")
            }

            if (vm.ProfileManagementModel.PersonalDetails.PayslipFile) {
                showFile(vm.ProfileManagementModel.PersonalDetails.PayslipFile, "payslip", "member")
            }

            if (vm.ProfileManagementModel.PersonalDetails.ProfileFile != null) {
                $('#imgProfile').attr('src', vm.ProfileManagementModel.PersonalDetails.ProfileFile.FileThumbnailPath)
            }
        }

        function loadBankDetails() {
            ddlBankName.val(vm.ProfileManagementModel.BankDetails.BankId)
            txtBankAccHolder.val(vm.ProfileManagementModel.BankDetails.BankAccountName)
            txtBankAccNo.val(vm.ProfileManagementModel.BankDetails.BankAccountNumber)

            txtBankOther.val(vm.ProfileManagementModel.BankDetails.BankOther)
            updateSelect(ddlBankName)
        }

        function prepare() {
            vm.BankDropdown.forEach(function (item) {
                ddlBankName.append(`<option value='${item.Key}'>${item.Text}</option>`)
            })
        }        

        $(document).ready(function () {

            //nav-tab ui control
            tabUIControl();
            $('#ddlBankName').select2({ 'theme': 'bootstrap-5', 'minimumResultsForSearch': -1 });

            vm = JSON.parse($('#<%= hfModelView.ClientID %>').val())
            prepare()            
            loadPersonalDetails()
            loadBankDetails()

        });

        var form = $('#form1')
        form.validate({
            rules: {
                emailAddress: {
                    email: true
                }
            },
            errorClass: 'is-invalid',
            errorPlacement: function (error, element) {
                error.addClass("invalid-feedback");
                error.insertAfter(element);
            }
        })

        btnSavePassword.click(async function (e) {            
            $(this).prop('disabled', true);

            const payload = {
                memberId: <%= CurrentLoginMember.MemberId %>,
                currentPassword: txtCurrentPass.val(),
                newPassword: txtNewPassword.val(),
                confirmNewPassword: txtConfirmNewPass.val(),
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
                memberId: <%= CurrentLoginMember.MemberId %>,
                bankId: ddlBankName.val(),
                bankOther: txtBankOther.val(),
                bankAccountNumber: txtBankAccNo.val(),
                bankAccountName: txtBankAccHolder.val(),
            }
            const res = await ApiHelper.post(window.location.origin + '/member/ChangeBankDetails', payload)
            if (!res.data.Error) {
                dialogHelper.success('Bank changed')
            } else {
                dialogHelper.error(res.data.Message)
            }

            $(this).prop('disabled', false);
        })

        btnSavePersonalDetails.click(async function (e) {

            if (!form.valid()) {
                dialogHelper.error('Please fill in the required fields.')
                return
            }


            $(this).prop('disabled', true);

            const payload = {
                memberId: <%= CurrentLoginMember.MemberId %>,
                firstName: txtFirstName.val(),
                lastName: txtLastName.val(),
                phoneNumber: txtPhoneNo.val(),
                email: txtEmail.val(),
                icFile: $('#upldic')[0].files[0],
                payslipFile: $('#upldpayslip')[0].files[0],
            }

            const res = await ApiHelper.postFormData(window.location.origin + '/member/ChangePersonalDetail', payload)
            if (!res.data.Error) {
                dialogHelper.success('Personal details changed')
            } else {
                dialogHelper.error(res.data.Message)
            }

            $(this).prop('disabled', false);
        })

        $('#profile-img-file-input').change(async function (e) {
            const payload = {
                memberId: <%= CurrentLoginMember.MemberId %>,
                imageFile: $(this)[0].files[0]
            }

            $('#imgProfile').attr('src', URL.createObjectURL(payload.imageFile))
            $('#navImageProfile').attr('src', URL.createObjectURL(payload.imageFile))

            const res = await ApiHelper.postFormData(window.location.origin + '/member/ChangeProfilePicture', payload)
            if (res.data.Error) {
                dialogHelper.error(res.data.Message)
            } 
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
    </script>
</asp:Content>
