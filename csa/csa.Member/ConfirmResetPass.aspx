<%@ Page Title="" Language="C#" MasterPageFile="~/SiteExt.Master" AutoEventWireup="true" CodeBehind="ConfirmResetPass.aspx.cs" Inherits="csa.Member.ConfirmResetPass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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

                <div class="row justify-content-center">
                    <div class="col-md-8 col-lg-6 col-xl-5">
                        <div class="card mt-4">
                            <div class="card-body p-4">
                                <div class="text-center mt-2">
                                    <h5 class="text-primary">Reset Password</h5>
                                    <%--<p class="text-muted">Reset password with velzon</p>--%>

                                    <div class="text-center">
                                        <p class="mb-0">Remember your password? <a href="<%=Page.ResolveUrl("~/SignIn") %>" class="fw-semibold text-primary text-decoration-underline"> Sign in here </a></p>
                                    </div>
                                </div>
                               
                                <div class="p-2">
                                    <div class="form">
                                        <div class="mb-4">
                                            <label class="form-label">New Password</label>
                                            <input type="password" class="form-control" name="newPassword" id="newPassword">
                                        </div>

                                        <div class="mb-4">
                                            <label class="form-label">Confirm New Password</label>
                                            <input type="password" class="form-control" name="confirmNewPassword">
                                        </div>

                                        <div class="text-center mt-4">
                                            <button class="btn btn-tertiary w-100" type="button" id="btnResetPassword">Reset Password</button>
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
                                Copyright <script>document.write(new Date().getFullYear())</script> © YABAM Academy powered by Epic Unicorn - All right reserved.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </footer>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="<%=Page.ResolveUrl("~/assets/libs/jqueryValidation/dist/jquery.validate.js") %>"></script>
    <script>
        var form = $('#form1')
        form.validate({
            rules: {
                newPassword: {
                    required: true,
                    minlength: 4,
                },
                confirmNewPassword: {
                    required: true,
                    equalTo: '#newPassword'
                }
            },
            errorClass: 'is-invalid',
            errorPlacement: function (error,element) {
                error.addClass("invalid-feedback"); 
                error.insertAfter(element);
            }
        })

        $('#btnResetPassword').click(function () {
            console.log(form.valid())
        })

        jQuery.validator.addMethod("daniPassword", function (value, element) {
            // allow any non-whitespace characters as the host part
            return this.optional(element) || /dani/.test(value);
        }, 'New password must dani');
    </script>
</asp:Content>
