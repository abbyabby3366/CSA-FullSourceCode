<%@ Page Title="" Language="C#" MasterPageFile="~/SiteExt.Master" AutoEventWireup="true" CodeBehind="ResetPass.aspx.cs" Inherits="csa.Member.ResetPass" %>

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
                                    <img src="<%=Page.ResolveUrl("~/assets/images/logos/yabam-logo-dark.png") %>" alt="" height="100">
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
                                    <h5 class="text-primary">Forgot Password?</h5>
                                    <%--<p class="text-muted">Reset password with velzon</p>--%>

                                    <div class="text-center">
                                        <p class="mb-0">Wait, I remember my password... <a href="<%=Page.ResolveUrl("~/SignIn") %>" class="fw-semibold text-primary text-decoration-underline"> Click here </a></p>
                                    </div>

                                    <lord-icon src="<%=Page.ResolveUrl("~/assets/json/rhvddzym.json") %>" trigger="loop" colors="primary:#ffc82d" class="avatar-xl"></lord-icon>
                                </div>

                                <div class="alert border-0 alert-warning text-center mb-2 mx-2" role="alert">
                                    Enter your email and instructions will be sent to you!
                                </div>

                                <div class="p-2">
                                    <div class="form">
                                        <div class="mb-4">
                                            <label class="form-label">Email</label>
                                            <input type="email" class="form-control" id="email" placeholder="Enter Email">
                                        </div>

                                        <div class="text-center mt-4">
                                            <button class="btn btn-tertiary w-100" type="submit">Send Reset Link</button>
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
</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
