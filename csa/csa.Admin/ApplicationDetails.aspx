<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApplicationDetails.aspx.cs" Inherits="csa.Admin.ApplicationDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .my-panel-info {
            background-color: #E9EBF0;
            padding: 10px 20px;
        }

            .my-panel-info > span {
                font-size: 16px;
            }

        .btn-tab {
            background-color: #FAFAFA;
            color: #8C9096;
        }

            .btn-tab:hover {
                background-color: #012d65;
                color: #ffffff;
            }

        .tab-active {
            background-color: #012d65;
            color: #ffffff;
        }

        .tab-done {
            color: #749EE0;
        }

        .bold {
            font-weight: 600;
        }

        .currentstatus {
            font-weight: 800;
        }

        .checkbox-control {
            margin-bottom: 5px;
        }

        .btn-text {
            padding: 0px !important;
        }

        .ri-delete-bin-2-line {
            color: red;
        }

            .ri-delete-bin-2-line:hover {
                background-color: lightpink;
            }

        .ri-download-line {
            color: darkblue;
            margin-right: 7px;
        }

            .ri-download-line:hover {
                background-color: whitesmoke;
            }

            .swal2-html {
            text-align: left; /* Align text to the left */
        }
    </style>
    <!-- select2 -->
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2.min.css") %>" />
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2-bootstrap-5-theme.min.css") %>" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css">
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
                                        <div class="col-xl-3 col-md-6 col-xs-12">
                                            <div class="row">
                                                <div class="col-5 bold">
                                                    Customer Full Name
                                                </div>
                                                <div class="col-7" id="contentCustomerFullName">
                                                    <%--<a href="MemberDetails.aspx?id=x">AZAHAR BIN MOHD</a>--%>
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
                                        <div class="col-xl-3 col-md-6 col-xs-12">
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
                                        <div class="col-xl-3 col-md-6 col-xs-12">
                                            <div class="row">
                                                <div class="col-5 bold">
                                                    Key In Date
                                                </div>
                                                <div class="col-7" id="contentKeyInDate">
                                                    <%--13 March 2024--%>
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
                                                <div class="col-5 bold mt-2">
                                                    Source
                                                </div>
                                                <div class="col-7 mt-2">
                                                    <select class="form-control" id="ddlSource">
                                                        <option value="1">Hero’s Individual</option>
                                                        <option value="2">Hero’s Former PFC</option>
                                                        <option value="3">Hero Seminar</option>
                                                        <option value="4">Roadshow</option>
                                                        <option value="5">Cold Call/Fresh</option>
                                                        <option value="6">Company Ads</option>
                                                        <option value="7">Agent/Member Individual</option>
                                                        <option value="8">Agent/Member Seminar</option>
                                                        <option value="9">YABAM</option>
                                                    </select>
                                                </div>
                                                <div class="col-5 bold mt-2">
                                                    Credit Status
                                                </div>
                                                <div class="col-7 mt-2">
                                                    <select class="form-control" id="ddlCreditStatus">
                                                        <option value="1">Rancang & Rezeki (RNR)</option>
                                                        <option value="2">Burst</option>
                                                        <option value="3">Payslip (PYSP)</option>
                                                        <option value="4">Review</option>
                                                        <option value="5">Pending</option>
                                                        <option value="6">Declined by Agency Manager (AM)</option>
                                                        <option value="7">Customer missing in action (MIA)</option>
                                                        <option value="8">Rezeki 2.0 (Direct)</option>
                                                        <option value="9">Rezeki 2.0 (Settlement)</option>
                                                        <option value="10">Single</option>
                                                    </select>
                                                </div>

                                                <div class="col-5 mt-2 bold">
                                                    Score/Class
                                                </div>
                                                <div class="col-7 mt-2">
                                                    <textarea class="form-control" style="min-height: 50px" id="txtScoreClass"></textarea>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xl-3 col-md-6 col-xs-12">
                                            <div class="row">



                                                <div class="col-5 bold">
                                                    Customer Status
                                                </div>
                                                <div class="col-7">
                                                    <select class="form-control me-4 source-customer-status" id="ddlCustomerStatus"></select>
                                                </div>
                                                <%--  show only when customer status is Burst--%>
                                                <div class="col-5 mt-2 bold content-burst-reason d-none">
                                                    Burst Reason
                                                </div>
                                                <div class="col-7 mt-2 content-burst-reason d-none">
                                                    <select class="form-control" id="ddlBurstReason">
                                                        <option value="1">Others</option>
                                                        <option value="2">High commitment</option>
                                                        <option value="3">Low Salary/ low income</option>
                                                        <option value="4">Sabah & Sarawak/ Limited bank</option>
                                                        <option value="5">Crime financial record/ legal action</option>
                                                        <option value="6">Bad payment record/ bad financial status record</option>
                                                        <option value="7">Short service year</option>
                                                        <option value="8">Contract worker</option>
                                                        <option value="9">High settlement/ Low financing margin</option>
                                                        <option value="10">Low DSR </option>
                                                        <option value="11">Low scoring (CCRIS/CTOS)</option>
                                                        <option value="12">Missing in Action/ customers not interested</option>
                                                        <option value="13">Self-employed/entitled company</option>
                                                        <option value="14">Uniform officer</option>
                                                        <option value="15">NGO</option>
                                                        <option value="16">Redundant Records</option>
                                                        <option value="17">Low fees</option>
                                                        <option value="18">Too Young</option>
                                                        <option value="19">Too old/ Max age</option>
                                                        <option value="20">No professional certificate</option>
                                                        <option value="21">No deduction on EPF/SOCSO/ Any statutory body/ Institution</option>
                                                        <option value="22">High exposure/ high risk</option>
                                                        <option value="23">Low net income (After R&R)</option>
                                                    </select>

                                                </div>
                                                <div class="col-5 mt-2 bold">
                                                    Credit Remark
                                                </div>
                                                <div class="col-7 mt-2">
                                                    <textarea class="form-control" style="min-height: 75px" id="txtCreditRemark"></textarea>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                </div>

                                <div class="card-footer">
                                    <div class="row">
                                        <%-- hide feature --%>
                                        <div class="col-md-6 col-xs-12 "> 
                                            <div class="hstack gap-2 d-none">
                                                <b>Current Status: </b>
                                                <select class="form-control me-4" style="width: fit-content !important" id="ddlCurrentStatus">
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
                                                    <%--<option value="11">Completed</option>--%>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-xs-12">
                                            <%--tampilin kalau manual ganti status pake dropdown ini. Kalau ada yg click Next, ini dihapus dari db dan dihide--%>
                                            <span class=" text-success" id="changestatusDesc"></span>
                                        </div>
                                        <div class="col-md-2 col-xs-12 mt-1" style="text-align: end">
                                            <button type="button" class="btn btn-primary" id="btnSaveInfo"><i class="me-1 ri-save-line"></i>Save</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-12">
                    <!-- tab content -->
                    <div class="tab-content">
                        <div class="tab-pane active" role="tabpanel">
                            <div class="card" id="contentApplicationByStatus">
                                <div class="card-header">
                                    <div class="btn-group" id="tabLinkApplication" role="group" aria-label="Basic example">
                                        <%--Mark current status with css class current status, if completed then no currentstatus--%>
                                        <button type="button" class="btn btn-tab tab-active currentstatus" target="tabPreChecking">Pre-checking</button>
                                        <button type="button" class="btn btn-tab" target="tabProposalPreparation">Proposal Preparation</button>
                                        <button type="button" class="btn btn-tab" target="">Proposal Presentation</button>
                                        <button type="button" class="btn btn-tab">Pre-Signing</button>
                                        <button type="button" class="btn btn-tab">Pending Zoom & Acceptance</button>
                                        <button type="button" class="btn btn-tab">Settlement</button>
                                        <button type="button" class="btn btn-tab">CCRIS Cleaning/Monitoring</button>
                                        <button type="button" class="btn btn-tab">Queue For Reloan</button>
                                        <button type="button" class="btn btn-tab">Reloan Submission</button>
                                        <button type="button" class="btn btn-tab">Collection</button>

                                    </div>
                                </div>

                                <div id="tabPreChecking" class="d-none">
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Pre-Checking Stage - Use RAMCI to gather commitment data and check applicant's eligibility</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Eligibility</label>
                                                    <select class="form-control source-customer-status" id="ddleligibility"></select>
                                                    <span class="text-success" id="eligibilityDesc"></span>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Payslip</label>
                                                    <span class="float-end d-none" id="payslip-precheckingAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="PayslipFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldpayslip-prechecking">Upload File</label>
                                                        <input class="form-control" id="txtpayslip-prechecking" readonly onclick="openInputFile('upldpayslip-prechecking')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldpayslip-prechecking" onchange="onChangeUpload(event,'txtpayslip-prechecking')">
                                                    <span class=" text-success" id="payslip-precheckingDesc"></span>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">RAMCI Report</label>
                                                    <span class="float-end d-none" id="ramciAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="RAMCIReportFileId" id="btnRamciDelete"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldramci">Upload File</label>
                                                        <input class="form-control" id="txtramci" readonly onclick="openInputFile('upldramci')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldramci" onchange="onChangeUpload(event,'txtramci')">
                                                    <span class=" text-success" id="ramciDesc"></span>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">CCRIS Document</label>

                                                    <span class="float-end d-none" id="ccrisAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="CCRISDocumentFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                     <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldccris">Upload File</label>
                                                        <input class="form-control" id="txtccris" readonly onclick="openInputFile('upldccris')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldccris" onchange="onChangeUpload(event,'txtccris')">
                                                    <span class="text-success" id="ccrisDesc"></span>
                                                </div>
                                            </div>                                                                                        
                                        </div>
                                    </div>

                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Profile Screening</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Legal Suits</label>
                                                    <input type="checkbox" class="checkbox-control" id="cbLegalSuits"/>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Bankruptcy</label>
                                                    <input type="checkbox" class="checkbox-control" id="cbBankruptcy" />
                                                    
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Special Attention Account</label>
                                                    <input type="checkbox" class="checkbox-control" id="cbSpecialAttentionAccount"/>
                                                    
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Bad Payment Record</label>
                                                    <input type="checkbox" class="checkbox-control" id="cbBadPaymentRecord"/>
                                                    
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Additional Documents</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row" id="contentAdditionalDocumentPreChecking">
                                            
                                        </div>
                                        <button class="col-12 btn btn-primary btn-add-additional-document" data-status="1" type="button" id="btnAddAdditionalPrechecking">+ Add New Row</button>
                                    </div>

                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Remark</h5>
                                    </div>
                                    <div class="card-body">
                                        <textarea class="form-control" style="height:100px" id="txtApplicationRemarkPrechecking"></textarea>
                                    </div>
                                    <div class="card-footer">
                                        <div class="col-lg-12">
                                            <div class="row">
                                                <div class="col-md-6 col-xs-12 mb-2">
                                                    <%--show only when rejected at this status, rename reject button to Cancel Reject--%>
                                                    <%--Reject button only show at Current status--%>
                                                    <div class="content-application-rejected">
                                                        <b>Application Rejected at DATE by ADMIN NAME</b><br />
                                                        "Rejection Reason"
                                                    </div>

                                                </div>
                                                <div class="col-md-6 col-xs-12 mb-2" style="text-align: end" data-status="1">
                                                    <%--pas reject di click, keluar popup dulu, suruh isi Rejection Reason, button Reject & Cancel--%>
                                                    <button type="button" id="btnRejectPreChecking" class="btn btn-danger me-2 btn-application-reject"><i class="me-1 ri-close-circle-line"></i>Reject</button>
                                                    <button type="button" class="btn btn-danger me-2 btn-application-cancel-reject"><i class="me-1 ri-close-circle-line"></i>Cancel Reject</button>
                                                    <button type="button" id="btnSavePreChecking" class="btn btn-primary me-1 btn-application-save"><i class="me-1 ri-save-line"></i>Save Changes</button>
                                                    <%--next button cuma muncul kalo current statusnya ini--%>
                                                    <button type="button" class="btn btn-info btn-application-next" id="btnNextPreChecking"><i class="me-1 ri-check-line"></i>Next</button>

                                                    <%--Validate move to next status for prechecking:
                                                    - RAMCI Report file must exist
                                                    - CCRIS Document must exist
                                                    - Eligiblity must be set to Yes
                                                    - All of the 4 checkboxes must be unchecked (Validation message: "None of these must be checked: Legal Suits, Bankruptcy, Special Attention, & Bad Payment Record")
                                                    below validation error message add: Do you wish to proceed? Yes or No
                                                    jadi ini validasi message doank, user admin masih bisa move ke next status, kalo No, diem di status ini, kalo Yes, maju ke tab berikutnya, Current Status ganti ke status berikutnya--%>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>

                                <div id="tabProposalPreparation" class="d-none">
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Proposal Preparation Stage - Prepare Proposal (key in customer details)</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Applicant's Proposal</label>
                                                    <span class="float-end d-none" id="proposalAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="ProposalFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldproposal">Upload File</label>
                                                        <input class="form-control" id="txtproposal" readonly onclick="openInputFile('upldproposal')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldproposal" onchange="onChangeUpload(event,'txtproposal')">
                                                    <span class="text-success" id="proposalDesc"></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Proposal Details</h5>
                                    </div>
                                    <div class="my-panel-info"><span>Salary</span></div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Gross (RM)</label>
                                                    <input class="form-control decimal-input" id="txtSalaryGross">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Deduction (RM)</label>
                                                    <input class="form-control decimal-input" id="txtSalaryDeduction">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Net Income (RM)</label>
                                                    <input class="form-control decimal-input" id="txtNetIncome">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="my-panel-info"><span>PRIOR DSR (BASIS DSR-REMAIN)</span></div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">B1</label>
                                                    <input class="form-control decimal-input" id="txtB1">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">B2</label>
                                                    <input class="form-control decimal-input" id="txtB2">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">B3</label>
                                                    <input class="form-control decimal-input" id="txtB3">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">B4</label>
                                                    <input class="form-control decimal-input" id="txtB4">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Average DSR</label>
                                                    <input class="form-control decimal-input" id="txtBAverage">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="my-panel-info"><span>COMMITMENT</span></div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">OUTSTD</label>
                                                    <input class="form-control decimal-input" id="txtCommitmentOutstanding">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">INSTALMENT</label>
                                                    <input class="form-control decimal-input" id="txtCommitmentInstallment">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="my-panel-info"><span>OTHERS</span></div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">NET BALANCE</label>
                                                    <input class="form-control decimal-input" id="txtOtherNetBalance">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">BPA</label>
                                                    <input class="form-control decimal-input" id="txtBPA">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">COMPARISON DSR</label>
                                                    <input class="form-control decimal-input" id="txtOtherComparionDSR">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">% COMMITMENT</label>
                                                    <input class="form-control decimal-input-pct" id="txtOtherComparisonDSRPctCommitment">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">% REFRESH</label>
                                                    <input class="form-control decimal-input-pct" id="txtOtherPctRefresh">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Proposed Refresh</label>
                                                    <input class="form-control decimal-input" id="txtOtherProposedRefresh">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Composition DSR</label>
                                                    <input class="form-control decimal-input" id="txtOtherCompositionDSR">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">% COMMITMENT</label>
                                                    <input class="form-control decimal-input-pct" id="txtOtherCompositionDSRPctCommitment">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="my-panel-info"><span>REFRESH</span></div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">TOTAL REFRESH</label>
                                                    <input class="form-control decimal-input" id="txtRefreshTotal">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">REMAIN COMMITMENT</label>
                                                    <input class="form-control decimal-input" id="txtRefreshRemainCommitment">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="my-panel-info"><span>RELOAN</span></div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">TOTAL RELOAN</label>
                                                    <input class="form-control decimal-input" id="txtReloanTotal">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">"MONTHLY INSTALLMENT"</label>
                                                    <input class="form-control decimal-input" id="txtReloanMonthy">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">BERSIH</label>
                                                    <input class="form-control decimal-input" id="txtReloanBersih">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">BELANJA (2*MONTHLY INSTALLMENT)</label>
                                                    <input class="form-control decimal-input" id="txtReloanBelanja">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">"DEPOSIT 0.65*(NET RELOAN - 1.4*SETTLEMENT)</label>
                                                    <input class="form-control decimal-input" id="txtReloanDeposit">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">4% DANA BANTUAN FROM CSA</label>
                                                    <input class="form-control decimal-input" id="txtReloanDanaBantuan">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">SERVICE FEE</label>
                                                    <input class="form-control decimal-input" id="txtReloanServiceFee">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">% SERVICE FEE</label>
                                                    <input class="form-control decimal-input" id="txtReloanServiceFeePct">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">"INCOME AFTER RNR"</label>
                                                    <input class="form-control decimal-input" id="txtReloanIncomeAfterRNR">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">"DIFFERENCE (>RM 3000)"</label>
                                                    <input class="form-control decimal-input" id="txtReloanDifference">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="my-panel-info"><span>MODEL</span></div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">BACKGROUND SCREENING</label>
                                                    <select class="form-control" id="ddlModelBackgroundScreening">
                                                        <option value="1">Pass Step 1</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">COMPOSITION DSR</label>
                                                    <select class="form-control" id="ddlModelCompositionDSR">
                                                        <option value="1">DSR Passed</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">COMMITMENT</label>
                                                    <select class="form-control" id="ddlModelCommitment">
                                                        <option value="1">Low</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">SETTLEMENT <50%</label>
                                                    <select class="form-control" id="ddlModelSettlement">
                                                        <option value="1">Low</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">SERVICE FEE</label>
                                                    <select class="form-control" id="ddlModelServiceFee">
                                                        <option value="1">High</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">NET INCOME AFTER RNR</label>
                                                    <select class="form-control" id="ddlModelNetIncomeAfterRNR">
                                                        <option value="1">High</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">STATUS</label>
                                                    <select class="form-control" id="ddlModelStatus">
                                                        <option value="1">Approved</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">"STATUS (PROPOSAL)"</label>
                                                    <select class="form-control" id="ddlModelStatusProposal">
                                                        <option value="1">Approved</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">CHECK</label>
                                                    <select class="form-control" id="ddlModelCheck">
                                                        <option value="1">True</option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Reviewed by</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                           <%-- <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">POSITION</label>
                                                    <select class="form-control">
                                                        <option selected>CREDIT TEAM</option>
                                                    </select>
                                                </div>
                                            </div>--%>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">ADMIN NAME</label>
                                                    <select class="form-control" id="ddlReviewAdmin">
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">STATUS</label>
                                                    <select class="form-control" id="ddlReviewStatus">
                                                        <option value="1">IN PROGRESS</option>
                                                        <option value="2">APPROVED</option>
                                                        <option value="3">REJECTED</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">DATE OF REVIEW</label>
                                                    <input class="form-control date-input" id="txtReviewDate"/>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <div class="mb-3">
                                                    <label class="form-label">COMMENT</label>
                                                    <textarea class="form-control" id="txtReviewComment"></textarea>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Approved by</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                         <%--   <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">POSITION</label>
                                                    <select class="form-control">
                                                        <option selected>OFFICER</option>
                                                        <option>MANAGER</option>
                                                        <option>CREDIT DIRECTOR</option>
                                                    </select>
                                                </div>
                                            </div>--%>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">ADMIN NAME</label>
                                                    <select class="form-control" id="ddlApproveAdmin">
                                                    </select>
                                                </div>
                                            </div>


                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">STATUS</label>
                                                    <select class="form-control" id="ddlApproveStatus">
                                                        <option value="1">IN PROGRESS</option>
                                                        <option value="2">APPROVED SINGLE</option>
                                                        <option value="3">APPROVED RNR</option>
                                                        <option value="4">REJECTED</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">DATE OF APPROVAL</label>
                                                    <input class="form-control date-input" id="txtApproveDate"/>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <div class="mb-3">
                                                    <label class="form-label">COMMENT</label>
                                                    <textarea class="form-control" id="txtApproveComment"></textarea>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Verified by</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">ADMIN NAME</label>
                                                    <select class="form-control" id="ddlVerifiedAdmin">
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">STATUS</label>
                                                    <select class="form-control" id="ddlVerifiedStatus">
                                                        <option value="1">IN PROGRESS</option>
                                                        <option value="2">APPROVED</option>
                                                        <option value="3">REJECTED</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">DATE OF VERIFIED</label>
                                                    <input class="form-control date-input" id="txtVerifiedDate"/>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <div class="mb-3">
                                                    <label class="form-label">COMMENT</label>
                                                    <textarea class="form-control" id="txtVerifiedComment"></textarea>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Credit Remark</h5>
                                    </div>
                                    <div class="card-body">
                                        <textarea class="form-control" style="height:100px" id="txtCreditRemarkProposalPreparation"></textarea>
                                    </div>
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Remark</h5>
                                    </div>
                                    <div class="card-body">
                                        <textarea class="form-control" style="height:100px" id="txtApplicationRemarkPreparation"></textarea>
                                    </div>
                                    <div class="card-footer">
                                        <div class="col-lg-12">
                                            <div class="row">
                                                <div class="col-md-6 col-xs-12 mb-2">
                                                    <%--show only when rejected at this status, rename reject button to Cancel Reject--%>
                                                    <%--Reject button only show at Current status--%>
                                                    <div class="content-application-rejected">
                                                        <b>Application Rejected at DATE by ADMIN NAME</b><br />
                                                        "Rejection Reason"
                                                    </div>

                                                </div>
                                                <div class="col-md-6 col-xs-12 mb-2" style="text-align: end" data-status="2">
                                                    <%--pas reject di click, keluar popup dulu, suruh isi Rejection Reason, button Reject & Cancel--%>
                                                    <button type="button" id="btnRejectProposalPreparation" class="btn btn-danger me-2 btn-application-reject"><i class="me-1 ri-close-circle-line"></i>Reject</button>
                                                    <button type="button" class="btn btn-danger me-2 btn-application-cancel-reject"><i class="me-1 ri-close-circle-line"></i>Cancel Reject</button>
                                                    <button type="button" id="btnSaveProposalPreparation" class="btn btn-primary me-1 btn-application-save"><i class="me-1 ri-save-line"></i>Save Changes</button>
                                                    <%--next button cuma muncul kalo current statusnya ini--%>
                                                    <button type="button" class="btn btn-info btn-application-next" id="btnNextPreparation"><i class="me-1 ri-check-line"></i>Next</button>

                                                    <%--Validate move to next status for proposal preparation:
                                                    - Application Proposal file must exist
                                                    - field2 yg laen not required di phase 1 (phase 2 nanti butuh buat proposal generation)
                                                   
                                                    below validation error message add: Do you wish to proceed? Yes or No
                                                    jadi ini validasi message doank, user admin masih bisa move ke next status, kalo No, diem di status ini, kalo Yes, maju ke tab berikutnya, Current Status ganti ke status berikutnya--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="tabProposalPresentation" class="d-none">
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Proposal Presentation Stage - Present proposal, review and proceed to next stage.</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Proposal Status</label>
                                                    <select class="form-control" id="ddlproposalstatus">
                                                        <option value="1">In Process</option>
                                                        <option value="2">Presented</option>
                                                        <option value="3">Proposal Accepted</option>
                                                        <option value="4">Proposal Rejected</option>
                                                    </select>
                                                    <span class="text-success" id="proposalstatusDesc"></span>
                                                </div>
                                            </div>
                                            <%--   <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Meeting Date Time</label>
                                                    <div class="row">
                                                        <div class="col-md-8">
                                                            <input class="form-control" type="date" />
                                                        </div>
                                                        <div class="col-md-4">
                                                            <input class="form-control" type="time" />
                                                        </div>
                                                    </div>


                                                </div>
                                            </div>--%>
                                            <%--  <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Meeting Status</label>
                                                    <select class="form-control">
                                                        <option selected>In Progress</option>
                                                        <option>Meeting Completed</option>
                                                        <option>Meeting Rejected</option>
                                                    </select>
                                                      <span class="text-success">*Selected by [Kevin] on 12/06/2024 12:03</span>
                                                </div>
                                            </div>--%>

                                            <%-- <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Customer Payment</label>
                                                    <div class="row">
                                                    <div class="col-md-2"><input type="checkbox" class="checkbox-control" /></div>
                                                    <div class="col-md-10"><input class="form-control" style="width:100px" type="number"></div>
                                                        </div>
                                                      <span class="text-success">*Checked by [Kevin] on 12/06/2024 12:03</span>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Referral Commission</label>
                                                    <input type="checkbox" class="checkbox-control" />
                                                      <span class="text-success">*Unchecked by [Kevin] on 12/06/2024 12:03</span>
                                                </div>
                                            </div>--%>
                                        </div>
                                    </div>

                                   
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Additional Documents</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row" id="contentAdditionalDocumentPresentation">                                            

                                        </div>
                                        <button class="col-12 btn btn-primary btn-add-additional-document" data-status="3" type="button">+ Add New Row</button>
                                    </div>
                                     <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Remark</h5>
                                    </div>
                                    <div class="card-body">
                                        <textarea class="form-control" style="height:100px" id="txtApplicationRemarkPresentation"></textarea>
                                    </div>

                                    <div class="card-footer">
                                        <div class="col-lg-12">
                                            <div class="row">
                                                <div class="col-md-6 col-xs-12 mb-2">
                                                    <%--show only when rejected at this status, rename reject button to Cancel Reject--%>
                                                    <%--Reject button only show at Current status--%>
                                                    <div class="content-application-rejected">
                                                        <b>Application Rejected at DATE by ADMIN NAME</b><br />
                                                        "Rejection Reason"
                                                    </div>

                                                </div>
                                                <div class="col-md-6 col-xs-12 mb-2" style="text-align: end" data-status="3">
                                                    <%--pas reject di click, keluar popup dulu, suruh isi Rejection Reason, button Reject & Cancel--%>
                                                    <button type="button" id="btnRejectProposalPresentation" class="btn btn-danger me-2 btn-application-reject"><i class="me-1 ri-close-circle-line"></i>Reject</button>
                                                    <button type="button" class="btn btn-danger me-2 btn-application-cancel-reject"><i class="me-1 ri-close-circle-line"></i>Cancel Reject</button>
                                                    <button type="button" id="btnSaveProposalPresentation" class="btn btn-primary me-1 btn-application-save"><i class="me-1 ri-save-line"></i>Save Changes</button>
                                                    <%--next button cuma muncul kalo current statusnya ini--%>
                                                    <button type="button" class="btn btn-info btn-application-next" id="btnNextProposalPresentation"><i class="me-1 ri-check-line"></i>Next</button>

                                                    <%--Validate move to next status:
                                                    - Proposal status must be Accepted
                                                   
                                                    bawah validation error message tambah: Do you wish to proceed? Yes or No
                                                    jadi ini validasi message doank, user admin masih bisa move ke next status, kalo No, diem di status ini, kalo Yes, maju ke tab berikutnya, Current Status ganti ke status berikutnya--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="tabPreSigning" class="d-none">
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Pre-Signing Stage - Prepare latest proposal, surat akuan, comprehensive form for applicant.</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Have you send the latest proposal to applicant?</label>
                                                    <select class="form-control" id="ddlproposalsend">
                                                        <option value="0">No</option>
                                                        <option value="1">Yes</option>
                                                    </select>
                                                    <span class="text-success" id="proposalsendDesc"></span>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Have you send Surat Akuan to applicant?</label>
                                                    <select class="form-control" id="ddlsuratakuan">
                                                        <option value="0">No</option>
                                                        <option value="1">Yes</option>
                                                    </select>
                                                    <span class="text-success" id="suratakuanDesc"></span>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Have you send the Comprehensive Form to applicant?</label>
                                                    <select class="form-control" id="ddlcomprehensive">
                                                        <option value="0">No</option>
                                                        <option value="1">Yes</option>
                                                    </select>
                                                    <span class="text-success" id="comprehensiveDesc"></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                   

                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Additional Documents</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row" id="contentAdditionalDocumentPreSigning">
                                            
                                        </div>
                                        <button class="col-12 btn btn-primary btn-add-additional-document" data-status="4" type="button">+ Add New Row</button>
                                    </div>
                                     <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Remark</h5>
                                    </div>
                                    <div class="card-body">
                                        <textarea class="form-control" style="height:100px" id="txtApplicationRemarkPresign"></textarea>
                                    </div>
                                    <div class="card-footer">
                                        <div class="col-lg-12">
                                            <div class="row">
                                                <div class="col-md-6 col-xs-12 mb-2">
                                                    <%--show only when rejected at this status, rename reject button to Cancel Reject--%>
                                                    <%--Reject button only show at Current status--%>
                                                    <div class="content-application-rejected">
                                                        <b>Application Rejected at DATE by ADMIN NAME</b><br />
                                                        "Rejection Reason"
                                                    </div>

                                                </div>
                                                <div class="col-md-6 col-xs-12 mb-2" style="text-align: end" data-status="4">
                                                    <%--pas reject di click, keluar popup dulu, suruh isi Rejection Reason, button Reject & Cancel--%>
                                                    <button type="button" id="btnRejectPresigning" class="btn btn-danger me-2 btn-application-reject"><i class="me-1 ri-close-circle-line"></i>Reject</button>
                                                    <button type="button" class="btn btn-danger me-2 btn-application-cancel-reject"><i class="me-1 ri-close-circle-line"></i>Cancel Reject</button>
                                                    <button type="button" id="btnSavePresigning" class="btn btn-primary me-1 btn-application-save"><i class="me-1 ri-save-line"></i>Save Changes</button>
                                                    <%--next button cuma muncul kalo current statusnya ini--%>
                                                    <button type="button" class="btn btn-info btn-application-next" id="btnNextPresigning"><i class="me-1 ri-check-line"></i>Next</button>

                                                    <%--Validate move to next status for proposal preparation:
                                                    - All 3 options must be Yes
                                                   
                                                    below validation error message add: Do you wish to proceed? Yes or No
                                                    jadi ini validasi message doank, user admin masih bisa move ke next status, kalo No, diem di status ini, kalo Yes, maju ke tab berikutnya, Current Status ganti ke status berikutnya--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>



                                <div id="tabPendingZoomAcceptance" class="d-none">
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Pending Zoom & Acceptance Stage - Review all documents before generate settlement invoice.</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Applicant's Payslip</label>
                                                    <span class="float-end d-none" id="payslipAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="PayslipFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldpayslip">Upload File</label>
                                                        <input class="form-control" id="txtpayslip" readonly onclick="openInputFile('upldpayslip')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldpayslip" onchange="onChangeUpload(event,'txtpayslip')"/>
                                                    <span class="text-success" id="payslipDesc"></span>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Applicant's RAMCI</label>
                                                    <span class="float-end d-none" id="ramci2Action">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="RAMCIFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldramci">Upload File</label>
                                                        <input class="form-control" id="txtramci2" readonly onclick="openInputFile('upldramci2')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldramci2" onchange="onChangeUpload(event,'txtramci2')">
                                                    <span class=" text-success" id="ramci2Desc"></span>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Applicant's CTOS</label>
                                                    <span class="float-end d-none" id="ctosAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="CTOSFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldramci">Upload File</label>
                                                        <input class="form-control" id="txtctos" readonly onclick="openInputFile('upldctos')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldctos" onchange="onChangeUpload(event,'txtctos')">
                                                    <span class=" text-success" id="ctosDesc"></span>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Upload redemption letter</label>
                                                     <span class="float-end d-none" id="redemptionletterAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                         <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="RedemptionLetterFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldramci">Upload File</label>
                                                        <input class="form-control" id="txtredemptionletter" readonly onclick="openInputFile('upldredemptionletter')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldredemptionletter" onchange="onChangeUpload(event,'txtredemptionletter')">
                                                    <span class=" text-success" id="redemptionletterDesc"></span>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Applicant's Address</label>
                                                    <input class="form-control" id="txtApplicantAddress" disabled/>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Bankruptcy Status</label>
                                                    <select class="form-select" id="ddlBankruptcyStatusZoom">
                                                        <option value='1'>Yes</option>
                                                        <option value='0'>No</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Legal Case</label>
                                                    <select class="form-select" id="ddlLegalCaseZoom">
                                                        <option value='1'>Yes</option>
                                                        <option value='0'>No</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Health Credit Score</label>
                                                    <select class="form-select" id="ddlHealthCreditScoreZoom">
                                                        <option value='10'>10</option>
                                                        <option value='9'>9</option>
                                                        <option value='8'>8</option>
                                                        <option value='7'>7</option>
                                                        <option value='6'>6</option>
                                                        <option value='5'>5</option>
                                                        <option value='4'>4</option>
                                                        <option value='3'>3</option>
                                                        <option value='2'>2</option>
                                                        <option value='1'>1</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Commitments</label>
                                                    <input class="form-control decimal-input" id="txtCommitments"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    

                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Additional Documents</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row" id="contentAdditionalDocumentPendingZoomAndAtteptance">
                                            
                                        </div>
                                        <button class="col-12 btn btn-primary btn-add-additional-document" data-status="5" type="button">+ Add New Row</button>
                                    </div>
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Remark</h5>
                                    </div>
                                    <div class="card-body">
                                        <textarea class="form-control" style="height:100px" id="txtApplicationRemarkPendingZoomAndAtteptance"></textarea>
                                    </div>
                                    <div class="card-footer">
                                        <div class="row">
                                            <div class="col-md-6 col-xs-12 mb-2">
                                                <%--show only when rejected at this status, rename reject button to Cancel Reject--%>
                                                <%--Reject button only show at Current status--%>
                                                <div class="content-application-rejected">
                                                    <b>Application Rejected at DATE by ADMIN NAME</b><br />
                                                    "Rejection Reason"
                                                </div>

                                            </div>
                                            <div class="col-md-6 col-xs-12 mb-2" style="text-align: end" data-status="5">
                                                <%--pas reject di click, keluar popup dulu, suruh isi Rejection Reason, button Reject & Cancel--%>
                                                <button type="button" id="btnRejectZoom" class="btn btn-danger me-2 btn-application-reject"><i class="me-1 ri-close-circle-line"></i>Reject</button>
                                                <button type="button" class="btn btn-danger me-2 btn-application-cancel-reject"><i class="me-1 ri-close-circle-line"></i>Cancel Reject</button>
                                                <button type="button" id="btnSaveZoom" class="btn btn-primary me-1 btn-application-save"><i class="me-1 ri-save-line"></i>Save Changes</button>
                                                <%--next button cuma muncul kalo current statusnya ini--%>
                                                <button type="button" class="btn btn-info btn-application-next" id="btnNextZoom"><i class="me-1 ri-check-line"></i>Next</button>

                                                <%--Validate move to next status for proposal preparation:
                                                    - Payslip file must exist
                                                    - RAMCI file must exist
                                                    - CTOS file must exist
                                                    - Redemption Letter file must exist
                                                   
                                                    below validation error message add: Do you wish to proceed? Yes or No
                                                    jadi ini validasi message doank, user admin masih bisa move ke next status, kalo No, diem di status ini, kalo Yes, maju ke tab berikutnya, Current Status ganti ke status berikutnya--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="tabSettlement" class="d-none">
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Settlement Stage - Upload settlement receipts into platform.</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <%--  <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Settlement Receipt</label>
                                                    <select class="form-control">
                                                        <option selected>No</option>
                                                        <option>Yes</option>
                                                    </select>
                                                    <span class="text-success">*Uploaded by [Kevin] on 12/06/2024 12:03</span>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Have you received payment from applicant?</label>
                                                    <select class="form-control">
                                                        <option selected>No</option>
                                                        <option>Yes</option>
                                                    </select>
                                                </div>
                                            </div>--%>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Applicant Payment Receipt</label>
                                                    <span class="float-end d-none" id="settlementAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="PaymentReceiptFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldsettlement">Upload File</label>
                                                        <input class="form-control" id="txtsettlement" readonly onclick="openInputFile('upldsettlement')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldsettlement" onchange="onChangeUpload(event,'txtsettlement')"/>
                                                    <span class="text-success" id="settlementDesc"></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Settlement Details</h5>
                                    </div>
                                    <div class="card-body" style="overflow-x:auto;overflow-y:hidden">
                                        <table id="tableSettlementDetails" class="table table-bordered dt-responsive nowrap table-striped align-middle" style="width: 100%">
                                            <thead>
                                                <tr>
                                                    <th class="text-center">ACTION</th>
                                                    <th class="text-center">TOTAL SETTLEMENT AMOUNT (RM)</th>
                                                    <th class="text-center">PERCENT OF TOTAL SETTLEMENT AMOUNT</th>
                                                    <th class="text-center">TOTAL AMOUNT IN PERCENT</th>
                                                    <th class="text-center">PAYMENT DATE</th>
                                                    <th class="text-center">BANK</th>
                                                    <th class="text-center">BANK ACCOUNT NUMBER</th>
                                                    <th class="text-center">DUE DATE</th>
                                                    <th class="text-center">FACILITIES</th>
                                                    <th class="text-center">AMOUNT FACILITES</th>
                                                    <th class="text-center">SETTLEMENT STATUS</th>
                                                    <th class="text-center">REMARK/UPDATES</th>
                                                    <th class="text-center">CREATED BY</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                
                                            </tbody>
                                        </table>
                                    </div>

                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Add Settlement Details</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Settlement Amount (RM)</label>
                                                    <input class="form-control decimal-input" id="txtSettlementAmount">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Percent of total settlement amount (%)</label>
                                                    <input class="form-control decimal-input-pct" id="txtSettlementAmountPct">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Total amount in percent (RM)</label>
                                                    <input class="form-control decimal-input" id="txtSettlementTotalAmount">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Payment Date</label>
                                                    <input class="form-control date-input" id="txtSettlementPaymentDate">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Bank</label>
                                                    <select class="form-control" id="ddlSettlementBank">
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Bank account number</label>
                                                    <input class="form-control" id="txtSettlementBankAccountNumber">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Due date</label>
                                                    <input class="form-control date-input" id="txtSettlementDueDate">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Facilities</label>
                                                    <select class="form-control nyatakan" id="ddlSettlementFacilities" name="ddlSettlementFacilities" data-nyatakan="<%= csa.Library.Constant.OTHER_NUMBER %>"></select>                                                    
                                                </div>
                                            </div>
                                            <div class="col-md-3 d-none" id="nyatakan_ddlSettlementFacilities">
                                                <div class="mb-3">
                                                    <label class="form-label">Facilities Other</label>
                                                    <input type="text" class="form-control" name="txtSettlementFacilitiesOther" id="txtSettlementFacilitiesOther" />
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Amount facilities</label>
                                                    <input class="form-control decimal-input" id="txtSettlementAmountFacilities">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Flexi campaign</label>
                                                    <select class="form-control nyatakan" id="ddlSettlementFlexiCampaign" name="ddlSettlementFlexiCampaign" data-nyatakan="<%= csa.Library.Constant.OTHER_NUMBER %>"></select>
                                                </div>
                                            </div>
                                            <div class="col-md-3 d-none" id="nyatakan_ddlSettlementFlexiCampaign">
                                                <div class="mb-3">
                                                    <label class="form-label">Flexi campaign Other</label>
                                                    <input type="text" class="form-control" name="txtSettlementFlexiCampaignOther" id="txtSettlementFlexiCampaignOther" />
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Total campaign</label>
                                                    <input class="form-control decimal-input" id="txtSettlementTotalCampaign">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Redemption Letter Date</label>
                                                    <input class="form-control date-input" id="txtRedemptionLetterDate">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Redemption Amount (RM)</label>
                                                    <input class="form-control decimal-input" id="txtRedemptionAmount">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Loan Release Date</label>
                                                    <input class="form-control date-input" id="txtLoanReleaseDate">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Settlement Status</label>
                                                    <select class="form-control" id="ddlSettlementStatus">
                                                        <option value='1'>Uncollected</option>
                                                        <option value='2'>Collected</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Remark/Updates</label>
                                                    <input class="form-control" id="txtSettlementRemark">
                                                </div>
                                            </div>
                                            <button class="col-12 btn btn-primary" type="button" id="btnAddSettlementDetails">Add Details</button>
                                        </div>
                                    </div>

                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Case Update</h5>
                                    </div>
                                    <div class="card-body" style="overflow-x:auto;overflow-y:hidden">
                                        <table id="tableCaseUpdate" class="table table-bordered dt-responsive nowrap table-striped align-middle" style="width: 100%">
                                            <thead>
                                                <tr>
                                                    <th class="text-center">ACTION</th>
                                                    <th class="text-center">BANK</th>
                                                    <th class="text-center">LOAN AMOUNT (RM)</th>
                                                    <th class="text-center">SUBMIT DATE</th>
                                                    <th class="text-center">BANKER</th>
                                                    <th class="text-center">CONSOLIDATE</th>
                                                    <th class="text-center">CASH NET</th>
                                                    <th class="text-center">INSTALMENT (RM)</th>
                                                    <th class="text-center">APPROVED DATE</th>
                                                    <th class="text-center">DISBURSEMENT DATE</th>
                                                    <th class="text-center">REMARK</th>
                                                    <th class="text-center">CREATED BY</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Add New Case Update</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Bank</label>
                                                    <select class="form-control" id="ddlCuBank">
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Loan Amount</label>
                                                    <input class="form-control decimal-input" id="txtCuLoanAmount">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Submit Date</label>
                                                    <input class="form-control date-input" id="txtCuSubmitDate">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Banker</label>
                                                    <input class="form-control" id="txtCuBanker">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Completed</label>
                                                    <select class="form-control" id="ddlCuStatus">
                                                        <option value='0'>No</option>
                                                        <option value='1'>Yes</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Consolidate</label>
                                                    <input class="form-control" id="txtCuConsolidate">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Cash Net</label>
                                                    <input class="form-control decimal-input" id="txtCuCashNet">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Instalment</label>
                                                    <input class="form-control decimal-input" id="txtCuInstallment">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Approved Date</label>
                                                    <input class="form-control date-input" id="txtCuApprovedDate">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Date Sign</label>
                                                    <input class="form-control date-input" id="txtCuSignDate">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Disbursement Date</label>
                                                    <input class="form-control date-input" id="txtCuDisbursementDate">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Update On</label>
                                                    <input class="form-control date-input" id="txtCuUpdateDate">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Loan Account Number</label>
                                                    <input class="form-control" id="txtCuLoanAccountNumber">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">1st Due Date</label>
                                                    <input class="form-control date-input" id="txtCuFirstDueDate">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Remarks</label>
                                                    <input class="form-control" id="txtCuRemark">
                                                </div>
                                            </div>

                                            <button class="col-12 btn btn-primary" type="button" id="btnAddCaseUpdate">Add Update</button>
                                        </div>
                                    </div>

                                    

                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Additional Documents</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row" id="contentAdditionalDocumentSettlement">
                                            
                                        </div>
                                        <button class="col-12 btn btn-primary btn-add-additional-document" data-status="6" type="button">+ Add New Row</button>
                                    </div>
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Remark</h5>
                                    </div>
                                    <div class="card-body">
                                        <textarea class="form-control" style="height:100px" id="txtApplicationRemarkSettlement"></textarea>
                                    </div>
                                    <div class="card-footer">
                                        <div class="col-lg-12">
                                            <div class="row">
                                                <div class="col-md-6 col-xs-12 mb-2">
                                                    <%--show only when rejected at this status, rename reject button to Cancel Reject--%>
                                                    <%--Reject button only show at Current status--%>
                                                    <div class="content-application-rejected">
                                                        <b>Application Rejected at DATE by ADMIN NAME</b><br />
                                                        "Rejection Reason"
                                                    </div>

                                                </div>
                                                <div class="col-md-6 col-xs-12 mb-2" style="text-align: end" data-status="6">
                                                    <%--pas reject di click, keluar popup dulu, suruh isi Rejection Reason, button Reject & Cancel--%>
                                                    <button type="button" id="btnRejectSettlement" class="btn btn-danger me-2 btn-application-reject"><i class="me-1 ri-close-circle-line"></i>Reject</button>
                                                    <button type="button" class="btn btn-danger me-2 btn-application-cancel-reject"><i class="me-1 ri-close-circle-line"></i>Cancel Reject</button>
                                                    <button type="button" id="btnSaveSettlement" class="btn btn-primary me-1 btn-application-save"><i class="me-1 ri-save-line"></i>Save Changes</button>
                                                    <%--next button cuma muncul kalo current statusnya ini--%>
                                                    <button type="button" class="btn btn-info" id="btnConvertToHero"><i class="me-1 ri-check-line"></i>Convert to Hero</button>
                                                    <button type="button" class="btn btn-info btn-application-next" id="btnNextSettlement"><i class="me-1 ri-check-line"></i>Next</button>

                                                    <%--Validate move to next status for prechecking:
                                                    - Must have at least 1 Settlement
                                                    - Must have at least 1 Case Update
                                                  
                                                    below validation error message add: Do you wish to proceed? Yes or No
                                                    jadi ini validasi message doank, user admin masih bisa move ke next status, kalo No, diem di status ini, kalo Yes, maju ke tab berikutnya, Current Status ganti ke status berikutnya--%>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>

                                <div id="tabCcrisCleaningMonitoring" class="d-none">
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>CCRIS Cleaning/Monitoring Stage - Upload Release Letter, CCRIS report(if any).</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Release Letter</label>
                                                    <span class="float-end d-none" id="releaseletterAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="ReleaseLetterFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldreleaseletter">Upload File</label>
                                                        <input class="form-control" id="txtreleaseletter" readonly onclick="openInputFile('upldreleaseletter')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldreleaseletter" onchange="onChangeUpload(event,'txtreleaseletter')"/>
                                                    <span class="text-success" id="releaseletterDesc"></span>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">CCRIS Report</label>
                                                    <span class="float-end d-none" id="ccrisreportAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="CCRISReportFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldccrisreport">Upload File</label>
                                                        <input class="form-control" id="txtccrisreport" readonly onclick="openInputFile('upldccrisreport')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldccrisreport" onchange="onChangeUpload(event,'txtccrisreport')"/>
                                                    <span class="text-success" id="ccrisreportDesc"></span>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">HRMIS</label>
                                                    <span class="float-end d-none" id="hrmis-ccrisAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="HRMISFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldhrmis-ccris">Upload File</label>
                                                        <input class="form-control" id="txthrmis-ccris" readonly onclick="openInputFile('upldhrmis-ccris')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldhrmis-ccris" onchange="onChangeUpload(event,'txthrmis-ccris')"/>
                                                    <span class="text-success" id="hrmis-ccrisDesc"></span>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">ANM</label>
                                                    <span class="float-end d-none" id="anm-ccrisAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="ANMFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldanm-ccris">Upload File</label>
                                                        <input class="form-control" id="txtanm-ccris" readonly onclick="openInputFile('upldanm-ccris')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldanm-ccris" onchange="onChangeUpload(event,'txtanm-ccris')"/>
                                                    <span class="text-success" id="anm-ccrisDesc"></span>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">LPSA</label>
                                                    <span class="float-end d-none" id="lpsa-ccrisAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="LPSAFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldlpsa-ccris">Upload File</label>
                                                        <input class="form-control" id="txtlpsa-ccris" readonly onclick="openInputFile('upldlpsa-ccris')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldlpsa-ccris" onchange="onChangeUpload(event,'txtlpsa-ccris')"/>
                                                    <span class="text-success" id="lpsa-ccrisDesc"></span>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Angkasa</label>
                                                    <span class="float-end d-none" id="angkasa-ccrisAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="AngkasaFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldangkasa-ccris">Upload File</label>
                                                        <input class="form-control" id="txtangkasa-ccris" readonly onclick="openInputFile('upldangkasa-ccris')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldangkasa-ccris" onchange="onChangeUpload(event,'txtangkasa-ccris')"/>
                                                    <span class="text-success" id="angkasa-ccrisDesc"></span>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Health Credit Score</label>
                                                    <select class="form-select" id="ddlHealthCreditScoreCcris">
                                                        <option value='10'>10</option>
                                                        <option value='9'>9</option>
                                                        <option value='8'>8</option>
                                                        <option value='7'>7</option>
                                                        <option value='6'>6</option>
                                                        <option value='5'>5</option>
                                                        <option value='4'>4</option>
                                                        <option value='3'>3</option>
                                                        <option value='2'>2</option>
                                                        <option value='1'>1</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Bankruptcy Status</label>
                                                    <select class="form-select" id="ddlBankruptcyStatusCcris">
                                                        <option value='1'>Yes</option>
                                                        <option value='0'>No</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Legal Case</label>
                                                    <select class="form-select" id="ddlLegalCaseCcris">
                                                        <option value='1'>Yes</option>
                                                        <option value='0'>No</option>
                                                    </select>
                                                </div>
                                            </div>
                                             <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Commitments</label>
                                                    <input class="form-control" id="txtCommitmentsCcris">
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Loan in Progress</h5>
                                    </div>
                                    <div class="card-body" style="overflow-x:auto;overflow-y:hidden">
                                        <table id="tableLoanInProgress" class="table table-bordered dt-responsive nowrap table-striped align-middle" style="width: 100%">
                                            <thead>
                                                <tr>
                                                    <th class="text-center">BANK</th>
                                                    <th class="text-center">LOAN AMOUNT (RM)</th>
                                                    <th class="text-center">SUBMIT DATE</th>
                                                    <th class="text-center">BANKER</th>
                                                    <th class="text-center">CONSOLIDATE</th>
                                                    <th class="text-center">CASH NET</th>
                                                    <th class="text-center">INSTALMENT (RM)</th>
                                                    <th class="text-center">APPROVED DATE</th>
                                                    <th class="text-center">DISBURSEMENT DATE</th>
                                                    <th class="text-center">REMARK</th>
                                                    <th class="text-center">CREATED BY</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                
                                            </tbody>
                                        </table>
                                    </div>

                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Additional Documents</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row" id="contentAdditionalDocumentCCRIS">

                                        </div>
                                        <button class="col-12 btn btn-primary btn-add-additional-document" data-status="7" type="button">+ Add New Row</button>
                                    </div>
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Remark</h5>
                                    </div>
                                    <div class="card-body">
                                        <textarea class="form-control" style="height:100px" id="txtApplicationRemarkCcris"></textarea>
                                    </div>
                                    <div class="card-footer">
                                        <div class="col-lg-12">
                                            <div class="row">
                                                <div class="col-md-6 col-xs-12 mb-2">
                                                    <%--show only when rejected at this status, rename reject button to Cancel Reject--%>
                                                    <%--Reject button only show at Current status--%>
                                                    <div class="content-application-rejected">
                                                        <b>Application Rejected at DATE by ADMIN NAME</b><br />
                                                        "Rejection Reason"
                                                    </div>

                                                </div>
                                                <div class="col-md-6 col-xs-12 mb-2" style="text-align: end" data-status="7">
                                                    <%--pas reject di click, keluar popup dulu, suruh isi Rejection Reason, button Reject & Cancel--%>
                                                    <button type="button" id="btnRejectCcris" class="btn btn-danger me-2 btn-application-reject"><i class="me-1 ri-close-circle-line"></i>Reject</button>
                                                    <button type="button" class="btn btn-danger me-2 btn-application-cancel-reject"><i class="me-1 ri-close-circle-line"></i>Cancel Reject</button>
                                                    <button type="button" id="btnSaveCcris" class="btn btn-primary me-1 btn-application-save"><i class="me-1 ri-save-line"></i>Save Changes</button>
                                                    <%--next button cuma muncul kalo current statusnya ini--%>
                                                    <button type="button" class="btn btn-info btn-application-next" id="btnNextCcris"><i class="me-1 ri-check-line"></i>Next</button>

                                                    <%--Validate move to next status for prechecking:
                                                    - Release Letter file must be uploaded
                                                    - CCRIS Report file must be uploaded
                                                  
                                                    below validation error message add: Do you wish to proceed? Yes or No
                                                    jadi ini validasi message doank, user admin masih bisa move ke next status, kalo No, diem di status ini, kalo Yes, maju ke tab berikutnya, Current Status ganti ke status berikutnya--%>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>

                                <div id="tabQueueForLoan" class="d-none">
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Queue For Loan Stage - Upload 13 documents (government workers) / 10 documents (private workers).</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Employer Type</label>
                                                    <select class="form-control" id="ddlWorkerType">
                                                        <option value="1">Government Worker</option>
                                                        <option value="2">Private Worker</option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Identity Card</label>
                                                    <span class="float-end d-none" id="identityAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="IdentityCardFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldidentity">Upload File</label>
                                                        <input class="form-control" id="txtidentity" readonly onclick="openInputFile('upldidentity')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldidentity" onchange="onChangeUpload(event,'txtidentity')"/>
                                                    <span class="text-success" id="identityDesc"></span>
                                                </div>
                                            </div>
                                            <%--Show if Private--%>
                                            <div class="col-md-4" data-worker="2">
                                                <div class="mb-3">
                                                    <label class="form-label">Staff Identity Card</label>
                                                     <span class="float-end d-none" id="staffcardAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                         <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="StaffCardFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldstaffcard">Upload File</label>
                                                        <input class="form-control" id="txtstaffcard" readonly onclick="openInputFile('upldstaffcard')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldstaffcard" onchange="onChangeUpload(event,'txtstaffcard')"/>
                                                    <span class="text-success" id="staffcardDesc"></span>
                                                </div>
                                            </div>

                                              <%--Show if Govt--%>
                                            <div class="col-md-4 d-none">
                                                <div class="mb-3">
                                                    <label class="form-label">Payslip (3 month latest)</label>
                                                   <span class="float-end d-none" id="payslipqueueAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                       <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="PayslipFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldpayslipqueue">Upload File</label>
                                                        <input class="form-control" id="txtpayslipqueue" readonly onclick="openInputFile('upldpayslipqueue')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldpayslipqueue" onchange="onChangeUpload(event,'txtpayslipqueue')"/>
                                                    <span class="text-success" id="payslipqueueDesc"></span>
                                                </div>
                                            </div>
                                            <%--Show if Private--%>
                                            <div class="col-md-4" data-worker="2">
                                                <div class="mb-3">
                                                    <label class="form-label">Post Dated Cheque</label>
                                                    <span class="float-end d-none" id="postdatedchequeAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="PostDatedChequeFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldpostdatedcheque">Upload File</label>
                                                        <input class="form-control" id="txtpostdatedcheque" readonly onclick="openInputFile('upldpostdatedcheque')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldpostdatedcheque" onchange="onChangeUpload(event,'txtpostdatedcheque')"/>
                                                    <span class="text-success" id="postdatedchequeDesc"></span>
                                                </div>
                                            </div>
                                            <%--Show if Private--%>
                                            <div class="col-md-4" data-worker="2">
                                                <div class="mb-3">
                                                    <label class="form-label">Company Confirmation</label>
                                                     <span class="float-end d-none" id="companyAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                         <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="CompanyConfirmationFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldcompany">Upload File</label>
                                                        <input class="form-control" id="txtcompany" readonly onclick="openInputFile('upldcompany')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldcompany" onchange="onChangeUpload(event,'txtcompany')"/>
                                                    <span class="text-success" id="companyDesc"></span>
                                                </div>
                                            </div>
                                            <%--Show if Govt--%>
                                            <div class="col-md-4" data-worker="1">
                                                <div class="mb-3">
                                                    <label class="form-label">EC</label>
                                                    <span class="float-end d-none" id="ecAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="ECFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldec">Upload File</label>
                                                        <input class="form-control" id="txtec" readonly onclick="openInputFile('upldec')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldec" onchange="onChangeUpload(event,'txtec')"/>
                                                    <span class="text-success" id="ecDesc"></span>
                                                </div>
                                            </div>
                                            <%--Show if Govt--%>
                                            <div class="col-md-4" data-worker="1">
                                                <div class="mb-3">
                                                    <label class="form-label">HRMIS</label>
                                                    <span class="float-end d-none" id="hrmisAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="HRMISFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldhrmis">Upload File</label>
                                                        <input class="form-control" id="txthrmis" readonly onclick="openInputFile('upldhrmis')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldhrmis" onchange="onChangeUpload(event,'txthrmis')"/>
                                                    <span class="text-success" id="hrmisDesc"></span>
                                                </div>
                                            </div>
                                            <%--Show both--%>
                                            <div class="col-md-4 d-none">
                                                <div class="mb-3">
                                                    <label class="form-label">Bank Statement (3 month latest)</label>
                                                    <span class="float-end d-none" id="bankstatementAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="BankStatementFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldbankstatement">Upload File</label>
                                                        <input class="form-control" id="txtbankstatement" readonly onclick="openInputFile('upldbankstatement')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldbankstatement" onchange="onChangeUpload(event,'txtbankstatement')"/>
                                                    <span class="text-success" id="bankstatementDesc"></span>
                                                </div>
                                            </div>
                                            <%--Show if Private--%>
                                            <div class="col-md-4" data-worker="2">
                                                <div class="mb-3">
                                                    <label class="form-label">EPF Current & Previous Year</label>
                                                     <span class="float-end d-none" id="epfAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                         <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="EPFFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldepf">Upload File</label>
                                                        <input class="form-control" id="txtepf" readonly onclick="openInputFile('upldepf')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldepf" onchange="onChangeUpload(event,'txtepf')"/>
                                                    <span class="text-success" id="epfDesc"></span>
                                                </div>
                                            </div>
                                            <%--Show if Private--%>
                                            <div class="col-md-4" data-worker="2">
                                                <div class="mb-3">
                                                    <label class="form-label">EA Form Current</label>
                                                     <span class="float-end d-none" id="eaformAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                         <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="EAFormFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldeaform">Upload File</label>
                                                        <input class="form-control" id="txteaform" readonly onclick="openInputFile('upldeaform')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldeaform" onchange="onChangeUpload(event,'txteaform')"/>
                                                    <span class="text-success" id="eaformDesc"></span>
                                                </div>
                                            </div>
                                            <%--Show if Private--%>
                                            <div class="col-md-4" data-worker="2">
                                                <div class="mb-3">
                                                    <label class="form-label">Bill Utilities</label>
                                                     <span class="float-end d-none" id="billutilitiesAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                         <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="BillUtilitiesFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldbillutilities">Upload File</label>
                                                        <input class="form-control" id="txtbillutilities" readonly onclick="openInputFile('upldbillutilities')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldbillutilities" onchange="onChangeUpload(event,'txtbillutilities')"/>
                                                    <span class="text-success" id="billutilitiesDesc"></span>
                                                </div>
                                            </div>
                                            <%--Show if Govt--%>
                                            <div class="col-md-4" data-worker="1">
                                                <div class="mb-3">
                                                    <label class="form-label">LPPSA</label>
                                                    <span class="float-end d-none" id="lppsaAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="LPPSAFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldlppsa">Upload File</label>
                                                        <input class="form-control" id="txtlppsa" readonly onclick="openInputFile('upldlppsa')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldlppsa" onchange="onChangeUpload(event,'txtlppsa')"/>
                                                    <span class="text-success" id="lppsaDesc"></span>
                                                </div>
                                            </div>
                                            <%--Show if Govt--%>
                                            <div class="col-md-4" data-worker="1">
                                                <div class="mb-3">
                                                    <label class="form-label">License (Except for Selangor & Wilayah Persekutuan)</label>
                                                     <span class="float-end d-none" id="licenseAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                         <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="LicenseFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldlicense">Upload File</label>
                                                        <input class="form-control" id="txtlicense" readonly onclick="openInputFile('upldlicense')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldlicense" onchange="onChangeUpload(event,'txtlicense')"/>
                                                    <span class="text-success" id="licenseDesc"></span>
                                                </div>
                                            </div>
                                            <%--Show if Govt--%>
                                            <div class="col-md-4" data-worker="1">
                                                <div class="mb-3">
                                                    <label class="form-label">Redemption Letter (if any)</label>
                                                    <span class="float-end d-none" id="redemptionletterqueueAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="RedemptionLetterFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldredemptionletterqueue">Upload File</label>
                                                        <input class="form-control" id="txtredemptionletterqueue" readonly onclick="openInputFile('upldredemptionletterqueue')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldredemptionletterqueue" onchange="onChangeUpload(event,'txtredemptionletterqueue')"/>
                                                    <span class="text-success" id="redemptionletterqueueDesc"></span>
                                                </div>
                                            </div>
                                            <%--Show if Govt--%>
                                            <div class="col-md-4" data-worker="1">
                                                <div class="mb-3">
                                                    <label class="form-label">Credit Card Latest Statement (if any)</label>
                                                    <span class="float-end d-none" id="ccAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="CCStatementFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldcc">Upload File</label>
                                                        <input class="form-control" id="txtcc" readonly onclick="openInputFile('upldcc')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldcc" onchange="onChangeUpload(event,'txtcc')"/>
                                                    <span class="text-success" id="ccDesc"></span>
                                                </div>
                                            </div>
                                            <%--Show if Govt--%>
                                            <div class="col-md-4" data-worker="1">
                                                <div class="mb-3">
                                                    <label class="form-label">RAMCI</label>
                                                    <span class="float-end d-none" id="ramciqueueAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="RAMCIFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldramciqueue">Upload File</label>
                                                        <input class="form-control" id="txtramciqueue" readonly onclick="openInputFile('upldramciqueue')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldramciqueue" onchange="onChangeUpload(event,'txtramciqueue')"/>
                                                    <span class="text-success" id="ramciqueueDesc"></span>
                                                </div>
                                            </div>
                                            <%--Show both--%>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Applicant's Signature</label>
                                                    <span class="float-end d-none" id="signatureAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="SignatureFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldsignature">Upload File</label>
                                                        <input class="form-control" id="txtsignature" readonly onclick="openInputFile('upldsignature')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldsignature" onchange="onChangeUpload(event,'txtsignature')"/>
                                                    <span class="text-success" id="signatureDesc"></span>
                                                </div>
                                            </div>
                                            <%--Show if Govt--%>
                                            <div class="col-md-4" data-worker="1">
                                                <div class="mb-3">
                                                    <label class="form-label">BIRO Angkasa</label>
                                                    <span class="float-end d-none" id="biroAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="BIROFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldbiro">Upload File</label>
                                                        <input class="form-control" id="txtbiro" readonly onclick="openInputFile('upldbiro')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldbiro" onchange="onChangeUpload(event,'txtbiro')"/>
                                                    <span class="text-success" id="biroDesc"></span>
                                                </div>
                                            </div>
                                            <%--Show if Govt--%>
                                            <div class="col-md-4" data-worker="1">
                                                <div class="mb-3">
                                                    <label class="form-label">KEW320 Form</label>
                                                     <span class="float-end d-none" id="kew320Action">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                         <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="KEW320FileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldkew320">Upload File</label>
                                                        <input class="form-control" id="txtkew320" readonly onclick="openInputFile('upldkew320')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldkew320" onchange="onChangeUpload(event,'txtkew320')"/>
                                                    <span class="text-success" id="kew320Desc"></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-4 col-sm-12">
                                            <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Payslip</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row" id="contentPayslips">

                                        </div>
                                            <button class="col-12 btn btn-primary btn-add-payslip" data-status="8" type="button">+ Add New Row</button>
                                    </div>
                                        </div>
                                        <div class="col-md-4 col-sm-12">
                                             <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Bank Statement</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row" id="contentBankStatements">

                                        </div>
                                            <button class="col-12 btn btn-primary btn-add-bank-statement" data-status="8" type="button">+ Add New Row</button>
                                    </div>
                                        </div>
                                    </div>

                                     
                                    
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Additional Documents</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row" id="contentAdditionalDocumentQueue">

                                        </div>
                                            <button class="col-12 btn btn-primary btn-add-additional-document" data-status="8" type="button">+ Add New Row</button>
                                    </div>
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Remark</h5>
                                    </div>
                                    <div class="card-body">
                                        <textarea class="form-control" style="height:100px" id="txtApplicationRemarkQueue"></textarea>
                                    </div>
                                    

                                    <div class="card-footer">
                                        <div class="col-lg-12">
                                            <div class="row">
                                                <div class="col-md-6 col-xs-12 mb-2">
                                                    <%--show only when rejected at this status, rename reject button to Cancel Reject--%>
                                                    <%--Reject button only show at Current status--%>
                                                    <div class="content-application-rejected">
                                                        <b>Application Rejected at DATE by ADMIN NAME</b><br />
                                                        "Rejection Reason"
                                                    </div>

                                                </div>
                                                <div class="col-md-6 col-xs-12 mb-2" style="text-align: end" data-status="8">
                                                    <%--pas reject di click, keluar popup dulu, suruh isi Rejection Reason, button Reject & Cancel--%>
                                                    <button type="button" id="btnRejectQueue" class="btn btn-danger me-2 btn-application-reject"><i class="me-1 ri-close-circle-line"></i>Reject</button>
                                                    <button type="button" class="btn btn-danger me-2 btn-application-cancel-reject"><i class="me-1 ri-close-circle-line"></i>Cancel Reject</button>
                                                    <button type="button" id="btnSaveQueue" class="btn btn-primary me-1 btn-application-save"><i class="me-1 ri-save-line"></i>Save Changes</button>
                                                    <%--next button cuma muncul kalo current statusnya ini--%>
                                                    <button type="button" class="btn btn-info btn-application-next" id="btnNextQueue"><i class="me-1 ri-check-line"></i>Next</button>

                                                    <%--Validate move to next status for prechecking:
                                                    - IC file must be uploaded
                                                    - Payslip file must be uploaded
                                                    - Bank Statement file must be uploaded
                                                    - Signature file must be uploaded
                                                  
                                                    below validation error message add: Do you wish to proceed? Yes or No
                                                    jadi ini validasi message doank, user admin masih bisa move ke next status, kalo No, diem di status ini, kalo Yes, maju ke tab berikutnya, Current Status ganti ke status berikutnya--%>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>

                                <div id="tabReloanSubmission" class="d-none">
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Reloan Submission Stage - Upload signed offer letter.</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Offer Letter</label>
                                                    <span class="float-end d-none" id="offerletterAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="OfferLetterFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldofferletter">Upload File</label>
                                                        <input class="form-control" id="txtofferletter" readonly onclick="openInputFile('upldofferletter')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldofferletter" onchange="onChangeUpload(event,'txtofferletter')"/>
                                                    <span class="text-success" id="offerletterDesc"></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Reloan Detail</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Reloan Status</label>
                                                    <select class="form-control" id="ddlReloanStatus">
                                                        <option value="1">In Progress</option>
                                                        <option value="2">Approve</option>
                                                        <option value="3">Rejected</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="d-md-block"></div>
                                             <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Approved Amount</label>
                                                    <input class="form-control decimal-input" id="txtApprovedAmount">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Approved Date</label>
                                                    <input class="form-control date-input" id="txtApprovedDate">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Signing Date</label>
                                                    <input class="form-control date-input" id="txtSigningDate">
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Additional Documents</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row" id="contentAdditionalDocumentReloan">
                                            
                                        </div>
                                        <button class="col-12 btn btn-primary btn-add-additional-document" data-status="9" type="button">+ Add New Row</button>
                                    </div>
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Remark</h5>
                                    </div>
                                    <div class="card-body">
                                        <textarea class="form-control" style="height:100px" id="txtApplicationRemarkReloan"></textarea>
                                    </div>

                                    <div class="card-footer">
                                        <div class="col-lg-12">
                                            <div class="row">
                                                <div class="col-md-6 col-xs-12 mb-2">
                                                    <%--show only when rejected at this status, rename reject button to Cancel Reject--%>
                                                    <%--Reject button only show at Current status--%>
                                                    <div class="content-application-rejected">
                                                        <b>Application Rejected at DATE by ADMIN NAME</b><br />
                                                        "Rejection Reason"
                                                    </div>

                                                </div>
                                                <div class="col-md-6 col-xs-12 mb-2" style="text-align: end" data-status="9">
                                                    <%--pas reject di click, keluar popup dulu, suruh isi Rejection Reason, button Reject & Cancel--%>
                                                    <button type="button" id="btnRejectReloan" class="btn btn-danger me-2 btn-application-reject"><i class="me-1 ri-close-circle-line"></i>Reject</button>
                                                    <button type="button" class="btn btn-danger me-2 btn-application-cancel-reject"><i class="me-1 ri-close-circle-line"></i>Cancel Reject</button>
                                                    <button type="button" id="btnSaveReloan" class="btn btn-primary me-1 btn-application-save"><i class="me-1 ri-save-line"></i>Save Changes</button>
                                                    <%--next button cuma muncul kalo current statusnya ini--%>
                                                    <button type="button" class="btn btn-info btn-application-next" id="btnNextReloan"><i class="me-1 ri-check-line"></i>Next</button>

                                                    <%--Validate move to next status for prechecking:
                                                    - Offer Letter must be uploaded.
                                                    - Reloan status must be Approved.
                                                  
                                                    below validation error message add: Do you wish to proceed? Yes or No
                                                    jadi ini validasi message doank, user admin masih bisa move ke next status, kalo No, diem di status ini, kalo Yes, maju ke tab berikutnya, Current Status ganti ke status berikutnya--%>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>


                                <div id="tabCollection" class="d-none">
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Collection Stage - Upload payment Receipt & transaction, YABAM receipt, rezeki agreement, MIFM membership form.</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Declaration Form</label>
                                                   <span class="float-end d-none" id="declarationAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                       <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="DeclarationFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="uplddeclaration">Upload File</label>
                                                        <input class="form-control" id="txtdeclaration" readonly onclick="openInputFile('uplddeclaration')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="uplddeclaration" onchange="onChangeUpload(event,'txtdeclaration')"/>
                                                    <span class="text-success" id="declarationDesc"></span>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Settlement Receipt</label>
                                                     <span class="float-end d-none" id="settlementreceiptAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                         <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="SettlementReceiptFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="uplsettlementreceipt">Upload File</label>
                                                        <input class="form-control" id="txtsettlementreceipt" readonly onclick="openInputFile('upldsettlementreceipt')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldsettlementreceipt" onchange="onChangeUpload(event,'txtsettlementreceipt')"/>
                                                    <span class="text-success" id="settlementreceiptDesc"></span>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Service Fee Receipt</label>
                                                     <span class="float-end d-none" id="servicefeeAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                         <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="ServiceFeeReceiptFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldservicefee">Upload File</label>
                                                        <input class="form-control" id="txtservicefee" readonly onclick="openInputFile('upldservicefee')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldservicefee" onchange="onChangeUpload(event,'txtservicefee')"/>
                                                    <span class="text-success" id="servicefeeDesc"></span>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Rezeki Receipt</label>
                                                    <span class="float-end d-none" id="rezekireceiptAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="RezekiReceiptFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldrezekireceipt">Upload File</label>
                                                        <input class="form-control" id="txtrezekireceipt" readonly onclick="openInputFile('upldrezekireceipt')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldrezekireceipt" onchange="onChangeUpload(event,'txtrezekireceipt')"/>
                                                    <span class="text-success" id="rezekireceiptDesc"></span>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Rezeki Agreement</label>
                                                    <span class="float-end d-none" id="rezekiagreementAction">
                                                        <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                        <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-delete-file" data-field="RezekeAgreementFileId"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldrezekiagreement">Upload File</label>
                                                        <input class="form-control" id="txtrezekiagreement" readonly onclick="openInputFile('upldrezekiagreement')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldrezekiagreement" onchange="onChangeUpload(event,'txtrezekiagreement')"/>
                                                    <span class="text-success" id="rezekiagreementDesc"></span>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Service Fee (RM)</label>
                                                    <input class="form-control decimal-input" id="txtServiceFee"/>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Deposit Amount (RM)</label>
                                                    <input class="form-control decimal-input" id="txtDepositAmount"/>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Deposit Date</label>
                                                    <input class="form-control date-input" id="txtDepositDate" />
                                                </div>
                                            </div>

                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Settlement Date</label>
                                                    <input class="form-control date-input" id="txtSettlementDate" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Settlement Amount</label>
                                                    <input class="form-control decimal-input" id="txtSettlementCollectionAmount" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Settlement C %</label>
                                                    <input class="form-control decimal-input-pct" id="txtSettlementCPct" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Collection Amount %</label>
                                                    <input class="form-control decimal-input-pct" id="txtCollectionAmountPct" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Settlement Duration</label>
                                                    <input class="form-control" type="number" id="txtSettlementDuration" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Total Reloand</label>
                                                    <input class="form-control decimal-input" id="txtTotalReloan" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Total Loan Repayment</label>
                                                    <input class="form-control decimal-input" type="number" id="txtTotalLoanRepayment" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">DBB Bank Account</label>
                                                    <input class="form-control" id="txtDBBBankAccount" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">DBB Tenure</label>
                                                    <input class="form-control" id="txtDBBTenure" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">DBB Agreement Date</label>
                                                    <input class="form-control date-input" id="txtDBBAgreementDate" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Monthly Fund</label>
                                                    <input class="form-control decimal-input" id="txtMonthlyFund" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">DBB Amount</label>
                                                    <input class="form-control decimal-input" id="txtDBBAmount" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Receipt No</label>
                                                    <input class="form-control" id="txtReceiptNo" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Tax Number</label>
                                                    <input class="form-control" id="txtTaxNumber" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Status</label>
                                                    <select class="form-select" id="ddlCollectionStatusId">
                                                        <option value="0">Select an option</option>
                                                        <option value="1">Registered</option>
                                                        <option value="2">Error</option>
                                                        <option value="3">Under Maintenance</option>
                                                        <option value="4">UM Review</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3">
                                                    <label class="form-label">Installment Date</label>
                                                    <input class="form-control date-input" id="txtInstallmentDate" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    

                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Additional Documents</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row" id="contentAdditionalDocumentCollection">

                                        </div>
                                        <button class="col-12 btn btn-primary btn-add-additional-document" data-status="10" type="button">+ Add New Row</button>
                                    </div>
                                    <div class="card-header align-items-center d-flex">
                                        <h5 class="card-title mb-0 flex-grow-1"><i class="me-1 ri-mail-add-line"></i>Remark</h5>
                                    </div>
                                    <div class="card-body">
                                        <textarea class="form-control" style="height:100px" id="txtApplicationRemarkCollection"></textarea>
                                    </div>
                                    <div class="card-footer">
                                        <div class="col-lg-12">
                                            <div class="row">
                                                <div class="col-md-6 col-xs-12 mb-2">
                                                    <%--show only when rejected at this status, rename reject button to Cancel Reject--%>
                                                    <%--Reject button only show at Current status--%>
                                                    <div class="content-application-rejected">
                                                        <b>Application Rejected at DATE by ADMIN NAME</b><br />
                                                        "Rejection Reason"
                                                    </div>

                                                </div>
                                                <div class="col-md-6 col-xs-12 mb-2" style="text-align: end" data-status="10">
                                                    <%--pas reject di click, keluar popup dulu, suruh isi Rejection Reason, button Reject & Cancel--%>
                                                    <button type="button" id="btnRejectCollection" class="btn btn-danger me-2 btn-application-reject"><i class="me-1 ri-close-circle-line"></i>Reject</button>
                                                    <button type="button" class="btn btn-danger me-2 btn-application-cancel-reject"><i class="me-1 ri-close-circle-line"></i>Cancel Reject</button>
                                                    <button type="button" id="btnSaveCollection" class="btn btn-primary me-1 btn-application-save"><i class="me-1 ri-save-line"></i>Save Changes</button>
                                                    <%--next button cuma muncul kalo current statusnya ini, kalo udah curent status == completed hide--%>
                                                    <%--<button type="button" class="btn btn-info btn-application-next" id="btnNextCollection"><i class="me-1 ri-check-line"></i>Convert to Hero</button>--%>

                                                    <%--Validate move to next status for prechecking:
                                                    - Declaration Form file must be uploaded
                                                    - Settlement Receipt file must be uploaded
                                                    - Service Fee Receipt file must be uploaded
                                                    - Rezeki Receipt file must be uploaded
                                                    - Rezeki Agreement file must be uploaded
                                                  
                                                    below validation error message add: Do you wish to proceed? Yes or No
                                                    jadi ini validasi message doank, user admin masih bisa move ke next status, kalo No, diem di status ini, kalo Yes, maju ke tab berikutnya, Current Status ganti ke status berikutnya--%>
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
        </div>
    </div>

    <!-- Default Modals -->
