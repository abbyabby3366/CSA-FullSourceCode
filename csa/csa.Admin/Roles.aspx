<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="csa.Admin.Roles" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
    <!-- datatables css -->
    <link href="<%=Page.ResolveUrl("~/assets/libs/datatables/datatables.min.css") %>" rel="stylesheet" type="text/css" />

    <!-- select2 -->
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2.min.css") %>" />
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2-bootstrap-5-theme.min.css") %>" />

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

        .select2-container--open {
            z-index: 9999999
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
                            <h4 class="fs-16 mb-1">Roles</h4>
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

            <!-- role info card -->
            <div class="row d-none">
                <div class="col-xl-3 col-md-6">
                    <div class="card card-height-100">
                        <div class="card-body">
                            <ul class="list-group list-group-flush border-dashed mb-0">
                                <li class="list-group-item d-flex align-items-center">
                                    <div class="d-flex row">
                                        <span class="circle_icon me-2"><i class="ri-user-settings-line icon"></i></span>
                                    </div>
                                    <div class="flex-grow-1 ms-3">
                                        <h6 class="fs-12 mb-1">ROLE NUMBER [1]</h6>
                                        <p class="text-muted mb-0">Total of 4 users</p>
                                    </div>
                                    <div class="flex-fill text-end">
                                        <a href="javascript:void(0);" class="btn btn-light btn-icon waves-effect waves-light layout-rightside-btn">
                                            <i class="ri-pencil-line"></i>
                                        </a>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-md-6">
                    <div class="card card-height-100">
                        <div class="card-body">
                            <ul class="list-group list-group-flush border-dashed mb-0">
                                <li class="list-group-item d-flex align-items-center">
                                    <div class="d-flex row">
                                        <span class="circle_icon me-2"><i class="ri-user-settings-line icon"></i></span>
                                    </div>
                                    <div class="flex-grow-1 ms-3">
                                        <h6 class="fs-12 mb-1">ROLE NUMBER [2]</h6>
                                        <p class="text-muted mb-0">Total of 4 users</p>
                                    </div>
                                    <div class="flex-fill text-end">
                                        <a href="javascript:void(0);" class="btn btn-light btn-icon waves-effect waves-light layout-rightside-btn">
                                            <i class="ri-pencil-line"></i>
                                        </a>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-md-6">
                    <div class="card card-height-100">
                        <div class="card-body">
                            <ul class="list-group list-group-flush border-dashed mb-0">
                                <li class="list-group-item d-flex align-items-center">
                                    <div class="d-flex row">
                                        <span class="circle_icon me-2"><i class="ri-user-settings-line icon"></i></span>
                                    </div>
                                    <div class="flex-grow-1 ms-3">
                                        <h6 class="fs-12 mb-1">ROLE NUMBER [3]</h6>
                                        <p class="text-muted mb-0">Total of 4 users</p>
                                    </div>
                                    <div class="flex-fill text-end">
                                        <a href="javascript:void(0);" class="btn btn-light btn-icon waves-effect waves-light layout-rightside-btn">
                                            <i class="ri-pencil-line"></i>
                                        </a>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-md-6">
                    <div class="card card-height-100">
                        <a href="javascript:void(0);" class="btn btn-primary btn-icon waves-effect waves-light w-100 h-100" data-bs-toggle="modal" data-bs-target="#addRoleModal">
                             <div class="d-flex">
                                <span class="my-auto mx-auto"><i class="ri-user-add-fill me-1"></i> ADD NEW ROLE</span>
                            </div>
                        </a>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                             <div class="row px-2 gy-2">
                            <div class="col-md-3 col-sm-12 d-flex align-items-center"><h5 class="m-0 p-0 "><%--All Admin Role Listing--%></h5></div>
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

                            <table id="gvRole" class="table dt-responsive nowrap align-middle" style="width: 100%">
                                <thead class="table-light text-muted">
                                    <tr>
                                        <th>FULL NAME</th>
                                        <th>IC</th>
                                        <th>CONTACT NO</th>
                                        <th>USER REFERRAL TYPE</th>
                                        <th>ROLE</th>
                                        <th class="text-center">ACTION</th>
                                    </tr>
                                </thead>
                            </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- modal -->
    <div id="addRoleModal" class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-title">
                    <div class="px-3 pt-4">
                        <h4>Add New Role</h4>
                    </div>
                </div>

                <div class="modal-body">
                    <div class="form">
                        <div class="row">
                            <div class="col-mx">
                                <div class="mb-3">
                                    <label for="txtRoleName_add" class="form-label">Role Name</label>
                                    <input type="text" class="form-control" id="txtRoleName_add" placeholder="Role Name">
                                </div>
                            </div>

                            <div class="col-mx">
                                <div class="d-flex justify-content-between align-items-center" style="height: 38px; background-color: #efefef;">
                                    <span class="fw-semibold ps-3">Administrator Access</span>
                                    <span class="fw-semibold pe-3">Enable / Disable</span>
                                </div>
                            </div>
                            
                            <div class="col-mx" data-simplebar style="max-height: 320px;">
                                <ul class="list-group list-group-flush border-groove" id="contentListRole">
                                    
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="hr" />

                <div class="modal-footer justify-content-center">
                    <button class="btn btn-soft-primary" id="btnSubmitRole" type="button"><i class="ri-send-plane-2-line me-1 align-middle"></i> Submit</button>
                    <button class="btn btn-soft-secondary" data-bs-dismiss="modal"><i class="ri-close-circle-line me-1 align-middle"></i> Cancel</button>
                </div>
            </div>
        </div>
    </div>

    <div id="editRoleModal" class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-title">
                    <div class="px-3 pt-4">
                        <h4>Edit Role</h4>
                    </div>
                </div>

                <div class="modal-body">
                    <div class="form">
                        <div class="row">
                            <div class="col-mx">
                                <div class="mb-3">
                                    <label for="ddlRoleName_edit" class="form-label">Select Role</label>
                                    <select class="form-control" id="ddlRoleName_edit"></select>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="hr" />

                <div class="modal-footer justify-content-center">
                    <button class="btn btn-soft-primary" id="btnSubmitChangeRole"><i class="ri-send-plane-2-line me-1 align-middle"></i> Submit</button>
                    <button class="btn btn-soft-secondary" data-bs-dismiss="modal"><i class="ri-close-circle-line me-1 align-middle"></i> Cancel</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent" runat="server">
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

        function initRoleGV() {
            gvTable = $("#gvRole").DataTable({
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
                    'url': '<%=Page.ResolveUrl("~/Role/GetAdminsGV")%>',
                    'type': "POST",
                    'data': function (e) {
                        let search = '';

                        let s = $('#txtSearch');

                        if (s.length > 0) { search = s.val(); }

                        return {
                            'start': e.start,
                            'length': e.length,
                            'order': JSON.stringify(e.order),
                            'search': search
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
                    { 'data': 'Name', 'type': 'string' },
                    { 'data': null, 'type': 'string' },
                    { 'data': null, 'type': 'string' },
                    { 'data': null, 'type': 'int' },
                    { 'data': 'RoleName', 'type': 'string' },
                    { 'data': null },
                ],
                'order': [
                    [0, "asc"]
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
                        'targets': 5,
                        'searchable': false,
                        'orderable': false,
                        'width': 160,
                        render: function (d, t, r, m) {
                            return `<div class="hstack gap-3 fs-13 justify-content-center">
                                               <a href="<%= Page.ResolveUrl("~/AdminDetails") %>?id=${d.AdminId}" class="text-warning">View</a>
                                               <a href="javascript:void(0);" class="text-success" onclick='changeRole(${d.AdminId},${d.RoleId})'>Edit Role</a>
                                               <a href="javascript:void(0);" class="text-danger">Suspend</a>
                                           </div>`
                        }
                    },
                    {
                        'targets': 0,
                        'searchable': true,
                        'orderable': true,
                        defaultContent: ''
                    },
                    {
                        'targets': 1,
                        'searchable': true,
                        'orderable': false,
                        'width': 100,
                        defaultContent: ''
                    },
                    {
                        'targets': 2,
                        'searchable': true,
                        'orderable': false,
                        'width': 100,
                        defaultContent: ''
                    },
                    {
                        'targets': 3,
                        'searchable': true,
                        'orderable': false,
                        'width': 60,
                        'className': 'dt-body-center',
                        defaultContent: ''
                        //'render': function (d, t, r, m) {
                        //    if (d != null) {
                        //        switch (d) {
                        //            case 2: return `<span>TYPE 2 (6%)</span>`;
                        //                break;
                        //            case 3: return `<span>TYPE 1 (6%)</span>`;
                        //                break;
                        //        }
                        //    } else { return ''; }
                        //}
                    },
                    {
                        'targets': 4,
                        'searchable': true,
                        'orderable': true,
                        'width': 100,
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

        $('#btnSubmitRole').click(async function (e) {
            $(this).prop('disabled', true);

            let accessList = []
            var roles = $('.item-role');

            $.each(roles, function (index, element) {
                var el = $(element)
                if (el.prop('checked')) {
                    accessList.push(el.val())
                }
            });
            const payload = {
                Name: $('#txtRoleName_add').val(),
                AccessIds: accessList
            }

            const res = await ApiHelper.postFormData(window.location.origin + '/role/create', payload)
            if (!res.data.Error) {
                dialogHelper.success('Role created')
                $('#addRoleModal').modal('hide')
                $('#ddlRoleName_edit').append(`<option value='${res.data.ObjVal.Value}'>${res.data.ObjVal.Text}</option>`)

                $('#txtRoleName_add').val('')
                $.each(roles, function (index, element) {
                    $(element).prop('checked',false)
                });

            } else {
                dialogHelper.error(res.data.Message)
            }

            $(this).prop('disabled', false);
        })

        var adminIdToChangeRole;
        function changeRole(adminId,roleId) {
            adminIdToChangeRole = adminId
            $('#ddlRoleName_edit').val(roleId)
            $('#ddlRoleName_edit').trigger('change')
            $('#editRoleModal').modal('show')
        }

        $('#btnSubmitChangeRole').click(async function (e) {
            $(this).prop('disabled', true);

            const payload = {
                AdminId: adminIdToChangeRole,
                RoleId: $('#ddlRoleName_edit').val()
            }

            const res = await ApiHelper.postFormData(window.location.origin + '/admin/changeRole', payload)
            if (!res.data.Error) {
                dialogHelper.success('Role changed')
                $('#editRoleModal').modal('hide')
                gvTable.ajax.reload();
            } else {
                dialogHelper.error(res.data.Message)
            }

            $(this).prop('disabled', false);
        })

        function prepare() {
            vm.AccessList.forEach(function (e) {
                $('#contentListRole').append(`<li class="list-group-item">
                                        <div class="d-flex justify-content-between">
                                            <label class="form-label">${e.Text}</label>
                                            <div class="form-check form-switch form-switch-md form-switch-secondary w-25" dir="ltr">
                                                <input class="form-check-input item-role" type="checkbox" data-id="${e.Value}" id="cb${e.Value}" value='${e.Value}'>
                                                <label class="form-check-label" for="cb${e.Value}"></label>
                                            </div>
                                        </div>
                                    </li>`)                
            })

            vm.RoleList.forEach(function (e) {
                $('#ddlRoleName_edit').append(`<option value='${e.Value}'>${e.Text}</option>`)
            })            
        }

        $(document).ready(function () {

            //gridView
            initRoleGV();

            <%--//export
            initExportExcel();--%>

            $('#ddlRoleName_edit').select2({ 'theme': 'bootstrap-5', 'minimumResultsForSearch': -1 });
            vm = JSON.parse($('#<%= hfModelView.ClientID %>').val())
            prepare()
        });       
    </script>
</asp:Content>
