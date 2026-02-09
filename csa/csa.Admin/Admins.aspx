<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admins.aspx.cs" Inherits="csa.Admin.Admins" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <!-- datatables css -->
    <link href="<%=Page.ResolveUrl("~/assets/libs/datatables/datatables.min.css") %>" rel="stylesheet" type="text/css" />
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
                            <h4 class="fs-16 mb-1">Admin List</h4>
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
                            <div class="col-md-3 col-sm-12 d-flex align-items-center"><h5 class="m-0 p-0 "></h5></div>
                                <div class="col-md-9 col-sm-12 ">
                                    <div class="row justify-content-end gy-3">
                                        <div class="col-md-3 col-sm-12">
                                            <div class="input-group">

                                            <input type="text" class="form-control" id="txtSearch" placeholder="Search" />
                                            <button type="button" class="btn btn-tertiary btn-label ms-auto" id="btnSearch"><i class="ri-search-line label-icon align-middle fs-16 me-2"></i> Search </button>
                                            </div>
                                        </div>

                                        <a href="<%=Page.ResolveUrl("~/AddNewAdmin") %>" class="btn btn-soft-primary waves-effect waves-light col-md-3 col-sm-12">Add New Admin</a>
                                    </div>
                                </div>
                            </div>
                         </div>
                            

                            <table id="gvMain" class="table dt-responsive nowrap align-middle" style="width: 100%">
                                <thead class="table-light text-muted">
                                    <tr>
                                        <th>FULL NAME</th>
                                        <th>EMAIL</th>
                                        <th>SUPER ADMIN</th>
                                        <th>STATUS</th>
                                        <th>CREATE DATE</th>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <!-- moment js -->
    <script src="<%=Page.ResolveUrl("~/assets/libs/moment/min/moment.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/assets/libs/moment/min/moment-with-locales.min.js") %>"></script>
    <!-- datatables js -->
    <script src="<%=Page.ResolveUrl("~/assets/libs/datatables/datatables.min.js") %>"></script>
    <%--<script src="<%=Page.ResolveUrl("~/assets/libs/datatables/DateTime-1.5.2/js/dataTables.dateTime.min.js") %>"></script>--%>
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
        function initGV() {
            gvTable = $("#gvMain").DataTable({
                'responsive': false,
                'processing': true,
                'serverSide': true,
                'language': {
                    'processing': '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only"><%=GetLangText("LblPleaseWait")%></span>',
                    'emptyTable': '<%=GetLangText("LblGridViewNoData")%>',
                    'lengthMenu': '<%=GetLangText("LblGridViewShow")%> <label class="form-control-label">_MENU_</label> <%=GetLangText("LblGridViewEntries")%>',
                    'info': 'Showing _START_ to _END_ of _TOTAL_ _ENTRIES-TOTAL_',
                    'search': '<%=GetLangText("LblGridViewSearch")%>'
                },
                'dom': '<"row mx-0"<"col-4"i><"col-8 text-right">><"row"<"col-12"tr>><"row mt-2"<"col-6 px-4"l><"col-6 d-flex justify-content-end"p>>',
                'info': false,
                'ajax': {
                    'url': '<%=Page.ResolveUrl("~/Admin/GetAdminsGV")%>',
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
                        };
                    },
                    'dataType': "json",
                    'dataSrc': function (d) {
                        return d.data;
                    },
                    'error': function (response) {
                        alert('Error!!');
                    }
                },
                initComplete: function () {
                    $('.dt-processing').closest('.row').removeClass('row').attr('style', 'overflow-x:auto;overflow-y:hidden');
                },
                'columns': [
                    { 'data': 'Name', 'type': 'string' },
                    { 'data': 'Email', 'type': 'string' },
                    { 'data': 'IsSuperAdmin', 'type': 'num' },
                    { 'data': 'StatusId', 'type': 'num' },
                    { 'data': 'CreateDate', 'type':'date' },
                    { 'data': 'AdminId',type:'num' },
                ],
                'order': [
                    [4, "desc"]
                ],
                'pageLength': 10,
                'columnDefs': [
                    { target: '_all', searchable: false, className: 'dt-head-left' },
                    { target: 2, render: function (d, t, r, m) { return d == 1 ? "SUPERADMIN": "" } },
                    { target: 3, render: function (d, t, r, m) { return d == 1 ? 'Active' : "" } },
                    { target: 4, render: function (d, t, r, m) { return d != null ? appHelper.dateToFormat(d) : "" } },
                    {
                        'targets': 5,
                        'orderable': false,
                        'width': 200,
                        'render': function (d, t, r, m) {
                            return `<div class="hstack gap-3 fs-13 justify-content-center">
                                               <a href="<%=Page.ResolveUrl("~/AdminDetails")%>?id=${d}" class="text-success">Edit</a>
                                           </div>`
                        }
                    },
                ]
            });

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
        }

        $(document).ready(function () {
            initGV();
        });
    </script>
</asp:Content>
