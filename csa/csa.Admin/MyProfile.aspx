<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="csa.Admin.MyProfile" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
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
                                    <img src="assets/images/users/user-dummy-img.jpg" class="rounded-circle avatar-xl img-thumbnail user-profile-image" alt="user-profile-image">
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
                                            <button type="submit" class="btn btn-primary" id="btnSavePassword">Save</button>
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
    <script type="text/javascript">
        var btnSavePassword = $('#btnSavePassword')
        var txtConfirmNewPass = $('#txtConfirmNewPass')
        var txtCurrentPass = $('#txtCurrentPass')
        var txtNewPassword = $('#txtNewPassword')
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

        btnSavePassword.click(async function (e) {
            $(this).prop('disabled', true);

            const payload = {
                AdminId: <%= CurrentLoginAdmin.AdminId %>,
                CurrentPassword: txtCurrentPass.val(),
                NewPassword: txtNewPassword.val(),
                ConfirmNewPassword: txtConfirmNewPass.val(),
            }
            const res = await ApiHelper.post(window.location.origin + '/Admin/ChangePassword', payload)
            if (!res.data.Error) {
                dialogHelper.success('Password changed')
            } else {
                dialogHelper.error(res.data.Message)
            }

            $(this).prop('disabled', false);
        })

        $(document).ready(function () {

            //nav-tab ui control
            tabUIControl();
        });
    </script>
</asp:Content>