<div id="modalEditSettlement" class="modal fade modal-xl" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="myModalLabel">Update Settlement</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"> </button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <input type="hidden" id="hidSettlementId" />
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Settlement Amount (RM)</label>
                                                    <input class="form-control decimal-input" id="txtUpdateSettlementAmount">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Percent of total settlement amount (%)</label>
                                                    <input class="form-control decimal-input-pct" id="txtUpdateSettlementAmountPct">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Total amount in percent (RM)</label>
                                                    <input class="form-control decimal-input" id="txtUpdateSettlementTotalAmount">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Payment Date</label>
                                                    <input class="form-control date-input" id="txtUpdateSettlementPaymentDate">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Bank</label>
                                                    <select class="form-control" id="ddlUpdateSettlementBank">
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Bank account number</label>
                                                    <input class="form-control" id="txtUpdateSettlementBankAccountNumber">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Due date</label>
                                                    <input class="form-control date-input" id="txtUpdateSettlementDueDate">
                                                </div>
                                            </div>                                            
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Facilities</label>
                                                    <select class="form-control nyatakan" id="ddlUpdateSettlementFacilities" name="ddlUpdateSettlementFacilities" data-nyatakan="<%= csa.Library.Constant.OTHER_NUMBER %>"></select>                                                    
                                                </div>
                                            </div>
                                            <div class="col-md-3 d-none" id="nyatakan_ddlUpdateSettlementFacilities">
                                                <div class="mb-3">
                                                    <label class="form-label">Facilities Other</label>
                                                    <input type="text" class="form-control" name="txtUpdateSettlementFacilitiesOther" id="txtUpdateSettlementFacilitiesOther" />
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Amount facilities</label>
                                                    <input class="form-control decimal-input" id="txtUpdateSettlementAmountFacilities">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Flexi campaign</label>
                                                    <select class="form-control nyatakan" id="ddlUpdateSettlementFlexiCampaign" name="ddlUpdateSettlementFlexiCampaign" data-nyatakan="<%= csa.Library.Constant.OTHER_NUMBER %>"></select>
                                                </div>
                                            </div>
                                            <div class="col-md-3 d-none" id="nyatakan_ddlUpdateSettlementFlexiCampaign">
                                                <div class="mb-3">
                                                    <label class="form-label">Flexi campaign Other</label>
                                                    <input type="text" class="form-control" name="txtUpdateSettlementFlexiCampaignOther" id="txtUpdateSettlementFlexiCampaignOther" />
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Total campaign</label>
                                                    <input class="form-control decimal-input" id="txtUpdateSettlementTotalCampaign">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Redemption Letter Date</label>
                                                    <input class="form-control date-input" id="txtUpdateRedemptionLetterDate">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Redemption Amount (RM)</label>
                                                    <input class="form-control decimal-input" id="txtUpdateRedemptionAmount">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Loan Release Date</label>
                                                    <input class="form-control date-input" id="txtUpdateLoanReleaseDate">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Settlement Status</label>
                                                    <select class="form-control" id="ddlUpdateSettlementStatus">
                                                        <option value='1'>Uncollected</option>
                                                        <option value='2'>Collected</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Remark/Updates</label>
                                                    <input class="form-control" id="txtUpdateSettlementRemark">
                                                </div>
                                            </div>
                                        </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-light" data-bs-dismiss="modal">Cancel</button>
                <button type="button" id="btnUpdateSettlement" class="btn btn-primary ">Update</button>
            </div>

        </div><!-- /.modal-content -->
    </div><!-- /.modal-dialog -->
