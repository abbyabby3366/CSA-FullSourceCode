<%@ Page Title="" Language="C#" MasterPageFile="~/SiteExt.Master" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="csa.Member.SignIn" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
    <style type="text/css">
        .form-control.password-input.is-invalid, .was-validated .form-control:invalid {
            background-position: right calc(.375em + 2.25rem) center !important;
        }

        .form-control.is-valid, .was-validated .form-control:valid {
            background-position: right calc(.375em + 2.25rem) center !important;
        }


        .navbar-menu {
    background: #279d69 !important;
}

.menu-title
{
    color:white !important;
}

.navbar-menu .navbar-nav .nav-link
{
    color: lightyellow !important;
}

.profile-wid-bg::before
{
    background: #279d69 !important;
    background: -webkit-gradient(linear,left bottom,left top,from(whitesmoke),to(#279d69)) !important;
    background: linear-gradient(to top,white,#279d69) !important;
}

.join-us
{
    background: #279d69 !important;
}

.auth-bg-cover {
    background: linear-gradient(-45deg, #279d69 55%, whitesmoke) !important;
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
                                    <img src="<%=Page.ResolveUrl("~/assets/images/logos/yabam-logo-dark.png") %>" alt="" height="100">
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
                                    <h5 class="text-primary fw-semibold">Member Signin</h5>

                                    <div class="text-center">
                                        <p class="mb-0">Don't have an account ? <a href="<%=Page.ResolveUrl("~/SignUp") %>" class="fw-semibold text-primary text-decoration-underline"> Sign up here </a></p>
                                    </div>
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
                                            <div class="mb-3">
                                        
                                                <label for="<%=txtPhoneNo.ClientID %>" class="form-label">Phone No</label>
                                                <div class="d-flex align-items-center gap-3">
                                            <span>+60</span>
                                                <input type="text" class="form-control" id="txtPhoneNo" runat="server" placeholder="Enter phone number">
                                        </div>

                                                <p class="text-muted">Use this format eg; 0123456789</p>
                                                <div class="invalid-feedback">
                                                    Please fill-in phone number.
                                                </div>
                                            </div>
                                        </div>

                                        <div class="mb-3">
                                            <div class="float-end">
                                                <a href="<%=Page.ResolveUrl("~/ResetPass") %>" class="text-muted">Forgot password?</a>
                                            </div>

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
                            <p class="mb-0">&copy;
                                 © Hak Cipta <script>document.write(new Date().getFullYear())</script> © YAYASAN AMANAH BANTUAN AWAM MALAYSIA - Hak cipta terpelihara.
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

                let phoneNo = $("#<%=txtPhoneNo.ClientID %>");
                let pass = $("#<%=txtPassword.ClientID %>");

                if (phoneNo.length == 0 || pass.length == 0) { return; }

                $(this).prop('disabled', true);

                //let phoneRegex = /^\(?\+?[0-9]{1,3}\)? ?-?[0-9]{1,3} ?-?[0-9]{3,5} ?-?[0-9]{4}( ?-?[0-9]{3})?$/i;
                //let passRegex = /^\S*$/i;

                ////validation

                //if (phoneNo.val().match(phoneRegex)) {
                //    //phoneNo.addClass('is-valid');
                //    phoneNo.removeClass('is-invalid');
                //} else {
                //    phoneNo.addClass('is-invalid');
                //    //phoneNo.removeClass('is-valid');

                //    isValid = false;
                //}

                //if (pass.val().length > 0 && pass.val().match(passRegex)) {
                //    //pass.addClass('is-valid');
                //    pass.removeClass('is-invalid');
                //} else {
                //    pass.addClass('is-invalid');
                //    //pass.removeClass('is-valid');

                //    isValid = false;
                //}

               <%-- if (isValid) { $("#<%=btnLogin.ClientID%>").click(); }
                else { $(this).prop('disabled', false); }--%>
                const payload = {
                    phoneNumber: phoneNo.val(),
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
            })
        }

        function showModal() {
            $("#loginModal").modal('show');
        }

        $(document).ready(function () {

            //hardcode
            
            <%--$("#<%=txtPhoneNo.ClientID %>").val('60123456789');
            $("#<%=txtPassword.ClientID %>").val('123456');--%>

            init();

        });
    </script>
</asp:Content>

