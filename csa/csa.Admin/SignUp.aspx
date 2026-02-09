<%@ Page Title="" Language="C#" MasterPageFile="~/SiteExt.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="csa.Admin.SignUp" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <div class="auth-page-wrapper auth-bg-cover py-5 d-flex justify-content-center align-items-center min-vh-100">
        <div class="bg-overlay"></div>

        <!-- auth page content -->
        <div class="auth-page-content">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="text-center text-white-50">
                            <div>
                                <a href="index.html" class="d-inline-block auth-logo">
                                    <img src="<%=Page.ResolveUrl("~/assets/images/logos/logo-dark.png") %>" alt="" height="100">
                                </a>
                            </div>
                            <%--<p class="mt-3 fs-15 fw-medium">Premium Admin & Dashboard Template</p>--%>
                        </div>
                    </div>
                </div>
                <!-- end row -->

                <div class="row justify-content-center">
                    <div class="col-md-8 col-lg-6 col-xl-5">
                        <div class="card mt-4">
                            <div class="card-body p-4">
                                <div class="text-center mt-2">
                                    <h5 class="text-primary">Create New Account</h5>
                                    <%--<p class="text-muted">Get your free velzon account now</p>--%>

                                    <div class="text-center">
                                        <p class="mb-0">Already have an account ? <a href="<%=Page.ResolveUrl("~/SignIn") %>" class="fw-semibold text-primary text-decoration-underline">Signin </a></p>
                                    </div>
                                </div>
                                <div class="p-2">
                                    <div class="mt-2 text-center">
                                        <div class="mb-3">
                                            <button type="button" class="btn btn-outline-primary waves-effect waves-light w-100"><i class="ri-google-fill fs-16"></i> Sign up with Google</button>
                                        </div>
                                        <div class="signin-other-title">
                                            <h5 class="fs-13 mb-2 title">Or</h5>
                                        </div>
                                    </div>

                                    <div class="form needs-validation" novalidate>
                                        <div class="mb-3">
                                            <label for="txtEmail" class="form-label">Email <span class="text-danger">*</span></label>
                                            <input type="email" class="form-control" id="txtEmail" placeholder="Enter email address" required>

                                            <div class="invalid-feedback">
                                                Please enter email
                                            </div>
                                        </div>

                                        <div class="mb-3">
                                            <label class="form-label" for="password-input">Password</label>
                                            <div class="position-relative auth-pass-inputgroup">
                                                <input type="password" class="form-control pe-5 password-input" onpaste="return false" placeholder="Enter password" id="password-input" aria-describedby="passwordInput" pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}" required>
                                                <button class="btn btn-link position-absolute end-0 top-0 text-decoration-none text-muted password-addon" type="button" id="password-addon"><i class="ri-eye-fill align-middle"></i></button>
                                                <div class="invalid-feedback">
                                                    Please enter password
                                                </div>
                                            </div>
                                        </div>

                                        <div class="mb-3">
                                            <label class="form-label" for="confirm-password-input">Confirmed Password</label>
                                            <div class="position-relative auth-pass-inputgroup">
                                                <input type="password" class="form-control pe-5 password-input" onpaste="return false" placeholder="Enter confirmed password" id="confirm-password-input" aria-describedby="passwordInput" pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}" required>
                                                <button class="btn btn-link position-absolute end-0 top-0 text-decoration-none text-muted password-addon" type="button" id="confirm-password-addon"><i class="ri-eye-fill align-middle"></i></button>
                                                
                                                <div class="invalid-feedback">
                                                    Please enter confirm password
                                                </div>
                                            </div>
                                        </div>

                                        <div class="mb-4">
                                            <p class="mb-0 fs-12 text-muted fst-italic">By registering you agree to the CSA Academy <a href="javascript:void(0);" class="text-primary text-decoration-underline fst-normal fw-medium">Terms of Use</a></p>
                                        </div>

                                        <div id="password-contain" class="p-3 bg-light mb-2 rounded">
                                            <h5 class="fs-13">Password must contain:</h5>
                                            <p id="pass-length" class="invalid fs-12 mb-2">Minimum <b>8 characters</b></p>
                                            <p id="pass-lower" class="invalid fs-12 mb-2">At <b>lowercase</b> letter (a-z)</p>
                                            <p id="pass-upper" class="invalid fs-12 mb-2">At least <b>uppercase</b> letter (A-Z)</p>
                                            <p id="pass-number" class="invalid fs-12 mb-0">A least <b>number</b> (0-9)</p>
                                        </div>

                                        <div class="mt-4">
                                            <button class="btn btn-tertiary w-100" type="submit">Sign Up</button>
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
</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        !function () {
            "use strict";
            window.addEventListener("load", function () {
                var t = document.getElementsByClassName("needs-validation");
                t && Array.prototype.filter.call(t, function (e) {
                    e.addEventListener("submit", function (t) {
                        !1 === e.checkValidity() && (t.preventDefault(), t.stopPropagation()), e.classList.add("was-validated")
                    }, !1)
                })
            }, !1);

            Array.from(document.querySelectorAll("form .auth-pass-inputgroup")).forEach(function (s) {
                Array.from(s.querySelectorAll(".password-addon")).forEach(function (t) {
                    t.addEventListener("click", function (t) {
                        var e = s.querySelector(".password-input");
                        "password" === e.type ? e.type = "text" : e.type = "password"
                    })
                })
            });
            var password = document.getElementById("password-input"),
                confirm_password = document.getElementById("confirm-password-input");

            function validatePassword() {
                password.value != confirm_password.value ? confirm_password.setCustomValidity("Passwords Don't Match") : confirm_password.setCustomValidity("")
            }

            password.onchange = validatePassword;

            var myInput = document.getElementById("password-input"),
                letter = document.getElementById("pass-lower"),
                capital = document.getElementById("pass-upper"),
                number = document.getElementById("pass-number"),
                length = document.getElementById("pass-length");

            myInput.onfocus = function () {
                document.getElementById("password-contain").style.display = "block"
            }, myInput.onblur = function () {
                document.getElementById("password-contain").style.display = "none"
            }, myInput.onkeyup = function () {
                myInput.value.match(/[a-z]/g) ? (letter.classList.remove("invalid"), letter.classList.add("valid")) : (letter.classList.remove("valid"), letter.classList.add("invalid")), myInput.value.match(/[A-Z]/g) ? (capital.classList.remove("invalid"), capital.classList.add("valid")) : (capital.classList.remove("valid"), capital.classList.add("invalid"));
                myInput.value.match(/[0-9]/g) ? (number.classList.remove("invalid"), number.classList.add("valid")) : (number.classList.remove("valid"), number.classList.add("invalid")), 8 <= myInput.value.length ? (length.classList.remove("invalid"), length.classList.add("valid")) : (length.classList.remove("valid"), length.classList.add("invalid"))
            };
        }();
    </script>
</asp:Content>
