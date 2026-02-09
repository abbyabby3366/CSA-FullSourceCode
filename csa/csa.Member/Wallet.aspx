<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Wallet.aspx.cs" Inherits="csa.Member.Wallet" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
    <!-- datatables css -->
    <link href="<%=Page.ResolveUrl("~/assets/libs/datatables/datatables.min.css") %>" rel="stylesheet" type="text/css" />

    <style type="text/css">
        span.circle_icon {
            background: #fff;
            border-radius: 50%;
            -moz-border-radius: 50%;
            -webkit-border-radius: 50%;
            color: #000;
            display: inline-block;
            padding: 0.65rem;
            text-align: center;
            vertical-align: middle;
            line-height: 0.95rem;
            font-size: 1rem;
            font-weight: 500;
            width: 38px;
            height: 38px;
            background-color: #efefef;
        }

        span.circle_icon > i.icon {
          vertical-align: middle;
        }
    </style>
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
                            <h4 class="fs-16 mb-1">Ganjaran</h4>
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
                <div class="col-12 col-xxl-4 col-xl-4 col-lg-4 col-md-5 col-sm-12">
                    <div class="card">
                        <div class="card-body px-1">
                            <ul class="list-group list-group-flush border-dashed mb-0">
                                <li class="list-group-item d-flex align-items-center">
                                    <div class="flex-shrink-0">
                                        <span class="circle_icon me-2"><i class="ri-wallet-line icon"></i></span>
                                    </div>
                                    <div class="flex-grow-1 ms-3">
                                        <h6 class="fs-14 mb-1">Ganjaran</h6>
                                        <%--<p class="text-muted mb-0">Malaysia Ringgit</p>--%>
                                    </div>
                                    <div class="flex-shrink-0 text-end">
                                        <h6 class="fs-16 fw-semibold m-0" id="lblMyWallet"></h6>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>

                    <div class="row g-2">
                        <div class="col-mx" role="tablist">
                            <button type="button" data-bs-toggle="tab" href="#withdrawal-request" role="tab" aria-selected="false" class="btn btn-primary bg-gradient waves-effect waves-light text-start w-100 mb-2 d-none" id="tabWithdrawalReq"><i class="ri-file-user-line"></i> Withdrawal Request</button>
                        </div>
                    </div>
                </div>

                <!-- right panel -->
                <div class="col-12 col-xxl-8 col-xl-8 col-lg-8 col-md-7 col-sm-12">
                    <!-- tab content -->
                    <div class="tab-content">
                        <div class="tab-pane active" id="withdrawal-request" role="tabpanel">
                            <div class="card d-none">
                                <div class="card-header align-items-center d-flex">
                                    <h5 class="card-title mb-0 flex-grow-1"><i class="ri-share-forward-2-fill"></i> Withdrawal Status</h5>
                                </div>

                                 <table class="table dt-responsive nowrap align-middle" id="tableWithdrawal" style="width: 100%">
                                            <thead class="table-light text-muted">
                                                <tr>
                                                    <th scope="col" class="text-center">DATETIME OF REQUEST</th>
                                                    <th scope="col" class="text-center">DATETIME OF APPROVAL</th>
                                                    <th scope="col" class="text-center">BANK ACCOUNT NUMBER</th>
                                                    <th scope="col" class="text-center">TRANSACTION AMOUNT</th>
                                                    <th scope="col" class="text-center">STATUS</th>
                                                </tr>
                                            </thead>
                                        </table>
                            </div>

                            <div class="card">
                                <div class="card-header align-items-center d-flex">
                                    <h5 class="card-title mb-0 flex-grow-1"><i class="ri-pages-line"></i> Transaction History</h5>
                                </div>

                                <table class="table dt-responsive nowrap align-middle" id="tableHistory" style="width: 100%">
                                            <thead class="table-light text-muted">
                                                <tr>
                                                    <th scope="col">TRANSACTION ID</th>
                                                    <th scope="col">CUSTOMER ID</th>
                                                    <th scope="col">TRANSACTION DATE</th>
                                                    <th scope="col">TRANSACTION TYPE</th>
                                                    <th scope="col">TRANSACTION AMOUNT</th>
                                                </tr>
                                            </thead>
                                        </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </div>
