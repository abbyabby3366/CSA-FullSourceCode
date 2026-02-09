<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="csa.Admin.Dashboard" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
    <!-- jsvectormap css -->
    <link href="<%=Page.ResolveUrl("~/assets/libs/jsvectormap/css/jsvectormap.min.css") %>" rel="stylesheet" type="text/css" />

    <!--Swiper slider css-->
    <link href="<%=Page.ResolveUrl("~/assets/libs/swiper/swiper-bundle.min.css") %>" rel="stylesheet" type="text/css" />
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
                            <h4 class="fs-16 mb-1">CSA Admin Dashboard</h4>
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
                <div class="row flex-row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col">
                                <div class="card card-height-100">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">New Users</h4>
                                        <div class="flex-shrink-0">
                                            <div class="dropdown card-header-dropdown">
                                                <a class="text-reset dropdown-btn" href="#" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    <span class="fw-semibold text-uppercase fs-12">Sort by: </span><span class="text-muted" id="spanTypeChart">Current Year<i class="mdi mdi-chevron-down ms-1"></i></span>
                                                </a>
                                                <div class="dropdown-menu dropdown-menu-end">
                                                    <a class="dropdown-item chart-type-item" href="#">Today</a>
                                                    <a class="dropdown-item chart-type-item" href="#">Last Week</a>
                                                    <a class="dropdown-item chart-type-item" href="#">Last Month</a>
                                                    <a class="dropdown-item chart-type-item" href="#">Current Year</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card-body">
                                        <%--<ul class="list-inline main-chart text-center mb-0">
                                            <li class="list-inline-item chart-border-left me-0 border-0">
                                                <h4 class="text-primary">$584k <span class="text-muted d-inline-block fs-13 align-middle ms-2">Revenue</span></h4>
                                            </li>
                                            <li class="list-inline-item chart-border-left me-0">
                                                <h4>$497k<span class="text-muted d-inline-block fs-13 align-middle ms-2">Expenses</span>
                                                </h4>
                                            </li>
                                            <li class="list-inline-item chart-border-left me-0">
                                                <h4><span data-plugin="counterup">3.6</span>%<span class="text-muted d-inline-block fs-13 align-middle ms-2">Profit Ratio</span></h4>
                                            </li>
                                        </ul>--%>

                                        <div id="revenue-expenses-charts" data-colors='["--vz-success"]' class="apex-charts" dir="ltr"></div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <div class="card card-height-100">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Top Performers</h4>
                                        <div>
                                            <button type="button" class="btn btn-soft-info btn-sm">
                                                1H
                                            </button>
                                            <button type="button" class="btn btn-soft-info btn-sm">
                                                1D
                                            </button>
                                            <button type="button" class="btn btn-soft-info btn-sm">
                                                7D
                                            </button>
                                            <button type="button" class="btn btn-soft-primary btn-sm">
                                                1M
                                            </button>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <ul class="list-group list-group-flush border-dashed mb-0">
                                            <li class="list-group-item d-flex align-items-center">
                                                <div class="flex-shrink-0">
                                                    <img src="assets/images/svg/crypto-icons/btc.svg" class="avatar-xs rounded-3 me-2" alt="">
                                                </div>
                                                <div class="flex-grow-1 ms-3">
                                                    <h6 class="fs-14 mb-1">KPI Counter</h6>
                                                    <p class="text-muted mb-0">Monthly KPI Counter</p>
                                                </div>
                                                <div class="flex-fill text-end">
                                                    <div class="live-preview w-100">
                                                        <div class="progress animated-progress mb-4">
                                                            <div class="progress-bar bg-primary" role="progressbar" style="width: 15%" aria-valuenow="15" aria-valuemin="0" aria-valuemax="100"></div>
                                                        </div>

                                                        <div class="me-auto">
                                                            <span class="text-sm text-gray-800 dark:text-white">14% Target</span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-4" style="display:none">
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

                    <div class="col-md-6">
                        <div class="card card-height-100">
                            <div class="card-header align-items-center d-flex">
                                <h4 class="card-title mb-0 flex-grow-1">Top 10 Performing Referrers</h4>
                                <div class="flex-shrink-0">
                                    <a href="<%= Page.ResolveUrl("~/Members") %>" class="btn btn-soft-primary btn-sm">
                                        View All
                                    </a>
                                </div>
                            </div>
                            <div class="card-body">
                                <ul class="list-group list-group-flush border-dashed mb-0" id="contentTopReferrals">
                                    
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row flex-row">
                    <div class="col-md-6">
                        <div class="col">
                            <div class="card card-height-100">
                                <div class="card-header align-items-center d-flex">
                                    <h4 class="card-title mb-0 flex-grow-1">Withdrawal Requests List</h4>
                                    <div class="flex-shrink-0">
                                        <a class="btn btn-soft-primary btn-sm" href="<%= Page.ResolveUrl("~/WithdrawalRequests") %>">
                                            View All
                                        </a>
                                    </div>
                                </div>

                                <div class="card-body">
                                    <div class="table-responsive table-card">
                                        <table class="table table-borderless table-nowrap align-middle mb-0">
                                            <thead class="table-light text-muted">
                                                <tr>
                                                    <th scope="col" class="text-center">DATETIME OF REQUEST</th>
                                                    <th scope="col" class="text-center">BANK ACCOUNT NUMBER</th>
                                                    <th scope="col" class="text-center">TRANSACTION AMOUNT</th>
                                                    <th scope="col" class="text-center">ACTION</th>
                                                </tr>
                                            </thead>
                                            <tbody id="tableBodyWithdrawalRequest">
                                               
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="col">
                            <div class="card card-height-100">
                                <div class="card-header align-items-center d-flex">
                                    <h4 class="card-title mb-0 flex-grow-1">New Member Approval List</h4>
                                    <div class="flex-shrink-0">
                                         <a class="btn btn-soft-primary btn-sm" href="<%= Page.ResolveUrl("~/NewMemberApproval") %>">
                                            View All
                                        </a>
                                    </div>
                                </div>

                                <div class="card-body">
                                    <div class="table-responsive table-card">
                                        <table class="table table-borderless table-nowrap align-middle mb-0">
                                            <thead class="table-light text-muted">
                                                <tr>
                                                    <th scope="col" class="text-center">FULL NAME</th>
                                                    <th scope="col" class="text-center">IC NUMBER</th>
                                                    <th scope="col" class="text-center">CONTACT NUMBER</th>
                                                    <th scope="col" class="text-center">ACTION</th>
                                                </tr>
                                            </thead>
                                            <tbody id="tableBodyMemberApproval">
                                                
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
    </div>
