<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Applications.aspx.cs" Inherits="csa.Admin.Applications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <!-- datatables css -->
    <link href="<%=Page.ResolveUrl("~/assets/libs/datatables/datatables.min.css") %>" rel="stylesheet" type="text/css" />
    <!-- select2 -->
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2.min.css") %>" />
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2-bootstrap-5-theme.min.css") %>" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="container-fluid">
            <!-- page header -->
            <div class="row mb-3 pb-1">
                <div class="col-12">
                    <div class="d-flex align-items-lg-center flex-lg-row flex-column">
                        <div class="flex-grow-1">
                            <h4 class="fs-16 mb-1">Application Management</h4>
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
                <div class="col-lg-12">
                    <div class="card">

                        <div class="card-header">
                             <div class="row px-2 gy-2">
                            <div class="col-md-3 col-sm-12 d-flex align-items-center"><h5 class="m-0 p-0 "><%--Applicant List--%></h5></div>
                                <div class="col-md-9 col-sm-12 ">
                                    <div class="row justify-content-end gy-3">
                                        <div class="col-md-3 col-sm-12">
                                            <div class="input-group">

                                            <input type="text" class="form-control" id="txtSearch" placeholder="Search" />
                                            <button type="button" class="btn btn-tertiary btn-label ms-auto" id="btnSearch"><i class="ri-search-line label-icon align-middle fs-16 me-2"></i> Search </button>
                                            </div>
                                        </div>

                                        <div class="col-md-2 col-sm-12">
                                            <select class="form-control" id="ddlApplicationStatusId">
                                                <option value="-1" selected>All</option>
                                                <option value="0">Unassigned</option>
                                                <option value="1">Pre-checking</option>
                                                    <option value="2">Proposal Preparation</option>
                                                    <option value="3">Proposal Presentation</option>
                                                    <option value="4">Pre-Signing</option>
                                                    <option value="5">Pending Zoom & Acceptance</option>
                                                    <option value="6">Settlement</option>
                                                    <option value="7">CCRIS Cleaning/Monitoring</option>
                                                    <option value="8">Queue For Reloan</option>
                                                    <option value="9">Reloan Submission</option>
                                                    <option value="10">Collection</option>
                                            </select>
                                        </div>
                                        <div class="col-md-2 col-sm-12">
                                            <select id="ddlAdmin"></select>
                                        </div>

                                        <a href="<%=Page.ResolveUrl("~/ApplicationCreate") %>" class="btn btn-soft-primary waves-effect waves-light col-md-3 col-sm-12">Add New Application</a>
                                    </div>
                                </div>
                            </div>
                         </div>
                        

                                <table id="gvApplicant" class="table dt-responsive nowrap align-middle" style="width: 100%">
                                <thead class="table-light text-muted">
                                    <tr>
                                        <th>ID</th>
                                        <th>CUSTOMER NAME</th>
                                        <th>FILE NUMBER</th>
                                        <th>CONTACT</th>
                                        <th>EMPLOYER</th>
                                        <th>SALARY RM</th>
                                        <th>STAGE</th>
                                        <th>CREATED DATE & TIME</th>
                                        <th class="text-center">ACTION</th>
                                    </tr>
                                </thead>
                            </table>
                    </div>
                </div>
            </div>
        </div>
        </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">
    <!-- moment js -->
    <script src="<%=Page.ResolveUrl("~/assets/libs/moment/min/moment.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/assets/libs/moment/min/moment-with-locales.min.js") %>"></script>
   <!-- datatables js -->
    <script src="<%=Page.ResolveUrl("~/assets/libs/datatables/datatables.min.js") %>"></script>
    <!-- select2 js -->
    <script  type='text/javascript' src="<%=Page.ResolveUrl("~/assets/libs/select2/js/select2.min.js") %>"></script>
       <script type="text/javascript">
           $("form").submit(function (e) {
               e.preventDefault();
           });
       </script>

    <script type="text/javascript">

       

        var gvTable = {};
        $('select').select2({ 'theme': 'bootstrap-5', 'minimumResultsForSearch': -1 });
        function initApplicantGV() {
            gvTable = $('#gvApplicant').DataTable({
                'responsive': false,
                'processing': true,
                'serverSide': true,
                'order': [
                    [7, "desc"]
                ],
                initComplete: function () {
                    $('.dt-processing').closest('.row').removeClass('row').attr('style', 'overflow-x:auto;overflow-y:hidden');
                },
                'pageLength': 10,
                'ajax': {
                    'url': '<%=Page.ResolveUrl("~/Application/GetApplicationGV")%>',
                    'type': "POST",
                    'data': function (e) {
                        let search = '';

                        let s = $('#txtSearch');

                        if (s.length > 0) { search = s.val(); }

                        return {
                            'search': search,
                            'start': e.start,
                            'length': e.length,
                            'order': JSON.stringify(e.order),
                            'applicationStatusId': $('#ddlApplicationStatusId').val(),
                            'adminId': $('#ddlAdmin').val()
                        };
                    },
                    //'contentType': "application/json; charset=utf-8",
                    'dataType': "json",
                    //'success': function (r) { debugger; },
                    'dataSrc': function (d) {
                        return d.data;
                    },
                    'error': function (response) {
                        alert('Error!!');
                    }
                },
                'language': {
                    'processing': '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only"><%=GetLangText("LblPleaseWait")%></span>',
                    'emptyTable': '<%=GetLangText("LblGridViewNoData")%>',
                    'lengthMenu': '<%=GetLangText("LblGridViewShow")%> <label class="form-control-label">_MENU_</label> <%=GetLangText("LblGridViewEntries")%>',
                    'info': 'Showing _START_ to _END_ of _TOTAL_ _ENTRIES-TOTAL_',
                    'search': '<%=GetLangText("LblGridViewSearch")%>'
                },
                'dom': '<"row mx-0"<"col-4"i><"col-8 text-right">><"row"<"col-12"tr>><"row mt-2 mx-clear"<"col-6 px-4"l><"col-6 d-flex justify-content-end"p>>',
                'info': false,
                'columns': [
                    { 'data': 'ApplicationId',type: 'num' },
                    { 'data': 'CustomerName', type: 'string' },
                    { 'data': 'FileNumber', type:'string' },
                    { 'data': 'ContactNo', type: 'string' },
                    { 'data': 'CompanyName',type: 'string' },
                    { 'data': 'Salary', type: 'num' },
                    { 'data': 'Status', type: 'string' },
                    { 'data': 'CreateDate', type: 'date' },
                    { 'data': null },
                ],
                'columnDefs': [
                    {
                        'targets': 0,
                        'searchable': false,
                        'orderable': false,
                        render: function (d, t, r, m) {
                            return d
                        }
                    },
                    {
                        'targets': 1,
                        'searchable': false,
                        'orderable': true,
                    },
                    {
                        'targets': 2,
                        'searchable': false,
                        'orderable': false,
                    },
                    {
                        'targets': 3,
                        'searchable': true,
                        'orderable': true,                        
                    },
                    {
                        'targets': 4,
                        'searchable': true,
                        'orderable': true,
                    },
                    {
                        'targets': 5,
                        'searchable': true,
                        'orderable': true,
                        render: (d, t, r, m) => {
                            if (d == null) {
                                return ''
                            }

                            return appHelper.formatPrice(d)
                        }
                    },
                    { className: 'dt-head-left', targets: '_all' },
                    {
                        'targets': 6,
                        'searchable': true,
                        'orderable': true,
                        render: function (d, t, r, m) {
                            if(d == '0') return ''
                            return d
                        }
                    },
                    {
                        'targets': 7,
                        'searchable': true,
                        'render': function (d, t, r, m) {
                            return appHelper.dateToFormat(d);
                        }
                    },
                    {
                        'targets': 8,
                        'searchable': false,
                        'orderable': false,
                        render: function (d, t, r, m) {
                            return `<div class="hstack gap-3 fs-13 justify-content-center">
                                               <a href="<%= Page.ResolveUrl("~/ApplicationDetails")%>?id=${d.ApplicationId}" class="text-success ${d.Status == 0 && 'd-none'}" style='text-decoration: underline;'>View</a>
                                               <a href="<%= Page.ResolveUrl("~/ApplicationAssign")%>?id=${d.ApplicationId}" class="text-warning ${d.CompletedAssign && 'd-none'}" style='text-decoration: underline;'>Assign</a>
                                               <a href="#" class="text-danger d-none" style='text-decoration: underline;'>Delete</a>
                                           </div>`
                        }
                    },
                ]
            })

            //gvTable.on('draw.dt', function () {
            //    let pgInfo = gvTable.data().page.info();

            //    gvTable.column(0, { page: 'current' }).nodes().each(function (cell, i) {
            //        cell.innerHTML = i + 1 + pgInfo.start;
            //    });
            //});

            $('#txtSearch').bind('keyup', function (e) {
                if (e.keyCode == 13) {
                    e.preventDefault();
                    gvTable.ajax.reload();
                }
            });

            $('#btnSearch').on('click', function (e) {
                e.preventDefault();

                gvTable.ajax.reload();
            });

            $('#ddlApplicationStatusId').on('change', function (e) {
                e.preventDefault();
                gvTable.ajax.reload();
            });
        }

        <%--function initExportExcel() {
            $('#btnExport').on('click', function (e) {
                //get last draw details
                let dtPrevPostData = gvTable.ajax.params();

                let data = {
                    'SearchText': dtPrevPostData.SearchText,
                    'Columns': dtPrevPostData.Columns,
                    'Order': dtPrevPostData.Order,
                    'Search': dtPrevPostData.Search
                };

                $.ajax({
                    'type': "POST",
                    'url': "<%=Page.ResolveUrl("~/Customer/ExportCustomers")%>",
                    'content': "application/json; charset=utf-8",
                    'data': data,
                    'success': function (d) {
                        var bytes = new Uint8Array(d.FileContents);
                        var blob = new Blob([bytes], { type: d.ContentType });
                        var link = document.createElement('a');
                        var url = window.URL.createObjectURL(blob);

                        link.href = url;
                        link.download = d.FileDownloadName;
                        link.click();

                        window.URL.revokeObjectURL(url);
                    },
                    'error': function (xhr, textStatus, errorThrown) {
                        alert('Export error!');
                    }
                });
            });
        }--%>

        $('#ddlAdmin').on('change', function (e) {
            e.preventDefault();
            gvTable.ajax.reload();
        })

        $('#ddlAdmin').select2({
            placeholder: 'Select admin',
            theme: 'bootstrap-5',
            minimumInputLength: 1,
            ajax: {
                url: '<%=Page.ResolveUrl("~/Admin/GetAdminSelectItems")%>', // Replace with your API endpoint
                type: 'POST',
                dataType: 'json',
                data: function (params) {
                    return {
                        q: params.term, // search term
                        exceptId: null
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

        $(document).ready(function () {

            
            //gridView
            initApplicantGV();

            
            <%--//export
            initExportExcel();--%>

        });
    </script>
</asp:Content>
