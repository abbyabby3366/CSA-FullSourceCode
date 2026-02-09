<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="YabamClients.aspx.cs" Inherits="csa.Admin.YabamClients" %>
<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
    <!-- datatables css -->
    <link href="<%=Page.ResolveUrl("~/assets/libs/datatables/datatables.min.css") %>" rel="stylesheet" type="text/css" />
    <!-- select2 -->
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2.min.css") %>" />
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2-bootstrap-5-theme.min.css") %>" />
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="container-fluid">
            <!-- page header -->
            <div class="row mb-3 pb-1">
                <div class="col-12">
                    <div class="d-flex align-items-lg-center flex-lg-row flex-column">
                        <div class="flex-grow-1">
                            <h4 class="fs-16 mb-1">YABAM List</h4>
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

                                        <a href="<%=Page.ResolveUrl("~/AddNewMember") %>" class="btn btn-soft-primary waves-effect waves-light col-md-3 col-sm-12">Add New Client</a>
                                    </div>
                                </div>
                            </div>
                         </div>
                            

                            <table id="gvData" class="table dt-responsive nowrap align-middle" style="width: 100%">
                                <thead class="table-light text-muted">
                                    <tr>
                                        <th>NO</th>
                                        <th>FILE NUMBER</th>
                                        <th>MEMBER TYPE</th>
                                        <th>FULL NAME</th>
                                        <th>IC</th>
                                        <th>CONTACT NO</th>
                                        <th>SALARY RANGE</th>
                                        <th>REFERRAL NAME</th>
                                        <th>EMPLOYER NAME</th>
                                        <th>STATE</th>
                                        <th>SECTOR</th>
                                        <th>OCCUPATION</th>
                                        <th>BANK ACCOUNT NAME</th>
                                        <th>BANK NAME</th>
                                        <th>BANK ACCOUNT NUMBER</th>
                                        <th>CREATED DATE</th>
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

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent" runat="server">
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
        var gvData = {};
        function initMemberGV() {
            gvData = $("#gvData").DataTable({
                'responsive': false,
                //'buttons': [
                //    'print',
                //    'copyHtml5',
                //    'excelHtml5',
                //    'csvHtml5',
                //    'pdfHtml5',
                //],
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
                /*'scrollY': true,*/
                //'scrollX': true,
                //'scrollXInner': true,
                //'scrollCollapse': true,
                'ajax': {
                    'url': '<%=Page.ResolveUrl("~/Member/GetYabamClientGV")%>',
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
                initComplete: function () {
                    $('.dt-processing').closest('.row').removeClass('row').attr('style', 'overflow-x:auto;overflow-y:hidden');
                },
                'columns': [
                    { 'data': null },
                    { 'data': 'FileNumber', 'type': 'string' },
                    { 'data': 'MemberType', 'type': 'string' },
                    { 'data': 'FullName', 'type': 'string' },
                    { 'data': 'ICNumber', 'type': 'string' },
                    { 'data': 'PhoneNumber', 'type': 'string' },
                    { 'data': 'SalaryRange', 'type': 'string' },
                    { 'data': 'ReferrerName', 'type': 'string' },
                    { 'data': 'EmployerName', 'type': 'string' },
                    { 'data': 'State', 'type': 'string' },
                    { 'data': 'Sector', 'type': 'string' },
                    { 'data': 'Occupation', 'type': 'string' },
                    { 'data': 'BankAccountName', 'type': 'string' },
                    { 'data': 'Bank', 'type': 'string' },
                    { 'data': 'BankAccountNumber', 'type': 'string' },
                    { 'data': 'CreateDate', 'type': 'date' },
                    { 'data': null },
                ],
                'order': [
                    [15, "desc"]
                ],
                'pageLength': 10,
                //'fixedHeader': {
                //    'header': true,
                //},
                //'fixedColumns': {
                //    'leftColumns': 1
                //},
                'columnDefs': [
                    {
                        'targets': [0, 16],
                        'searchable': false,
                        'orderable': false
                    },
                    {
                        'targets': [1,15],
                        'searchable': true,
                        'orderable': true
                    },
                    {
                        'targets': 15,
                        'render': function (d, t, r, m) {
                            if (d != null) {
                                return appHelper.dateToFormat(d);
                            } else { return ''; }
                        }
                    },
                    {
                        'targets': 16,
                        'width': 200,
                        'render': function (d, t, r, m) {
                            return `<div class="hstack gap-3 fs-13 justify-content-center">
                                               <a href="javascript:void(0);" class="text-warning"><i class="ri-price-tag-line"></i></a>
                                               <a href="<%=Page.ResolveUrl("~/MemberDetails")%>?id=${d.MemberId}" class="text-success"><i class="ri-edit-line"></i></a>
                                               <a href="javascript:void(0);" class="text-primary"><i class="ri-lock-unlock-line"></i></a>
                                               <a href="javascript:void(0);" class="text-primary btnCopyRefLink" data-ref="${d.ReferralLink}"><i class="ri-links-line"></i></a>
                                           </div>`
                        }
                    },
                ]
            });

            gvData.on('draw.dt', function () {
                let pgInfo = gvData.data().page.info();

                gvData.column(0, { page: 'current' }).nodes().each(function (cell, i) {
                    cell.innerHTML = i + 1 + pgInfo.start;
                });
            });

            $(document).on('click', '.btnCopyRefLink', function () {
                navigator.clipboard.writeText($(this).data('ref'));
                $(this).removeClass('text-primary').addClass('text-success')
                $(this).html(`<i class="ri-links-line"></i>Copied`)
                setTimeout(() => {
                    $(this).removeClass('text-success').addClass('text-primary')
                    $(this).html(`<i class="ri-links-line"></i>`)
                }, 2000)
            })

            $('#txtSearch').bind('keyup', function (e) {
                if (e.keyCode == 13) {
                    e.preventDefault();
                    gvData.ajax.reload();
                }
            });

            $('#btnSearch').on('click', function (e) {
                e.preventDefault();

                gvData.ajax.reload();
            });
        }

        $(document).ready(function () {
            initMemberGV();
        });
    </script>
</asp:Content>
