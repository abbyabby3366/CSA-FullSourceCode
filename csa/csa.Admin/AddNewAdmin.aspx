<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewAdmin.aspx.cs" Inherits="csa.Admin.AddNewAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfModelView" runat="server" />
    <div class="page-content">
        <div class="container-fluid">
            <!-- page header -->
            <div class="row mb-3 pb-1">
                <div class="col-12">
                    <div class="d-flex align-items-lg-center flex-lg-row flex-column">
                        <div class="flex-grow-1">
                            <h4 class="fs-16 mb-1">New Admin</h4>
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
                            <div class="row ">
                                <div class="col-mx" role="tablist">
                                    <button type="button" data-bs-toggle="tab" href="#personal-details" role="tab" aria-selected="false" class="btn btn-primary bg-gradient waves-effect waves-light text-start w-100 mb-2" id="tabPersonalDetails"><i class="ri-file-user-line"></i> Personal Details</button>
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
                                                            <label for="txtFullName" class="form-label">Name</label>
                                                            <input type="text" class="form-control" id="txtName" placeholder="Name"  />
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
                                                            <label for="ddlRole" class="form-label">Admin Type</label>
                                                            <select class="form-control" id="ddlAdminType">
                                                                <option value="0" selected>Admin</option>
                                                                <option value="1">Super Admin</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                     <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="ddlRole" class="form-label">Role</label>
                                                            <select class="form-control" name="team"></select>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtNewPassword" class="form-label">Password</label>
                                                            <input type="password" class="form-control" id="txtNewPassword" placeholder="New Password" >
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtConfirmNewPass" class="form-label">Confirm Password</label>
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
                                            <button class="btn btn-soft-primary" id="btnSavePersonalDetails"><i class="ri-send-plane-2-line me-1 align-middle"></i> Save</button>
                                            <a href="<%=Page.ResolveUrl("~/Roles")%>" class="btn btn-soft-secondary"><i class="ri-close-circle-line me-1 align-middle"></i> Cancel</a>
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
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">

    <script src="<%=Page.ResolveUrl("~/assets/libs/jqueryValidation/dist/jquery.validate.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/Scripts/customValidator.js") %>"></script>
    <!-- select2 js -->
    <script  type='text/javascript' src="<%=Page.ResolveUrl("~/assets/libs/select2/js/select2.min.js") %>"></script>

    <script type="text/javascript">
        var vm
        var btnSavePersonalDetails = $('#btnSavePersonalDetails')
        var ddlAdminType = $('#ddlAdminType')
        var ddlTeam = $('#ddlTeam')
        var txtName = $('#txtName')
        var txtEmail = $('#txtEmail')
        var txtNewPassword = $('#txtNewPassword')
        var txtConfirmNewPassword = $('#txtConfirmNewPass')

        function tabUIControl() {
            $('button[data-bs-toggle="tab"]').on('shown.bs.tab', function (e) {
                let target = $(e.target).attr("href");
                let tabPrsnl = $('#tabPersonalDetails');

                if (target == null) { return; }
                if (tabPrsnl.length == 0) { return; }

                switch (target) {
                    case '#personal-details':
                        tabPrsnl.removeClass('btn-soft-primary').addClass('btn-primary');
                        break;
                }
            });

            $('#tabPersonalDetails').click();
        }


        function prepare() {            
            vm.RoleList.forEach(function (item) {
                $('[name="team"]').append(`<option value='${item.Value}'>${item.Text}</option>`)
            })
        }

        var form = $('#form1')
        form.validate({
            rules: {
                team: {
                    notZero: true
                }
            },
            errorClass: 'is-invalid',
            errorPlacement: function (error, element) {
                error.addClass("invalid-feedback");
                error.insertAfter(element);
            }
        })

        $(document).ready(function () {

            //nav-tab ui control
            tabUIControl();
            vm = JSON.parse($('#<%= hfModelView.ClientID %>').val())
            prepare()            
        });

        btnSavePersonalDetails.click(async function (e) {

            if (!form.valid()) {
                dialogHelper.error('Please fill in the required fields.')
                return
            }


            $(this).prop('disabled', true);

            const payload = {
                Name: txtName.val(),
                Email: txtEmail.val(),
                AdminTypeId: ddlAdminType.val(),
                TeamId: $('[name="team"]').val(),
                Password: txtNewPassword.val(),
                ConfirmPassword: txtConfirmNewPassword.val()
            }

            const res = await ApiHelper.postFormData(window.location.origin + '/admin/create', payload)
            if (!res.data.Error) {
                dialogHelper.successAutoRedirect('Admin created',"<%= Page.ResolveUrl("~/Roles")%>")
            } else {
                dialogHelper.error(res.data.Message)
            }

            $(this).prop('disabled', false);
        })
    </script>
</asp:Content>
