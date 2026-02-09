<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FinanceTransHistory.aspx.cs" Inherits="csa.Admin.FinanceTransHistory" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
    <!-- datatables css -->
    <link href="<%=Page.ResolveUrl("~/assets/libs/datatables/datatables.min.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="container-fluid">
            <!-- page header -->
            <div class="row mb-3 pb-1">
                <div class="col-12">
                    <div class="d-flex align-items-lg-center flex-lg-row flex-column">
                        <div class="flex-grow-1">
                            <h4 class="fs-16 mb-1">Transaction History</h4>
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
                            <div class="col-md-3 col-sm-12 d-flex align-items-center"><h5 class="m-0 p-0 "><%--Transaction History List--%></h5></div>
                                <div class="col-md-9 col-sm-12 ">
                                    <div class="row justify-content-end gy-3">
                                        <div class="col-md-3 col-sm-12">
                                            <div class="input-group">

                                            <input type="text" class="form-control" id="txtSearch" placeholder="Search" />
                                            <button type="button" class="btn btn-tertiary btn-label ms-auto" id="btnSearch"><i class="ri-search-line label-icon align-middle fs-16 me-2"></i> Search </button>
                                            </div>
                                        </div>

                                        
                                    </div>
                                </div>
                            </div>
                         </div>

                            <table id="gvHistory" class="table dt-responsive nowrap align-middle" style="width: 100%">
                                <thead class="table-light text-muted">
                                    <tr>
                                        <th>TRANSACTION ID</th>
                                        <th>MEMBER</th>
                                        <th>TRANSACTION DATE</th>
                                        <th>TRANSACTION TYPE</th>
                                        <th>TRANSACTION AMOUNT</th>
                                        <th class="text-center">STATUS</th>
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

    <script type="text/javascript">
        $("form").submit(function (e) {
            e.preventDefault();
        });
    </script>

    <script type="text/javascript">
        var gvTable = {};

        function initHistoryGV() {
            gvTable = $("#gvHistory").DataTable({
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
                /*'scrollX': true,*/
                /*'scrollY': true,*/
                //'scrollXInner': true,
                //'scrollCollapse': true,
                'ajax': {
                    'url': '<%=Page.ResolveUrl("~/Withdrawal/GetWithdrawalHistoryGV")%>',
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
                    { 'data': 'WithdrawalId', 'type': 'int' },
                    { 'data': 'MemberName', 'type': 'string' },
                    { 'data': 'ResponseDate', 'type': 'date' },
                    { 'data': 'TransactionType', 'type': 'string' },
                    { 'data': 'Amount', 'type': 'num' },
                    { 'data': 'StatusId', 'type': 'int' }
                ],
                'order': [
                    [2, "desc"]
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
                        'targets': 0,
                        'searchable': true,
                        'orderable': true,
                        'width': 80
                    },
                    {
                        'targets': 1,
                        'searchable': true,
                        'orderable': true
                    },
                    {
                        'targets': 2,
                        'searchable': true,
                        'orderable': true,
                        'width': 100,
                        'className': 'dt-body-center',
                        'render': function (d, t, r, m) {
                            if (d != null) {
                                return appHelper.dateToFormat(d);
                            } else { return ''; }
                        }
                    },
                    {
                        'targets': 3,
                        'searchable': true,
                        'orderable': false,
                        'width': 90,
                        'className': 'dt-body-center',
                        //'render': function (d, t, r, m) {
                        //    debugger;
                        //    if (d != null) {
                        //        switch (d) {
                        //            case 405: return `<span>WITHDRAWAL</span>`;
                        //                break;
                        //        }
                        //    } else { return ''; }
                        //}
                    },
                    {
                        'targets': 4,
                        'searchable': true,
                        'orderable': true,
                        'className': 'dt-body-right',
                        'width': 100
                    },
                    {
                        'targets': 5,
                        'searchable': true,
                        'orderable': true,
                        'width': 100,
                        'className': 'dt-body-center',
                        'render': function (d, t, r, m) {
                            if (d != null) {
                                switch (d) {
                                    case 3: return `<span class="badge bg-success">PAID</span>`;
                                        break;
                                    case 4: return `<span class="badge bg-danger">CANCEL</span>`;
                                        break;
                                }
                            } else { return ''; }
                        }
                    }
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

        function withdrawEdit(args) {
            let id = args.closest('tr[id]').id;
            location.href = `<%=Page.ResolveUrl("~/WithdrawReqDtls")%>?id=${id}`;
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

        $(document).ready(function () {

            //gridView
            initHistoryGV();

            <%--//export
            initExportExcel();--%>

        });
    </script>
</asp:Content>
