<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewAgent.aspx.cs" Inherits="csa.Member.AddNewAgent" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
    <style type="text/css">
        .join-us {
            background: linear-gradient(to top, #171e32, #405189);
            box-shadow: rgba(0, 0, 0, 0.24) 0px 3px 8px;
            position: relative;
            padding: 22px 5px 22px 15px;
        }

        span.circle_icon {
            background: #fff;
            border-radius: 50%;
            -moz-border-radius: 50%;
            -webkit-border-radius: 50%;
            color: #fff;
            display: inline-block;
            padding: 0.65rem;
            text-align: center;
            vertical-align: middle;
            line-height: 0.95rem;
            font-size: 1rem;
            font-weight: 500;
            width: 39px;
            height: 39px;
            background-color: transparent;
            border: 1.5px solid #fff;
        }
    </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <div class="container-fluid">

            <div class="profile-foreground position-relative mx-n4 mt-n4">
                <div class="profile-wid-bg">
                    <img src="assets/images/profile-bg.jpg" alt="" class="profile-wid-img" />
                </div>
            </div>

            <div style="height: 300px;"></div>

            <div class="row">
                <div class="col-12 col-xxl-10 col-xl-10 col-lg-12 col-md-12 col-sm-12 offset-xxl-1 offset-xl-1">
                    <div class="card mt-n4">
                        <div class="card-header join-us">
                            <div class="d-flex flex-row align-items-center">
                                <div class="flex-grow-1">
                                    <span class="fs-22 fw-semibold text-white">WHY JOIN US</span>
                                </div>
                                <div class="flex-fill text-end">
                                    <div class="d-flex flex-row">
                                        <div class="row justify-content-center">
                                            <div class="col-mx">
                                                <div class="d-flex justify-content-center text-center">
                                                    <span class="circle_icon">
                                                        <i class="ri-camera-fill"></i>
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="col-mx">
                                                <div class="text-center mt-2">
                                                    <span class="fs-12 fw-semibold text-white mb-1">Benefit Content #1</span>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row justify-content-center">
                                            <div class="col-mx">
                                                <div class="d-flex justify-content-center text-center">
                                                    <span class="circle_icon">
                                                        <i class="ri-camera-fill"></i>
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="col-mx">
                                                <div class="text-center mt-2">
                                                    <span class="fs-12 fw-semibold text-white mb-1">Benefit Content #2</span>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row justify-content-center">
                                            <div class="col-mx">
                                                <div class="d-flex justify-content-center text-center">
                                                    <span class="circle_icon">
                                                        <i class="ri-camera-fill"></i>
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="col-mx">
                                                <div class="text-center mt-2">
                                                    <span class="fs-12 fw-semibold text-white mb-1">Benefit Content #3</span>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row justify-content-center">
                                            <div class="col-mx">
                                                <div class="d-flex justify-content-center text-center">
                                                    <span class="circle_icon">
                                                        <i class="ri-camera-fill"></i>
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="col-mx">
                                                <div class="text-center mt-2">
                                                    <span class="fs-12 fw-semibold text-white mb-1">Benefit Content #4</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                            </div>
                        </div>

                        <div class="card-header align-items-center d-flex">
                            <h5 class="card-title mb-0 flex-grow-1"><i class=" ri-survey-line"></i> Apply Now</h5>
                        </div>

                        <div class="card-body">
                            <div class="card-body">
                                <div class="row g-3 mb-2">
                                    <div class="col-mx">
                                        <div class="form">
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label for="txtFullName" class="form-label">First Name</label>
                                                        <input type="text" class="form-control" id="txtFirstName" placeholder="First Name" />
                                                    </div>
                                                </div>
                                                 <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label for="txtFullName" class="form-label">Last Name</label>
                                                        <input type="text" class="form-control" id="txtLastName" placeholder="Last Name" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label for="txtICNumber" class="form-label">Your IC Number</label>
                                                        <input type="text" class="form-control" id="txtICNumber" placeholder="IC Number" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label for="txtPhoneNo" class="form-label">Telephone Number</label>
                                                        <input type="text" class="form-control" id="txtPhoneNo" placeholder="Phone No">
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label for="txtEmail" class="form-label">Email</label>
                                                        <input type="email" class="form-control" id="txtEmail" placeholder="Email">
                                                    </div>
                                                </div>

                                                <hr class="hr mt-3" />

                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label for="upldPayslip" class="form-label">Upload Your Payslip</label>
                                                        <input class="form-control" type="file" id="upldPayslip">
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
                                            <button class="btn btn-soft-primary" id="btnSubmit"><i class="ri-send-plane-2-line me-1 align-middle"></i> Save</button>
                                            <a href="<%=Page.ResolveUrl("~/Referrals")%>" class="btn btn-soft-secondary"><i class="ri-close-circle-line me-1 align-middle"></i> Cancel</a>
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

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        var btnSubmit = $('#btnSubmit')
        var upldPayslip = $('#upldPayslip')
        var txtFirstName = $('#txtFirstName')
        var txtLastName = $('#txtLastName')
        var txtICNumber = $('#txtICNumber')
        var txtPhoneNo = $('#txtPhoneNo')
        var txtEmail = $('#txtEmail')

        btnSubmit.click(async function (e) {
            $(this).prop('disabled', true);

            const payload = {
                CreatorMemberId: <%= CurrentLoginMember.MemberId %>,
                FirstName: txtFirstName.val(),
                LastName: txtLastName.val(),
                ICNumber: txtICNumber.val(),
                PhoneNumber: txtPhoneNo.val(),
                Email: txtEmail.val(),
                PayslipFile: upldPayslip[0].files[0]
            }

            const res = await ApiHelper.postFormData(window.location.origin + '/member/CreateAgent', payload)
            if (!res.data.Error) {
                txtFirstName.val('')
                txtLastName.val('')
                txtICNumber.val('')
                txtPhoneNo.val('')
                txtEmail.val('')
                upldPayslip.val('')

                dialogHelper.success('Agent Created')
            } else {
                dialogHelper.error(res.data.Message)
            }

            $(this).prop('disabled', false);
        })
    </script>
</asp:Content>
