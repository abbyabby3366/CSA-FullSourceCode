<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApplicationAssign.aspx.cs" Inherits="csa.Admin.ApplicationAssign" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <!-- select2 -->
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2.min.css") %>" />
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2-bootstrap-5-theme.min.css") %>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfModelView" runat="server" />
    <div class="page-content">
        <div class="container-fluid">
            <!-- page header -->
            <div class="row mb-3 pb-1">
                <div class="col-12">
                    <div class="d-flex align-items-lg-center flex-lg-row flex-column">
                        <div class="flex-grow-1">
                            <h4 class="fs-16 mb-1">Application Management</h4>
                        </div>
                    </div>
                </div>
            </div>



            <div class="row">
                <div class="col-12">
                    <!-- tab content -->
                    <div class="tab-content">
                        <div class="tab-pane active" id="general-info2" role="tabpanel">
                            <div class="card">
                                <div class="card-header align-items-center d-flex">
                                    <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Application Details</h5>
                                    <div class="float-end">
                                        <div class="d-flex">
                                        </div>
                                    </div>
                                </div>

                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-xl-4 col-md-6 col-xs-12">
                                            <div class="row">
                                                <div class="col-5 bold">
                                                    Customer Full Name
                                                </div>
                                                <div class="col-7" id="contentCustomerFullName">
                                                    <%--AZAHAR BIN MOHD--%>
                                                </div>
                                                <div class="col-5 mt-2 bold">
                                                    IC Number
                                                </div>
                                                <div class="col-7 mt-2" id="contentIcNumber">
                                                    <%--92112-14-2114--%>
                                                </div>
                                                <div class="col-5 mt-2 bold">
                                                    Gross Salary
                                                </div>
                                                <div class="col-7 mt-2" id="contentGrossSalary">
                                                    <%--RM 7000.00--%>
                                                </div>
                                                <div class="col-5 mt-2 bold">
                                                    Salary Range
                                                </div>
                                                <div class="col-7 mt-2" id="contentSalaryRange">
                                                    <%--RM 7000.00 - RM 10000.00--%>
                                                </div>
                                          <div class="col-5 mt-2 bold">
                                                    PFC / RM Name
                                                </div>
                                                <div class="col-7 mt-2" id="contentPfc">
                                                    <%--NAZZI--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-4 col-md-6 col-xs-12">
                                            <div class="row">
                                                <div class="col-5 bold">
                                                    Employer
                                                </div>
                                                <div class="col-7" id="contentEmployer">
                                                    <%--Hospital Besar SG Bakap--%>
                                                </div>
                                                <div class="col-5 mt-2 bold">
                                                    State
                                                </div>
                                                <div class="col-7 mt-2" id="contentState">
                                                    <%--Kedah--%>
                                                </div>
                                                <div class="col-5 mt-2 bold">
                                                    Retirement Age
                                                </div>
                                                <div class="col-7 mt-2" id="contentRetirementAge">
                                                    <%--60--%>
                                                </div>
                                                
                                           

                                                  <div class="col-5 mt-2 bold">
                                                    Referrer Full Name
                                                </div>
                                                <div class="col-7 mt-2" id="contentReferrerFullName">
                                                    <%--AZAHAR BIN MOHD--%>
                                                </div>
                                                <div class="col-5 mt-2 bold">
                                                    Referrer File Number
                                                </div>
                                                <div class="col-7 mt-2" id="contentReferrerFileNumber">
                                                    <%--CSA-X0000*Y--%>
                                                  <%--  Penjelasan file number:
                                                    - X diganti jadi G atau P, G itu government (karyawan pemerintah) atau P (karyawan private swasta) ini harus ditambah "employer type" di customer profile
                                                    - 0000 itu primary key referrer usernya, padding 0 supaya pas 4 digit, kalau udah 1000 ngak usah padding, 10000 juga biarin
                                                    - Y ganti ke R = RNR (application lagi in process), X = application rejected, M = customer status Burst (walaupun rejected, tetep show M kalo customer statusnya burst)--%>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-4 col-md-6 col-xs-12">
                                            <div class="row">
                                                <div class="col-5 bold">
                                                    Key In Date
                                                </div>
                                                <div class="col-7" id="contentKeyInDate">
                                                    <%--13 March 2024--%>
                                                </div>
                                                <div class="col-5 bold mt-2">
                                                    Source
                                                </div>
                                                <div class="col-5 mt-2" id="contentSource">
                                                    <%-----%>
                                                </div>
                                                <div class="col-5 bold mt-2">
                                                    Prepared By
                                                </div>
                                                <div class="col-7 mt-2" id="contentPreparedBy">
                                                    <%--PIKAH--%>
                                                </div>
                                                <div class="col-5 mt-2 bold">
                                                    Verified By
                                                </div>
                                                <div class="col-7 mt-2" id="contentVerifiedBy">
                                                    <%--Mira--%>
                                                </div>
                                                <div class="col-5 mt-2 bold">
                                                    Credit Remark
                                                </div>
                                                <div class="col-7 mt-2" id="contentCreditRemark">
                                                    <%--PROPOSED FOR RNR UNDER GROUP B (DONE 5/8 @ 2.42PM FEES - RM104K++)--%>
                                                </div>
                                              

                                            </div>
                                        </div>

                                         
                                       

                                    </div>

                                    
                                
                                </div>

                                <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Role Assignation</h5>
                                    </div>
                                <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Referrer</label>
                                                    <select class="form-control" id="ddlMember">
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">PFC</label>
                                                    <select class="form-control dropdown-with-search" id="ddlPfc">
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">UM</label>
                                                    <select class="form-control dropdown-with-search" id="ddlUm">
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">AM</label>
                                                    <select class="form-control dropdown-with-search" id="ddlAm">
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">RM</label>
                                                    <select class="form-control dropdown-with-search" id="ddlRm">
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">PA</label>
                                                    <select class="form-control dropdown-with-search" id="ddlPa">
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Prepared By</label>
                                                    <select class="form-control dropdown-with-search" id="ddlPreparedBy">
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Analyzed By</label>
                                                    <select class="form-control dropdown-with-search" id="ddlAnalyzedBy">
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                <div class="card-footer">
                                   <div class="col-lg-12">
                                            <div class="hstack gap-2 justify-content-end">
                                                <button type="button" id="btnSave" class="btn btn-primary"><i class="me-1 ri-save-line"></i>Save</button>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <!-- select2 js -->
    <script  type='text/javascript' src="<%=Page.ResolveUrl("~/assets/libs/select2/js/select2.min.js") %>"></script>
    <script>
        $('.dropdown-with-search').select2({theme: 'bootstrap-5'});

        var vm
        var ddlMember = $('#ddlMember')
        var ddlPfc = $('#ddlPfc')
        var ddlUm = $('#ddlUm')
        var ddlAm = $('#ddlAm')
        var ddlRm = $('#ddlRm')
        var ddlPa = $('#ddlPa')
        var ddlPreparedBy = $('#ddlPreparedBy')
        var ddlAnalyzedBy = $('#ddlAnalyzedBy')

        function prepare() {

            vm.CreditTeams.forEach(function (item) {
                ddlPfc.append(`<option value='${item.Value}'>${item.Text}</option>`)
                ddlPreparedBy.append(`<option value='${item.Value}'>${item.Text}</option>`)
                ddlAnalyzedBy.append(`<option value='${item.Value}'>${item.Text}</option>`)                
            })
            vm.SalesDirector.forEach(function (item) {
                ddlUm.append(`<option value='${item.Value}'>${item.Text}</option>`)
                ddlAm.append(`<option value='${item.Value}'>${item.Text}</option>`)                
            })
            vm.RM.forEach(function (item) {
                ddlRm.append(`<option value='${item.Value}'>${item.Text}</option>`)                
            })
            vm.PA.forEach(function (item) {
                ddlPa.append(`<option value='${item.Value}'>${item.Text}</option>`)
            })
        }

        function loadDetails() {
            $('#contentCustomerFullName').text(vm.Info.CustomerName)
            $('#contentIcNumber').text(vm.Info.ICNumber)
            $('#contentGrossSalary').text(vm.Info.GrossSalary)
            $('#contentSalaryRange').text(vm.Info.SalaryRange)
            $('#contentPfc').text(vm.Info.PFC)
            $('#contentEmployer').text(vm.Info.Employer)
            $('#contentState').text(vm.Info.State)
            $('#contentRetirementAge').text(vm.Info.RetirementAge)
            $('#contentReferrerFullName').text(vm.Info.ReferralName)
            $('#contentReferrerFileNumber').text(vm.Info.ReferralFileNumber)
            $('#contentKeyInDate').text(vm.Info.KeyInDate)
            $('#contentSource').text(vm.Info.Source)
            $('#contentPreparedBy').text(vm.Info.PreparedBy)
            $('#contentVerifiedBy').text(vm.Info.VerifiedBy)
            $('#contentCreditRemark').text(vm.Info.Remark)

            setDropdownValue(ddlPfc,vm.PFCAdminId)
            setDropdownValue(ddlUm, vm.UMAdminId)
            setDropdownValue(ddlAm, vm.AMAdminId)
            setDropdownValue(ddlRm, vm.RMAdminId)
            setDropdownValue(ddlPa, vm.PAAdminId)
            setDropdownValue(ddlPreparedBy, vm.PreparedAdminId)
            setDropdownValue(ddlAnalyzedBy, vm.AnalyzedAdminId)
            if (vm.ReferrerMemberId != null) {
                var newOption = new Option(vm.ReferrerMemberId.Text, vm.ReferrerMemberId.Value, false, false);
                ddlMember.append(newOption).trigger('change')
            }
        }

        function setDropdownValue(control, value) {
            control.val(value)
            if (control.val() == null) {
                control.prop('selectedIndex',0)
            }
            control.trigger('change')
        }

        ddlMember.select2({
            placeholder: 'Select an option',
            theme: 'bootstrap-5',
            minimumInputLength: 1,
            ajax: {
                url: '<%=Page.ResolveUrl("~/Application/GetMemberVts")%>', // Replace with your API endpoint
                dataType: 'json',
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

        $('#btnSave').click(async function () {
            $(this).prop('disabled', true)

            const data = {
                ApplicationId: vm.ApplicationId,
                AdminId: <%= CurrentLoginAdmin.AdminId %>,
                MemberId: ddlMember.val(),
                PfcAdminId: ddlPfc.val(),
                AmAdminId: ddlAm.val(),
                UmAdminId: ddlUm.val(),
                RmAdminId: ddlRm.val(),
                PaAdminId: ddlPa.val(),
                PreparedAdminId: ddlPreparedBy.val(),
                AnalyzedAdminId: ddlAnalyzedBy.val(),
            }

            const res = await ApiHelper.postFormData(window.location.origin + '/application/Assign', data)
            if (!res.data.Error) {
                dialogHelper.successAutoRedirect("Save successfully",`<%= Page.ResolveUrl("~/ApplicationDetails?id=") %>${vm.ApplicationId}`)
            } else {
                dialogHelper.error(res.data.Message)
            }


            $(this).prop('disabled', false)
        })


        $(document).ready(function () {
            vm = JSON.parse($('#<%= hfModelView.ClientID %>').val())
            prepare()
            loadDetails()
        })
    </script>
</asp:Content>
