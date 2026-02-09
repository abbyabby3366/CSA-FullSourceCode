<%@ Page Title="" Language="C#" MasterPageFile="~/SiteExt.Master" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="csa.Admin.SignIn" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
    <style type="text/css">
        .form-control.password-input.is-invalid, .was-validated .form-control:invalid {
            background-position: right calc(.375em + 2.25rem) center !important;
        }

        .form-control.is-valid, .was-validated .form-control:valid {
            background-position: right calc(.375em + 2.25rem) center !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <div class="auth-page-wrapper auth-bg-cover py-5 d-flex justify-content-center align-items-center min-vh-100">
        <div class="bg-overlay"></div>

        <div class="auth-page-content">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="text-center mt-sm-3 text-white-50">
                            <div>
                                <a href="javascript:void(0);" class="d-inline-block auth-logo">
                                    <img src="<%=Page.ResolveUrl("~/assets/images/logos/logo-dark.png") %>" alt="" height="100">
                                </a>
                            </div>
                            <%--<p class="mt-3 fs-15 fw-medium">How we can serve you better</p>--%>
                        </div>
                    </div>
                </div>

                <div class="row justify-content-center">
                    <div class="col-md-8 col-lg-6 col-xl-5">
                        <div class="card mt-4">
                            <div class="card-body p-4">
                                <div class="text-center">
                                    <h5 class="text-primary fw-semibold">Admin Signin</h5>

                                    <%--<div class="text-center">
                                        <p class="mb-0">Don't have an account ? <a href="<%=Page.ResolveUrl("~/SignUp") %>" class="fw-semibold text-primary text-decoration-underline"> Sign up here </a></p>
                                    </div>--%>
                                </div>

                                <div class="p-2">
                                    <%--<div class="mt-2 text-center">
                                        <div class="mb-3">
                                            <button type="button" class="btn btn-outline-primary waves-effect waves-light w-100"><i class="ri-google-fill fs-16"></i> Sign in with Google</button>
                                        </div>
                                        <div class="signin-other-title">
                                            <h5 class="fs-13 mb-2 title">Or</h5>
                                        </div>
                                    </div>--%>

                                    <div class="form">
                                        <div class="mb-3">
                                            <label for="<%=txtEmail.ClientID %>" class="form-label">Email</label>
                                            <input type="email" class="form-control" id="txtEmail" runat="server" placeholder="Enter email">

                                            <div class="invalid-feedback">
                                                Please fill-in Email.
                                            </div>
                                        </div>

                                        <div class="mb-3">
                                           <%-- <div class="float-end">
                                                <a href="<%=Page.ResolveUrl("~/ResetPass") %>" class="text-muted">Forgot password?</a>
                                            </div>--%>

                                            <label for="<%=txtPassword.ClientID %>" class="form-label">Password</label>

                                            <div class="position-relative auth-pass-inputgroup mb-3">
                                                <input type="password" class="form-control pe-5 password-input" id="txtPassword" runat="server" placeholder="Enter password">
                                                    <div class="invalid-feedback">
                                                    Please fill-in Password.
                                                </div>
                                                <button class="btn btn-link position-absolute end-0 top-0 text-decoration-none text-muted password-addon" type="button" id="password-addon"><i class="ri-eye-fill align-middle"></i></button>
                                            </div>
                                        </div>

                                        <div class="form-check">
                                            <input class="form-check-input" type="checkbox" value="" id="auth-remember-check">
                                            <label class="form-check-label" for="auth-remember-check">Remember me</label>
                                        </div>

                                        <div class="mt-3">
                                            <button type="submit" id="btnSignIn" class="btn btn-tertiary w-100">Sign In</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- footer -->
        <footer class="footer">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="text-center">
                            <p class="mb-0 text-muted">&copy;
                                Copyright <script>document.write(new Date().getFullYear())</script> © CSA Academy powered by Epic Unicorn - All right reserved.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </footer>
    </div>

    <!-- button -->
    <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" CssClass="d-none" />
    <asp:HiddenField runat="server" ID="hfSessionLogin" />
    <!-- modal -->
    <div id="loginModal" class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-body text-center p-5">
                    <img id="header-lang-img" src="<%=Page.ResolveUrl("~/assets/images/svg/alert-triangle.svg") %>" alt="Header Language" height="120" class="rounded">

                    <div class="mt-4">
                        <h4 class="mb-3">
                            <asp:Literal ID="litLoginModalTitle" runat="server"></asp:Literal>
                        </h4>

                        <p class="text-muted mb-4">
                            <asp:Literal ID="litLoginModalMessage" runat="server"></asp:Literal>
                        </p>

                        <button class="btn btn-warning" data-bs-dismiss="modal"><i class="ri-close-line me-1 align-middle"></i> Continue</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent" runat="server">
    <!-- js script -->
    <script type="text/javascript">
        !function () {
            Array.from(document.querySelectorAll("form .auth-pass-inputgroup")).forEach(function (e) {
                Array.from(e.querySelectorAll(".password-addon")).forEach(function (r) {
                    r.addEventListener("click", function (r) {
                        var o = e.querySelector(".password-input");
                        "password" === o.type ? o.type = "text" : o.type = "password"
                    })
                })
            })
        }();
    </script>

    <script type="text/javascript">
        function init() {
            $("#btnSignIn").on('click', async function (e) {
                e.preventDefault();

                let isValid = true;

                let email = $("#<%=txtEmail.ClientID %>");
                let pass = $("#<%=txtPassword.ClientID %>");

                if (email.length == 0 || pass.length == 0) { return; }

                $(this).prop('disabled', true);

                let emailRegex = /^\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b$/i;
                let passRegex = /^\S*$/i;

                //validation
                if (email.val().match(emailRegex)) {
                    //email.addClass('is-valid');
                    email.removeClass('is-invalid');
                } else {
                    email.addClass('is-invalid');
                    //email.removeClass('is-valid');

                    isValid = false;
                }

                if (pass.val().length > 0 && pass.val().match(passRegex)) {
                    //pass.addClass('is-valid');
                    pass.removeClass('is-invalid');
                } else {
                    pass.addClass('is-invalid');
                    //pass.removeClass('is-valid');

                    isValid = false;
                }

                if (!isValid) {
                    $(this).prop('disabled', false);
                    return
                }

                const payload = {
                    email: email.val(),
                    password: pass.val()
                }
                const res = await ApiHelper.post(window.location.origin + '/auth/login', payload)
                if (!res.data.Error) {
                    $('#<%= hfSessionLogin.ClientID %>').val(JSON.stringify(res.data.ObjVal))
                    $("#<%=btnLogin.ClientID%>").click()
                } else {
                    dialogHelper.error(res.data.Message)
                }

                $(this).prop('disabled', false);
            });
        }

        function showModal() {
            $("#loginModal").modal('show');
        }

        $(document).ready(function () {

            //hardcode
            <%--$("#<%=txtEmail.ClientID %>").val('testmember1@mail.com');
            $("#<%=txtPassword.ClientID %>").val('123456');--%>

            init();

        });
    </script>
</asp:Content>
