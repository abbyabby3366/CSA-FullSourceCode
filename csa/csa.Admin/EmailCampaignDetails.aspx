<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmailCampaignDetails.aspx.cs" Inherits="csa.Admin.EmailCampaignDetails" %>
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
                            <h4 class="fs-16 mb-1">New Campaign</h4>
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
                    <button type="button" data-bs-toggle="tab" href="#user-details" role="tab" aria-selected="false" class="btn btn-primary bg-gradient waves-effect waves-light text-start w-100 mb-2" id="tabGeneralInfo"><i class="ri-mail-add-line"></i> General Information</button>
                </div>

                <!-- right panel -->
                <div class="col-12 col-xxl-9 col-xl-9 col-lg-8 col-md-7 col-sm-12">
                    <!-- tab content -->
                    <div class="tab-content">
                        <div class="tab-pane active" id="user-details" role="tabpanel">
                            <div class="card">
                                <div class="card-header align-items-center d-flex">
                                    <h5 class="card-title mb-0 flex-grow-1"><i class="ri-mail-add-line"></i> General Information</h5>
                                </div>

                                <div class="card-body">
                                    <div class="row g-3 mb-2">
                                        <div class="col-mx">
                                            <div class="form">
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="ddlStatus" class="form-label">Campaign Status</label>
                                                            <select class="form-control" id="ddlStatus">
                                                                <option value="1" selected>Enable</option>
                                                                <option value="2">Disable</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtCampaignName" class="form-label">Campaign Name</label>
                                                            <input type="text" class="form-control" id="txtCampaignName" placeholder="Campaign Name" name="campaignName" />
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtSubject" class="form-label">Subject</label>
                                                            <input type="text" class="form-control" id="txtSubject" placeholder="Campaign Subject" name="campaignSubject"/>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="ddlTemplate" class="form-label">Template</label>
                                                            <select class="form-control" id="ddlTemplate" name="campaignEmailTemplate">
                                                                
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="card-header align-items-center d-flex">
                                    <h5 class="card-title mb-0 flex-grow-1"><i class="ri-mail-add-line"></i> Target Audience</h5>
                                </div>

                                <div class="card-body">
                                    <div class="row g-3 mb-2">
                                        <div class="col-mx">
                                            <div class="form">
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="ddlAudienceGroup" class="form-label">Audience Group</label>
                                                            <select class="form-control" id="ddlAudienceGroup">
                                                               
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="ddlTags" class="form-label">Tags</label>
                                                            <select class="form-control" id="ddlTags" multiple="multiple">
                                                                
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="card-header align-items-center d-flex">
                                    <h5 class="card-title mb-0 flex-grow-1"><i class="ri-mail-add-line"></i> Schedule</h5>
                                </div>

                                <div class="card-body">
                                    <div class="row g-3 mb-2">
                                        <div class="col-mx">
                                            <div class="form">
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="dtmScheduleDate" class="form-label">Scheduler Date</label>
                                                            <input type="date" class="form-control" id="dtmScheduleDate">
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="dtmScheduleTime" class="form-label">Schedule Time</label>
                                                            <input type="time" class="form-control" id="dtmScheduleTime">
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
                                            <a href="<%=Page.ResolveUrl("~/EmailCampaign") %>" type="button" class="btn btn-soft-secondary"><i class="ri-close-circle-line"></i> Cancel</a>
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
        function tabUIControl() {
            $('button[data-bs-toggle="tab"]').on('shown.bs.tab', function (e) {
                let target = $(e.target).attr("href");
                let tabGeneralInfo = $('#tabGeneralInfo');
                let tabOthers = $('#tabOthers');

                if (target == null) { return; }
                if (tabGeneralInfo.length == 0 || tabOthers.length == 0) { return; }

                switch (target) {
                    case '#user-details':
                        tabGeneralInfo.removeClass('btn-soft-primary').addClass('btn-primary');
                        tabOthers.removeClass('btn-primary').addClass('btn-soft-primary');
                        break;
                    case '#bank-details':
                        tabGeneralInfo.removeClass('btn-primary').addClass('btn-soft-primary');
                        tabOthers.removeClass('btn-soft-primary').addClass('btn-primary');
                        break;
                }
            });
        }

        var btnSubmit = $('#btnSave')
        var ddlStatus = $('#ddlStatus')
        var txtCampaignName = $('#txtCampaignName')
        var txtSubject = $('#txtSubject')
        var ddlTemplate = $('#ddlTemplate')
        var ddlAudienceGroup = $('#ddlAudienceGroup')
        var dtmScheduleDate = $('#dtmScheduleDate')
        var dtmScheduleTime = $('#dtmScheduleTime')

        var form = $('#form1')
        form.validate({
            rules: {
                campaignName: {
                    required: true,
                },
                campaignSubject: {
                    required: true,
                },
                campaignEmailTemplate: {
                    selectTemplate: true,
                },
            },
            errorClass: 'is-invalid',
            errorPlacement: function (error, element) {
                error.addClass("invalid-feedback");
                error.insertAfter(element);
            }
        })

        btnSubmit.click(async function (e) {
            e.preventDefault()

            let scheduledDate
            if (dtmScheduleDate.val() != '') {
                scheduledDate = new Date(dtmScheduleDate.val() + ' ' + dtmScheduleTime.val())
            }

            if (form.valid()) {
                $(this).prop('disabled', true)

                const payload = {
                    CampaignId: vm.Campaign.CampaignId,
                    StatusId: ddlStatus.val(),
                    Name: txtCampaignName.val(),
                    Subject: txtSubject.val(),
                    EmailTemplateId: ddlTemplate.val(),
                    AudienceGroupId: ddlAudienceGroup.val(),
                    ScheduledDate: scheduledDate
                }
                const res = await ApiHelper.post(window.location.origin + '/Campaign/Update', payload)
                if (!res.data.Error) {
                    dialogHelper.successAutoRedirect('Campaign updated', '<%= Page.ResolveUrl("~/EmailCampaign") %>')
                } else {
                    dialogHelper.error(res.data.Message)
                }

                $(this).prop('disabled', false)
            }
        })

        function prepare() {
            vm.EmailTemplates.forEach(function (item) {
                ddlTemplate.append(`<option value='${item.Key}'>${item.Text}</option>`)
            })

            vm.AudienceGroups.forEach(function (item) {
                ddlAudienceGroup.append(`<option value='${item.Key}'>${item.Text}</option>`)
            })
        }

        $.validator.addMethod(
            "selectTemplate",
            function (value, element) {
                //var selectedCountry = $('#Country').val();
                if (ddlTemplate.val() === '0') {
                    return false;
                } else return true;
            },
            "This field is required."
        );

        function loadDetails() {
            ddlStatus.val(vm.Campaign.StatusId)
            txtCampaignName.val(vm.Campaign.Name)
            txtSubject.val(vm.Campaign.Subject)
            ddlTemplate.val(vm.Campaign.EmailTemplateId)
            ddlAudienceGroup.val(vm.Campaign.AudienceGroupId)
            const scheduledDate = new Date(vm.Campaign.ScheduledDate)

            let year = scheduledDate.getFullYear();
            let month = String(scheduledDate.getMonth() + 1).padStart(2, '0'); // Months are zero-indexed, so we add 1
            let day = String(scheduledDate.getDate()).padStart(2, '0');

            let hours = String(scheduledDate.getHours()).padStart(2, '0');
            let minutes = String(scheduledDate.getMinutes()).padStart(2, '0');

            dtmScheduleDate.val(`${year}-${month}-${day}`)
            dtmScheduleTime.val(`${hours}:${minutes}`)
        }

        $(document).ready(function () {
            //nav-tab ui control
            //tabUIControl();

            $('#ddlTags').select2();
            vm = JSON.parse($('#<%= hfModelView.ClientID %>').val())
            prepare()
            loadDetails()
        });
    </script>
</asp:Content>
