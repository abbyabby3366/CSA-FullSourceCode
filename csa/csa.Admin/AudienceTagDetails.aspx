<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AudienceTagDetails.aspx.cs" Inherits="csa.Admin.AudienceTagDetails" %>
<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
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
                            <h4 class="fs-16 mb-1">Audience Tag</h4>
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
                    <button type="button" data-bs-toggle="tab" href="#tag-info" role="tab" aria-selected="false" class="btn btn-primary bg-gradient waves-effect waves-light text-start w-100 mb-2" id="tabTagInfo"><i class="ri-price-tag-3-line"></i> Tag Information</button>
                </div>

                <!-- right panel -->
                <div class="col-12 col-xxl-9 col-xl-9 col-lg-8 col-md-7 col-sm-12">
                    <!-- tab content -->
                    <div class="tab-content">
                        <div class="tab-pane active" id="tag-info" role="tabpanel">
                            <div class="card">
                                <div class="card-header align-items-center d-flex">
                                    <h5 class="card-title mb-0 flex-grow-1"><i class="ri-price-tag-3-line"></i> Tag Information</h5>
                                </div>

                                <div class="card-body">
                                    <div class="row g-3 mb-2">
                                        <div class="col-mx">
                                            <div class="form">
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtTagName" class="form-label">Tag Label</label>
                                                            <input type="text" class="form-control" id="txtTagName" name="tagName" placeholder="Tag label">
                                                        </div>
                                                    </div>
                                                   <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="ddlStatus" class="form-label">Status</label>
                                                            <select class="form-control" id="ddlStatus">
                                                                <option value="1" selected>Enable</option>
                                                                <option value="2">Disable</option>
                                                            </select>
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
                                            <button type="submit" class="btn btn-primary" id="btnSave"><i class="ri-save-line"></i> Save</button>
                                            <a href="<%=Page.ResolveUrl("~/AudienceTag") %>" type="button" class="btn btn-soft-secondary"><i class="ri-close-circle-line"></i> Cancel</a>
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
    <script type="text/javascript">
        var vm
        var btnSubmit = $('#btnSave')
        var ddlStatus = $('#ddlStatus')
        var txtTagName = $('#txtTagName')

        var form = $('#form1')
        form.validate({
            rules: {
                tagName: {
                    required: true,
                },
            },
            errorClass: 'is-invalid',
            errorPlacement: function (error, element) {
                error.addClass("invalid-feedback");
                error.insertAfter(element);
            }
        })

        function tabUIControl() {
            $('button[data-bs-toggle="tab"]').on('shown.bs.tab', function (e) {
                let target = $(e.target).attr("href");
                let tabTagInfo = $('#tabTagInfo');
                let tabOthers = $('#tabOthers');

                if (target == null) { return; }
                if (tabTagInfo.length == 0 || tabOthers.length == 0) { return; }

                switch (target) {
                    case '#tag-info':
                        tabGeneralInfo.removeClass('btn-soft-primary').addClass('btn-primary');
                        tabOthers.removeClass('btn-primary').addClass('btn-soft-primary');
                        break;
                    case '#others':
                        tabGeneralInfo.removeClass('btn-primary').addClass('btn-soft-primary');
                        tabOthers.removeClass('btn-soft-primary').addClass('btn-primary');
                        break;
                }
            });
        }

        btnSubmit.click(async function (e) {
            e.preventDefault()

            if (form.valid()) {
                $(this).prop('disabled', true)

                const payload = {
                    TagId: vm.Tag.TagId,
                    Label: txtTagName.val(),
                    StatusId: ddlStatus.val(),
                }
                const res = await ApiHelper.post(window.location.origin + '/Tag/UpdateTag', payload)
                if (!res.data.Error) {
                    dialogHelper.successAutoRedirect('Tag updated','<%= Page.ResolveUrl("~/AudienceTag") %>')
                } else {
                    dialogHelper.error(res.data.Message)
                }

                $(this).prop('disabled', false)
            }
        })

        function loadDetails() {
            txtTagName.val(vm.Tag.Label)
            ddlStatus.val(vm.Tag.StatusId)
        }

        $(document).ready(function () {
            vm = JSON.parse($('#<%= hfModelView.ClientID%>').val())
            loadDetails()
        });
    </script>
</asp:Content>
