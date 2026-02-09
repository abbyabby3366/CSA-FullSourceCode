<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApplicationDetails.aspx.cs" Inherits="csa.Member.ApplicationDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .btn-tab {
            background-color: #ffffff;
            color: #8C9096;
            border: dashed 1px solid red;
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
            background-color: #012d65;
            color: #ffffff;
        }

        .my-panel-info {
            background-color: #012D65;
            padding: 10px 20px;
        }

        .my-panel-info > span {
            font-size: 14px;
            color: #ffffff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfModelView" runat="server" />
    <div class="page-content">
        <div class="container-fluid">

            <div class="profile-foreground position-relative mx-n4 mt-n4">
                <div class="profile-wid-bg">
                    <img src="assets/images/profile-bg.jpg" alt="" class="profile-wid-img" />
                </div>
            </div>

            <div class="" style="height: 300px;"></div>

            <div class="row d-none" id="contentPrecheking">
                <div class="col-12 col-xxl-10 col-xl-10 col-lg-10 col-md-12 col-sm-12 offset-xxl-1 offset-xl-1 offset-lg-1">
                    <div class="card mt-n4">
                        <div class="card-header d-flex align-items-center">
                            <h5 class="card-title mb-0"><i class="mdi mdi-file-document-edit-outline"></i> Application Pre-checking Stage </h5> <span>&nbsp;- Kindly upload your Payslip & Surat Akuan below.</span>
                        </div>

                        <!-- personal details -->
                        <div class="card-body">
                            <div class="card-body">
                                <div class="row g-3 mb-2">
                                    <div class="col-mx">
                                        <div class="form">
                                            <div class="row">
                                                 <div class="col-lg-4">
                                                    <div class="mb-3">
                                                        <label for="upldIC" class="form-label">Upload Your Payslip</label>
                                                        <input class="form-control" type="file" id="upldPayslip">
                                                    </div>
                                                </div>
                                                 <div class="col-lg-4">
                                                    <div class="mb-3">
                                                        <label for="upldIC" class="form-label">Upload Your Surat Akuan</label>
                                                        <input class="form-control" type="file" id="upldSuratAkuan">
                                                    </div>
                                                </div>
                                                <div class="col-lg-4">
                                                    <div class="mb-3">
                                                        <label class="form-label">&nbsp;</label>
                                                        <button id="btnDownloadSuratAkuanTemplate" type="button" class="btn btn-primary w-100">Download Surat Akuan Template</button>
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
                                        <button class="btn btn-soft-primary" id="btnSubmitPreChecking" type="button"><i class="ri-send-plane-2-line me-1 align-middle"></i> Submit</button>
                                        <a href="<%=Page.ResolveUrl("~/ApplicationStatus")%>" class="btn btn-soft-secondary"><i class="ri-close-circle-line me-1 align-middle"></i> Cancel</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row d-none" id="contentProcessing">
                <div class="col-12 col-xxl-10 col-xl-10 col-lg-10 col-md-12 col-sm-12 offset-xxl-1 offset-xl-1 offset-lg-1">
                    <div class="card mt-n4">
                        <div class="card-header d-flex align-items-center">
                            <h5 class="card-title mb-0"><i class="mdi mdi-file-document-edit-outline"></i> Application Processing Stage </h5> <span>&nbsp;- Kindly upload your Payslip & Surat Akuan below.</span>
                        </div>

                        <!-- personal details -->
                        <div class="card-body">
                            <div class="card-body">
                                <div class="row g-3 mb-2">
                                    <div class="col-mx">
                                        <div class="form">
                                            <div class="row d-none" id="tabProcessing1">
                                                 <div class="btn-group" role="group" aria-label="Basic example">
                                                      <button type="button" class="btn btn-tab tab-active">Processing Stage #1</button>
                                                      <button type="button" class="btn btn-tab ">Processing Stage #2</button>
                                                      <button type="button" class="btn btn-tab">Processing Stage #3</button>
                                                </div>

                                                <label class="col-12 mt-3">Kindly download your Application Agreement Document below:</label>     
                                               
                                                <div class="col-4 mb-3">
                                                    <button class="btn btn-primary" id="btnDownloadApplicationAgreementDocument" type="button">Download Application Agreement Document</button>                                                      
                                                </div>
                                                <div class="d-md-block"></div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label for="upldIc" class="form-label">Upload Your IC</label>
                                                        <input class="form-control" type="file" id="upldIc">
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label for="upldReleaseLetter" class="form-label">Upload Release Letter</label>
                                                        <input class="form-control" type="file" id="upldReleaseLetter">
                                                    </div>
                                                </div>
                                                 <div class="card-footer">
                                                    <div class="col-lg-12">
                                                        <div class="hstack gap-2 justify-content-end">
                                                            <button class="btn btn-soft-primary" id="btnSubmitProcessing1" type="button"><i class="ri-send-plane-2-line me-1 align-middle"></i> Submit</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row d-none" id="tabProcessing2">
                                                 <div class="btn-group" role="group" aria-label="Basic example">
                                                      <button type="button" class="btn btn-tab tab-done">Processing Stage #1</button>
                                                      <button type="button" class="btn btn-tab tab-active">Processing Stage #2</button>
                                                      <button type="button" class="btn btn-tab">Processing Stage #3</button>
                                                </div>

                                                <div class="col-lg-6 mt-3">
                                                    <div class="mb-3">
                                                        <label for="upldCreditDisbursement" class="form-label">Upload Your Credit Disbursement ScreenShot</label>
                                                        <input class="form-control" type="file" id="upldCreditDisbursement">
                                                    </div>
                                                </div>

                                                 <label class="col-12 mt-3">ID / Password for (ANM,HRMIS,LPPSA, ANGKASA) - Kindly provide your ID & Password below</label>
                                                <div class="col-lg-6 mt-3">
                                                    <div class="my-panel-info mb-3"><span>ANM</span></div>
                                                    <div class="mb-3">
                                                        <label class="form-label">ID</label>
                                                        <input class="form-control" >
                                                    </div>
                                                    <div class="mb-3">
                                                        <label class="form-label">Password</label>
                                                        <input class="form-control" >
                                                    </div>

                                                    <div class="my-panel-info mb-3"><span>LPPSA</span></div>
                                                    <div class="mb-3">
                                                        <label class="form-label">ID</label>
                                                        <input class="form-control" >
                                                    </div>
                                                    <div class="mb-3">
                                                        <label class="form-label">Password</label>
                                                        <input class="form-control" >
                                                    </div>
                                                </div>

                                                <div class="col-lg-6 mt-3">
                                                    <div class="my-panel-info mb-3"><span>HRMIS</span></div>
                                                    <div class="mb-3">
                                                        <label class="form-label">ID</label>
                                                        <input class="form-control" >
                                                    </div>
                                                    <div class="mb-3">
                                                        <label class="form-label">Password</label>
                                                        <input class="form-control" >
                                                    </div>

                                                    <div class="my-panel-info mb-3"><span>ANGKASA</span></div>
                                                    <div class="mb-3">
                                                        <label class="form-label">ID</label>
                                                        <input class="form-control" >
                                                    </div>
                                                    <div class="mb-3">
                                                        <label class="form-label">Password</label>
                                                        <input class="form-control" >
                                                    </div>
                                                </div>

                                                <p class="text-danger">If you have not registered LPPSA & ANGKASA, kindly register from link below.</p>
                                            <p class="mb-0">LPPSA</p>
                                            <a href="https://lms.lppsa.gov.my/JACCESS/portal/">https://lms.lppsa.gov.my/JACCESS/portal/</a>
                                            <p class="mb-0 mt-3">ANGKASA</p>
                                            <a href="https://angkasa.coop/bm/index.php/pages/sistem-potongan-gaji-angkasa-spga/borang-permohonan-majikan-biro-angkasa">https://angkasa.coop/bm/index.php/pages/sistem-potongan-gaji-angkasa-spga/borang-permohonan-majikan-biro-angkasa</a>

                                                <div class="card mt-3">
                                                    <div class="card-header">
                                                        <h5>YABAM Application Terms & Conditions</h5>
                                                    </div>
                                                    <div class="card-body">
                                                        <p>Example terms & condition content</p>
                                                    </div>
                                                    <div class="card-footer d-flex justify-content-center">
                                                        <p><input type="checkbox" id="cbTerm"/> I accept the <b>Terms & Conditions</b></p>
                                                    </div>
                                                </div>
                                                <div class="card-footer">
                                                    <div class="col-lg-12">
                                                        <div class="hstack gap-2 justify-content-end">
                                                            <button class="btn btn-soft-primary" id="btnSubmitProcessing2" type="button"><i class="ri-send-plane-2-line me-1 align-middle"></i> Submit</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            

                                            <div class="row d-none" id="tabProcessing3">
                                                 <div class="btn-group" role="group" aria-label="Basic example">
                                                      <button type="button" class="btn btn-tab tab-done">Processing Stage #1</button>
                                                      <button type="button" class="btn btn-tab tab-done">Processing Stage #2</button>
                                                      <button type="button" class="btn btn-tab tab-done">Processing Stage #3</button>
                                                </div>

                                                <div class="col-lg-6 mt-3">
                                                    <div class="mb-3">
                                                        <label for="upldPaymentSlip" class="form-label">Upload Your Payment Slip</label>
                                                        <input class="form-control" type="file" id="upldPaymentSlip">
                                                    </div>
                                                </div>
                                                <div class="card-footer">
                                                    <div class="col-lg-12">
                                                        <div class="hstack gap-2 justify-content-end">
                                                            <button class="btn btn-soft-primary" id="btnSubmitProcessing3" type="button"><i class="ri-send-plane-2-line me-1 align-middle"></i> Submit</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                           <%-- <div class="card-footer">
                                <div class="col-lg-12">
                                    <div class="hstack gap-2 justify-content-end">
                                        <button class="btn btn-soft-primary"><i class="ri-send-plane-2-line me-1 align-middle"></i> Submit</button>
                                    </div>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>


        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        var vm
        var contentPreChecking = $('#contentPrecheking')
        var contentProcessing = $('#contentProcessing')
        var upldPayslip = $('#upldPayslip')
        var upldSuratAkuan = $('#upldSuratAkuan')
        var upldIc = $('#upldIc')
        var upldReleaseLetter = $('#upldReleaseLetter')
        var upldCreditDisbursement = $('#upldCreditDisbursement')
        var upldPaymentSlip = $('#upldPaymentSlip')
        var tabProcessing1 = $('#tabProcessing1')
        var tabProcessing2 = $('#tabProcessing2')
        var tabProcessing3 = $('#tabProcessing3')

        function loadDetails() {
            const preCheckingStatus = 1
            const processingStatus = 2
            if (vm.StatusId == preCheckingStatus) {
                contentPreChecking.removeClass('d-none')
            }
            else if (vm.StatusId == processingStatus) {
                contentProcessing.removeClass('d-none')
                moveTabProcessing(tabProcessing1)
            }
        }

        function moveTabProcessing(tab) {
            if (tab == tabProcessing1) {
                tab.removeClass('d-none')
                tabProcessing2.addClass('d-none')
                tabProcessing3.addClass('d-none')
            }
            else if (tab == tabProcessing2) {
                tabProcessing1.addClass('d-none')
                tab.removeClass('d-none')
                tabProcessing3.addClass('d-none')
            }
            else if (tab == tabProcessing3) {
                tabProcessing1.addClass('d-none')
                tabProcessing2.addClass('d-none')
                tab.removeClass('d-none')
            }
        }

        $('#btnDownloadSuratAkuanTemplate').click(async function (e) {
            ApiHelper.download(window.location.origin + '/application/DownloadSuratAkuanTemplate').then(response => {
                // Create a URL for the Blob object
                const url = window.URL.createObjectURL(new Blob([response.data]));

                // Create a link element
                const a = document.createElement('a');
                a.href = url;
                a.download = 'Template Surat Akuan.txt';

                // Append the link to the body (not visible)
                document.body.appendChild(a);

                // Trigger a click event on the link to start the download
                a.click();

                // Clean up
                window.URL.revokeObjectURL(url);
                document.body.removeChild(a);
            })
                .catch(error => {
                    console.error('There was an error downloading the file:', error);
                });
        })

        $('#btnDownloadApplicationAgreementDocument').click(async function (e) {
            ApiHelper.download(window.location.origin + '/application/DownloadApplicationAgreementDocument').then(response => {
                // Create a URL for the Blob object
                const url = window.URL.createObjectURL(new Blob([response.data]));

                // Create a link element
                const a = document.createElement('a');
                a.href = url;
                a.download = 'Application Agreement Document.docx';

                // Append the link to the body (not visible)
                document.body.appendChild(a);

                // Trigger a click event on the link to start the download
                a.click();

                // Clean up
                window.URL.revokeObjectURL(url);
                document.body.removeChild(a);
            })
                .catch(error => {
                    console.error('There was an error downloading the file:', error);
                });
        })

        $('#btnSubmitPreChecking').click(async function (e) {
            if (upldPayslip[0].files[0] == undefined || upldSuratAkuan[0].files[0] == undefined) {
                dialogHelper.error('Please fill required field')
                return
            }
            $(this).prop('disabled', true)

            const res = await ApiHelper.postFormData(window.location.origin + '/application/PreCheckingByMember', null)
            if (!res.data.Error) {
                //to do write code
            } else {
                dialogHelper.error(res.data.Message)
            }


            $(this).prop('disabled',false)
        })

        $('#btnSubmitProcessing1').click(function (e) {
            if (upldIc[0].files[0] == undefined || upldReleaseLetter[0].files[0] == undefined) {
                dialogHelper.error('Please fill required field')
                return
            }

            moveTabProcessing(tabProcessing2)
        })

        $('#btnSubmitProcessing2').click(function (e) {
            if (upldCreditDisbursement[0].files[0] == undefined) {
                dialogHelper.error('Please fill required field')
                return
            }

            if (!$('#cbTerm').prop('checked')) {
                dialogHelper.error('Agree to Terms and Conditions')
                return
            }

            moveTabProcessing(tabProcessing3)
        })

        $('#btnSubmitProcessing3').click(async function (e) {
            if (upldPaymentSlip[0].files[0] == undefined) {
                dialogHelper.error('Please fill required field')
                return
            }

            $(this).prop('disabled', true)

            const res = await ApiHelper.postFormData(window.location.origin + '/application/ProcessingByMember', null)
            if (!res.data.Error) {
                //to do write code
            } else {
                dialogHelper.error(res.data.Message)
            }

            $(this).prop('disabled', false)
        })

        $(document).ready(function () {
            vm = JSON.parse($('#<%= hfModelView.ClientID %>').val())

            //overide status for test
            const urlParams = new URLSearchParams(window.location.search);
            const s = urlParams.get('s');
            console.log(s)
            if (s != undefined) {
                vm.StatusId = s
            }

            loadDetails()

            
        });
    </script>
</asp:Content>