</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent" runat="server">
    <!-- apexcharts js -->
    <script src="<%=Page.ResolveUrl("~/assets/libs/apexcharts/apexcharts.min.js") %>"></script>

    <!-- vector map js -->
    <script src="<%=Page.ResolveUrl("~/assets/libs/jsvectormap/js/jsvectormap.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/assets/libs/jsvectormap/maps/world-merc.js") %>"></script>

    <%--https://www.cssscript.com/interactive-vector-map/--%>

    <!-- swiper slider js -->
    <script src="<%=Page.ResolveUrl("~/assets/libs/swiper/swiper-bundle.min.js") %>"></script>
    <!-- moment js -->
    <script src="<%=Page.ResolveUrl("~/assets/libs/moment/min/moment.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/assets/libs/moment/min/moment-with-locales.min.js") %>"></script>
    <script type="text/javascript">
        function getChartColorsArray(e) {
            if (null !== document.getElementById(e)) {
                var t = document.getElementById(e).getAttribute("data-colors");
                if (t) return (t = JSON.parse(t)).map(function (e) {
                    var t = e.replace(" ", "");
                    return -1 === t.indexOf(",") ? getComputedStyle(document.documentElement).getPropertyValue(t) || t : 2 == (e = e.split(",")).length ? "rgba(" + getComputedStyle(document.documentElement).getPropertyValue(e[0]) + "," + e[1] + ")" : t
                });
                console.warn("data-colors Attribute not found on:", e)
            }
        }

        //var options, chart, areachartSalesColors = getChartColorsArray("sales-forecast-chart"),
        //    dealTypeChartsColors = (areachartSalesColors && (options = {
        //        series: [{
        //            name: "Goal",
        //            data: [37]
        //        }, {
        //            name: "Pending Forcast",
        //            data: [12]
        //        }, {
        //            name: "Revenue",
        //            data: [18]
        //        }],
        //        chart: {
        //            type: "bar",
        //            height: 341,
        //            toolbar: {
        //                show: !1
        //            }
        //        },
        //        plotOptions: {
        //            bar: {
        //                horizontal: !1,
        //                columnWidth: "65%"
        //            }
        //        },
        //        stroke: {
        //            show: !0,
        //            width: 5,
        //            colors: ["transparent"],
        //        },
        //        xaxis: {
        //            categories: [""],
        //            axisTicks: {
        //                show: !1,
        //                borderType: "solid",
        //                color: "#78909C",
        //                height: 6,
        //                offsetX: 0,
        //                offsetY: 0
        //            },
        //            title: {
        //                text: "Total Forecasted Value",
        //                offsetX: 0,
        //                offsetY: -30,
        //                style: {
        //                    color: "#78909C",
        //                    fontSize: "12px",
        //                    fontWeight: 400
        //                }
        //            }
        //        },
        //        yaxis: {
        //            labels: {
        //                formatter: function (e) {
        //                    return "$" + e + "k"
        //                }
        //            },
        //            tickAmount: 4,
        //            min: 0
        //        },
        //        fill: {
        //            opacity: 1
        //        },
        //        legend: {
        //            show: !0,
        //            position: "bottom",
        //            horizontalAlign: "center",
        //            fontWeight: 500,
        //            offsetX: 0,
        //            offsetY: -14,
        //            itemMargin: {
        //                horizontal: 8,
        //                vertical: 0
        //            },
        //            markers: {
        //                width: 10,
        //                height: 10
        //            }
        //        },
        //        colors: areachartSalesColors
        //    }, (chart = new ApexCharts(document.querySelector("#sales-forecast-chart"), options)).render()), getChartColorsArray("deal-type-charts")),
        //    revenueExpensesChartsColors = (dealTypeChartsColors && (options = {
        //        series: [{
        //            name: "Pending",
        //            data: [80, 50, 30, 40, 100, 20]
        //        }, {
        //            name: "Loss",
        //            data: [20, 30, 40, 80, 20, 80]
        //        }, {
        //            name: "Won",
        //            data: [44, 76, 78, 13, 43, 10]
        //        }],
        //        chart: {
        //            height: 341,
        //            type: "radar",
        //            dropShadow: {
        //                enabled: !0,
        //                blur: 1,
        //                left: 1,
        //                top: 1
        //            },
        //            toolbar: {
        //                show: !1
        //            }
        //        },
        //        stroke: {
        //            width: 2
        //        },
        //        fill: {
        //            opacity: .2
        //        },
        //        legend: {
        //            show: !0,
        //            fontWeight: 500,
        //            offsetX: 0,
        //            offsetY: -8,
        //            markers: {
        //                width: 8,
        //                height: 8,
        //                radius: 6
        //            },
        //            itemMargin: {
        //                horizontal: 10,
        //                vertical: 0
        //            }
        //        },
        //        markers: {
        //            size: 0
        //        },
        //        colors: dealTypeChartsColors,
        //        xaxis: {
        //            categories: ["2016", "2017", "2018", "2019", "2020", "2021"]
        //        }
        //    }, (chart = new ApexCharts(document.querySelector("#deal-type-charts"), options)).render()), getChartColorsArray("revenue-expenses-charts"));
        //revenueExpensesChartsColors && (options = {
        //    series: [{
        //        name: "Users",
        //        data: [20, 25, 30, 35, 40, 55, 70, 110, 150, 180, 210, 50]
        //    }],
        //    chart: {
        //        height: 320,
        //        type: "area",
        //        toolbar: "false"
        //    },
        //    dataLabels: {
        //        enabled: !1
        //    },
        //    stroke: {
        //        curve: "straight",
        //        width: 2
        //    },
        //    xaxis: {
        //        categories: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"]
        //    },
        //    yaxis: {
        //        labels: {
        //            formatter: function (e) {
        //                return e + " users"
        //            }
        //        },
        //        tickAmount: 5,
        //        min: 0,
        //        max: 260
        //    },
        //    colors: revenueExpensesChartsColors,
        //    fill: {
        //        opacity: .06,
        //        colors: revenueExpensesChartsColors,
        //        type: "solid"
        //    }
        //}, (chart = new ApexCharts(document.querySelector("#revenue-expenses-charts"), options)).render());

        // Chart options
        var options = {
            chart: {
                type: 'bar',
                height: 350
            },
            series: [{
                name: 'Count',
                data: []
            }],
            xaxis: {
                categories: []
            }
        };

        // Create the chart
        var chart = new ApexCharts(document.querySelector("#revenue-expenses-charts"), options);
        chart.render();

        $('.chart-type-item').on('click', function () {
            fetchNewUser($(this).text())
        })

        async function fetchNewUser(type) {
            try {                
                $('#spanTypeChart').text(type)

                const res = await ApiHelper.post(window.location.origin + '/member/NewUserDashboard', {
                    type: type.toLowerCase()
                })

                if (!res.data.Error) {
                    chart.updateOptions({
                        series: [{
                            name: 'New User',
                            data: res.data.ObjVal.data
                        }],
                        xaxis: {
                            categories: res.data.ObjVal.categories
                        }
                    });
                }
            } catch (e) {
                console.log(e)
            }
        }

        var vm
        function loadDetails() {
            vm.MemberApprovals.data.forEach(function (e) {
                $('#tableBodyMemberApproval').append(`<tr class="text-center">
                                                    <td class="d-flex">
                                                        <div>
                                                            <span class="fw-medium">${e.FullName}</span>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <span class="fw-medium">${e.ICNumber ?? ''}</span>
                                                    </td>
                                                    <td>
                                                        <span class="fw-medium">${e.PhoneNumber}</span>
                                                    </td>
                                                    <td class="d-flex justify-content-around">
                                                        <span class="fs-5 fw-medium text-warning">Add Tag</span>
                                                        <span class="fs-5 fw-medium text-success">Approve</span>
                                                        <span class="fs-5 fw-medium text-danger">Reject</span>
                                                        <span class="fs-5 fw-medium text-primary">Reset Password</span>
                                                    </td>
                                                </tr>`)
            })

            vm.Withdrawals.data.forEach(function (e) {
                $('#tableBodyWithdrawalRequest').append(`<tr class="text-center">
                                                    <td class="d-flex">
                                                        <div>
                                                            <span class="fw-medium">${appHelper.dateToFormat(e.CreateDate)}</span>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <span class="fw-medium">${e.BankAccountNumber}</span>
                                                    </td>
                                                    <td>
                                                        <span class="fw-medium">${e.Amount.toFixed(2)}</span>
                                                    </td>
                                                    <td class="d-flex justify-content-around">
                                                        <span class="fs-5 fw-medium text-success">Approve</span>
                                                        <span class="fs-5 fw-medium text-danger">Reject</span>
                                                    </td>
                                                </tr>`)
            })

            vm.ListTopReferrals.forEach(function (e) {
                $('#contentTopReferrals').append(`<li class="list-group-item d-flex align-items-center">
                                        <div class="flex-grow-1 ms-3">
                                            <h6 class="fs-14 mb-1">${e.Name}</h6>
                                        </div>
                                        <div class="flex-fill text-end">
                                            <span class="fs-14">${e.Count}</span>
                                        </div>
                                    </li>`)
            })
        }

        $(document).ready(function () {

            //https://stackoverflow.com/questions/56142354/how-can-i-add-a-vertical-line-to-a-datatable
            vm = JSON.parse($('#<%= hfModelView.ClientID %>').val())
            loadDetails()

            fetchNewUser('Current Year')
        });
    </script>
</asp:Content>