</div><!-- /.modal -->

    <!-- Default Modals -->
<div id="modalEditCaseUpdate" class="modal fade modal-xl" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Update Case Update</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"> </button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <input type="hidden" id="hidCaseUpdateId" />
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Bank</label>
                                                    <select class="form-control" id="ddlCuUpdateBank">
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Loan Amount</label>
                                                    <input class="form-control decimal-input" id="txtCuUpdateLoanAmount">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Submit Date</label>
                                                    <input class="form-control date-input" id="txtCuUpdateSubmitDate">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Banker</label>
                                                    <input class="form-control" id="txtCuUpdateBanker">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Completed</label>
                                                    <select class="form-control" id="ddlCuUpdateStatus">
                                                        <option value='0'>No</option>
                                                        <option value='1'>Yes</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Consolidate</label>
                                                    <input class="form-control" id="txtCuUpdateConsolidate">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Cash Net</label>
                                                    <input class="form-control decimal-input" id="txtCuUpdateCashNet">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Instalment</label>
                                                    <input class="form-control decimal-input" id="txtCuUpdateInstallment">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Approved Date</label>
                                                    <input class="form-control date-input" id="txtCuUpdateApprovedDate">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Date Sign</label>
                                                    <input class="form-control date-input" id="txtCuUpdateSignDate">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Disbursement Date</label>
                                                    <input class="form-control date-input" id="txtCuUpdateDisbursementDate">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Update On</label>
                                                    <input class="form-control date-input" id="txtCuUpdateUpdateDate">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Loan Account Number</label>
                                                    <input class="form-control" id="txtCuUpdateLoanAccountNumber">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">1st Due Date</label>
                                                    <input class="form-control date-input" id="txtCuUpdateFirstDueDate">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3">
                                                    <label class="form-label">Remarks</label>
                                                    <input class="form-control" id="txtCuUpdateRemark">
                                                </div>
                                            </div>

                                        </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-light" data-bs-dismiss="modal">Cancel</button>
                <button type="button" id="btnUpdateCase" class="btn btn-primary ">Update</button>
            </div>

        </div><!-- /.modal-content -->
    </div><!-- /.modal-dialog -->
