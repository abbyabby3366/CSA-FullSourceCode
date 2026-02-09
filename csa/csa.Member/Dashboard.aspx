<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="csa.Member.Dashboard" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
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
                            <h4 class="fs-16 mb-1">Dashboard</h4>
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

            <div>
                <div class="row">
                    <div class="col-lg-6">
                        <div class="row">
                            <div class="col-auto">
                                <div class="card">
                                    <div id="carouselExampleFade" class="carousel slide carousel-fade" data-ride="carousel">
                                        <div class="carousel-indicators">
                                            <button type="button" data-bs-target="#carouselExampleFade" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
                                            <button type="button" data-bs-target="#carouselExampleFade" data-bs-slide-to="1" aria-label="Slide 2"></button>
                                            <button type="button" data-bs-target="#carouselExampleFade" data-bs-slide-to="2" aria-label="Slide 3"></button>
                                        </div>
                                        <div class="carousel-inner">
                                            <div class="carousel-item active">
                                                <img class="d-block img-fluid mx-auto" src="/images/banner1.jpeg" alt="First slide">
                                            </div>
                                            <div class="carousel-item">
                                                <img class="d-block img-fluid mx-auto" src="/images/banner1.jpeg" alt="Second slide">
                                            </div>
                                            <div class="carousel-item">
                                                <img class="d-block img-fluid mx-auto" src="/images/banner1.jpeg" alt="Third slide">
                                            </div>
                                        </div>
                                        <a class="carousel-control-prev" href="#carouselExampleFade" role="button" data-bs-slide="prev">
                                            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                            <span class="sr-only">Previous</span>
                                        </a>
                                        <a class="carousel-control-next" href="#carouselExampleFade" role="button" data-bs-slide="next">
                                            <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                            <span class="sr-only">Next</span>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <div class="card card-height-100">
                                    <div class="card-body">
                                        <ul class="list-group list-group-flush border-dashed mb-0">
                                            <li class="list-group-item d-flex align-items-center">
                                                <div class="flex-shrink-0">
                                                    <img src="assets/images/gift.png" class="avatar-xs rounded-3 me-2" alt="">
                                                </div>
                                                <div class="flex-grow-1 ms-3">
                                                    <h6 class="fs-14 mb-1">Ganjaran</h6>
                                                    <%--<p class="text-muted mb-0">Malaysia Ringgit</p>--%>
                                                </div>
                                                <div class="flex-fill text-end">
                                                    <h6 class="fs-14 fw-semibold mb-1" id="lblMyWalletNow"></h6>
                                                    <p class="text-danger fs-12 mb-0" id="lblMyWalletTraffic"></p>
                                                    <%--<p class="text-success fs-12 mb-0"><i class="mdi mdi-trending-up align-middle me-1"></i> +30.00 MYR</p>--%>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>

                            <div class="col">
                                <div class="card card-height-100">
                                    <div class="card-body">
                                        <ul class="list-group list-group-flush border-dashed mb-0">
                                            <li class="list-group-item d-flex align-items-center">
                                                <div class="flex-shrink-0">
                                                    <img src="assets/images/gift.png" class="avatar-xs rounded-3 me-2" alt="">
                                                </div>
                                                <div class="flex-grow-1 ms-3">
                                                    <h6 class="fs-14 mb-1">Peserta</h6>
                                                    <p class="text-muted mb-0">Jumlah Peserta Rujukan</p>
                                                </div>
                                                <div class="flex-fill text-end">
                                                    <h6 class="fs-14 fw-semibold mb-1" id="lblMyReferralNow"></h6>
                                                    <p class="text-success fs-12 mb-0" id="lblMyReferralTraffic"></p>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>

                            <div class="col">
                                <div class="card card-height-100">
                                    <div class="card-body">
                                        <ul class="list-group list-group-flush border-dashed mb-0">
                                            <li class="list-group-item d-flex align-items-center">
                                                <div class="flex-shrink-0">
                                                    <img src="assets/images/gift.png" class="avatar-xs rounded-3 me-2" alt="">
                                                </div>
                                                <div class="flex-grow-1 ms-3">
                                                    <h6 class="fs-14 mb-1">Savings</h6>
                                                    <%--<p class="text-muted mb-0">Total number of my referral</p>--%>
                                                </div>
                                                <div class="flex-fill text-end">
                                                    <h6 class="fs-14 fw-semibold mb-1" id="lblSavingsNow"></h6>
                                                    <%--<p class="text-success fs-12 mb-0" id="lblMyReferralTraffic"></p>--%>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-6" style="display:none">
                        <div class="card card-height-100">
                            <div class="card-header align-items-center d-flex">
                                <h4 class="card-title mb-0 flex-grow-1">Calendar (Upcoming Events)</h4>
                                <div class="flex-shrink-0">
                                    <button type="button" class="btn btn-soft-primary btn-sm">
                                        View All
                                    </button>
                                </div>
                            </div>

                            <div class="card-body">
                                <div class="overflow-x-hidden overflow-y-scroll" style="height: 500px">
                                    <div class="timeline-2">
                                        <div class="timeline-year">
                                            <p>12 Dec 2021</p>
                                        </div>
                                        <div class="timeline-continue">
                                            <div class="row timeline-right">
                                                <div class="col-12">
                                                    <p class="timeline-date">
                                                        02:35AM to 04:30PM
                                                    </p>
                                                </div>
                                                <div class="col-12">
                                                    <div class="timeline-box">
                                                        <div class="timeline-text">
                                                            <div class="d-flex">
                                                                <img src="assets/images/users/avatar-7.jpg" alt="" class="avatar-sm rounded" />
                                                                <div class="flex-grow-1 ms-3">
                                                                    <h5 class="mb-1">Frank hook joined with our company</h5>
                                                                    <p class="text-muted mb-0">It makes a statement, it’s impressive graphic design. Increase or decrease the letter spacing depending on the situation and try, try again until it looks right, and each letter has the perfect spot of its own. </p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row timeline-right">
                                                <div class="col-12">
                                                    <p class="timeline-date">
                                                        02:35AM to 04:30PM
                                                    </p>
                                                </div>
                                                <div class="col-12">
                                                    <div class="timeline-box">
                                                        <div class="timeline-text">
                                                            <h5>Trip planning</h5>
                                                            <p class="text-muted">In the trip planner, keep only one or two activities in a day if the purpose of the trip is to relax and take it easy during the vacation :</p>
                                                            <div class="row border border-dashed rounded gx-2 p-2">
                                                                <div class="col-3">
                                                                    <a href="javascript:void(0);">
                                                                        <img src="assets/images/small/img-7.jpg" alt="" class="img-fluid rounded"></a>
                                                                </div>
                                                                <!--end col-->
                                                                <div class="col-3">
                                                                    <a href="javascript:void(0);">
                                                                        <img src="assets/images/small/img-3.jpg" alt="" class="img-fluid rounded"></a>
                                                                </div>
                                                                <!--end col-->
                                                                <div class="col-3">
                                                                    <a href="javascript:void(0);">
                                                                        <img src="assets/images/small/img-10.jpg" alt="" class="img-fluid rounded"></a>
                                                                </div>
                                                                <!--end col-->
                                                                <div class="col-3">
                                                                    <a href="javascript:void(0);">
                                                                        <img src="assets/images/small/img-9.jpg" alt="" class="img-fluid rounded"></a>
                                                                </div>
                                                                <!--end col-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="timeline-year">
                                                        <p>08 Dec 2021</p>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row timeline-right">
                                                <div class="col-12">
                                                    <p class="timeline-date">
                                                        02:35AM to 04:30PM
                                                    </p>
                                                </div>
                                                <div class="col-12">
                                                    <div class="timeline-box">
                                                        <div class="timeline-text">
                                                            <h5>Velzon - Project Discussion</h5>
                                                            <p class="text-muted mb-0">The purpose of the discussion is to interpret and describe the significance of your findings in light of what was already known about the research problem being investigated, and to explain any new understanding or fresh insights about the problem after you've taken the findings into consideration.</p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row timeline-right">
                                                <div class="col-12">
                                                    <p class="timeline-date">
                                                        10:24AM to 11:15PM
                                                    </p>
                                                </div>
                                                <div class="col-12">
                                                    <div class="timeline-box">
                                                        <div class="timeline-text">
                                                            <h5>Client Meeting (Nancy Martino)</h5>
                                                            <p class="text-muted mb-0">A client meeting, meaning, direct collaboration and communication with a customer, is the best way to understand their needs and how you can help support them.</p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row timeline-right">
                                                <div class="col-12">
                                                    <p class="timeline-date">
                                                        9:20AM to 10:05PM
                                                    </p>
                                                </div>
                                                <div class="col-12">
                                                    <div class="timeline-box">
                                                        <div class="timeline-text">
                                                            <h5>Designer & Developer Meeting</h5>
                                                            <ul class="mb-0 text-muted vstack gap-2">
                                                                <li>Last updates</li>
                                                                <li>Weekly Planning</li>
                                                                <li>Work Distribution</li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="timeline-year">
                                            <p><span>05 Dec 2021</span></p>
                                        </div>
                                        <div class="timeline-launch">
                                            <div class="timeline-box ms-2">
                                                <div class="timeline-text">
                                                    <h5>Our Company Activity</h5>
                                                    <p class="text-muted text-capitalize mb-0">Wow...!!! What a Journey So Far...!!!</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-6">
                        <div class="card card-height-100">
                            <div class="card-header align-items-center d-flex">
                                <h4 class="card-title mb-0 flex-grow-1">Announcement</h4>
                                <div class="flex-shrink-0">
                                    <button type="button" class="btn btn-soft-primary btn-sm">
                                        View All
                                    </button>
                                </div>
                            </div>
                            <div class="card-body">
                                <ul class="list-group list-group-flush border-dashed mb-0" id="bodyAnnouncement">
                                    
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row flex-row">
                    <div class="col-lg-6">
                        <div class="card card-height-100">
                            <div class="card-header align-items-center d-flex">
                                <h4 class="card-title mb-0 flex-grow-1">Recent Transactions</h4>
                                <div class="flex-shrink-0">
                                    <a class="btn btn-soft-primary btn-sm" href="<%= Page.ResolveUrl("~/Wallet") %>">
                                        View All
                                    </a>
                                </div>
                            </div>

                            <div class="card-body">
                                <div class="table-responsive table-card">
                                    <table class="table table-borderless table-nowrap align-middle mb-0">
                                        <thead class="table-light text-muted">
                                            <tr>
                                                <th scope="col" class="text-center">TRANSACTION ID</th>
                                                <th scope="col" class="text-center">CUSTOMER ID</th>
                                                <th scope="col" class="text-center">TRANSACTION DATE</th>
                                                <th scope="col" class="text-center">TRANSACTION TYPE</th>
                                                <th scope="col" class="text-center">TRANSACTION AMOUNT</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tableBodyHistory">
                                            
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-6">
                        <div class="card card-height-100">
                            <div class="card-header align-items-center d-flex">
                                <h4 class="card-title mb-0 flex-grow-1">List of Peserta</h4>
                                <div class="flex-shrink-0">
                                    <a class="btn btn-soft-primary btn-sm" href="<%= Page.ResolveUrl("~/Referrals") %>">
                                        View All
                                    </a>
                                </div>
                            </div>

                            <div class="card-body">
                                <div class="table-responsive table-card">
                                    <table class="table table-borderless table-nowrap align-middle mb-0">
                                        <thead class="table-light text-muted">
                                            <tr>
                                                <th scope="col" class="text-center">NO.</th>
                                                <th scope="col" class="text-center">CUSTOMER NAME</th>
                                                <th scope="col" class="text-center">CUSTOMER ID</th>
                                                <th scope="col" class="text-center">CONTACT NO</th>
                                                <th scope="col" class="text-center">STATUS</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tableBodyReferral">
                                            
                                        </tbody>
                                    </table>
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
    <!-- moment js -->
    <script src="<%=Page.ResolveUrl("~/assets/libs/moment/min/moment.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/assets/libs/moment/min/moment-with-locales.min.js") %>"></script>
    <script type="text/javascript">
        var vm
        var lblMyWalletLast = $('#lblMyWalletTraffic')
        var lblMyReferralTraffic = $('#lblMyReferralTraffic')

        function loadDetails() {
            $('#lblMyWalletNow').text(`${vm.MyWalletNow.toFixed(2)}`)
            let differentAmount = vm.MyWalletNow - vm.MyWalletLast
            let operatorAmount = ''
            if (vm.MyWalletNow > vm.MyWalletLast) {
                operatorAmount = '+'
                lblMyWalletLast.removeClass('text-danger').addClass('text-success')
                lblMyWalletLast.html('<i class="mdi mdi-trending-up align-middle me-1">')
            }
            else if (vm.MyWalletNow < vm.MyWalletLast) {
                operatorAmount = '-'
                lblMyWalletLast.html('<i class="mdi mdi-trending-down align-middle me-1">')
            }
            lblMyWalletLast.append(`${operatorAmount}${differentAmount.toFixed(2)}`)


            $('#lblMyReferralNow').text(`${vm.MyReferralNow}`)
            let differentReferralCount = vm.MyReferralNow - vm.MyReferralLast
            operatorAmount = ''
            if (vm.MyReferralNow > vm.MyReferralLast) {
                operatorAmount = '+'
                lblMyReferralTraffic.removeClass('text-danger').addClass('text-success')
                lblMyReferralTraffic.html('<i class="mdi mdi-trending-up align-middle me-1">')
            }
            else if (vm.MyReferralNow < vm.MyReferralLast) {
                operatorAmount = '-'
                lblMyReferralTraffic.html('<i class="mdi mdi-trending-down align-middle me-1">')
            }
            lblMyReferralTraffic.append(`${operatorAmount}${differentReferralCount}`)

            vm.LastHistories.data.forEach(function (e) {
                $('#tableBodyHistory').append(`<tr class="text-center">
                                                <td>
                                                    <span class="fw-medium">#${e.HistoryId}</span>
                                                </td>
                                                <td>
                                                    <span class="fw-medium">#${e.MemberId}</span>
                                                </td>
                                                <td>
                                                    <span class="fw-medium">${moment(e.CreateDate).format("YYYY-MM-DD HH:mm:ss")}</span>
                                                </td>
                                                <td>
                                                    <span class="fw-medium">${e.TransactionType}</span>
                                                </td>
                                                <td>
                                                    <span class="fw-medium">${e.TransactionAmount.toFixed(2)}</span>
                                                </td>
                                            </tr>`)
            })

            vm.LastReferrals.data.forEach(function (e) {
                $('#tableBodyReferral').append(`<tr class="text-center">
                                                <td>
                                                    <span class="fw-medium">${e.SequenceId}</span>
                                                </td>
                                                <td>
                                                    <span class="fw-medium">${e.FullName}</span>
                                                </td>
                                                <td>
                                                    <span class="fw-medium">${e.MemberId}</span>
                                                </td>
                                                <td>
                                                    <span class="fw-medium">${e.PhoneNumber}</span>
                                                </td>
                                                <td>
                                                    <span class="fw-medium">${e.Status}</span>
                                                </td>
                                            </tr>`)
            })

            vm.ListAnnouncement.data.forEach(function (e) {
                $('#bodyAnnouncement').append(`<li class="list-group-item d-flex align-items-center">
                                        <div class="flex-shrink-0">
                                            <img src="assets/images/svg/crypto-icons/btc.svg" class="avatar-xs rounded-3 me-2" alt="">
                                        </div>
                                        <div class="flex-grow-1 ms-3">
                                            <h6 class="fs-14 mb-1">${e.Title}</h6>
                                            <p class="text-muted mb-0">${e.Content}</p>
                                        </div>
                                    </li>`)
            })

            $('#lblSavingsNow').text(`${vm.SavingsNow.toFixed(2)}`)
        }

        $(document).ready(function () {
            vm = JSON.parse($('#<%= hfModelView.ClientID %>').val())
            loadDetails()
        });
    </script>
</asp:Content>

