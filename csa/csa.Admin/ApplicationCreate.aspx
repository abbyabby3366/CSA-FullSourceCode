<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApplicationCreate.aspx.cs" Inherits="csa.Admin.ApplicationCreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <!-- select2 -->
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2.min.css") %>" />
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2-bootstrap-5-theme.min.css") %>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
                                    <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Application Create</h5>
                                    <div class="float-end">
                                        <div class="d-flex">
                                        </div>
                                    </div>
                                </div>

                                <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Member</label>
                                                    <select class="form-control" id="ddlMember">
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
        $('select').select2({ 'theme': 'bootstrap-5', 'minimumResultsForSearch': -1 });

        var ddlMember = $('#ddlMember')
       
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
                MemberId: ddlMember.val(),
            }

            const res = await ApiHelper.postFormData(window.location.origin + '/application/ApplicationCreate', data)
            if (!res.data.Error) {
                dialogHelper.successAutoRedirect("Save successfully", '<%= Page.ResolveUrl("~/Applications") %>')
            } else {
                dialogHelper.error(res.data.Message)
            }


            $(this).prop('disabled', false)
        })
    </script>
</asp:Content>