</div><!-- /.modal -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="<%=Page.ResolveUrl("~/assets/libs/jqueryValidation/dist/jquery.validate.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/Scripts/customValidator.js") %>"></script>
    <!-- moment js -->
    <script src="<%=Page.ResolveUrl("~/assets/libs/moment/min/moment.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/assets/libs/moment/min/moment-with-locales.min.js") %>"></script>
    <!-- select2 js -->
    <script type='text/javascript' src="<%=Page.ResolveUrl("~/assets/libs/select2/js/select2.min.js") %>"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
    <script>
        $("form").submit(function (e) {
            e.preventDefault();
        });
        var vms
        var listDocumentPreChecking = []
        var listDocumentToDelete = []
        var listFileToDelete = []
        var listSettlementToDelete = []
        var listCaseUpdateToDelete = []
        var listSettlement = []
        var listCaseUpdate = []
        var listPayslip = []
        var listBankStatement = []
        var listApplicationFileDelete = []

        //control application info
        var ddlSource = $('#ddlSource')
        var ddlCreditStatus = $('#ddlCreditStatus')
        var txtScoreClass = $('#txtScoreClass')
        var ddlCustomerStatus = $('#ddlCustomerStatus')
        var ddlBurstReason = $('#ddlBurstReason')
        var txtCreditRemark = $('#txtCreditRemark')
        var ddlCurrentStatus = $('#ddlCurrentStatus')

        //control prechecking
        var cbLegalSuits = $('#cbLegalSuits')
        var cbBankruptcy = $('#cbBankruptcy')
        var cbSpecialAttentionAccount = $('#cbSpecialAttentionAccount')
        var cbBadPaymentRecord = $('#cbBadPaymentRecord')

        //control preparation
        var txtSalaryGross = $('#txtSalaryGross')
        var txtSalaryDeduction = $('#txtSalaryDeduction')
        var txtNetIncome = $('#txtNetIncome')
        var txtB1 = $('#txtB1')
        var txtB2 = $('#txtB2')
        var txtB3 = $('#txtB3')
        var txtB4 = $('#txtB4')
        var txtBAverage = $('#txtBAverage')
        var txtCommitmentOutstanding = $('#txtCommitmentOutstanding')
        var txtCommitmentInstallment = $('#txtCommitmentInstallment')
        var txtOtherNetBalance = $('#txtOtherNetBalance')
        var txtBPA = $('#txtBPA')
        var txtOtherComparionDSR = $('#txtOtherComparionDSR')
        var txtOtherComparisonDSRPctCommitment = $('#txtOtherComparisonDSRPctCommitment')
        var txtOtherPctRefresh = $('#txtOtherPctRefresh')
        var txtOtherProposedRefresh = $('#txtOtherProposedRefresh')
        var txtOtherCompositionDSR = $('#txtOtherCompositionDSR')
        var txtOtherCompositionDSRPctCommitment = $('#txtOtherCompositionDSRPctCommitment')
        var txtRefreshTotal = $('#txtRefreshTotal')
        var txtRefreshRemainCommitment = $('#txtRefreshRemainCommitment')
        var txtReloanTotal = $('#txtReloanTotal')
        var txtReloanMonthy = $('#txtReloanMonthy')
        var txtReloanBersih = $('#txtReloanBersih')
        var txtReloanBelanja = $('#txtReloanBelanja')
        var txtReloanDeposit = $('#txtReloanDeposit')
        var txtReloanDanaBantuan = $('#txtReloanDanaBantuan')
        var txtReloanServiceFee = $('#txtReloanServiceFee')
        var txtReloanServiceFeePct = $('#txtReloanServiceFeePct')
        var txtReloanIncomeAfterRNR = $('#txtReloanIncomeAfterRNR')
        var txtReloanDifference = $('#txtReloanDifference')
        var ddlModelBackgroundScreening = $('#ddlModelBackgroundScreening')
        var ddlModelCompositionDSR = $('#ddlModelCompositionDSR')
        var ddlModelCommitment = $('#ddlModelCommitment')
        var ddlModelSettlement = $('#ddlModelSettlement')
        var ddlModelServiceFee = $('#ddlModelServiceFee')
        var ddlModelNetIncomeAfterRNR = $('#ddlModelNetIncomeAfterRNR')
        var ddlModelStatus = $('#ddlModelStatus')
        var ddlModelStatusProposal = $('#ddlModelStatusProposal')
        var ddlModelCheck = $('#ddlModelCheck')
        var ddlReviewAdmin = $('#ddlReviewAdmin')
        var ddlReviewStatus = $('#ddlReviewStatus')
        var txtReviewDate = $('#txtReviewDate')
        var txtReviewComment = $('#txtReviewComment')
        var ddlApproveAdmin = $('#ddlApproveAdmin')
        var ddlApproveStatus = $('#ddlApproveStatus')
        var txtApproveDate = $('#txtApproveDate')
        var txtApproveComment = $('#txtApproveComment')
        var ddlVerifiedAdmin = $('#ddlVerifiedAdmin')
        var ddlVerifiedStatus = $('#ddlVerifiedStatus')
        var txtVerifiedDate = $('#txtVerifiedDate')
        var txtVerifiedComment = $('#txtVerifiedComment')
        var txtCreditRemarkProposalPreparation = $('#txtCreditRemarkProposalPreparation')

        //control proposal
        var ddlproposalstatus = $('#ddlproposalstatus')

        //control presign
        var ddlproposalsend = $('#ddlproposalsend')
        var ddlsuratakuan = $('#ddlsuratakuan')
        var ddlcomprehensive = $('#ddlcomprehensive')

        //control zoomacceptance
        var txtApplicantAddress = $('#txtApplicantAddress')
        var ddlBankruptcyStatusZoom = $('#ddlBankruptcyStatusZoom')
        var ddlLegalCaseZoom = $('#ddlLegalCaseZoom')
        var ddlHealthCreditScoreZoom = $('#ddlHealthCreditScoreZoom')
        var txtCommitments = $('#txtCommitments')

        //control ccris
        var ddlHealthCreditScoreCcris = $('#ddlHealthCreditScoreCcris')
        var ddlBankruptcyStatusCcris = $('#ddlBankruptcyStatusCcris')
        var ddlLegalCaseCcris = $('#ddlLegalCaseCcris')
        var txtCommitmentsCcris = $('#txtCommitmentsCcris')

        //control settlement
        var txtSettlementAmount = $('#txtSettlementAmount')
        var txtSettlementAmountPct = $('#txtSettlementAmountPct')
        var txtSettlementTotalAmount = $('#txtSettlementTotalAmount')
        var txtSettlementPaymentDate = $('#txtSettlementPaymentDate')
        var ddlSettlementBank = $('#ddlSettlementBank')
        var txtSettlementBankAccountNumber = $('#txtSettlementBankAccountNumber')
        var txtSettlementDueDate = $('#txtSettlementDueDate')
        var ddlSettlementFacilities = $('#ddlSettlementFacilities')
        var txtSettlementFacilitiesOther = $('#txtSettlementFacilitiesOther')
        var txtSettlementAmountFacilities = $('#txtSettlementAmountFacilities')
        var ddlSettlementFlexiCampaign = $('#ddlSettlementFlexiCampaign')
        var txtSettlementFlexiCampaignOther = $('#txtSettlementFlexiCampaignOther')
        var txtSettlementTotalCampaign = $('#txtSettlementTotalCampaign')
        var txtRedemptionLetterDate = $('#txtRedemptionLetterDate')
        var txtRedemptionAmount = $('#txtRedemptionAmount')
        var txtLoanReleaseDate = $('#txtLoanReleaseDate')
        var ddlSettlementStatus = $('#ddlSettlementStatus')
        var txtSettlementRemark = $('#txtSettlementRemark')

        var txtUpdateSettlementAmount = $('#txtUpdateSettlementAmount')
        var txtUpdateSettlementAmountPct = $('#txtUpdateSettlementAmountPct')
        var txtUpdateSettlementTotalAmount = $('#txtUpdateSettlementTotalAmount')
        var txtUpdateSettlementPaymentDate = $('#txtUpdateSettlementPaymentDate')
        var ddlUpdateSettlementBank = $('#ddlUpdateSettlementBank')
        var txtUpdateSettlementBankAccountNumber = $('#txtUpdateSettlementBankAccountNumber')
        var txtUpdateSettlementDueDate = $('#txtUpdateSettlementDueDate')
        var ddlUpdateSettlementFacilities = $('#ddlUpdateSettlementFacilities')
        var txtUpdateSettlementFacilitiesOther = $('#txtUpdateSettlementFacilitiesOther')
        var txtUpdateSettlementAmountFacilities = $('#txtUpdateSettlementAmountFacilities')
        var ddlUpdateSettlementFlexiCampaign = $('#ddlUpdateSettlementFlexiCampaign')
        var txtUpdateSettlementFlexiCampaignOther = $('#txtUpdateSettlementFlexiCampaignOther')
        var txtUpdateSettlementTotalCampaign = $('#txtUpdateSettlementTotalCampaign')
        var txtUpdateRedemptionLetterDate = $('#txtUpdateRedemptionLetterDate')
        var txtUpdateRedemptionAmount = $('#txtUpdateRedemptionAmount')
        var txtUpdateLoanReleaseDate = $('#txtUpdateLoanReleaseDate')
        var ddlUpdateSettlementStatus = $('#ddlUpdateSettlementStatus')
        var txtUpdateSettlementRemark = $('#txtUpdateSettlementRemark')

        $(".date-input").datepicker({
            format: 'dd-mm-yyyy',
            autoclose: true
        });

        //control case update
        var ddlCuBank = $('#ddlCuBank')
        var txtCuLoanAmount = $('#txtCuLoanAmount')
        var txtCuSubmitDate = $('#txtCuSubmitDate')
        var txtCuBanker = $('#txtCuBanker')
        var ddlCuStatus = $('#ddlCuStatus')
        var txtCuConsolidate = $('#txtCuConsolidate')
        var txtCuCashNet = $('#txtCuCashNet')
        var txtCuInstallment = $('#txtCuInstallment')
        var txtCuApprovedDate = $('#txtCuApprovedDate')
        var txtCuSignDate = $('#txtCuSignDate')
        var txtCuDisbursementDate = $('#txtCuDisbursementDate')
        var txtCuUpdateDate = $('#txtCuUpdateDate')
        var txtCuLoanAccountNumber = $('#txtCuLoanAccountNumber')
        var txtCuFirstDueDate = $('#txtCuFirstDueDate')
        var txtCuRemark = $('#txtCuRemark')

        var ddlCuUpdateBank = $('#ddlCuUpdateBank')
        var txtCuUpdateLoanAmount = $('#txtCuUpdateLoanAmount')
        var txtCuUpdateSubmitDate = $('#txtCuUpdateSubmitDate')
        var txtCuUpdateBanker = $('#txtCuUpdateBanker')
        var ddlCuUpdateStatus = $('#ddlCuUpdateStatus')
        var txtCuUpdateConsolidate = $('#txtCuUpdateConsolidate')
        var txtCuUpdateCashNet = $('#txtCuUpdateCashNet')
        var txtCuUpdateInstallment = $('#txtCuUpdateInstallment')
        var txtCuUpdateApprovedDate = $('#txtCuUpdateApprovedDate')
        var txtCuUpdateSignDate = $('#txtCuUpdateSignDate')
        var txtCuUpdateDisbursementDate = $('#txtCuUpdateDisbursementDate')
        var txtCuUpdateUpdateDate = $('#txtCuUpdateUpdateDate')
        var txtCuUpdateLoanAccountNumber = $('#txtCuUpdateLoanAccountNumber')
        var txtCuUpdateFirstDueDate = $('#txtCuUpdateFirstDueDate')
        var txtCuUpdateRemark = $('#txtCuUpdateRemark')

        //remark
        var txtApplicationRemarkPrechecking = $('#txtApplicationRemarkPrechecking')
        var txtApplicationRemarkPreparation = $('#txtApplicationRemarkPreparation')
        var txtApplicationRemarkPresentation = $('#txtApplicationRemarkPresentation')
        var txtApplicationRemarkPresign = $('#txtApplicationRemarkPresign')
        var txtApplicationRemarkPendingZoomAndAtteptance = $('#txtApplicationRemarkPendingZoomAndAtteptance')
        var txtApplicationRemarkSettlement = $('#txtApplicationRemarkSettlement')
        var txtApplicationRemarkCcris = $('#txtApplicationRemarkCcris')
        var txtApplicationRemarkQueue = $('#txtApplicationRemarkQueue')
        var txtApplicationRemarkReloan = $('#txtApplicationRemarkReloan')
        var txtApplicationRemarkCollection = $('#txtApplicationRemarkCollection')
        //control queue
        $('#ddlWorkerType').on('change', onchangeWorkerType)

        //control reloan
        var txtServiceFee = $('#txtServiceFee')
        var txtDepositAmount = $('#txtDepositAmount')
        var txtDepositDate = $('#txtDepositDate')

        //control reloan
        var ddlReloanStatus = $('#ddlReloanStatus')
        var txtApprovedDate = $('#txtApprovedDate')
        var txtSigningDate = $('#txtSigningDate')
        var txtApprovedAmount = $('#txtApprovedAmount')

        //control collection
        var txtSettlementDate = $('#txtSettlementDate')
        var txtSettlementCollectionAmount = $('#txtSettlementCollectionAmount')
        var txtSettlementCPct = $('#txtSettlementCPct')
        var txtCollectionAmountPct = $('#txtCollectionAmountPct')
        var txtSettlementDuration = $('#txtSettlementDuration')
        var txtTotalReloan = $('#txtTotalReloan')
        var txtTotalLoanRepayment = $('#txtTotalLoanRepayment')
        var txtDBBBankAccount = $('#txtDBBBankAccount')
        var txtDBBTenure = $('#txtDBBTenure')
        var txtDBBAgreementDate = $('#txtDBBAgreementDate')
        var txtMonthlyFund = $('#txtMonthlyFund')
        var txtDBBAmount = $('#txtDBBAmount')
        var txtReceiptNo = $('#txtReceiptNo')
        var txtTaxNumber = $('#txtTaxNumber')
        var ddlCollectionStatusId = $('#ddlCollectionStatusId')
        var txtInstallmentDate = $('#txtInstallmentDate')

        $('select').select2({ 'theme': 'bootstrap-5', 'minimumResultsForSearch': -1 });
        //navigate tab
        $('.btn-tab').click(function (e) {
            var childs = $(this).parent().children();
            const currentIndex = $(this).index()

            //cant move upward tab status
            if (currentIndex + 1 > <%=CurrentApplicationStatusId%>) {
                return
            }

            childs.each(function (index) {
                $(this).removeClass('tab-active')
                $(this).removeClass('tab-done')
                if (currentIndex == index) {
                    $(this).addClass('tab-active')
                }
                else if (currentIndex > index) {
                    $(this).addClass('tab-done')
                }
            })
            $('#contentApplicationByStatus').children().not(':first').each(function (index) {
                $(this).addClass('d-none')
                if (currentIndex == index) {
                    $(this).removeClass('d-none')
                }
            })
        })

        function changeTab(index) {
            var childs = $('#tabLinkApplication').children();
            const currentIndex = index
            childs.each(function (index) {
                $(this).removeClass('tab-active')
                $(this).removeClass('currentstatus')
                $(this).removeClass('tab-done')
                if (currentIndex == index) {
                    $(this).addClass('tab-active')
                    $(this).addClass('currentstatus')
                }
                else if (currentIndex > index) {
                    $(this).addClass('tab-done')
                }
            })
            $('#contentApplicationByStatus').children().not(':first').each(function (index) {
                $(this).addClass('d-none')
                if (currentIndex == index) {
                    $(this).removeClass('d-none')
                }
            })
        }

        $('.decimal-input').on('input', function () {
            // Get the current value
            let value = $(this).val();

            // Use regex to allow only numbers and one decimal point
            value = value.replace(/[^0-9.]/g, ''); // Remove non-numeric characters

            // Check for multiple decimal points
            const decimalCount = (value.match(/\./g) || []).length;
            if (decimalCount > 1) {
                value = value.replace(/\.+$/, ''); // Remove the last decimal point
            }

            // Check if there are more than 2 decimal places
            const decimalPart = value.split('.')[1];
            if (decimalPart && decimalPart.length > 2) {
                value = value.substring(0, value.indexOf('.') + 3); // Limit to 2 decimal places
            }

            // Update the input value
            $(this).val(value);
        });

        $('.decimal-input-pct').on('input', function () {
            // Get the current value
            let value = $(this).val();

            // Use regex to allow only numbers and one decimal point
            value = value.replace(/[^0-9.]/g, ''); // Remove non-numeric characters

            // Check for multiple decimal points
            const decimalCount = (value.match(/\./g) || []).length;
            if (decimalCount > 1) {
                value = value.replace(/\.+$/, ''); // Remove the last decimal point
            }

            // Check if there are more than 2 decimal places
            const decimalPart = value.split('.')[1];
            if (decimalPart && decimalPart.length > 2) {
                value = value.substring(0, value.indexOf('.') + 3); // Limit to 2 decimal places
            }

            // Check for maximum value of 100
            if (parseFloat(value) > 100) {
                value = '100'; // Set to 100 if exceeds
            }

            // Update the input value
            $(this).val(value);
        });

        function appendApplicationFile(doc, container) {
            $(container).append(`<div class="col-md-12 applicationfile${doc.ApplicationFileId}">
                                                <div class="mb-3">
                                                    <label class="form-label">Filename</label>
                                                    <span class="float-end">
                                                        <button type="button" class="btn btn-text btn-download-file" data-download='${window.location.origin}/DownloadFile.aspx?f=${doc.File}&n=${doc.FileName}&t=application' ><i class="me-1 ri-download-line"></i></button>
                                                        <a href="${window.location.origin}/ViewFile.aspx?f=${doc.File}&n=${doc.FileName}&t=application" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-remove-application-file" data-id='${doc.ApplicationFileId}'><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <input class="form-control" readonly value='${doc.FileName}'>
                                                    <span class="text-success">${doc.UploadedBy.Desc}</span>
                                                </div>
                                            </div>
                                            `)
        }

        $('.btn-add-payslip').click(function (e) {

            const uniqueId = Date.now() * -1
            listPayslip.push(uniqueId)

            $($(this).parent().children()[0]).append(`<div class="col-md-12 applicationfile${uniqueId}">
                                            <div class="mb-3">
                                                <label class="form-label">Filename</label>
                                                    <span class="float-end">
                                                        <button type="button" class="btn btn-text btn-remove-application-file" data-id="${uniqueId}"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldfilepayslip${uniqueId}">Upload File</label>
                                                        <input class="form-control" id="txtfilepayslip${uniqueId}" readonly onclick="openInputFile('upldfilepayslip${uniqueId}')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldfilepayslip${uniqueId}" onchange="onChangeUpload(event,'txtfilepayslip${uniqueId}')"/>
                                            </div>
                                        </div>`)
        })

        $('.btn-add-bank-statement').click(function (e) {

            const uniqueId = Date.now() * -1
            listBankStatement.push(uniqueId)

            $($(this).parent().children()[0]).append(`<div class="col-md-12 applicationfile${uniqueId}">
                                            <div class="mb-3">
                                                <label class="form-label">Filename</label>
                                                    <span class="float-end">
                                                        <button type="button" class="btn btn-text btn-remove-application-file" data-id="${uniqueId}"><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <div class="input-group">
                                                        <label class="btn btn-primary input-group-text btn-upload" for="upldfilebankstatement${uniqueId}">Upload File</label>
                                                        <input class="form-control" id="txtfilebankstatement${uniqueId}" readonly onclick="openInputFile('upldfilebankstatement${uniqueId}')"/>
                                                    </div>
                                                    <input class="form-control d-none" type="file" id="upldfilebankstatement${uniqueId}" onchange="onChangeUpload(event,'txtfilebankstatement${uniqueId}')"/>
                                            </div>
                                        </div>`)
        })

       
        $('.btn-add-additional-document').click(function (e) {

            const uniqueId = Date.now() * -1
            const markId = 'docprechecking'
            const status = $(this).data('status')
            listDocumentPreChecking.push(uniqueId)

            $($(this).parent().children()[0]).append(`<div class="col-md-4 docprechecking-${uniqueId}">
                                            <div class="mb-3">
                                                <label class="form-label">Upload Additional Documents</label>
                                                    <span class="float-end">
                                                        <button type="button" class="btn btn-text btn-remove-additional-document" data-id='${uniqueId}'><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <input class="form-control" type="file" id='upld${markId}_${uniqueId}_${status}'>
                                            </div>
                                        </div>
                                        <div class="col-md-8 docprechecking-${uniqueId}"">
                                            <div class="mb-3">
                                                <label class="form-label">Remarks</label>
                                                    <input class="form-control" id='txtremark${markId}_${uniqueId}_${status}'>
                                            </div>
                                        </div>`)
        })

        function appendDoc(doc, container,applicationStatusId) {            
            $(container).append(`<div class="col-md-4 docprechecking-${doc.ApplicationDocumentId}">
                                                <div class="mb-3">
                                                    <label class="form-label">Filename</label>
                                                    <span class="float-end">
                                                        <button type="button" class="btn btn-text btn-download-file" data-download='${window.location.origin}/DownloadFile.aspx?f=${doc.File}&n=${doc.FileName}&t=application_document' ><i class="me-1 ri-download-line"></i></button>
                                                        <a href="${window.location.origin}/ViewFile.aspx?f=${doc.File}&n=${doc.FileName}&t=application_document" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                        <button type="button" class="btn btn-text btn-remove-additional-document" data-id='${doc.ApplicationDocumentId}'><i class="me-1 ri-delete-bin-2-line"></i></button>
                                                    </span>
                                                    <input class="form-control" readonly value='${doc.FileName}'>
                                                </div>
                                            </div>
                                            <div class="col-md-4 docprechecking-${doc.ApplicationDocumentId}">
                                                <div class="mb-3">
                                                    <label class="form-label">Remarks</label>
                                                    <input class="form-control doc-remark-editable" data-status='${applicationStatusId}' data-id='${doc.ApplicationDocumentId}' value='${doc.Remark}'>
                                                </div>
                                            </div>
                                            <div class="col-md-2 docprechecking-${doc.ApplicationDocumentId}">
                                                <div class="mb-3">
                                                    <label class="form-label">Uploaded by</label>
                                                    <input class="form-control" readonly value='${doc.Uploaded}'>
                                                </div>
                                            </div>
                                            <div class="col-md-2 docprechecking-${doc.ApplicationDocumentId}">
                                                <div class="mb-3">
                                                    <label class="form-label">Uploaded Date</label>
                                                    <input class="form-control" readonly value='${appHelper.dateToFormat(doc.CreateDate)}'>
                                                </div>
                                            </div>`)
        }

        $('#btnAddSettlementDetails').click(function (e) {

            const uniqueId = Date.now() * -1

            var form = $('#form1')
            form.validate().destroy()
            form.validate({
                rules: {
                    ddlSettlementFlexiCampaign: {
                        notZero: true
                    },
                    txtSettlementFlexiCampaignOther: {
                        required: {
                            depends: function () {
                                return $('[name="ddlSettlementFlexiCampaign"]').val().includes(String($('[name="ddlSettlementFlexiCampaign"]').data('nyatakan')))
                            }
                        }
                    },
                    ddlSettlementFacilities: {
                        notZero: true
                    },
                    txtSettlementFacilitiesOther: {
                        required: {
                            depends: function () {
                                return $('[name="ddlSettlementFacilities"]').val().includes(String($('[name="ddlSettlementFacilities"]').data('nyatakan')))
                            }
                        }
                    },
                },
                errorClass: 'is-invalid',
                errorPlacement: function (error, element) {
                    error.addClass("invalid-feedback");
                    error.insertAfter(element);
                }
            })

            if (!form.valid()) {
                dialogHelper.error('Please fill in the required fields.')
                return
            }           

            const newSettlement = {
                SettlementId: uniqueId,
                Amount: txtSettlementAmount.val(),
                TotalPct: txtSettlementAmountPct.val(),
                TotalPctAmount: txtSettlementTotalAmount.val(),
                PaymentDate: txtSettlementPaymentDate.val(),
                BankId: ddlSettlementBank.val(),
                BankAccountNumber: txtSettlementBankAccountNumber.val(),
                DueDate: txtSettlementDueDate.val(),
                FacilitiesId: ddlSettlementFacilities.val(),
                FacilitiesOther: txtSettlementFacilitiesOther.val(),
                AmountFacilities: txtSettlementAmountFacilities.val(),
                FlexyCampaignId: ddlSettlementFlexiCampaign.val(),
                FlexyCampaignOther: txtSettlementFlexiCampaignOther.val(),
                TotalCampaign: txtSettlementTotalCampaign.val(),
                RedemptionLetterDate: txtRedemptionLetterDate.val(),
                RedemptionAmount: txtRedemptionAmount.val(),
                LoanReleaseDate: txtLoanReleaseDate.val(),
                SettlementStatusId: ddlSettlementStatus.val(),
                Remark: txtSettlementRemark.val(),
                Admin: '<%=CurrentLoginAdmin.Name%>'
            }
            
            if ($("#tableSettlementDetails tbody tr").hasClass('row-is-empty')) {
                $('#tableSettlementDetails tbody').empty()
            }

            appendSettlement(newSettlement)

            clearInputSettlement()
            
        })

        function appendSettlement(newSettlement) {
            const vm = vms[6]
            listSettlement.push(newSettlement)

            const bank = vm.VtBanks.find(x => x.Key == newSettlement.BankId)
            const content = `<tr id='settlement-tr-${newSettlement.SettlementId}'>
<td><div class="hstack gap-3 fs-13 justify-content-center">
                                               <a href="javascript:void(0);" onclick='setEditSettlement(${newSettlement.SettlementId})' class="text-success">Edit</a>
                                               <a href="javascript:void(0);" class="text-success btn-remove-settlement" data-id='${newSettlement.SettlementId}'>Delete</a>
                                           </div></td>
<td>${appHelper.formatPrice(newSettlement.Amount)}</td>
<td>${appHelper.formatPrice(newSettlement.TotalPct)}</td>
<td>${appHelper.formatPrice(newSettlement.TotalPctAmount)}</td>
<td>${newSettlement.PaymentDate}</td>
<td>${bank.Text}</td>
<td>${newSettlement.BankAccountNumber}</td>
<td>${newSettlement.DueDate}</td>
<td>${newSettlement.FacilitiesId == <%= csa.Library.Constant.OTHER_NUMBER %> ? newSettlement.FacilitiesOther : vm.Facilities.find(x => x.Value == newSettlement.FacilitiesId).Text}</td>
<td>${appHelper.formatPrice(newSettlement.AmountFacilities)}</td>
<td>${newSettlement.SettlementStatusId == '1' ? 'Uncollected' : 'Collected'}</td>
<td>${newSettlement.Remark}</td>
<td>${newSettlement.Admin}</td>
</tr>`
            $('#tableSettlementDetails tbody').append(content)            
        }

        $('#btnAddCaseUpdate').click(function (e) {

            const uniqueId = Date.now() * -1

            const newCase = {
                CaseUpdateId: uniqueId,
                BankId: ddlCuBank.val(),
                LoanAmount: txtCuLoanAmount.val(),
                SubmitDate: txtCuSubmitDate.val(),
                Banker: txtCuBanker.val(),
                CompleteStatusId: ddlCuStatus.val(),
                Consolidate: txtCuConsolidate.val(),
                CashNet: txtCuCashNet.val(),
                Installment: txtCuInstallment.val(),
                ApprovedDate: txtCuApprovedDate.val(),
                SignDate: txtCuSignDate.val(),
                DisbursementDate: txtCuDisbursementDate.val(),
                UpdateDate: txtCuUpdateDate.val(),
                LoanAccountNumber: txtCuLoanAccountNumber.val(),
                FirstDueDate: txtCuFirstDueDate.val(),
                Remarkds: txtCuRemark.val(),
                Admin: '<%=CurrentLoginAdmin.Name%>'
            }

            if ($("#tableCaseUpdate tbody tr").hasClass('row-is-empty')) {
                $('#tableCaseUpdate tbody').empty()
            }

            appendCaseUpdate(newCase)
            
            clearCaseUpdateInput()
        })

        function setEditCaseUpdate(id) {
            const c = listCaseUpdate.find(x => x.CaseUpdateId == id);
            
            $('#hidCaseUpdateId').val(id)
            ddlCuUpdateBank.val(c.BankId);
            txtCuUpdateLoanAmount.val(c.LoanAmount);
            txtCuUpdateSubmitDate.data('datepicker').setDate(appHelper.convertToDate(appHelper.convertToApiDate(c.SubmitDate)));
            txtCuUpdateBanker.val(c.Banker);
            ddlCuUpdateStatus.val(c.CompleteStatusId);
            txtCuUpdateConsolidate.val(c.Consolidate);
            txtCuUpdateCashNet.val(c.CashNet);
            txtCuUpdateInstallment.val(c.Installment);
            txtCuUpdateApprovedDate.data('datepicker').setDate(appHelper.convertToDate(appHelper.convertToApiDate(c.ApprovedDate)));
            txtCuUpdateSignDate.data('datepicker').setDate(appHelper.convertToDate(appHelper.convertToApiDate(c.SignDate)));
            txtCuUpdateDisbursementDate.data('datepicker').setDate(appHelper.convertToDate(appHelper.convertToApiDate(c.DisbursementDate)));
            txtCuUpdateUpdateDate.data('datepicker').setDate(appHelper.convertToDate(appHelper.convertToApiDate(c.UpdateDate)));
            txtCuUpdateLoanAccountNumber.val(c.LoanAccountNumber);
            txtCuUpdateFirstDueDate.data('datepicker').setDate(appHelper.convertToDate(appHelper.convertToApiDate(c.FirstDueDate)));
            txtCuUpdateRemark.val(c.Remarkds);

            ddlCuUpdateBank.trigger('change')
            ddlCuUpdateStatus.trigger('change')
            $('#modalEditCaseUpdate').modal('show')
        }

        $('#btnUpdateCase').click(function () {

            let vm = vms[6]
            
            const index = listCaseUpdate.findIndex(x => x.CaseUpdateId == $('#hidCaseUpdateId').val())

            const newCase = {
                CaseUpdateId: $('#hidCaseUpdateId').val(),
                BankId: ddlCuUpdateBank.val(),
                LoanAmount: txtCuUpdateLoanAmount.val(),
                SubmitDate: txtCuUpdateSubmitDate.val(),
                Banker: txtCuUpdateBanker.val(),
                CompleteStatusId: ddlCuUpdateStatus.val(),
                Consolidate: txtCuUpdateConsolidate.val(),
                CashNet: txtCuUpdateCashNet.val(),
                Installment: txtCuUpdateInstallment.val(),
                ApprovedDate: txtCuUpdateApprovedDate.val(),
                SignDate: txtCuUpdateSignDate.val(),
                DisbursementDate: txtCuUpdateDisbursementDate.val(),
                UpdateDate: txtCuUpdateUpdateDate.val(),
                LoanAccountNumber: txtCuUpdateLoanAccountNumber.val(),
                FirstDueDate: txtCuUpdateFirstDueDate.val(),
                Remarkds: txtCuUpdateRemark.val(),
                Admin: listCaseUpdate[index].Admin
            }

            listCaseUpdate[index] = newCase

            const bank = vm.VtBanks.find(x => x.Key == newCase.BankId)

            $(`#caseupdate-tr-${newCase.CaseUpdateId}`).html(`<td><div class="hstack gap-3 fs-13 justify-content-center">
                                               <a href="javascript:void(0);" onclick='setEditCaseUpdate(${newCase.CaseUpdateId})' class="text-success">Edit</a>
                                               <a href="javascript:void(0);" class="text-success btn-remove-caseupdate" data-id='${newCase.CaseUpdateId}'>Delete</a>
                                           </div></td>
<td>${bank.Text}</td>
<td>${appHelper.formatPrice(newCase.LoanAmount)}</td>
<td>${newCase.SubmitDate}</td>
<td>${newCase.Banker}</td>
<td>${newCase.Consolidate}</td>
<td>${appHelper.formatPrice(newCase.CashNet)}</td>
<td>${appHelper.formatPrice(newCase.Installment)}</td>
<td>${newCase.ApprovedDate}</td>
<td>${newCase.DisbursementDate}</td>
<td>${newCase.Remarkds}</td>
<td>${newCase.Admin}</td>
</tr>`)

            $('#modalEditCaseUpdate').modal('hide')
        })

        $('#btnUpdateSettlement').click(function () {

            let vm = vms[6]

            var form = $('#form1')
            form.validate().destroy()
            form.validate({
                rules: {
                    ddlUpdateSettlementFlexiCampaign: {
                        notZero: true
                    },
                    txtUpdateSettlementFlexiCampaignOther: {
                        required: {
                            depends: function () {
                                return $('[name="ddlUpdateSettlementFlexiCampaign"]').val().includes(String($('[name="ddlUpdateSettlementFlexiCampaign"]').data('nyatakan')))
                            }
                        }
                    },
                    ddlUpdateSettlementFacilities: {
                        notZero: true
                    },
                    txtUpdateSettlementFacilitiesOther: {
                        required: {
                            depends: function () {
                                return $('[name="ddlUpdateSettlementFacilities"]').val().includes(String($('[name="ddlUpdateSettlementFacilities"]').data('nyatakan')))
                            }
                        }
                    },
                },
                errorClass: 'is-invalid',
                errorPlacement: function (error, element) {
                    error.addClass("invalid-feedback");
                    error.insertAfter(element);
                }
            })

            if (!form.valid()) {
                dialogHelper.error('Please fill in the required fields.')
                return
            }


            const index = listSettlement.findIndex(x => x.SettlementId == $('#hidSettlementId').val())

            const newSettlement = {
                SettlementId: $('#hidSettlementId').val(),
                Amount: txtUpdateSettlementAmount.val(),
                TotalPct: txtUpdateSettlementAmountPct.val(),
                TotalPctAmount: txtUpdateSettlementTotalAmount.val(),
                PaymentDate: txtUpdateSettlementPaymentDate.val(),
                BankId: ddlUpdateSettlementBank.val(),
                BankAccountNumber: txtUpdateSettlementBankAccountNumber.val(),
                DueDate: txtUpdateSettlementDueDate.val(),
                FacilitiesId: ddlUpdateSettlementFacilities.val(),
                FacilitiesOther: txtUpdateSettlementFacilitiesOther.val(),
                AmountFacilities: txtUpdateSettlementAmountFacilities.val(),
                FlexyCampaignId: ddlUpdateSettlementFlexiCampaign.val(),
                FlexyCampaignOther: txtUpdateSettlementFlexiCampaignOther.val(),
                TotalCampaign: txtUpdateSettlementTotalCampaign.val(),
                RedemptionLetterDate: txtUpdateRedemptionLetterDate.val(),
                RedemptionAmount: txtUpdateRedemptionAmount.val(),
                LoanReleaseDate: txtUpdateLoanReleaseDate.val(),
                SettlementStatusId: ddlUpdateSettlementStatus.val(),
                Remark: txtUpdateSettlementRemark.val(),
                Admin: listSettlement[index].Admin
             }

            listSettlement[index] = newSettlement

            const bank = vm.VtBanks.find(x => x.Key == newSettlement.BankId)

            $(`#settlement-tr-${newSettlement.SettlementId}`).html(`<td><div class="hstack gap-3 fs-13 justify-content-center">
                                               <a href="javascript:void(0);" onclick='setEditSettlement(${newSettlement.SettlementId})' class="text-success">Edit</a>
                                               <a href="javascript:void(0);" class="text-success btn-remove-settlement" data-id='${newSettlement.SettlementId}'>Delete</a>
                                           </div></td>
<td>${appHelper.formatPrice(newSettlement.Amount)}</td>
<td>${appHelper.formatPrice(newSettlement.TotalPct)}</td>
<td>${appHelper.formatPrice(newSettlement.TotalPctAmount)}</td>
<td>${newSettlement.PaymentDate}</td>
<td>${bank.Text}</td>
<td>${newSettlement.BankAccountNumber}</td>
<td>${newSettlement.DueDate}</td>
<td>${newSettlement.FacilitiesId == <%= csa.Library.Constant.OTHER_NUMBER %> ? newSettlement.FacilitiesOther : vm.Facilities.find(x => x.Value == newSettlement.FacilitiesId).Text}</td>
<td>${appHelper.formatPrice(newSettlement.AmountFacilities)}</td>
<td>${newSettlement.SettlementStatusId == '1' ? 'Uncollected' : 'Collected'}</td>
<td>${newSettlement.Remark}</td>
<td>${newSettlement.Admin}</td>
</tr>`)

            $('#modalEditSettlement').modal('hide')
        })

        function setEditSettlement(id) {
            const c = listSettlement.find(x => x.SettlementId == id);
            $('#hidSettlementId').val(id)
            txtUpdateSettlementAmount.val(c.Amount)
            txtUpdateSettlementAmountPct.val(c.TotalPct)
            txtUpdateSettlementTotalAmount.val(c.TotalPctAmount)
            txtUpdateSettlementPaymentDate.data('datepicker').setDate(appHelper.convertToDate(appHelper.convertToApiDate(c.PaymentDate)))
            ddlUpdateSettlementBank.val(c.BankId)
            txtUpdateSettlementBankAccountNumber.val(c.BankAccountNumber)
            txtUpdateSettlementDueDate.data('datepicker').setDate(appHelper.convertToDate(appHelper.convertToApiDate(c.DueDate)))
            ddlUpdateSettlementFacilities.val(c.FacilitiesId)
            txtUpdateSettlementAmountFacilities.val(c.AmountFacilities)
            ddlUpdateSettlementFlexiCampaign.val(c.FlexyCampaignId)
            txtUpdateSettlementTotalCampaign.val(c.TotalCampaign)
            txtUpdateRedemptionLetterDate.data('datepicker').setDate(appHelper.convertToDate(appHelper.convertToApiDate(c.RedemptionLetterDate)))
            txtUpdateRedemptionAmount.val(c.RedemptionAmount)
            txtUpdateLoanReleaseDate.data('datepicker').setDate(appHelper.convertToDate(appHelper.convertToApiDate(c.LoanReleaseDate)))
            ddlUpdateSettlementStatus.val(c.SettlementStatusId)
            txtUpdateSettlementRemark.val(c.Remark)

            txtUpdateSettlementFacilitiesOther.val(c.FacilitiesOther)
            txtUpdateSettlementFlexiCampaignOther.val(c.FlexyCampaignOther)

            ddlUpdateSettlementBank.trigger('change')
            ddlUpdateSettlementFlexiCampaign.trigger('change')
            ddlUpdateSettlementFacilities.trigger('change')
            ddlUpdateSettlementStatus.trigger('change')
            $('#modalEditSettlement').modal('show')
        }

        function appendCaseUpdate(newCase) {
            const vm = vms[6]
            listCaseUpdate.push(newCase)

            const bank = vm.VtBanks.find(x => x.Key == newCase.BankId)

            const content = `<tr id='caseupdate-tr-${newCase.CaseUpdateId}'>
<td><div class="hstack gap-3 fs-13 justify-content-center">
                                               <a href="javascript:void(0);" onclick='setEditCaseUpdate(${newCase.CaseUpdateId})' class="text-success">Edit</a>
                                               <a href="javascript:void(0);" class="text-success btn-remove-caseupdate" data-id='${newCase.CaseUpdateId}'>Delete</a>
                                           </div></td>
<td>${bank.Text}</td>
<td>${appHelper.formatPrice(newCase.LoanAmount)}</td>
<td>${newCase.SubmitDate}</td>
<td>${newCase.Banker}</td>
<td>${newCase.Consolidate}</td>
<td>${appHelper.formatPrice(newCase.CashNet)}</td>
<td>${appHelper.formatPrice(newCase.Installment)}</td>
<td>${newCase.ApprovedDate}</td>
<td>${newCase.DisbursementDate}</td>
<td>${newCase.Remarkds}</td>
<td>${newCase.Admin}</td>
</tr>`
            $('#tableCaseUpdate tbody').append(content)
        }

        function appendLoanInProgress(newCase) {
            const vm = vms[6]
            const bank = vm.VtBanks.find(x => x.Key == newCase.BankId)

            const content = `<tr id='loan-in-progress-tr-${newCase.CaseUpdateId}'>
<td>${bank.Text}</td>
<td>${appHelper.formatPrice(newCase.LoanAmount)}</td>
<td>${newCase.SubmitDate}</td>
<td>${newCase.Banker}</td>
<td>${newCase.Consolidate}</td>
<td>${appHelper.formatPrice(newCase.CashNet)}</td>
<td>${appHelper.formatPrice(newCase.Installment)}</td>
<td>${newCase.ApprovedDate}</td>
<td>${newCase.DisbursementDate}</td>
<td>${newCase.Remarkds}</td>
<td>${newCase.Admin}</td>
</tr>`
            $('#tableLoanInProgress tbody').append(content)
        }

        function clearInputSettlement() {
            txtSettlementAmount.val('')
            txtSettlementAmountPct.val('')
            txtSettlementTotalAmount.val('')
            txtSettlementPaymentDate.val('')
            ddlSettlementBank.prop('selectedIndex', 0)
            txtSettlementBankAccountNumber.val('')
            txtSettlementDueDate.val('')
            ddlSettlementFacilities.prop('selectedIndex', 0)
            txtSettlementFacilitiesOther.val('')
            txtSettlementAmountFacilities.val('')
            ddlSettlementFlexiCampaign.prop('selectedIndex', 0)
            txtSettlementFlexiCampaignOther.val('')
            txtSettlementTotalCampaign.val('')
            txtRedemptionLetterDate.val('')
            txtRedemptionAmount.val('')
            txtLoanReleaseDate.val('')
            ddlSettlementStatus.prop('selectedIndex', 0)
            txtSettlementRemark.val('')

            ddlSettlementBank.trigger('change')
            ddlSettlementFlexiCampaign.trigger('change')
            ddlSettlementStatus.trigger('change')
            ddlSettlementFacilities.trigger('change')
        }

        function clearCaseUpdateInput() {
            ddlCuBank.prop('selectedIndex',0);
            txtCuLoanAmount.val('');
            txtCuSubmitDate.val('');
            txtCuBanker.val('');
            ddlCuStatus.prop('selectedIndex',0);
            txtCuConsolidate.val('');
            txtCuCashNet.val('');
            txtCuInstallment.val('');
            txtCuApprovedDate.val('');
            txtCuSignDate.val('');
            txtCuDisbursementDate.val('');
            txtCuUpdateDate.val('');
            txtCuLoanAccountNumber.val('');
            txtCuFirstDueDate.val('');
            txtCuRemark.val('');

            ddlCuBank.trigger('change')
            ddlCuStatus.trigger('change')
        }

        function doSavePreChecking() {
            const markId = 'docprechecking'
            const formData = new FormData()
            let remarks = []
            const status = 1
            listDocumentPreChecking.forEach(i => {
                formData.append('FileAdditionalDocuments', $(`#upld${markId}_${i}_${status}`)[0].files[0])
                remarks.push($(`#txtremark${markId}_${i}_${status}`).val())
            })


            let addDocModify = []
            $('.doc-remark-editable').each(function (index) {
                if ($(this).data('status') == status) {
                    addDocModify.push({
                        ApplicationDocumentId: $(this).data('id'),
                        Remark: $(this).val()
                    })
                }
            })

            const data = {
                ApplicationId: <%=CurrentApplicationId%>,
                AdminId: <%= CurrentLoginAdmin?.AdminId %>,
                Eligibility: $('#ddleligibility').val(),
                LegalSuit: cbLegalSuits.is(':checked'),
                Bankruptcy: cbBankruptcy.is(':checked'),
                SpecialAttentionAccount: cbSpecialAttentionAccount.is(':checked'),
                BadPaymentRecordCheck: cbBadPaymentRecord.is(':checked'),
                AdditionalDocumentToModify: addDocModify,
                DocumentDelete: listDocumentToDelete,
                FileDelete: listFileToDelete,
                RemarkAdditionalDocuments: remarks,
                ApplicationRemark: txtApplicationRemarkPrechecking.val()
            }

            formData.append('RamciFile', $('#upldramci')[0].files[0])
            formData.append('CcrisFile', $('#upldccris')[0].files[0])
            formData.append('PayslipFile', $('#upldpayslip-prechecking')[0].files[0])
            formData.append('Json', JSON.stringify(data))

            return ApiHelper.postFormData(window.location.origin + '/application/PreChecking', formData)
        }


        $('#btnSavePreChecking').click(async function (e) {

            $(this).prop('disabled', true)
            
            try {
                const res = await doSavePreChecking()
                if (!res.data.Error) {
                    dialogHelper.successAutoRedirect("Save successfully", location.href)
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }            


            $(this).prop('disabled', false)
        })

        async function doCanNextApplicationStatus() {
            var resCheck = await ApiHelper.post(window.location.origin + '/application/CanNextApplicationStatus', { ApplicationId: <%=CurrentApplicationId%> })
            if (!resCheck.data.Error) {
                await doNextApplicationStatus();
            }
            else {
                if (resCheck.data.Code == -1000)//custom error
                {
                    dialogHelper.errorHTML(resCheck.data.Message)
                }
                else {
                    dialogHelper.confirmationHTML(`${resCheck.data.Message} <p>Do you wish to proceed?</p>`, async () => {
                        await doNextApplicationStatus();
                    })
                }
            }
        }

        async function doCanConvertHero() {
            var resCheck = await ApiHelper.post(window.location.origin + '/application/CanNextApplicationStatus', { ApplicationId: <%=CurrentApplicationId%> })
            if (!resCheck.data.Error) {
                await doConvertHero();
            }
            else {
                dialogHelper.confirmationHTML(`${resCheck.data.Message} <p>Do you wish to proceed?</p>`, async () => {
                    await doConvertHero();
                })
            }
        }

        async function doNextApplicationStatus() {
            try {
                var resIncreaseStatus = await ApiHelper.post(window.location.origin + '/application/NextApplicationStatus', { ApplicationId: <%=CurrentApplicationId%>, AdminId: <%= CurrentLoginAdmin.AdminId%> })
                if (!resIncreaseStatus.data.Error) {
                    dialogHelper.successAutoRedirect("Save successfully", location.href)
                }
                else {
                    dialogHelper.errorHTML(resIncreaseStatus.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }
        }

        async function doConvertHero() {
            try {
                var res = await ApiHelper.post(window.location.origin + '/application/ConvertHero', { ApplicationId: <%=CurrentApplicationId%>, AdminId: <%= CurrentLoginAdmin.AdminId%> })
                if (!res.data.Error) {
                    dialogHelper.successAutoRedirect("Save successfully", location.href)
                }
                else {
                    dialogHelper.errorHTML(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }
        }

        $('#btnNextPreChecking').click(async function (e) {

            $(this).prop('disabled', true)

            try {
                const res = await doSavePreChecking()
                if (!res.data.Error) {
                    try {
                        await doCanNextApplicationStatus()
                    } catch (e) {
                        dialogHelper.error(e.message)
                    }
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }

            $(this).prop('disabled', false)
        })

        function doSavePreparation() {

            const formData = new FormData()

            const data = {
                ApplicationId: <%=CurrentApplicationId%>,
                AdminId: <%= CurrentLoginAdmin?.AdminId %>,
                FileDelete: listFileToDelete,
                SalaryGross: txtSalaryGross.val(),
                SalaryDeduction: txtSalaryDeduction.val(),
                NetIncome: txtNetIncome.val(),
                B1: txtB1.val(),
                B2: txtB2.val(),
                B3: txtB3.val(),
                B4: txtB4.val(),
                BAverage: txtBAverage.val(),
                CommitmentOutstanding: txtCommitmentOutstanding.val(),
                CommitmentInstallment: txtCommitmentInstallment.val(),
                OtherNetBalance: txtOtherNetBalance.val(),
                OtherBPA: txtBPA.val(),
                OtherComparisonDSR: txtOtherComparionDSR.val(),
                OtherComparisonDSRPctCommitment: txtOtherComparisonDSRPctCommitment.val(),
                OtherPctRefresh: txtOtherPctRefresh.val(),
                OtherProposedRefresh: txtOtherProposedRefresh.val(),
                OtherCompositionDSR: txtOtherCompositionDSR.val(),
                OtherCompositionDSRPctCommitment: txtOtherCompositionDSRPctCommitment.val(),
                RefreshTotal: txtRefreshTotal.val(),
                RefreshRemainCommitment: txtRefreshRemainCommitment.val(),
                ReloanTotal: txtReloanTotal.val(),
                ReloanMonthly: txtReloanMonthy.val(),
                ReloanBersih: txtReloanBersih.val(),
                ReloanBelanja: txtReloanBelanja.val(),
                ReloanDeposit: txtReloanDeposit.val(),
                ReloanDanaBantuan: txtReloanDanaBantuan.val(),
                ReloanServiceFee: txtReloanServiceFee.val(),
                ReloanServiceFeePct: txtReloanServiceFeePct.val(),
                ReloanIncomeAfterRNR: txtReloanIncomeAfterRNR.val(),
                ReloanDifference: txtReloanDifference.val(),
                ModelBackgroundScreeningId: ddlModelBackgroundScreening.val(),
                ModelCompositionDSRId: ddlModelCompositionDSR.val(),
                ModelCommitmentId: ddlModelCommitment.val(),
                ModelSettlementId: ddlModelSettlement.val(),
                ModelServiceFeeId: ddlModelServiceFee.val(),
                ModelNetIncomeAfterRNRId: ddlModelNetIncomeAfterRNR.val(),
                ModelStatusId: ddlModelStatus.val(),
                ModelStatusProposalId: ddlModelStatusProposal.val(),
                ModelCheckId: ddlModelCheck.val(),
                ReviewAdminId: ddlReviewAdmin.val(),
                ReviewStatusId: ddlReviewStatus.val(),
                ReviewDate: appHelper.convertToApiDate(txtReviewDate.val()),
                ReviewComment: txtReviewComment.val(),
                ApproveAdminId: ddlApproveAdmin.val(),
                ApproveStatusId: ddlApproveStatus.val(),
                ApproveDate: appHelper.convertToApiDate(txtApproveDate.val()),
                ApproveComment: txtApproveComment.val(),
                ApplicationRemark: txtApplicationRemarkPreparation.val(),
                VerifiedAdminId: ddlVerifiedAdmin.val(),
                VerifiedStatusId: ddlVerifiedStatus.val(),
                VerifiedDate: appHelper.convertToApiDate(txtVerifiedDate.val()),
                VerifiedComment: txtVerifiedComment.val(),
                CreditRemark: txtCreditRemarkProposalPreparation.val(),
            }

            formData.append('ProposalFile', $('#upldproposal')[0].files[0])
            formData.append('Json', JSON.stringify(data))

            return ApiHelper.postFormData(window.location.origin + '/application/ProposalPreparation', formData)
        }        

        $('#btnSaveProposalPreparation').click(async function (e) {
            $(this).prop('disabled', true)

            try {
                const res = await doSavePreparation()
                if (!res.data.Error) {
                    dialogHelper.successAutoRedirect("Save successfully", location.href)
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }


            $(this).prop('disabled', false)
        })

        $('#btnNextPreparation').click(async function (e) {

            $(this).prop('disabled', true)

            try {
                const res = await doSavePreparation()
                if (!res.data.Error) {
                    try {
                        await doCanNextApplicationStatus()
                    } catch (e) {
                        dialogHelper.error(e.message)
                    }
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }

            $(this).prop('disabled', false)
        })

        function doSaveProposal() {
            const markId = 'docprechecking'
            const formData = new FormData()
            let remarks = []
            const status = 3
            listDocumentPreChecking.forEach(i => {
                formData.append('FileAdditionalDocuments', $(`#upld${markId}_${i}_${status}`)[0].files[0])
                remarks.push($(`#txtremark${markId}_${i}_${status}`).val())
            })


            let addDocModify = []
            $('.doc-remark-editable').each(function (index) {
                if ($(this).data('status') == status) {
                    addDocModify.push({
                        ApplicationDocumentId: $(this).data('id'),
                        Remark: $(this).val()
                    })
                }
            })

            const data = {
                ApplicationId: <%=CurrentApplicationId%>,
                AdminId: <%= CurrentLoginAdmin?.AdminId %>,
                ProposalStatusId: ddlproposalstatus.val(),
                AdditionalDocumentToModify: addDocModify,
                DocumentDelete: listDocumentToDelete,
                RemarkAdditionalDocuments: remarks,
                ApplicationRemark: txtApplicationRemarkPresentation.val()
            }

            formData.append('Json', JSON.stringify(data))

            return ApiHelper.postFormData(window.location.origin + '/application/ProposalPresentation', formData)
        }

        $('#btnSaveProposalPresentation').click(async function (e) {
            $(this).prop('disabled', true)

            try {
                const res = await doSaveProposal()
                if (!res.data.Error) {
                    dialogHelper.successAutoRedirect("Save successfully", location.href)
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }


            $(this).prop('disabled', false)
        })

        $('#btnNextProposalPresentation').click(async function (e) {

            $(this).prop('disabled', true)

            try {
                const res = await doSaveProposal()
                if (!res.data.Error) {
                    try {
                        await doCanNextApplicationStatus()
                    } catch (e) {
                        dialogHelper.error(e.message)
                    }
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }

            $(this).prop('disabled', false)
        })

        function doSavePresign() {
            const markId = 'docprechecking'
            const formData = new FormData()
            let remarks = []
            const status = 4
            listDocumentPreChecking.forEach(i => {
                formData.append('FileAdditionalDocuments', $(`#upld${markId}_${i}_${status}`)[0].files[0])
                remarks.push($(`#txtremark${markId}_${i}_${status}`).val())
            })


            let addDocModify = []
            $('.doc-remark-editable').each(function (index) {
                if ($(this).data('status') == status) {
                    addDocModify.push({
                        ApplicationDocumentId: $(this).data('id'),
                        Remark: $(this).val()
                    })
                }
            })

            const data = {
                ApplicationId: <%=CurrentApplicationId%>,
                AdminId: <%= CurrentLoginAdmin?.AdminId %>,
                ProposalSendId: ddlproposalsend.val(),
                SuratAkuanId: ddlsuratakuan.val(),
                ComprehensiveFormId: ddlcomprehensive.val(),
                AdditionalDocumentToModify: addDocModify,
                DocumentDelete: listDocumentToDelete,
                RemarkAdditionalDocuments: remarks,
                ApplicationRemark: txtApplicationRemarkPresign.val()
            }

            formData.append('Json', JSON.stringify(data))

            return ApiHelper.postFormData(window.location.origin + '/application/PreSigning', formData)
        }

        $('#btnSavePresigning').click(async function (e) {
            $(this).prop('disabled', true)

            try {
                const res = await doSavePresign()
                if (!res.data.Error) {
                    dialogHelper.successAutoRedirect("Save successfully", location.href)
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }


            $(this).prop('disabled', false)
        })

        $('#btnNextPresigning').click(async function (e) {

            $(this).prop('disabled', true)

            try {
                const res = await doSavePresign()
                if (!res.data.Error) {
                    try {
                        await doCanNextApplicationStatus()
                    } catch (e) {
                        dialogHelper.error(e.message)
                    }
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }

            $(this).prop('disabled', false)
        })

        function doSaveZoomAcceptance() {
            const markId = 'docprechecking'
            const formData = new FormData()
            let remarks = []
            const status = 5
            listDocumentPreChecking.forEach(i => {
                formData.append('FileAdditionalDocuments', $(`#upld${markId}_${i}_${status}`)[0].files[0])
                remarks.push($(`#txtremark${markId}_${i}_${status}`).val())
            })


            let addDocModify = []
            $('.doc-remark-editable').each(function (index) {
                if ($(this).data('status') == status) {
                    addDocModify.push({
                        ApplicationDocumentId: $(this).data('id'),
                        Remark: $(this).val()
                    })
                }
            })

            const data = {
                ApplicationId: <%=CurrentApplicationId%>,
                AdminId: <%= CurrentLoginAdmin?.AdminId %>,
                ApplicantAddress: txtApplicantAddress.val(),
                BankruptcyStatus: ddlBankruptcyStatusZoom.val(),
                LegalCase: ddlLegalCaseZoom.val(),
                HealthCreditScore: ddlHealthCreditScoreZoom.val(),
                Commitments: txtCommitments.val(),
                AdditionalDocumentToModify: addDocModify,
                DocumentDelete: listDocumentToDelete,
                FileDelete: listFileToDelete,
                RemarkAdditionalDocuments: remarks,
                ApplicationRemark: txtApplicationRemarkPendingZoomAndAtteptance.val()
            }
            formData.append('PaySlipFile', $('#upldpayslip')[0].files[0])
            formData.append('RamciFile', $('#upldramci2')[0].files[0])
            formData.append('CtosFile', $('#upldctos')[0].files[0])
            formData.append('RedemptionLetterFile', $('#upldredemptionletter')[0].files[0])
            formData.append('Json', JSON.stringify(data))

            return ApiHelper.postFormData(window.location.origin + '/application/PendingZoomAtteptance', formData)
        }

        $('#btnSaveZoom').click(async function (e) {
            $(this).prop('disabled', true)

            try {
                const res = await doSaveZoomAcceptance()
                if (!res.data.Error) {
                    dialogHelper.successAutoRedirect("Save successfully", location.href)
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }


            $(this).prop('disabled', false)
        })

        $('#btnNextZoom').click(async function (e) {

            $(this).prop('disabled', true)

            try {
                const res = await doSaveZoomAcceptance()
                if (!res.data.Error) {
                    try {
                        await doCanNextApplicationStatus()
                    } catch (e) {
                        dialogHelper.error(e.message)
                    }
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }

            $(this).prop('disabled', false)
        })

        $('#btnSavePendingAtteptance').click(async function (e) {
            $(this).prop('disabled', true)

            const res = await ApiHelper.postFormData(window.location.origin + '/application/PendingZoomAtteptance', null)
            if (!res.data.Error) {
                //to do write code
            } else {
                dialogHelper.error(res.data.Message)
            }


            $(this).prop('disabled', false)
        })

        function doSaveSettlement() {
            const markId = 'docprechecking'
            const formData = new FormData()
            let remarks = []
            const status = 6
            listDocumentPreChecking.forEach(i => {
                formData.append('FileAdditionalDocuments', $(`#upld${markId}_${i}_${status}`)[0].files[0])
                remarks.push($(`#txtremark${markId}_${i}_${status}`).val())
            })


            let addDocModify = []
            $('.doc-remark-editable').each(function (index) {
                if ($(this).data('status') == status) {
                    addDocModify.push({
                        ApplicationDocumentId: $(this).data('id'),
                        Remark: $(this).val()
                    })
                }
            })

            listCaseUpdate.forEach(item => {
                //convert to ymd
                item.SubmitDate = appHelper.convertToApiDate(item.SubmitDate)
                item.ApprovedDate = appHelper.convertToApiDate(item.ApprovedDate)
                item.SignDate = appHelper.convertToApiDate(item.SignDate)
                item.DisbursementDate = appHelper.convertToApiDate(item.DisbursementDate)
                item.UpdateDate = appHelper.convertToApiDate(item.UpdateDate)
                item.FirstDueDate = appHelper.convertToApiDate(item.FirstDueDate)
            })

            listSettlement.forEach(item => {
                //convert to ymd
                item.PaymentDate = appHelper.convertToApiDate(item.PaymentDate)
                item.DueDate = appHelper.convertToApiDate(item.DueDate)
                item.RedemptionLetterDate = appHelper.convertToApiDate(item.RedemptionLetterDate)
                item.LoanReleaseDate = appHelper.convertToApiDate(item.LoanReleaseDate)
            })

            const data = {
                ApplicationId: <%=CurrentApplicationId%>,
                AdminId: <%= CurrentLoginAdmin?.AdminId %>,
                AdditionalDocumentToModify: addDocModify,
                DocumentDelete: listDocumentToDelete,
                FileDelete: listFileToDelete,
                RemarkAdditionalDocuments: remarks,
                SettlementDetails: listSettlement,
                CaseUpdates: listCaseUpdate,
                SettlementDelete: listSettlementToDelete,
                CaseUpdateDelete: listCaseUpdateToDelete,
                ApplicationRemark: txtApplicationRemarkSettlement.val()
            }
            formData.append('SettlementFile', $('#upldsettlement')[0].files[0])
            formData.append('Json', JSON.stringify(data))

            return ApiHelper.postFormData(window.location.origin + '/application/Settlement', formData)
        }

        function doConvertToHero() {
            const data = {
                ApplicationId: <%=CurrentApplicationId%>,
                AdminId: <%= CurrentLoginAdmin?.AdminId %>,
            }

            return ApiHelper.postFormData(window.location.origin + '/application/ConvertHero', data)
        }

        $('#btnNextSettlement').click(async function (e) {

            $(this).prop('disabled', true)

            try {
                const res = await doSaveSettlement()
                if (!res.data.Error) {
                    try {
                        await doCanNextApplicationStatus()
                    } catch (e) {
                        dialogHelper.errorHTML(e.message)
                    }
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }

            $(this).prop('disabled', false)
        })

        $('#btnSaveSettlement').click(async function (e) {
            $(this).prop('disabled', true)

           try {
                const res = await doSaveSettlement()
                if (!res.data.Error) {
                    dialogHelper.successAutoRedirect("Save successfully", location.href)
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
           }


            $(this).prop('disabled', false)
        })

        $('#btnConvertToHero').on('click', async function () {
            $(this).prop('disabled', true)

            try {
                const res = await doConvertToHero()
                if (!res.data.Error) {
                    dialogHelper.successAutoRedirect("Convert to hero successfully", location.href)
                } else {
                    dialogHelper.errorHTML(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }


            $(this).prop('disabled', false)
        })

        function doSaveCcris() {
            const markId = 'docprechecking'
            const formData = new FormData()
            let remarks = []
            const status = 7
            listDocumentPreChecking.forEach(i => {
                formData.append('FileAdditionalDocuments', $(`#upld${markId}_${i}_${status}`)[0].files[0])
                remarks.push($(`#txtremark${markId}_${i}_${status}`).val())
            })


            let addDocModify = []
            $('.doc-remark-editable').each(function (index) {
                if ($(this).data('status') == status) {
                    addDocModify.push({
                        ApplicationDocumentId: $(this).data('id'),
                        Remark: $(this).val()
                    })
                }
            })

            const data = {
                ApplicationId: <%=CurrentApplicationId%>,
                AdminId: <%= CurrentLoginAdmin?.AdminId %>,
                AdditionalDocumentToModify: addDocModify,
                DocumentDelete: listDocumentToDelete,
                FileDelete: listFileToDelete,
                RemarkAdditionalDocuments: remarks,
                ApplicationRemark: txtApplicationRemarkCcris.val(),
                BankruptcyStatus: ddlBankruptcyStatusCcris.val(),
                LegalCase: ddlLegalCaseCcris.val(),
                HealthCreditScore: ddlHealthCreditScoreCcris.val(),
                Commitments: txtCommitmentsCcris.val()
            }
            formData.append('ReleaseLetterFile', $('#upldreleaseletter')[0].files[0])
            formData.append('CcrisReportFile', $('#upldccrisreport')[0].files[0])
            formData.append('HrmisFile', $('#upldhrmis-ccris')[0].files[0])
            formData.append('AnmFile', $('#upldanm-ccris')[0].files[0])
            formData.append('LpsaFile', $('#upldlpsa-ccris')[0].files[0])
            formData.append('AngkasaFile', $('#upldangkasa-ccris')[0].files[0])
            formData.append('Json', JSON.stringify(data))

            return ApiHelper.postFormData(window.location.origin + '/application/CcrisCleaning', formData)
        }

        $('#btnSaveCcris').click(async function (e) {
            $(this).prop('disabled', true)

            try {
                const res = await doSaveCcris()
                if (!res.data.Error) {
                    dialogHelper.successAutoRedirect("Save successfully", location.href)
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }

            $(this).prop('disabled', false)
        })

        $('#btnNextCcris').click(async function (e) {

            $(this).prop('disabled', true)

            try {
                const res = await doSaveCcris()
                if (!res.data.Error) {
                    try {
                        await doCanNextApplicationStatus()
                    } catch (e) {
                        dialogHelper.error(e.message)
                    }
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }

            $(this).prop('disabled', false)
        })

        function doSaveQueue() {
            const markId = 'docprechecking'
            const formData = new FormData()
            let remarks = []
            const status = 8
            listDocumentPreChecking.forEach(i => {
                formData.append('FileAdditionalDocuments', $(`#upld${markId}_${i}_${status}`)[0].files[0])
                remarks.push($(`#txtremark${markId}_${i}_${status}`).val())
            })

            listPayslip.forEach(i => {
                formData.append('Payslips', $(`#upldfilepayslip${i}`)[0].files[0])
            })

            listBankStatement.forEach(i => {
                formData.append('BankStatements', $(`#upldfilebankstatement${i}`)[0].files[0])
            })

            let addDocModify = []
            $('.doc-remark-editable').each(function (index) {
                if ($(this).data('status') == status) {
                    addDocModify.push({
                        ApplicationDocumentId: $(this).data('id'),
                        Remark: $(this).val()
                    })
                }
            })

            const data = {
                ApplicationId: <%=CurrentApplicationId%>,
                AdminId: <%= CurrentLoginAdmin?.AdminId %>,
                AdditionalDocumentToModify: addDocModify,
                DocumentDelete: listDocumentToDelete,
                FileDelete: listFileToDelete,
                RemarkAdditionalDocuments: remarks,
                WorkerTypeId: $('#ddlWorkerType').val(),
                AppFileDelete: listApplicationFileDelete,
                ApplicationRemark: txtApplicationRemarkQueue.val()
            }
            formData.append('IdentityFile', $('#upldidentity')[0].files[0])
            formData.append('PaySlipFile', $('#upldpayslipqueue')[0].files[0])
            formData.append('EcFile', $('#upldec')[0].files[0])
            formData.append('HrmisFile', $('#upldhrmis')[0].files[0])
            formData.append('BankStatementFile', $('#upldbankstatement')[0].files[0])
            formData.append('LppsaFile', $('#upldlppsa')[0].files[0])
            formData.append('LicenseFile', $('#upldlicense')[0].files[0])


            formData.append('RedemptionLetterFile', $('#upldredemptionletterqueue')[0].files[0])
            formData.append('CreditCardFile', $('#upldcc')[0].files[0])
            formData.append('RamciFile', $('#upldramciqueue')[0].files[0])
            formData.append('SignatureFile', $('#upldsignature')[0].files[0])
            formData.append('BiroAngkasaFile', $('#upldbiro')[0].files[0])
            formData.append('Kew320File', $('#upldkew320')[0].files[0])
            formData.append('StaffCardFile', $('#upldstaffcard')[0].files[0])
            formData.append('PostDatedChequeFile', $('#upldpostdatedcheque')[0].files[0])
            formData.append('CompanyConfirmationFile', $('#upldcompany')[0].files[0])
            formData.append('EpfFile', $('#upldepf')[0].files[0])
            formData.append('EaformFile', $('#upldeaform')[0].files[0])
            formData.append('BillUtilitiesFile', $('#upldbillutilities')[0].files[0])
            formData.append('Json', JSON.stringify(data))

            return ApiHelper.postFormData(window.location.origin + '/application/QueueForLoan', formData)
        }

        $('#btnSaveQueue').click(async function (e) {
            $(this).prop('disabled', true)

            try {
                const res = await doSaveQueue()
                if (!res.data.Error) {
                    dialogHelper.successAutoRedirect("Save successfully", location.href)
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }

            $(this).prop('disabled', false)
        })


        $('#btnNextQueue').click(async function (e) {

            $(this).prop('disabled', true)

            try {
                const res = await doSaveQueue()
                if (!res.data.Error) {
                    try {
                        await doCanNextApplicationStatus()
                    } catch (e) {
                        dialogHelper.error(e.message)
                    }
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }

            $(this).prop('disabled', false)
        })

        function doSaveReloan() {
            const markId = 'docprechecking'
            const formData = new FormData()
            let remarks = []
            const status = 9
            listDocumentPreChecking.forEach(i => {
                formData.append('FileAdditionalDocuments', $(`#upld${markId}_${i}_${status}`)[0].files[0])
                remarks.push($(`#txtremark${markId}_${i}_${status}`).val())
            })


            let addDocModify = []
            $('.doc-remark-editable').each(function (index) {
                if ($(this).data('status') == status) {
                    addDocModify.push({
                        ApplicationDocumentId: $(this).data('id'),
                        Remark: $(this).val()
                    })
                }
            })

            const data = {
                ApplicationId: <%=CurrentApplicationId%>,
                AdminId: <%= CurrentLoginAdmin?.AdminId %>,
                AdditionalDocumentToModify: addDocModify,
                DocumentDelete: listDocumentToDelete,
                FileDelete: listFileToDelete,
                RemarkAdditionalDocuments: remarks,
                ReloanStatusId: ddlReloanStatus.val(),
                ApprovedDate: appHelper.convertToApiDate(txtApprovedDate.val()),
                SigningDate: appHelper.convertToApiDate(txtSigningDate.val()),
                ApprovedAmount: txtApprovedAmount.val(),
                ApplicationRemark: txtApplicationRemarkReloan.val()
            }            
            formData.append('OfferLetterFile', $('#upldofferletter')[0].files[0])
            formData.append('Json', JSON.stringify(data))

            return ApiHelper.postFormData(window.location.origin + '/application/Reloan', formData)
        }

        $('#btnSaveReloan').click(async function (e) {
            $(this).prop('disabled', true)

            try {
                const res = await doSaveReloan()
                if (!res.data.Error) {
                    dialogHelper.successAutoRedirect("Save successfully", location.href)
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }

            $(this).prop('disabled', false)
        })

        $('#btnNextReloan').click(async function (e) {

            $(this).prop('disabled', true)

            try {
                const res = await doSaveReloan()
                if (!res.data.Error) {
                    try {
                        await doCanNextApplicationStatus()
                    } catch (e) {
                        dialogHelper.error(e.message)
                    }
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }

            $(this).prop('disabled', false)
        })

        function doSaveCollection() {
            const markId = 'docprechecking'
            const formData = new FormData()
            let remarks = []
            const status = 10
            listDocumentPreChecking.forEach(i => {
                formData.append('FileAdditionalDocuments', $(`#upld${markId}_${i}_${status}`)[0].files[0])
                remarks.push($(`#txtremark${markId}_${i}_${status}`).val())
            })


            let addDocModify = []
            $('.doc-remark-editable').each(function (index) {
                if ($(this).data('status') == status) {
                    addDocModify.push({
                        ApplicationDocumentId: $(this).data('id'),
                        Remark: $(this).val()
                    })
                }
            })

            const data = {
                ApplicationId: <%=CurrentApplicationId%>,
                AdminId: <%= CurrentLoginAdmin?.AdminId %>,
                AdditionalDocumentToModify: addDocModify,
                DocumentDelete: listDocumentToDelete,
                FileDelete: listFileToDelete,
                RemarkAdditionalDocuments: remarks,
                DepositAmount: txtDepositAmount.val(),
                ServiceFee: txtServiceFee.val(),
                DepositDate: appHelper.convertToApiDate(txtDepositDate.val()),
                ApplicationRemark: txtApplicationRemarkCollection.val(),
                SettlementDate: appHelper.convertToApiDate(txtSettlementDate.val()),
                DBBAgreementDate: appHelper.convertToApiDate(txtDBBAgreementDate.val()),
                InstallmentDate: appHelper.convertToApiDate(txtInstallmentDate.val()),
                SettlementAmount: txtSettlementCollectionAmount.val(),
                SettlementCPct: txtSettlementCPct.val(),
                CollectionAmountPct: txtCollectionAmountPct.val(),
                SettlementDuration: txtSettlementDuration.val(),
                TotalReloan: txtTotalReloan.val(),
                TotalLoanRepayment: txtTotalLoanRepayment.val(),
                DBBBankAccount: txtDBBBankAccount.val(),
                DBBTenure: txtDBBTenure.val(),
                MonthlyFund: txtMonthlyFund.val(),
                DBBAmount: txtDBBAmount.val(),
                ReceiptNo: txtReceiptNo.val(),
                TaxNumber: txtTaxNumber.val(),
                StatusId: ddlCollectionStatusId.val()
            }
            formData.append('DeclarationFile', $('#uplddeclaration')[0].files[0])
            formData.append('SettlementFile', $('#upldsettlementreceipt')[0].files[0])
            formData.append('ServiceFeeFile', $('#upldservicefee')[0].files[0])
            formData.append('RezekiFile', $('#upldrezekireceipt')[0].files[0])
            formData.append('RezekiAgreementFile', $('#upldrezekiagreement')[0].files[0])
            formData.append('Json', JSON.stringify(data))

            return ApiHelper.postFormData(window.location.origin + '/application/Collection', formData)
        }

        $('#btnSaveCollection').click(async function (e) {
            $(this).prop('disabled', true)

            try {
                const res = await doSaveCollection()
                if (!res.data.Error) {
                    dialogHelper.successAutoRedirect("Save successfully", location.href)
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }

            $(this).prop('disabled', false)
        })

        $('#btnNextCollection').click(async function () {
            $(this).prop('disabled', true)

            try {
                const res = await doSaveCollection()
                if (!res.data.Error) {
                    try {
                        await doCanConvertHero()
                    } catch (e) {
                        dialogHelper.error(e.message)
                    }
                } else {
                    dialogHelper.error(res.data.Message)
                }
            } catch (e) {
                dialogHelper.error(e.message)
            }

            $(this).prop('disabled', false)
        })

        $('#btnConvertToWira').click(async function (e) {
            $(this).prop('disabled', true)

            const res = await ApiHelper.postFormData(window.location.origin + '/application/Collection', null)
            if (!res.data.Error) {
                //to do write code
            } else {
                dialogHelper.error(res.data.Message)
            }


            $(this).prop('disabled', false)
        })

        $('#btnSaveInfo').click(async function () {
            $(this).prop('disabled', true)

            const data = {
                ApplicationId: <%=CurrentApplicationId%>,
                AdminId: <%=CurrentLoginAdmin.AdminId%>,
                SourceId: ddlSource.val(),
                CreditStatusId: ddlCreditStatus.val(),
                CustomerStatusId: ddlCustomerStatus.val(),
                BurstReasonId: ddlBurstReason.val(),
                CreditRemark: txtCreditRemark.val(),
                ScoreClass: txtScoreClass.val(),
                ApplicationStatusId: ddlCurrentStatus.val()
            }
            const res = await ApiHelper.postFormData(window.location.origin + '/application/SaveApplicationInfo', data)
            if (!res.data.Error) {
                dialogHelper.successAutoRedirect("Save successfully", location.href)
            } else {
                dialogHelper.errorHTML(res.data.Message)
            }


            $(this).prop('disabled', false)
        })

        $(document).on('click', '.btn-download-file', function () {
            // Create an anchor element and trigger the download
            const a = document.createElement('a');
            a.href = $(this).data('download');
            //a.download = ; // Specify the file name
            document.body.appendChild(a); // Append to body (needed for Firefox)
            a.click(); // Trigger the download
            document.body.removeChild(a); // Clean up
            URL.revokeObjectURL(a.href); // Free up memory
        })

        $(document).on('click', '.btn-remove-application-file', function () {
            const id = $(this).data('id')
            if (id > 0) {
                dialogHelper.confirmation('Are you sure to delete?', () => {
                    $(`.applicationfile${id}`).remove()
                    listApplicationFileDelete.push(id)
                })
            }
            else {
                $(`.applicationfile${id}`).remove()
                listPayslip = listPayslip.filter(num => num !== id);
                listBankStatement = listBankStatement.filter(num => num !== id);
            }
        });

        $(document).on('click', '.btn-remove-additional-document', function () {
            const id = $(this).data('id')
            if (id > 0) {
                dialogHelper.confirmation('Are you sure to delete?', () => {
                    $(`.docprechecking-${id}`).remove()                    
                    listDocumentToDelete.push(id)
                })
            }
            else {
                $(`.docprechecking-${id}`).remove()
                listDocumentPreChecking = listDocumentPreChecking.filter(num => num !== id);
            }
        });

        $('.btn-application-reject').click(async function () {

            Swal.fire({
                title: "Reject Reason",
                input: "text",
                inputAttributes: {
                    autocapitalize: "off"
                },
                showCancelButton: true,
                confirmButtonText: "Submit",
            }).then(async (result) => {
                if (result.isConfirmed) {
                    $(this).prop('disabled', true)

                    const data = {
                        ApplicationId: <%=CurrentApplicationId%>,
                        AdminId: <%= CurrentLoginAdmin.AdminId %>,
                        RejectReason: result.value
                    }

                    const res = await ApiHelper.postFormData(window.location.origin + '/application/Reject', data)
                    if (!res.data.Error) {
                        dialogHelper.successAutoRedirect("Reject successfully", location.href)
                    } else {
                        dialogHelper.error(res.data.Message)
                    }
                    $(this).prop('disabled', false)
                }
            });
        })

        $('.btn-application-cancel-reject').click(async function () {
            dialogHelper.confirmation('Are you sure to cancel reject?', async () => {
                $(this).prop('disabled', true)

                const data = {
                    ApplicationId: <%=CurrentApplicationId%>,
                    AdminId: <%= CurrentLoginAdmin.AdminId %>,
                }

                const res = await ApiHelper.postFormData(window.location.origin + '/application/CancelReject', data)
                if (!res.data.Error) {
                    dialogHelper.successAutoRedirect("Cancel Reject successfully", location.href)
                } else {
                    dialogHelper.error(res.data.Message)
                }


                $(this).prop('disabled', false)
            })
        })

        $(document).on('click', '.btn-remove-settlement', function () {
            const id = $(this).data('id')
            if (id > 0) {
                dialogHelper.confirmation('Are you sure to delete?', () => {
                    $(`#settlement-tr-${id}`).remove()
                    listSettlementToDelete.push(id)
                    updateSettlementTable()
                })
            }
            else {
                $(`#settlement-tr-${id}`).remove()
                listSettlement = listSettlement.filter(x => x.SettlementId !== id);
                updateSettlementTable()
            }            
        });

        $(document).on('click', '.btn-remove-caseupdate', function () {
            const id = $(this).data('id')
            if (id > 0) {
                dialogHelper.confirmation('Are you sure to delete?', () => {
                    $(`#caseupdate-tr-${id}`).remove()
                    listCaseUpdateToDelete.push(id)
                    updateSettlementTable()
                })
            }
            else {
                $(`#caseupdate-tr-${id}`).remove()
                listCaseUpdate = listCaseUpdate.filter(x => x.CaseUpdateId !== id);
                updateSettlementTable()
            }
        });

        $(document).on('click', '.btn-delete-file', function () {
            dialogHelper.confirmation('Are you sure to delete?', () => {
                const id = $(this).data('id')
                const field = $(this).data('field')

                var parent = $(this).parent()
                parent.addClass('d-none')

                const action = parent.attr('id').replace('Action', '')

                $('#txt' + action).val('')
                $('#upld' + action).val('')

                listFileToDelete.push(field)
            })
        });

        function showFile(objFile, actionName, type) {
            let action = $('#' + actionName + "Action")
            action.removeClass("d-none")
            $('#' + actionName + "Desc").html(objFile.Desc)
            $('#txt' + actionName).val(objFile.Value.Text)
            $(action.children().eq(0)).data('download', `${window.location.origin}/DownloadFile.aspx?f=${objFile.Value.Value}&n=${objFile.Value.Text}&t=${type}`)
            $(action.children().eq(1)).attr('href', `${window.location.origin}/ViewFile.aspx?f=${objFile.Value.Value}&n=${objFile.Value.Text}&t=${type}`)
        }

        function showDropdown(objFile, actionName) {
            $('#' + actionName + "Desc").html(objFile.Desc)
            $('#ddl' + actionName).val(objFile.Value ? '1' : '0')
            if ($('#ddl' + actionName).val() == null) {
                $('#ddl' + actionName).prop('selectedIndex', 0)
            }
            $('#ddl' + actionName).trigger('change')
        }

        function showDropdownValue(objFile, actionName) {
            $('#' + actionName + "Desc").html(objFile.Desc)
            $('#ddl' + actionName).val(objFile.Value)
            if ($('#ddl' + actionName).val() == null) {
                $('#ddl' + actionName).prop('selectedIndex',0)
            }
            $('#ddl' + actionName).trigger('change')
        }

        function openInputFile(inputId) {
            $('#' + inputId).trigger('click')
        }

        function onChangeUpload(e, inputId) {
            $('#' + inputId).val(event.target.files[0].name)
        }

        function loadPreChecking() {
            const vm = vms[1]
            if (vm.RamciFile) {
                showFile(vm.RamciFile, "ramci", "application_prechecking")
            }

            if (vm.CcrisFile) {
                showFile(vm.CcrisFile, "ccris", "application_prechecking")
            }

            if (vm.PayslipFile) {
                showFile(vm.PayslipFile, "payslip-prechecking", "application_prechecking")
            }

            if (vm.Eligibility) {
                showDropdownValue(vm.Eligibility, "eligibility")
            }

            cbLegalSuits.prop('checked', vm.LegalSuit)
            cbBankruptcy.prop('checked', vm.Bankruptcy)
            cbSpecialAttentionAccount.prop('checked', vm.SpecialAttentionAccount)
            cbBadPaymentRecord.prop('checked', vm.BadPaymentRecordCheck)

            vm.AdditionalDocuments.forEach(item => {
                appendDoc(item,'#contentAdditionalDocumentPreChecking',1)
            })

            txtApplicationRemarkPrechecking.val(vm.ApplicationRemark)
        }

        function loadPreparation() {
            const vm = vms[2]
            vm.AdminVerified.forEach(function (item) {
                ddlVerifiedAdmin.append(`<option value='${item.Value}'>${item.Text}</option>`)
            })

            vm.AdminReviewed.forEach(function (item) {
                ddlReviewAdmin.append(`<option value='${item.Value}'>${item.Text}</option>`)
            })

            vm.AdminApproved.forEach(function (item) {
                ddlApproveAdmin.append(`<option value='${item.Value}'>${item.Text}</option>`)
            })

            if (vm.ProposalFile) {
                showFile(vm.ProposalFile, "proposal", "application_preparation")
            }

            txtSalaryGross.val(vm.SalaryGross)
            txtSalaryDeduction.val(vm.SalaryDeduction)
            txtNetIncome.val(vm.NetIncome)
            txtB1.val(vm.B1)
            txtB2.val(vm.B2)
            txtB3.val(vm.B3)
            txtB4.val(vm.B4)
            txtBAverage.val(vm.BAverage)
            txtCommitmentOutstanding.val(vm.CommitmentOutstanding)
            txtCommitmentInstallment.val(vm.CommitmentInstallment)
            txtOtherNetBalance.val(vm.OtherNetBalance)
            txtBPA.val(vm.OtherBPA)
            txtOtherComparionDSR.val(vm.OtherComparisonDSR)
            txtOtherComparisonDSRPctCommitment.val(vm.OtherComparisonDSRPctCommitment)
            txtOtherPctRefresh.val(vm.OtherPctRefresh)
            txtOtherProposedRefresh.val(vm.OtherProposedRefresh)
            txtOtherCompositionDSR.val(vm.OtherCompositionDSR)
            txtOtherCompositionDSRPctCommitment.val(vm.OtherCompositionDSRPctCommitment)
            txtRefreshTotal.val(vm.RefreshTotal)
            txtRefreshRemainCommitment.val(vm.RefreshRemainCommitment)
            txtReloanTotal.val(vm.ReloanTotal)
            txtReloanMonthy.val(vm.ReloanMonthly)
            txtReloanBersih.val(vm.ReloanBersih)
            txtReloanBelanja.val(vm.ReloanBelanja)
            txtReloanDeposit.val(vm.ReloanDeposit)
            txtReloanDanaBantuan.val(vm.ReloanDanaBantuan)
            txtReloanServiceFee.val(vm.ReloanServiceFee)
            txtReloanServiceFeePct.val(vm.ReloanServiceFeePct)
            txtReloanIncomeAfterRNR.val(vm.ReloanIncomeAfterRNR)
            txtReloanDifference.val(vm.ReloanDifference)
            ddlModelBackgroundScreening.val(vm.ModelBackgroundScreeningId)
            ddlModelCompositionDSR.val(vm.ModelCompositionDSRId)
            ddlModelCommitment.val(vm.ModelCommitmentId)
            ddlModelSettlement.val(vm.ModelSettlementId)
            ddlModelServiceFee.val(vm.ModelServiceFeeId)
            ddlModelNetIncomeAfterRNR.val(vm.ModelNetIncomeAfterRNRId)
            ddlModelStatus.val(vm.ModelStatusId)
            ddlModelStatusProposal.val(vm.ModelStatusProposalId)
            ddlModelCheck.val(vm.ModelCheckId)
            ddlReviewAdmin.val(vm.ReviewAdminId)
            ddlReviewStatus.val(vm.ReviewStatusId)
            txtReviewComment.val(vm.ReviewComment)
            ddlApproveAdmin.val(vm.ApproveAdminId)
            txtApproveComment.val(vm.ApproveComment)
            ddlApproveStatus.val(vm.ApproveStatusId)
            ddlVerifiedAdmin.val(vm.VerifiedAdminId)
            ddlVerifiedStatus.val(vm.VerifiedStatusId)
            txtVerifiedComment.val(vm.VerifiedComment)
            $(txtReviewDate).data('datepicker').setDate(appHelper.convertToDate(vm.ReviewDate))
            $(txtApproveDate).data('datepicker').setDate(appHelper.convertToDate(vm.ApproveDate))
            $(txtVerifiedDate).data('datepicker').setDate(appHelper.convertToDate(vm.VerifiedDate))
            txtApplicationRemarkPreparation.val(vm.ApplicationRemark)
            txtCreditRemarkProposalPreparation.val(vm.Info.CreditRemark)

            if (ddlModelBackgroundScreening.val() == null) {
                ddlModelBackgroundScreening.prop('selectedIndex', 0)
            }

            if (ddlModelCompositionDSR.val() == null) {
                ddlModelCompositionDSR.prop('selectedIndex', 0)
            }

            if (ddlModelCommitment.val() == null) {
                ddlModelCommitment.prop('selectedIndex', 0)
            }

            if (ddlModelSettlement.val() == null) {
                ddlModelSettlement.prop('selectedIndex', 0)
            }

            if (ddlModelServiceFee.val() == null) {
                ddlModelServiceFee.prop('selectedIndex', 0)
            }

            if (ddlModelNetIncomeAfterRNR.val() == null) {
                ddlModelNetIncomeAfterRNR.prop('selectedIndex', 0)
            }

            if (ddlModelStatus.val() == null) {
                ddlModelStatus.prop('selectedIndex', 0)
            }

            if (ddlModelStatusProposal.val() == null) {
                ddlModelStatusProposal.prop('selectedIndex', 0)
            }

            if (ddlModelCheck.val() == null) {
                ddlModelCheck.prop('selectedIndex', 0)
            }
            
            if (ddlReviewAdmin.val() == null) {
                ddlReviewAdmin.val(0)
            }
            if (ddlApproveAdmin.val() == null) {
                ddlApproveAdmin.val(0)
            }
            if (ddlVerifiedAdmin.val() == null) {
                ddlVerifiedAdmin.val(0)
            }

            if (ddlReviewStatus.val() == null) {
                ddlReviewStatus.val(1)
            }
            if (ddlApproveStatus.val() == null) {
                ddlApproveStatus.val(1)
            }
            if (ddlVerifiedStatus.val() == null) {
                ddlVerifiedStatus.val(1)
            }

            ddlReviewAdmin.trigger('change')
            ddlApproveAdmin.trigger('change')
            ddlReviewStatus.trigger('change')
            ddlApproveStatus.trigger('change')
            ddlVerifiedStatus.trigger('change')
            ddlVerifiedAdmin.trigger('change')
        }

        function loadProposal() {
            const vm = vms[3]
            if (vm.ProposalStatusId) {
                showDropdownValue(vm.ProposalStatusId, "proposalstatus")
            }
            
            vm.AdditionalDocuments.forEach(item => {
                appendDoc(item,'#contentAdditionalDocumentPresentation',3)
            })

            txtApplicationRemarkPresentation.val(vm.ApplicationRemark)
        }

        function loadPresign() {
            const vm = vms[4]
            if (vm.ProposalSendId) {
                showDropdownValue(vm.ProposalSendId, "proposalsend")
            }

            if (vm.SuratAkuanId) {
                showDropdownValue(vm.SuratAkuanId, "suratakuan")
            }

            if (vm.ComprehensiveFormId) {
                showDropdownValue(vm.ComprehensiveFormId, "comprehensive")
            }

            vm.AdditionalDocuments.forEach(item => {
                appendDoc(item, '#contentAdditionalDocumentPreSigning',4)
            })
            txtApplicationRemarkPresign.val(vm.ApplicationRemark)
        }

        function loadZoomAcceptance() {
            const vm = vms[5]
            if (vm.PayslipFile) {
                showFile(vm.PayslipFile, "payslip", "application_zoomacceptance")
            }

            if (vm.RamciFile) {
                showFile(vm.RamciFile, "ramci2", "application_zoomacceptance")
            }

            if (vm.CtosFile) {
                showFile(vm.CtosFile, "ctos", "application_zoomacceptance")
            }

            if (vm.RedemptionLetterFile) {
                showFile(vm.RedemptionLetterFile, "redemptionletter", "application_zoomacceptance")
            }

            txtApplicantAddress.val(vm.ApplicantAddress)
            ddlBankruptcyStatusZoom.val(vm.BankruptcyStatus)
            ddlLegalCaseZoom.val(vm.LegalCase)
            ddlHealthCreditScoreZoom.val(vm.HealthCreditScore)
            txtCommitments.val(vm.Commitments)

            vm.AdditionalDocuments.forEach(item => {
                appendDoc(item, '#contentAdditionalDocumentPendingZoomAndAtteptance',5)
            })

            txtApplicationRemarkPendingZoomAndAtteptance.val(vm.ApplicationRemark)
            updateSelect(ddlBankruptcyStatusZoom)
            updateSelect(ddlLegalCaseZoom)
            updateSelect(ddlHealthCreditScoreZoom)
        }

        function loadSettlement() {
            const vm = vms[6]
            vm.VtBanks.forEach(function (item) {
                ddlSettlementBank.append(`<option value='${item.Key}'>${item.Text}</option>`)
                ddlCuBank.append(`<option value='${item.Key}'>${item.Text}</option>`)
                ddlCuUpdateBank.append(`<option value='${item.Key}'>${item.Text}</option>`)
                ddlUpdateSettlementBank.append(`<option value='${item.Key}'>${item.Text}</option>`)
            })

            if (vm.SettlementFile) {
                showFile(vm.SettlementFile, "settlement", "application_settlement")
            }

            vm.SettlementDetails.forEach(item => {
                //change format display date
                    item.PaymentDate = appHelper.convertDateToDMY(appHelper.convertToDate(item.PaymentDate))
                    item.DueDate = appHelper.convertDateToDMY(appHelper.convertToDate(item.DueDate))
                item.RedemptionLetterDate = appHelper.convertDateToDMY(appHelper.convertToDate(item.RedemptionLetterDate))
                item.LoanReleaseDate = appHelper.convertDateToDMY(appHelper.convertToDate(item.LoanReleaseDate))
                appendSettlement(item)
            })

            vm.CaseUpdates.forEach(item => {

                //change format display date
                    item.SubmitDate = appHelper.convertDateToDMY(appHelper.convertToDate(item.SubmitDate))
                    item.ApprovedDate = appHelper.convertDateToDMY(appHelper.convertToDate(item.ApprovedDate))
                item.SignDate = appHelper.convertDateToDMY(appHelper.convertToDate(item.SignDate))
                    item.DisbursementDate = appHelper.convertDateToDMY(appHelper.convertToDate(item.DisbursementDate))
                item.UpdateDate = appHelper.convertDateToDMY(appHelper.convertToDate(item.UpdateDate))
                item.FirstDueDate = appHelper.convertDateToDMY(appHelper.convertToDate(item.FirstDueDate))
                appendCaseUpdate(item)
            })

            updateSettlementTable()

            vm.AdditionalDocuments.forEach(item => {
                appendDoc(item, '#contentAdditionalDocumentSettlement',6)
            })

            vm.Facilities.forEach(item => {
                ddlSettlementFacilities.append(`<option value='${item.Value}'>${item.Text}</option>`)
                ddlUpdateSettlementFacilities.append(`<option value='${item.Value}'>${item.Text}</option>`)
            })

            vm.FlexyCampaign.forEach(item => {
                ddlSettlementFlexiCampaign.append(`<option value='${item.Value}'>${item.Text}</option>`)
                ddlUpdateSettlementFlexiCampaign.append(`<option value='${item.Value}'>${item.Text}</option>`)
            })

            txtApplicationRemarkSettlement.val(vm.ApplicationRemark)
        }

        function updateSettlementTable() {
            if ($("#tableCaseUpdate tbody tr").length === 0) {
                $('#tableCaseUpdate tbody').append("<tr class='row-is-empty'><td colspan='12'>No Data Available</td></tr>")
            }

            if ($("#tableSettlementDetails tbody tr").length === 0) {
                $('#tableSettlementDetails tbody').append("<tr class='row-is-empty'><td colspan='13'>No Data Available</td></tr>")
            }
        }

        function loadCcris() {
            const vm = vms[7]
            if (vm.ReleaseLetterFile) {
                showFile(vm.ReleaseLetterFile, "releaseletter", "application_ccris")
            }

            if (vm.CcrisReportFile) {
                showFile(vm.CcrisReportFile, "ccrisreport", "application_ccris")
            }

            if (vm.HrmisFile) {
                showFile(vm.HrmisFile, "hrmis-ccris", "application_ccris")
            }

            if (vm.AnmFile) {
                showFile(vm.AnmFile, "anm-ccris", "application_ccris")
            }

            if (vm.LpsaFile) {
                showFile(vm.LpsaFile, "lpsa-ccris", "application_ccris")
            }

            if (vm.AngkasaFile) {
                showFile(vm.AngkasaFile, "angkasa-ccris", "application_ccris")
            }

            vm.AdditionalDocuments.forEach(item => {
                appendDoc(item, '#contentAdditionalDocumentCCRIS',7)
            })

            txtApplicationRemarkCcris.val(vm.ApplicationRemark)
            ddlHealthCreditScoreCcris.val(vm.HealthCreditScore)
            ddlBankruptcyStatusCcris.val(vm.BankruptcyStatus)
            ddlLegalCaseCcris.val(vm.LegalCase)
            txtCommitmentsCcris.val(vm.Commitments)

            vm.CaseUpdates.forEach(item => {
                //change format display date
                item.SubmitDate = appHelper.convertDateToDMY(appHelper.convertToDate(item.SubmitDate))
                item.ApprovedDate = appHelper.convertDateToDMY(appHelper.convertToDate(item.ApprovedDate))
                item.SignDate = appHelper.convertDateToDMY(appHelper.convertToDate(item.SignDate))
                item.DisbursementDate = appHelper.convertDateToDMY(appHelper.convertToDate(item.DisbursementDate))
                item.UpdateDate = appHelper.convertDateToDMY(appHelper.convertToDate(item.UpdateDate))
                item.FirstDueDate = appHelper.convertDateToDMY(appHelper.convertToDate(item.FirstDueDate))
                appendLoanInProgress(item)
            })

            updateSelect(ddlHealthCreditScoreCcris)
            updateSelect(ddlBankruptcyStatusCcris)
            updateSelect(ddlLegalCaseCcris)
        }

        function loadQueue() {
            const vm = vms[8]
            if (vm.IdentityCardFile) {
                showFile(vm.IdentityCardFile, "identity", "application_queue")
            }

            if (vm.PayslipFile) {
                showFile(vm.PayslipFile, "payslipqueue", "application_queue")
            }

            if (vm.EcFileFile) {
                showFile(vm.EcFileFile, "ec", "application_queue")
            }

            if (vm.HrmisFile) {
                showFile(vm.HrmisFile, "hrmis", "application_queue")
            }

            if (vm.BankStatementFile) {
                showFile(vm.BankStatementFile, "bankstatement", "application_queue")
            }

            if (vm.LppsaFile) {
                showFile(vm.LppsaFile, "lppsa", "application_queue")
            }

            if (vm.LicenseFile) {
                showFile(vm.LicenseFile, "license", "application_queue")
            }

            if (vm.RedemptionLetterFile) {
                showFile(vm.RedemptionLetterFile, "redemptionletterqueue", "application_queue")
            }

            if (vm.CcStatementFile) {
                showFile(vm.CcStatementFile, "cc", "application_queue")
            }

            if (vm.RamciFile) {
                showFile(vm.RamciFile, "ramciqueue", "application_queue")
            }

            if (vm.SignatureFile) {
                showFile(vm.SignatureFile, "signature", "application_queue")
            }

            if (vm.BiroFile) {
                showFile(vm.BiroFile, "biro", "application_queue")
            }
            if (vm.Kew320File) {
                showFile(vm.Kew320File, "kew320", "application_queue")
            }
            if (vm.StaffCardFile) {
                showFile(vm.StaffCardFile, "staffcard", "application_queue")
            }

            if (vm.PostDatedChequeFile) {
                showFile(vm.PostDatedChequeFile, "postdatedcheque", "application_queue")
            }

            if (vm.CompanyConfirmationFile) {
                showFile(vm.CompanyConfirmationFile, "company", "application_queue")
            }

            if (vm.EpfFile) {
                showFile(vm.EpfFile, "epf", "application_queue")
            }
            if (vm.EaformFile) {
                showFile(vm.EaformFile, "eaform", "application_queue")
            }
            if (vm.BillUtilitiesFile) {
                showFile(vm.BillUtilitiesFile, "billutilities", "application_queue")
            }

            $('#ddlWorkerType').val(vm.WorkerTypeId)
            $('#ddlWorkerType').trigger('change')
            vm.AdditionalDocuments.forEach(item => {
                appendDoc(item, '#contentAdditionalDocumentQueue',8)
            })
            vm.Payslips.forEach(item => {
                appendApplicationFile(item, '#contentPayslips')
            })

            vm.BankStatement.forEach(item => {
                appendApplicationFile(item, '#contentBankStatements')
            })

            txtApplicationRemarkQueue.val(vm.ApplicationRemark)
        }

        function onchangeWorkerType() {
            $('[data-worker="1"]').addClass('d-none')
            $('[data-worker="2"]').addClass('d-none')

            if ($(this).val() == 1) {
                $('[data-worker="1"]').removeClass('d-none')
            }
            else {
                $('[data-worker="2"]').removeClass('d-none')
            }
        }

        function loadReloan() {
            const vm = vms[9]
            if (vm.OfferLetterFile) {
                showFile(vm.OfferLetterFile, "offerletter", "application_reloan")
            }

            txtApprovedDate.data('datepicker').setDate(appHelper.convertToDate(vm.ApprovedDate))
            txtSigningDate.data('datepicker').setDate(appHelper.convertToDate(vm.SigningDate))
            ddlReloanStatus.val(vm.ReloanStatusId)
            txtApprovedAmount.val(vm.ApprovedAmount)

            if (ddlReloanStatus.val() == null) {
                ddlReloanStatus.prop('selectedIndex',0)
            }
            ddlReloanStatus.trigger('change')
            vm.AdditionalDocuments.forEach(item => {
                appendDoc(item, '#contentAdditionalDocumentReloan',9)
            })

            txtApplicationRemarkReloan.val(vm.ApplicationRemark)
        }

        function loadCollection() {
            const vm = vms[10]
            if (vm.DeclarationFile) {
                showFile(vm.DeclarationFile, "declaration", "application_collection")
            }

            if (vm.SettlementFile) {
                showFile(vm.SettlementFile, "settlementreceipt", "application_collection")
            }

            if (vm.ServiceFeeFile) {
                showFile(vm.ServiceFeeFile, "servicefee", "application_collection")
            }
            if (vm.RezekiFile) {
                showFile(vm.RezekiFile, "rezekireceipt", "application_collection")
            }

            if (vm.RezekiAgreementFile) {
                showFile(vm.RezekiAgreementFile, "rezekiagreement", "application_collection")
            }

            txtDepositDate.data('datepicker').setDate(appHelper.convertToDate(vm.DepositDate))
            txtServiceFee.val(vm.ServiceFee)
            txtDepositAmount.val(vm.DepositAmount)
            vm.AdditionalDocuments.forEach(item => {
                appendDoc(item, '#contentAdditionalDocumentCollection',10)
            })

            txtApplicationRemarkCollection.val(vm.ApplicationRemark)
            txtSettlementDate.data('datepicker').setDate(appHelper.convertToDate(vm.SettlementDate))
            txtSettlementCollectionAmount.val(vm.SettlementAmount)
            txtSettlementCPct.val(vm.SettlementCPct)
            txtCollectionAmountPct.val(vm.CollectionAmountPct)
            txtSettlementDuration.val(vm.SettlementDuration)
            txtTotalReloan.val(vm.TotalReloan)
            txtTotalLoanRepayment.val(vm.TotalLoanRepayment)
            txtDBBBankAccount.val(vm.DBBBankAccount)
            txtDBBTenure.val(vm.DBBTenure)
            txtDBBAgreementDate.data('datepicker').setDate(appHelper.convertToDate(vm.DBBAgreementDate))
            txtMonthlyFund.val(vm.MonthlyFund)
            txtDBBAmount.val(vm.DBBAmount)
            txtReceiptNo.val(vm.ReceiptNo)
            txtTaxNumber.val(vm.TaxNumber)
            ddlCollectionStatusId.val(vm.StatusId)
            txtInstallmentDate.data('datepicker').setDate(appHelper.convertToDate(vm.InstallmentDate))
            updateSelect(ddlCollectionStatusId)
        }

        function handleApplicationRejected() {
            const vm = vms[1]
            $('.btn-application-reject').addClass('d-none')
            $('.btn-application-next').addClass('d-none')
            if (vm.Info.RejectedDate != null) {
                $('.btn-application-save').addClass('d-none')
                $('.content-application-rejected').html(`<b>Application Rejected at ${appHelper.convertDateToDMY(appHelper.convertToDate(vm.Info.RejectedDate))} by ${vm.Info.RejectedAdmin}</b><br />
                                                        "${vm.Info.RejectedReason}"`)
            }
            else {
                $('.btn-application-cancel-reject').addClass('d-none')                
                $('.content-application-rejected').html('')

                $('.btn-application-reject').each(function (_) {
                    if ($(this).parent().data('status') == vm.ApplicationStatusId) {
                        $(this).removeClass('d-none')
                    }
                })
                $('.btn-application-next').each(function (_) {
                    if ($(this).parent().data('status') == vm.ApplicationStatusId) {
                        $(this).removeClass('d-none')
                    }
                })
            }

           
        }

        function loadDetailsInfo() {
            const vm = vms[1]

            vm.CustomerStatusType.forEach(item => {
                $('.source-customer-status').append(`<option value='${item.Value}'>${item.Text}</option>`)
            })

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
            $('#contentPreparedBy').text(vm.Info.PreparedBy)
            $('#contentVerifiedBy').text(vm.Info.VerifiedBy)

            ddlSource.val(vm.Info.SourceId)
            ddlCreditStatus.val(vm.Info.CreditStatus)
            txtScoreClass.val(vm.Info.ScoreClass)
            txtCreditRemark.val(vm.Info.Remark)
            ddlCustomerStatus.val(vm.Info.CustomerStatusId)
            ddlBurstReason.val(vm.Info.BurstReasonId)
            ddlCurrentStatus.val(vm.Info.ApplicationStatusId)
            ddlCreditStatus.val(vm.Info.CreditStatusId)

            if (ddlSource.val() == null) {
                ddlSource.prop('selectedIndex',0)
            }

            if (ddlCreditStatus.val() == null) {
                ddlCreditStatus.prop('selectedIndex',0)
            }

            if (ddlCustomerStatus.val() == null) {
                ddlCustomerStatus.prop('selectedIndex',0)
            }

            if (ddlBurstReason.val() == null) {
                ddlBurstReason.prop('selectedIndex',0)
            }

            ddlSource.trigger('change')
            ddlCreditStatus.trigger('change')
            ddlCustomerStatus.trigger('change')
            ddlBurstReason.trigger('change')
            ddlCurrentStatus.trigger('change')
            if (vm.Info.ApplicationStatusLastChangeAdminId != null) {
                $('#changestatusDesc').html(vm.Info.ApplicationStatusLastChangeAdminId.Desc)
            }

            
        }

        function updateSelect(el) {
            if (el.val() == null) {
                el.prop('selectedIndex', 0)
            }

            el.trigger('change')
        }

        $(ddlCustomerStatus).change(function () {
            if ($(this).val() == 2)//burst 
            {
                $('.content-burst-reason').removeClass('d-none')
            }
            else {
                $('.content-burst-reason').addClass('d-none')
            }
        })

        $('.nyatakan').on('change', function (e) {
            $('#nyatakan_' + $(this).attr('name')).addClass('d-none')
            let showNyatakan = false
            const dataNyatakan = String($(this).data('nyatakan'))
            if ($(this).data('select2') !== undefined) {
                showNyatakan = $(this).val().includes(dataNyatakan)
            }
            else {
                showNyatakan = dataNyatakan == $(this).val()
            }
            if (showNyatakan) {
                $('#nyatakan_' + $(this).attr('name')).removeClass('d-none')
            }
        })

        $(document).ready(function () {
            vms = JSON.parse($('#<%= hfModelView.ClientID %>').val())
            handleApplicationRejected()
            changeTab(<%=CurrentApplicationStatusId%> - 1);
            loadDetailsInfo()
            loadPreChecking()
            loadPreparation()
            loadProposal()
            loadPresign()
            loadZoomAcceptance()
            loadSettlement()
            loadCcris()
            loadQueue()
            loadReloan()
            loadCollection()
            //switch (vm.ApplicationStatusId) {
            //    case 1:
            //        loadPreChecking()
            //        break;
            //    case 2:
            //        loadPreparation()
            //        break;
            //    case 3:
            //        loadProposal()
            //        break;
            //    case 4:
            //        loadPresign()
            //        break;
            //    case 5:
            //        loadZoomAcceptance()
            //        break;
            //    case 6:
            //        loadSettlement()
            //        break;
            //    case 7:
            //        loadCcris()
            //        break;
            //    case 8:
            //        loadQueue()
            //        break;
            //    case 9:
            //        loadReloan()
            //        break;
            //    case 10:
            //        loadCollection()
            //        break;
            //    default:
            //        break;
            //}

         })
    </script>
</asp:Content>
