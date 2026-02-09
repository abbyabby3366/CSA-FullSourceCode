<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmailCampaign.aspx.cs" Inherits="csa.Admin.EmailCampaign" %>

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
                            <h4 class="fs-16 mb-1">Email Campaign</h4>
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
                            <div class="col-md-3 col-sm-12 d-flex align-items-center"><h5 class="m-0 p-0 "><%--Email Campaign List--%></h5></div>
                                <div class="col-md-9 col-sm-12 ">
                                    <div class="row justify-content-end gy-3">
                                        <div class="col-md-3 col-sm-12">
                                            <div class="input-group">

                                            <input type="text" class="form-control" id="txtSearch" placeholder="Search" />
                                            <button type="button" class="btn btn-tertiary btn-label ms-auto" id="btnSearch"><i class="ri-search-line label-icon align-middle fs-16 me-2"></i> Search </button>
                                            </div>
                                        </div>

                                        <a href="<%=Page.ResolveUrl("~/AddNewCampaign") %>" class="btn btn-soft-primary waves-effect waves-light col-md-3 col-sm-12">Add New Campaign</a>
                                    </div>
                                </div>
                            </div>
                         </div>

                            <table id="gvCampaign" class="table dt-responsive nowrap align-middle" style="width: 100%">
                                <thead class="table-light text-muted">
                                    <tr>
                                        <th>NO</th>
                                        <th>NAME</th>
                                        <th>SUBJECT</th>
                                        <th>STATUS</th>
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

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent" runat="server"><!-- datatables js -->
    <script src="<%=Page.ResolveUrl("~/assets/libs/datatables/datatables.min.js") %>"></script>
    <%--<script src="<%=Page.ResolveUrl("~/assets/libs/datatables/DateTime-1.5.2/js/dataTables.dateTime.min.js") %>"></script>--%>

    <script type="text/javascript">
        $("form").submit(function (e) {
            e.preventDefault();
        });
    </script>

    <script type="text/javascript">
        var gvTable = {};

        function initCampaignGV() {
            gvTable = $("#gvCampaign").DataTable({
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
                    'url': '<%=Page.ResolveUrl("~/Campaign/GetGV")%>',
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
                    { 'data': 'Name', 'type': 'string' },
                    { 'data': 'Subject', 'type': 'string' },
                    { 'data': 'StatusId', 'type': 'int' },
                    { 'data': null },
                ],
                'order': [
                    [1, "asc"]
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
                        'targets': 4,
                        'searchable': false,
                        'orderable': false,
                        'width': 100,
                        'render': function (d, t, r, m) {
                            return `<div class="hstack gap-3 fs-13 justify-content-center">
                                               <a href="<%=Page.ResolveUrl("~/EmailCampaignDetails")%>?id=${d.CampaignId}" class="text-success">Edit</a>
                                               <a href='#' class="text-danger" onclick='onDelete(${d.CampaignId})'>Delete</a>
                                           </div>`
                        }
                    },
                    {
                        'targets': 0,
                        'searchable': false,
                        'orderable': false,
                        'width': 50,
                    },
                    {
                        'targets': 1,
                        'searchable': true,
                        'orderable': true,
                        'width': 250
                    },
                    {
                        'targets': 2,
                        'searchable': true,
                        'orderable': true
                    },
                    {
                        'targets': 3,
                        'searchable': true,
                        'orderable': true,
                        'width': 60,
                        'className': 'dt-body-center',
                        'render': function (d, t, r, m) {
                            if (d != null) {
                                switch (d) {
                                    case 1: return `<span>ENABLE</span>`;
                                        break;
                                    case 2: return `<span>DISABLE</span>`;
                                        break;
                                }
                            } else { return ''; }
                        }
                    }
                ]
            });

            gvTable.on('draw.dt', function () {
                let pgInfo = gvTable.data().page.info();

                gvTable.column(0, { page: 'current' }).nodes().each(function (cell, i) {
                    cell.innerHTML = i + 1 + pgInfo.start;
                });
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

        function onDelete(id) {
            async function doDelete() {
                try {
                    const res = await ApiHelper.post(window.location.origin + '/Campaign/Delete', { id: id })
                    if (!res.data.Error) {
                        gvTable.ajax.reload();
                    } else {
                        dialogHelper.error(res.data.Message)
                    }
                } catch (e) {
                    dialogHelper.error(e)
                }

            }
            dialogHelper.confirmation('are you sure to delete?', doDelete)
        }

        $(document).ready(function () {

            //gridView
            initCampaignGV();

            <%--//export
            initExportExcel();--%>

        });
    </script>
</asp:Content>