</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="<%=Page.ResolveUrl("~/assets/libs/moment/min/moment.min.js") %>"></script>
<script src="<%=Page.ResolveUrl("~/assets/libs/moment/min/moment-with-locales.min.js") %>"></script>

    <!-- datatables js -->
    <script src="<%=Page.ResolveUrl("~/assets/libs/datatables/datatables.min.js") %>"></script>
    <%--<script src="<%=Page.ResolveUrl("~/assets/libs/datatables/DateTime-1.5.2/js/dataTables.dateTime.min.js") %>"></script>--%>

    <script type="text/javascript">
        var tableWithdrawal = {};
        var tableHistory = {};

        function initDatatable() {
            tableWithdrawal = $("#tableWithdrawal").DataTable({
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
                /*'scrollY': true,*/
                'scrollX': true,
                'scrollXInner': true,
                'scrollCollapse': true,
                'ajax': {
                    'url': '<%=Page.ResolveUrl("~/Withdrawal/GetMemberGV")%>',
                    'type': "POST",
                    'data': function (e) {
                        return {
                            'Start': e.start,
                            'Length': e.length,
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
                'columns': [
                    { 'data': 'CreateDate', type:'date' },
                    { 'data': 'ResponseDate', 'type': 'date' },
                    { 'data': 'BankAccountNumber', 'type': 'string' },
                    { 'data': 'Amount', 'type': 'num' },
                    { 'data': 'StatusId', 'type': 'int' },
                ],
                'pageLength': 10,
                'order': [0,'desc'],
                'columnDefs': [
                    {
                        'targets': '_all',
                        'orderable': false,
                    },
                    {
                        'targets': [0,1],
                        'render': function (d, t, r, m) {
                            if(d == null) return ''
                            return moment(d).format("YYYY-MM-DD HH:mm:ss");
                        }
                    },                    
                    {
                        'targets': 3,
                        'render': function (d, t, r, m) {
                            return d.toFixed(2);
                        }
                    },
                    {
                        'targets': 4,
                        'className': 'dt-body-center',
                        'render': function (d, t, r, m) {
                            if (d != null) {
                                switch (d) {                                    
                                    default: return `${d}`
                                        break;
                                }
                            } else { return ''; }
                        }
                    },
                ]
            });


            tableHistory = $("#tableHistory").DataTable({
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
                /*'scrollY': true,*/
                'scrollX': true,
                'scrollXInner': true,
                'scrollCollapse': true,
                'ajax': {
                    'url': '<%=Page.ResolveUrl("~/History/GetMemberGV")%>',
                    'type': "POST",
                    'data': function (e) {
                        return {
                            'Start': e.start,
                            'Length': e.length,
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
                'columns': [
                    { 'data': 'HistoryId', 'type': 'int' },
                    { 'data': 'MemberCode', 'type': 'string' },
                    { 'data': 'CreateDate', 'type': 'date' },
                    { 'data': 'TransactionType', 'type': 'string' },
                    { 'data': 'TransactionAmount', 'type': 'num' },
                ],
                'pageLength': 10,
                'order': [0, 'desc'],
                'columnDefs': [
                    {
                        'targets': '_all',
                        'orderable': false,
                    },
                    {
                        'targets': [0, 1],
                        'render': function (d, t, r, m) {
                            if (d == null) return ''
                            return '#' + d;
                        }
                    },
                    {
                        'targets': 2,
                        'className': 'dt-body-right',
                        'render': function (d, t, r, m) {
                            if (d == null) return ''
                            return moment(d).format("YYYY-MM-DD HH:mm:ss");
                        }
                    },
                    {
                        'targets': 4,
                        'className': 'dt-body-right',
                        'render': function (d, t, r, m) {
                            return d.toFixed(2);
                        }
                    },
                ]
            });
        }

        $(document).ready(function () {
            vm = JSON.parse($('#<%= hfModelView.ClientID %>').val())
            initDatatable();
            $('#lblMyWallet').text(`${vm.MyWallet.toFixed(2)}`)
        });
    </script>
</asp:Content>
