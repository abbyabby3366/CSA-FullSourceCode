<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmailTemplateDetails.aspx.cs" Inherits="csa.Admin.EmailTemplateDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveUrl("~/assets/libs/quill/quill.snow.css") %>"/>
    <style>
  .quill-editor {
    height: 300px; /* Set your desired height */
    overflow-y: auto; /* Enable scrolling if content exceeds height */
  }
</style>
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
                            <h4 class="fs-16 mb-1">Email Template</h4>
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
                    <button type="button" data-bs-toggle="tab" href="#template-info" role="tab" aria-selected="false" class="btn btn-primary bg-gradient waves-effect waves-light text-start w-100 mb-2" id="tabTemplateInfo"><i class="ri-mail-add-line"></i> Template Information</button>
                </div>

                <!-- right panel -->
                <div class="col-12 col-xxl-9 col-xl-9 col-lg-8 col-md-7 col-sm-12">
                    <!-- tab content -->
                    <div class="tab-content">
                        <div class="tab-pane active" id="template-info" role="tabpanel">
                            <div class="card">
                                <div class="card-header align-items-center d-flex">
                                    <h5 class="card-title mb-0 flex-grow-1"><i class="ri-mail-add-line"></i> Template Information</h5>
                                </div>

                                <div class="card-body">
                                    <div class="row g-3 mb-2">
                                        <div class="col-mx">
                                            <div class="form">
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="ddlStatus" class="form-label">Template Status</label>
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
                                                            <label for="txtTemplateName1" class="form-label">Template Name</label>
                                                            <input type="text" class="form-control" id="txtTemplateName1" name="templateName1" placeholder="Template Name 1" >
                                                        </div>
                                                    </div>
                                                   <%-- <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="txtSubject" class="form-label">Template Template</label>
                                                            <input type="text" class="form-control" id="txtTemplateName2" placeholder="Template Name 2" value="Template Name 2">
                                                        </div>
                                                    </div>--%>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">
                                                            <label for="ddlTemplate" class="form-label">Template Type</label>
                                                            <select class="form-control" id="ddlTemplate">
                                                                <option value="1" selected>HTML</option>
                                                                <option value="2">TEXT</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="mb-3">

                                                        </div>
                                                    </div>

                                                    <div class="col-12">
                                                        <div class="mb-3">
                                                            <div id="editor" class="quill-editor">
                                                                
                                                            </div>
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
                                            <a href="<%=Page.ResolveUrl("~/EmailTemplate") %>" type="button" class="btn btn-soft-secondary"><i class="ri-close-circle-line"></i> Cancel</a>
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
    <script  type='text/javascript' src="<%=Page.ResolveUrl("~/assets/libs/quill/quill.min.js") %>"></script>
    <script>
        const quill = new Quill('#editor', {
            theme: 'snow'
        });

        var vm
        var btnSave = $('#btnSave')
        var ddlStatus = $('#ddlStatus')
        var txtTemplateName1 = $('#txtTemplateName1')
        var ddlTemplate = $('#ddlTemplate')

        var form = $('#form1')
        form.validate({
            rules: {
                templateName1: {
                    required: true,
                },
            },
            errorClass: 'is-invalid',
            errorPlacement: function (error, element) {
                error.addClass("invalid-feedback");
                error.insertAfter(element);
            }
        })

        btnSave.click(async function (e) {
            e.preventDefault()

            if (form.valid()) {
                $(this).prop('disabled', true)

                const payload = {
                    EmailTemplateId: vm.EmailTemplate.EmailTemplateId,
                    Name: txtTemplateName1.val(),
                    TemplateTypeId: ddlTemplate.val(),
                    StatusId: ddlStatus.val(),
                    Content: quill.root.innerHTML
                }
                const res = await ApiHelper.post(window.location.origin + '/Tmplateemail/Update', payload)
                if (!res.data.Error) {
                    dialogHelper.successAutoRedirect('Email template updated', '<%= Page.ResolveUrl("~/EmailTemplate") %>')
                } else {
                    dialogHelper.error(res.data.Message)
                }

                $(this).prop('disabled', false)
            }
        })

        function loadDetails() {
            txtTemplateName1.val(vm.EmailTemplate.Name)
            ddlTemplate.val(vm.EmailTemplate.TemplateTypeId)
            ddlStatus.val(vm.EmailTemplate.StatusId)
            
            let delta = quill.clipboard.convert(vm.EmailTemplate.Content);
            quill.setContents(delta);
        }

        $(document).ready(function () {
            vm = JSON.parse($('#<%= hfModelView.ClientID%>').val())
            loadDetails()
        });
</script>
</asp:Content>
