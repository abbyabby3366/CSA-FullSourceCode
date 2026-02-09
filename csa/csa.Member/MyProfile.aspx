<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="csa.Member.MyProfile" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
    <style type="text/css">
        span.circle_icon {
            background: #fff;
            border-radius: 50%;
            -moz-border-radius: 50%;
            -webkit-border-radius: 50%;
            color: #000;
            display: inline-block;
            padding: 0.65rem;
            text-align: center;
            vertical-align: middle;
            line-height: 0.95rem;
            font-size: 1rem;
            font-weight: 500;
            width: 38px;
            height: 38px;
            background-color: #efefef;
        }

        span.circle_icon > i.icon {
          vertical-align: middle;
        }

        .select2-container--open {
            z-index: 9999999
        }
    </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfModelView" runat="server" />
    <div class="page-content">
        <div class="container-fluid">

            <div class="profile-foreground position-relative mx-n4 mt-n4">
                <div class="profile-wid-bg">
                    <img src="assets/images/profile-bg.jpg" alt="" class="profile-wid-img" />
                </div>
            </div>

            <div class="pt-4 mb-4 mb-lg-3 pb-lg-4 profile-wrapper">
                <div class="row justify-content-center mt-4 mb-4">
                    <div class="col-mx">
                        <div class="d-flex justify-content-center text-center">
                            <div class="profile-user position-relative d-inline-block mx-auto mb-4">
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
                    </div>

                    <div class="col-mx">
                        <div class="text-center">
                            <h3 class="text-white mb-1" id="lblFullName"></h3>

                            <div class="text-white text-opacity-75">
                                <i class="ri-map-pin-user-line me-1 text-white text-opacity-75 fs-16 align-middle"></i><span id="lblEmail"></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <!-- left panel -->
                <div class="col-12 col-xxl-8 col-xl-8 col-lg-8 col-md-6 col-sm-12">
                    <div class="card mt-n4">
                        <div class="card-header align-items-center d-flex">
                            <h5 class="card-title mb-0 flex-grow-1"><i class="ri-account-pin-box-line"></i> User Details</h5>
                        </div>

                        <!-- personal details -->
                        <div class="card-body">
                            <div class="table-responsive">
                                <table class="table table-borderless mb-0">
                                    <tbody>
                                        <tr>
                                            <th class="ps-0" scope="row" style="width:340.3px">
                                                <div class="d-flex justify-content-between">
                                                    <span>User ID</span>
                                                    <span>:</span>
                                                </div>
                                            </th>
                                            <td class="text-muted" id="tdUserId"></td>
                                        </tr>
                                        <tr>
                                            <th class="ps-0" scope="row">
                                                <div class="d-flex justify-content-between">
                                                    <span>Member ID</span>
                                                    <span>:</span>
                                                </div>
                                            </th>
                                            <td class="text-muted" id="tdMemberId"></td>
                                        </tr>
                                        <tr>
                                            <th class="ps-0" scope="row">
                                                <div class="d-flex justify-content-between">
                                                    <span>Account Status</span>
                                                    <span>:</span>
                                                </div>
                                            </th>
                                            <td class="text-muted">
                                                <div class="d-flex align-items-center" id="divStatus">
                                                    
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th class="ps-0" scope="row">
                                                <div class="d-flex justify-content-between">
                                                    <span>Referral ID</span>
                                                    <span>:</span>
                                                </div>
                                            </th>
                                            <td class="text-muted" id="tdReferralId"></td>
                                        </tr>
                                        <tr>
                                            <th class="ps-0" scope="row">
                                                <div class="d-flex justify-content-between">
                                                    <span>Referral Name</span>
                                                    <span>:</span>
                                                </div>
                                            </th>
                                            <td class="text-muted" id="tdReferralName"></td>
                                        </tr>
                                        <tr>
                                            <th class="ps-0" scope="row">
                                                <div class="d-flex justify-content-between">
                                                    <span>Full Name</span>
                                                    <span>:</span>
                                                </div>
                                            </th>
                                            <td class="text-muted" id="tdFullName"></td>
                                        </tr>
                                        <tr>
                                            <th class="ps-0" scope="row">
                                                <div class="d-flex justify-content-between">
                                                    <span>NIRC</span>
                                                    <span>:</span>
                                                </div>
                                            </th>
                                            <td class="text-muted">
                                                <div id="divNric">
                                                    
                                                </div>
                                            </td>
                                        </tr>
                                         <tr>
                                            <th class="ps-0" scope="row">
                                                <div class="d-flex justify-content-between">
                                                    <span>Payslip</span>
                                                    <span>:</span>
                                                </div>
                                            </th>
                                            <td class="text-muted">
                                                <div id="divpayslip">
                                                    
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th class="ps-0" scope="row">
                                                <div class="d-flex justify-content-between">
                                                    <span>Salary Range</span>
                                                    <span>:</span>
                                                </div>
                                            </th>
                                            <td class="text-muted" id="tdSalary"></td>
                                        </tr>
                                        <tr>
                                            <th class="ps-0" scope="row">
                                                <div class="d-flex justify-content-between">
                                                    <span>Email Address</span>
                                                    <span>:</span>
                                                </div>
                                            </th>
                                            <td class="text-muted" id="tdEmail"></td>
                                        </tr>
                                        <tr>
                                            <th class="ps-0" scope="row">
                                                <div class="d-flex justify-content-between">
                                                    <span>Gender</span>
                                                    <span>:</span>
                                                </div>
                                            </th>
                                            <td class="text-muted" id="tdGender"></td>
                                        </tr>
                                        <tr>
                                            <th class="ps-0" scope="row">
                                                <div class="d-flex justify-content-between">
                                                    <span>Phone No</span>
                                                    <span>:</span>
                                                </div>
                                            </th>
                                            <td class="text-muted" id="tdPhoneNumber"></td>
                                        </tr>
                                        <tr>
                                            <th class="ps-0" scope="row">
                                                <div class="d-flex justify-content-between">
                                                    <span>Address</span>
                                                    <span>:</span>
                                                </div>
                                            </th>
                                            <td class="text-muted" id="tdAddress"></td>
                                        </tr>
                                        <tr>
                                            <th class="ps-0" scope="row">
                                                <div class="d-flex justify-content-between">
                                                    <span>Company / Employer Name</span>
                                                    <span>:</span>
                                                </div>
                                            </th>
                                            <td class="text-muted" id="tdCompany"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>

                        <!-- bank details -->
                        <div class="card-header align-items-center d-flex">
                            <h5 class="card-title mb-0 flex-grow-1"><i class="ri-account-pin-box-line"></i> User Details</h5>
                        </div>

                        <div class="card-body ">
                            <div class="table-responsive">
                                <table class="table table-borderless mb-0">
                                    <tbody>
                                        <tr>
                                            <th class="ps-0" scope="row" style="width:340.3px">
                                                <div class="d-flex justify-content-between">
                                                    <span>Bank Name</span>
                                                    <span>:</span>
                                                </div>
                                            </th>
                                            <td class="text-muted" id="tdBankName"></td>
                                        </tr>
                                        <tr>
                                            <th class="ps-0" scope="row">
                                                <div class="d-flex justify-content-between">
                                                    <span>Bank Account Name</span>
                                                    <span>:</span>
                                                </div>
                                            </th>
                                            <td class="text-muted" id="tdBankAccountName"></td>
                                        </tr>
                                        <tr>
                                            <th class="ps-0" scope="row">
                                                <div class="d-flex justify-content-between">
                                                    <span>Bank Account Number</span>
                                                    <span>:</span>
                                                </div>
                                            </th>
                                            <td class="text-muted" id="tdBankAccountNumber"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- right panel -->
                <div class="col-12 col-xxl-4 col-xl-4 col-lg-4 col-md-6 col-sm-12">
                    <!-- wallet -->
                    <div class="card mt-n4">
                        <div class="card-body px-1">
                            <ul class="list-group list-group-flush border-dashed mb-0">
                                <li class="list-group-item d-flex align-items-center">
                                    <div class="flex-shrink-0">
                                        <span class="circle_icon me-2"><i class="ri-wallet-line icon"></i></span>
                                    </div>
                                    <div class="flex-grow-1 ms-3">
                                        <h6 class="fs-14 mb-1">Ganjaran</h6>
                                        <%--<p class="text-muted mb-0">Malaysia Ringgit</p>--%>
                                    </div>
                                    <div class="flex-shrink-0 text-end">
                                        <h6 class="fs-16 fw-semibold m-0" id="headingWallet"></h6>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>

                    <!-- referral -->
                    <div class="card">
                        <div class="card-body px-1">
                            <ul class="list-group list-group-flush border-dashed mb-0">
                                <li class="list-group-item d-flex align-items-center">
                                    <div class="flex-shrink-0">
                                        <span class="circle_icon me-2"><i class="ri-user-add-line icon"></i></span>
                                    </div>
                                    <div class="flex-grow-1 ms-3">
                                        <h6 class="fs-14 mb-1">Peserta</h6>
                                        <p class="text-muted mb-0">Jumlah Peserta Rujukan</p>
                                    </div>
                                    <div class="flex-shrink-0 text-end">
                                        <h6 class="fs-16 fw-semibold m-0" id="headingReferral"></h6>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="d-flex align-items-center mb-2">
                                <div class="flex-grow-1">
                                    <h5 class="card-title mb-0">Peserta Survey Sign Up Link</h5>
                                </div>
                                <div class="flex-shrink-0">
                                    <a href="#" id="btnCopyRefLink" class="badge bg-light text-primary fs-12"><i class="ri-edit-box-line align-bottom me-1"></i>Copy</a>
                                </div>
                            </div>
                            <input type="hidden" id="txtRefLink" />
                            <a href="#" id="anchorReferralLink" target="_blank"></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        var vm
        function loadDetails() {
            $('#tdUserId').html(vm.Member.MemberId)
            $('#tdMemberId').html(vm.Member.FileNumber)
            $('#divStatus').html(`<span class="me-2">${vm.Member.Status}</span><i class="las la-certificate fs-22" style="color: #23aa4f;"></i>`)
            $('#tdReferralId').html(vm.Member.ReferralFileNumber)
            $('#tdReferralName').html(vm.Member.ReferralName)
            $('#tdFullName').html(vm.Member.FullName)
            $('#divNric').html(`<span class="me-3">${vm.Member.ICNumber}</span>`)
            if (vm.Member.ICFile != null) {
                $('#divNric').append(`<i class="ri-image-fill"></i>
                                                    <a href="${vm.Member.ICFile.FilePath}" target="_blank" class="link-primary ms-1" style="text-decoration: underline;">View Identity Card</a>`)
            }

            if (vm.Member.PayslipFile != null) {
                $('#divpayslip').append(`<i class="ri-image-fill"></i>
                                                    <a href="${vm.Member.PayslipFile.FilePath}" target="_blank" class="link-primary ms-1" style="text-decoration: underline;">View Payslip</a>`)
            }
           
            $('#tdSalary').html(vm.Member.SalaryRange)
            $('#tdEmail').html(vm.Member.Email)
            $('#tdGender').html(vm.Member.Gender)
            $('#tdPhoneNumber').html(vm.Member.PhoneNumber)
            $('#tdAddress').html(vm.Member.StreetAddress1)
            $('#tdCompany').html(vm.Member.CompanyName)

            //bank details
            $('#tdBankName').html(vm.Bank.BankName)
            $('#tdBankAccountName').html(vm.Bank.BankAccountName)
            $('#tdBankAccountNumber').html(vm.Bank.BankAccountNumber)
            $('#headingWallet').text(`${vm.MyWallet.toFixed(2)}`)
            $('#headingReferral').text(`${vm.MyReferral}`)


            $('#lblFullName').text(vm.Member.FullName)
            $('#lblEmail').text(vm.Member.Email)

            //if (vm.ProfileManagementModel.PersonalDetails.ICFile != null) {
            //    anchorIc.removeClass('d-none').attr('href', vm.ProfileManagementModel.PersonalDetails.ICFile.FilePath).text(vm.ProfileManagementModel.PersonalDetails.ICFile.FileName)
            //    upldIC.hide()
            //}

            if (vm.Member.ProfileFile != null) {
                $('#imgProfile').attr('src', vm.Member.ProfileFile.FileThumbnailPath)
            }
            $('#txtRefLink').val(vm.ReferralLink)
            $('#anchorReferralLink').attr('href', vm.ReferralLink).text(vm.ReferralLink)
        }

        $('#btnCopyRefLink').on('click', function () {
            navigator.clipboard.writeText($('#txtRefLink').val());
            $(this).removeClass('text-primary').addClass('text-success')
            $(this).html(`<i class="ri-edit-box-line align-bottom me-1"></i>Copied`)
            setTimeout(() => {
                $(this).removeClass('text-success').addClass('text-primary')
                $(this).html(`<i class="ri-edit-box-line align-bottom me-1"></i>Copy`)
            }, 2000)
        })

        $(document).ready(function () {
            vm = JSON.parse($('#<%= hfModelView.ClientID %>').val())
            loadDetails()
        });

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
    </script>
</asp:Content>
