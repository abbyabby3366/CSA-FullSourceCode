<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewApplication.aspx.cs" Inherits="csa.Member.AddNewApplication" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="server">
    <!-- select2 -->
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2.min.css") %>" />
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2-bootstrap-5-theme.min.css") %>" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfModelView" runat="server" />
    <div class="page-content">
        <div class="container-fluid">

            <div class="profile-foreground position-relative mx-n4 mt-n4">
                <div class="profile-wid-bg">
                    <img src="assets/images/profile-bg.jpg" alt="" class="profile-wid-img" />
                </div>
            </div>

            <div class="" style="height: 300px;"></div>

            <div class="row">
                <div class="col-12 col-xxl-10 col-xl-10 col-lg-10 col-md-12 col-sm-12 offset-xxl-1 offset-xl-1 offset-lg-1">
                    <div class="card mt-n4">
                        <div class="card-header d-flex align-items-center">
                            <h5 class="card-title mb-0 flex-grow-1"><i class="mdi mdi-file-document-edit-outline"></i> Apply Now</h5>
                        </div>

                        <!-- personal details -->
                        <div class="card-body" id="card-content">
                                <div class="row g-3 mb-2">
                                    <div class="col-mx">
                                        <div class="form">
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Program/Event Name</label>
                                                        <select class="form-select" name="programEvent">
                                                            <option value="0">Select an option</option>
                                                            <option value="1" selected>YABAM</option>
                                                            <%--<option value="2">PFC</option>--%>
                                                            <%--<option value="3">Siri jelajah etc</option>--%>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Full Name as per IC</label>
                                                        <input type="text" class="form-control" name="fullName"/>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Date of Birth</label>
                                                        <input type="text" class="form-control date-input" name="dateOfBirth" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Gender</label>
                                                        <select class="form-select" name="gender">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">Male</option>
                                                            <option value="2">Female</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Identification Number</label>
                                                        <input type="text" class="form-control" name="icNumber"/>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Address (Home/Current)</label>
                                                        <input type="text" class="form-control" name="address" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Phone Number</label>
                                                        <div class="d-flex align-items-center gap-3">
                                            <span>+60</span>
                                                        <input type="text" class="form-control" name="phoneNumber" disabled />
                                                            </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Email Address</label>
                                                        <input type="email" class="form-control" name="email" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Tax Number</label>
                                                        <input type="text" class="form-control" name="taxNumber" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Race</label>
                                                        <select class="form-select" name="race">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">Malay</option>
                                                            <option value="2">Chinese</option>
                                                            <option value="3">Indian</option>
                                                            <option value="4">Sabahan</option>
                                                            <option value="5">Sarawakian</option>
                                                            <option value="6">Other</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Religion</label>
                                                        <select class="form-select" name="religion">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">Islam</option>
                                                            <option value="2">Hindu</option>
                                                            <option value="3">Buddhist</option>
                                                            <option value="4">Christian</option>
                                                            <option value="6">Others</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Highest Level of Education</label>
                                                        <select class="form-select" name="education">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">Diploma</option>
                                                            <option value="2">Bachelor</option>
                                                            <option value="3">Master</option>
                                                            <option value="4">PhD</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Marital Status</label>
                                                        <select class="form-select" name="maritalStatus">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">Married</option>
                                                            <option value="2">Single</option>
                                                            <option value="3">Divorce</option>
                                                            <option value="4">Polygamy</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <h5 class="fs-15 col-12 mt-3">Spouse Details</h5>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Spouse's Full Name</label>
                                                        <input type="text" class="form-control" name="spouseName" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Spouse's Identification Number</label>
                                                        <input type="text" class="form-control" name="spouseIC" />
                                                    </div>
                                                </div> 
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Spouse's Contact Information</label>
                                                        <input type="text" class="form-control" name="spouseContactInformation" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Spouse's Occupation</label>
                                                        <input type="text" class="form-control" name="spouseOccupation" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Spouse's Company Address</label>
                                                        <input type="text" class="form-control" name="spouseCompanyAddress" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Spouse's Salary</label>
                                                        <input type="text" class="form-control decimal-input" name="spouseSalary" />
                                                    </div>
                                                </div>

                                                <h5 class="fs-15 col-12 mt-3">Family Details</h5>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Number of Dependent</label>
                                                        <input type="number" class="form-control" name="familyNumberOfDependent" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Do you have any OKU family members</label>
                                                        <select class="form-select" name="familyHasOKU">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">Yes</option>
                                                            <option value="2">No</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Father's Name</label>
                                                        <input type="text" class="form-control" name="familyFatherName" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Father's Contact Number</label>
                                                        <input type="text" class="form-control" name="familyFatherContactNumber" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Father's Address</label>
                                                        <input type="text" class="form-control" name="familyFatherAddress" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Mother's Name</label>
                                                        <input type="text" class="form-control" name="familyMotherName" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Mother's Contact Number</label>
                                                        <input type="text" class="form-control" name="familyMotherContactNumber" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Mother's Address</label>
                                                        <input type="text" class="form-control" name="familyMotherAddress" />
                                                        <div class="mt-2">
                                                            <label>same with father's address</label>
                                                            <input type="checkbox" id="cbCopyFatherAddress" />
                                                        </div>
                                                    </div>
                                                </div>


                                                <h5 class="fs-15 col-12 mt-3">Company/Employment Details</h5>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Employer Type</label>
                                                        <select class="form-select nyatakan" name="companyEmpoyerTypeId" data-nyatakan="6">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">GOV/AN</option>
                                                            <option value="2">GLC</option>
                                                            <option value="3">MNC</option>
                                                            <option value="4">Private</option>
                                                            <option value="5">Professional Certs</option>
                                                            <option value="6">Others</option>
                                                        </select>
                                                         <div id="nyatakan_companyEmpoyerTypeId" class="d-none">
                                                            <label class="mt-2">Sila nyatakan</label>
                                                            <input type="text" class="form-control" name="companyEmpoyerTypeOther" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Employer Name</label>
                                                        <input type="text" class="form-control" name="companyEmployerName" />
                                                    </div>
                                                </div>
                                                <%--soon company name delete--%>
                                                <div class="col-lg-6 d-none">
                                                    <div class="mb-3">
                                                        <label>Company Name</label>
                                                        <input type="text" class="form-control" name="companyName" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Job Title</label>
                                                        <input type="text" class="form-control" name="companyJobTitle" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Detailed sector</label>
                                                        <select class="form-select nyatakan" name="companySectorId" data-nyatakan="<%= csa.Library.Constant.OTHER_NUMBER %>">
                                                            
                                                        </select>
                                                        <div id="nyatakan_companySectorId" class="d-none">
                                                            <label class="mt-2">Sila nyatakan</label>
                                                            <input type="text" class="form-control" name="companySectorOther" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Department</label>
                                                        <select class="form-select nyatakan" name="companyDepartmentId" data-nyatakan="<%= csa.Library.Constant.OTHER_NUMBER %>">
                                                        </select>
                                                        <div id="nyatakan_companyDepartmentId" class="d-none">
                                                            <label class="mt-2">Sila nyatakan</label>
                                                            <input type="text" class="form-control" name="companyDepartmentOther" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Company Address</label>
                                                        <input type="text" class="form-control" name="companyAddress" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Office Contact Number/HR</label>
                                                        <input type="text" class="form-control" name="companyOfficeContactNumber" />
                                                    </div>
                                                </div>                                                
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Employment Status</label>
                                                        <select class="form-select nyatakan" name="companyEmploymentStatusId" data-nyatakan="4">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">Fixed</option>
                                                            <option value="2">Contract</option>
                                                            <option value="3">Self Employed</option>
                                                            <option value="4">Other</option>
                                                        </select>
                                                        <div id="nyatakan_companyEmploymentStatusId" class="d-none">
                                                            <label class="mt-2">Sila nyatakan</label>
                                                            <input type="text" class="form-control" name="companyEmploymentStatusOther" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Retirement Age</label>
                                                        <input type="number" class="form-control" name="companyRetirementAge" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Year of Service</label>
                                                        <select class="form-select" name="companyYearOfService">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">Kurang dari 1</option>
                                                            <option value="2">1-2</option>
                                                            <option value="3">3-5</option>
                                                            <option value="4">Lebih dari 5</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Salary Range</label>
                                                        <select class="form-select" name="salaryRange">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">Kurang RM2,500</option>
                                                            <option value="2">RM2,501 - RM3,170</option>
                                                            <option value="3">RM3,171 - RM3,970</option>
                                                            <option value="4">RM3,971 - RM4,850</option>
                                                            <option value="5">RM4,851 - RM5,880</option>
                                                            <option value="6">RM5,881 - RM7,100</option>
                                                            <option value="7">RM7,101 - RM8,700</option>
                                                            <option value="8">RM8,701 - RM10,970</option>
                                                            <option value="9">RM10,971 - RM15,040</option>
                                                            <option value="10">RM15,041 dan lebih</option>
                                                        </select>
                                                    </div>
                                                </div>



                                                <h5 class="fs-15 col-12 mt-3">Emergency Contact (Not Living Together)</h5>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Emergency Contact Person</label>
                                                        <input type="text" class="form-control" name="emergencyContactPerson" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Relationship to Emergency Contact</label>
                                                        <select class="form-select" name="emergencyRelationShipId">
                                                            <option value="0">Select an option</option>
                                                            <option value="1">Father</option>
                                                            <option value="2">Mother</option>
                                                            <option value="3">Sibling</option>
                                                            <option value="4">Wife</option>
                                                            <option value="5">Husband</option>
                                                            <option value="6">Others</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Emergency Contact Number</label>
                                                        <input type="text" class="form-control" name="emergencyContactNumber" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Emergency Contact ICNumber</label>
                                                        <input type="text" class="form-control" name="emergencyIC" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Emergency Contact Occupation</label>
                                                        <input type="text" class="form-control" name="emergencyOccupation" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Emergency Contact Address</label>
                                                        <input type="text" class="form-control" name="emergencyAddress" />
                                                    </div>
                                                </div>



                                                <h5 class="fs-15 col-12 mt-3">Bank Details</h5>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Bank Name</label>
                                                        <select class="form-select nyatakan" data-nyatakan="<%= csa.Library.Constant.OTHER_NUMBER %>" name="bankId"></select>
                                                        <div id="nyatakan_bankId" class="d-none">
                                                                <label class="mt-2">Sila nyatakan</label>
                                                                <input type="text" class="form-control" name="bankOther" />
                                                            </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Account Number</label>
                                                        <input type="text" class="form-control" name="bankAccountNumber" />
                                                    </div>
                                                </div>

                                                <h5 class="fs-15 col-12 mt-3">Upload Documents</h5>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label class="form-label ">IC</label>
                                                        <%--<a id="anchorIc" href="#" target="_blank" class="d-none"></a>
                                                        <input type="file" class="form-control" name="upldIc" accept=".png, .jpg, .jpeg, .pdf"/>--%>
                                                        <span class="float-end d-none" id="icAction">
                                                                <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                                <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                            </span>
                                                            <div class="input-group">
                                                                <label class="btn btn-primary input-group-text btn-upload" for="upldic">Upload File</label>
                                                                <input class="form-control" id="txtic" readonly onclick="openInputFile('upldic')"/>
                                                            </div>
                                                            <input class="form-control d-none" type="file" id="upldic" onchange="onChangeUpload(event,'txtic')">
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label class="form-label ">Payslip</label>
                                                        <%--<a id="anchorPayslip" href="#" target="_blank" class="d-none"></a>
                                                        <input type="file" class="form-control" name="upldPayslip" accept=".png, .jpg, .jpeg, .pdf"/>--%>
                                                        <span class="float-end d-none" id="payslipAction">
                                                                <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                                <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                            </span>
                                                            <div class="input-group">
                                                                <label class="btn btn-primary input-group-text btn-upload" for="upldpayslip">Upload File</label>
                                                                <input class="form-control" id="txtpayslip" readonly onclick="openInputFile('upldpayslip')"/>
                                                            </div>
                                                            <input class="form-control d-none" type="file" id="upldpayslip" onchange="onChangeUpload(event,'txtpayslip')">
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label class="form-label ">Offer letter / Confirmation letter</label>
                                                        <%--<a id="anchorOfferLetter" href="#" target="_blank" class="d-none"></a>
                                                        <input type="file" class="form-control" name="upldOfferLetter" accept=".png, .jpg, .jpeg, .pdf"/>--%>
                                                        <span class="float-end d-none" id="offerletterAction">
                                                                <button type="button" class="btn btn-text btn-download-file"><i class="me-1 ri-download-line"></i></button>
                                                                <a href="#" target="_blank" class="btn btn-text"><i class="me-1 ri-search-line"></i></a>
                                                            </span>
                                                            <div class="input-group">
                                                                <label class="btn btn-primary input-group-text btn-upload" for="upldofferletter">Upload File</label>
                                                                <input class="form-control" id="txtofferletter" readonly onclick="openInputFile('upldofferletter')"/>
                                                            </div>
                                                            <input class="form-control d-none" type="file" id="upldofferletter" onchange="onChangeUpload(event,'txtofferletter')">
                                                    </div>
                                                </div>



                                                <h5 class="fs-15 col-12 mt-3">Other Information</h5>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Preferred Language</label>
                                                        <input type="text" class="form-control" name="otherLanguage" />
                                                    </div>
                                                </div> 
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Hobbies/Interest</label>
                                                        <input type="text" class="form-control" name="otherHobbies" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>Social Media Handles</label>
                                                        <input type="text" class="form-control" name="otherSocialMediaHandles" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="mb-3">
                                                        <label>FB Name</label>
                                                        <input type="text" class="form-control" name="otherFbName" />
                                                    </div>
                                                </div>



                                            </div>
                                        </div>
                                    </div>
                                </div>

                            <div class="card-footer">
                                <div class="col-lg-12">
                                    <div class="hstack gap-2 justify-content-end">
                                        <button class="btn btn-soft-primary" id="btnSubmit"><i class="ri-send-plane-2-line me-1 align-middle"></i> Submit</button>
                                        <a href="<%=Page.ResolveUrl("~/MyProfile")%>" class="btn btn-soft-secondary"><i class="ri-close-circle-line me-1 align-middle"></i> Cancel</a>
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
    <script src="<%=Page.ResolveUrl("~/assets/libs/jqueryValidation/dist/jquery.validate.js") %>"></script>
    <!-- select2 js -->
    <script  type='text/javascript' src="<%=Page.ResolveUrl("~/assets/libs/select2/js/select2.min.js") %>"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">

        $(".date-input").datepicker({
            format: 'dd-mm-yyyy',
            autoclose: true
        });

        //control
        var programEvent = $('[name="programEvent"]')
        var fullName = $('[name="fullName"]')
        var dateOfBirth = $('[name="dateOfBirth"]')
        var gender = $('[name="gender"]')
        var icNumber = $('[name="icNumber"]')
        var address = $('[name="address"]')
        var phoneNumber = $('[name="phoneNumber"]')
        var email = $('[name="email"]')
        var taxNumber = $('[name="taxNumber"]')
        var race = $('[name="race"]')
        var religion = $('[name="religion"]')
        var education = $('[name="education"]')
        var maritalStatus = $('[name="maritalStatus"]')
        var referralName = $('[name="referralName"]')
        var spouseName = $('[name="spouseName"]')
        var spouseIC = $('[name="spouseIC"]')
        var spouseContactInformation = $('[name="spouseContactInformation"]')
        var spouseOccupation = $('[name="spouseOccupation"]')
        var spouseCompanyAddress = $('[name="spouseCompanyAddress"]')
        var spouseSalary = $('[name="spouseSalary"]')
        var familyNumberOfDependent = $('[name="familyNumberOfDependent"]')
        var familyHasOKU = $('[name="familyHasOKU"]')
        var familyFatherName = $('[name="familyFatherName"]')
        var familyFatherContactNumber = $('[name="familyFatherContactNumber"]')
        var familyFatherAddress = $('[name="familyFatherAddress"]')
        var familyMotherName = $('[name="familyMotherName"]')
        var familyMotherContactNumber = $('[name="familyMotherContactNumber"]')
        var familyMotherAddress = $('[name="familyMotherAddress"]')
        var companyEmpoyerTypeId = $('[name="companyEmpoyerTypeId"]')
        var companyName = $('[name="companyName"]')
        var companyJobTitle = $('[name="companyJobTitle"]')
        var companySectorId = $('[name="companySectorId"]')
        var companySectorOther = $('[name="companySectorOther"]')
        var companyDepartmentId = $('[name="companyDepartmentId"]')
        var companyDepartmentOther = $('[name="companyDepartmentOther"]')
        var companyAddress = $('[name="companyAddress"]')
        var companyOfficeContactNumber = $('[name="companyOfficeContactNumber"]')
        var companyEmploymentStatusId = $('[name="companyEmploymentStatusId"]')
        var companyRetirementAge = $('[name="companyRetirementAge"]')
        var companyYearOfService = $('[name="companyYearOfService"]')
        var salaryRange = $('[name="salaryRange"]')
        var emergencyContactPerson = $('[name="emergencyContactPerson"]')
        var emergencyRelationShipId = $('[name="emergencyRelationShipId"]')
        var emergencyContactNumber = $('[name="emergencyContactNumber"]')
        var emergencyIC = $('[name="emergencyIC"]')
        var emergencyOccupation = $('[name="emergencyOccupation"]')
        var emergencyAddress = $('[name="emergencyAddress"]')
        var bankId = $('[name="bankId"]')
        var bankAccountNumber = $('[name="bankAccountNumber"]')
        var bankOther = $('[name="bankOther"]')
        //var upldIc = $('[name="upldIc"]')
        //var upldPayslip = $('[name="upldPayslip"]')
        //var upldOfferLetter = $('[name="upldOfferLetter"]')
        var otherLanguage = $('[name="otherLanguage"]')
        var otherHobbies = $('[name="otherHobbies"]')
        var otherSocialMediaHandles = $('[name="otherSocialMediaHandles"]')
        var otherFbName = $('[name="otherFbName"]')
        var companyEmploymentStatusOther = $('[name="companyEmploymentStatusOther"]')
        var companyEmpoyerTypeOther = $('[name="companyEmpoyerTypeOther"]')
        var companyEmployerName = $('[name="companyEmployerName"]')

        var form = $('#form1')
        form.validate({
            rules: {
                programEvent: {
                    notZero: true
                },
                gender: {
                    notZero: true,
                },
                icNumber: {
                    required: true,
                },
                companyEmpoyerTypeId: {
                    notZero: true,
                },
                //companyName: {
                //    required: true,
                //},
                companySectorId: {
                    notZero: true,
                },
                companyEmploymentStatusId: {
                    notZero: true,
                },
                companyRetirementAge: {
                    required: true,
                },
                companyYearOfService: {
                    required: true,
                },
                bankId: {
                    notZero: true,
                },
                bankAccountNumber: {
                    required: true,
                },
                upldIc: {
                    required: {
                        depends: function (e) {
                            return vm.ICFile == null
                        }
                    },
                },
                upldPayslip: {
                    required: {
                        depends: function (e) {
                            return vm.PayslipFile == null
                        }
                    },
                },
                salaryRange: {
                    notZero: true
                }
            },
            errorClass: 'is-invalid',
            errorPlacement: function (error, element) {
                error.addClass("invalid-feedback");
                error.insertAfter(element);
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

        $('#btnSubmit').click(async function (e) {
            e.preventDefault()
            if (!form.valid()) {
                dialogHelper.error('Please fill in the required fields.')
                return
            }

            $(this).prop("disabled", true)

            try {
                const formData = new FormData()
                formData.append('IcFile', $('#upldic')[0].files[0])
                formData.append('PayslipFile', $('#upldpayslip')[0].files[0])
                formData.append('OfferLetterFile', $('#upldofferletter')[0].files[0])


            const data = {
                MemberId: <%= csa.Member.Helpers.SessionManager.CurrentLoginMember.MemberId %>,
                ProgramEventId: programEvent.val(),
                FullName: fullName.val(),
                DateOfBirth: appHelper.convertToApiDate(dateOfBirth.val()),
                Gender: gender.val(),
                ICNumber: icNumber.val(),
                Address: address.val(),
                PhoneNumber: phoneNumber.val(),
                Email: email.val(),
                TaxNumber: taxNumber.val(),
                RaceId: race.val(),
                ReligionId: religion.val(),
                HighestLevelOfEducationId: education.val(),
                MaritalStatusId: maritalStatus.val(),
                Spouse: {
                    FullName: spouseName.val(),
                    ICNumber: spouseIC.val(),
                    ContactInformation: spouseContactInformation.val(),
                    Occupation: spouseOccupation.val(),
                    CompanyAddress: spouseCompanyAddress.val(),
                    Salary: spouseSalary.val(),
                },
                Family: {
                    NumberOfDependent: familyNumberOfDependent.val(),
                    IsHasOKU: familyHasOKU.val(),
                    FatherName: familyFatherName.val(),
                    FatherContactNumber: familyFatherContactNumber.val(),
                    FatherAddress: familyFatherAddress.val(),
                    MotherName: familyMotherName.val(),
                    MotherContactNumber: familyMotherContactNumber.val(),
                    MotherAddress: familyMotherAddress.val(),
                },
                Company: {
                    EmployerTypeId: companyEmpoyerTypeId.val(),
                    CompanyName: companyName.val(),
                    JobTitle: companyJobTitle.val(),
                    SectorId: companySectorId.val() === 'null' ? null : companySectorId.val(),
                    SectorOther: companySectorOther.val(),
                    DepartmentId: companyDepartmentId.val() === 'null' ? null : companyDepartmentId.val(),
                    DepartmentOther: companyDepartmentOther.val(),
                    CompanyAddress: companyAddress.val(),
                    OfficeContactNumber: companyOfficeContactNumber.val(),
                    EmploymentStatusId: companyEmploymentStatusId.val(),
                    RetirementAge: companyRetirementAge.val(),
                    YearOfService: companyYearOfService.val(),
                    EmployerTypeOther: companyEmpoyerTypeOther.val(),
                    EmploymentStatusOther: companyEmploymentStatusOther.val(),
                    EmployerName: companyEmployerName.val()
                },
                Emergency: {
                    ContactPerson: emergencyContactPerson.val(),
                    RelationShipId: emergencyRelationShipId.val(),
                    ContactNumber: emergencyContactNumber.val(),
                    ICNumber: emergencyIC.val(),
                    Occupation: emergencyOccupation.val(),
                    Address: emergencyAddress.val(),
                },
                Bank: {
                    BankId: bankId.val(),
                    BankOther: bankOther.val(),
                    AccountNumber: bankAccountNumber.val(),
                    SalaryRangeId: salaryRange.val(),
                },
                Other: {
                    PreferredLanguage: otherLanguage.val(),
                    Hobbies: otherHobbies.val(),
                    SocialMediaHandles: otherSocialMediaHandles.val(),
                    FBName: otherFbName.val(),
                }
            }
            formData.append('Json', JSON.stringify(data))
            const res = await ApiHelper.post(window.location.origin + '/Member/SaveMemberBeforeApplicationCreate', formData)
            if (!res.data.Error) {
                const resCreate = await ApiHelper.postFormData(window.location.origin + '/application/create', {
                    MemberId: <%= csa.Member.Helpers.SessionManager.CurrentLoginMember.MemberId %>
                })
                if (!resCreate.data.Error) {
                    dialogHelper.successAutoRedirect('Application submitted', location.href)
                } else {
                    dialogHelper.error(resCreate.data.Message)
                }
            } else {
                dialogHelper.errorHTML(res.data.Message)
            }
            } catch (e) {
                dialogHelper.error(e.message)
            }

            <%--const payload = {
                Name: $('#txtFullName').val(),
                ContactNo: $('#txtContactNo').val(),
                CompanyName: $('#txtCompanyName').val(),
                CreatorMemberId: <%= CurrentLoginMember.MemberId %>,
                    PositionId: $('#ddlPosition').val(),
                    SalaryRangeId: $('#ddlSalary').val()
                }

                const res = await ApiHelper.postFormData(window.location.origin + '/application/create', payload)
                if (!res.data.Error) {
                    dialogHelper.successAutoRedirect('Application created', "<%= Page.ResolveUrl("~/ApplicationStatus")%>")
                } else {
                    dialogHelper.error(res.data.Message)
                }--%>

                $(this).prop("disabled", false)
        })

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

        $('#cbCopyFatherAddress').on('change', function () {
            if ($(this).is(':checked')) {
                $('[name="familyMotherAddress"]').val($('[name="familyFatherAddress"]').val())
            }
            else {
                $('[name="familyMotherAddress"]').val('')
            }
        })

        $.validator.addMethod("notZero", function (value, element) {
            if ($(element).data('select2') !== undefined) {
                return value.length > 0;
            }
            return this.optional(element) || value !== "0";
        }, "Please select a valid option.");

        function prepare() {
            vm.Banks.forEach(function (item) {
                bankId.append(`<option value='${item.Key}'>${item.Text}</option>`)
            })

            vm.Sectors.splice(0, 0, { Key: 0, Text: 'Select an option'})
            vm.Sectors.forEach(function (item) {
                $(companySectorId).append(`<option value='${item.Key}'>${item.Text}</option>`)
            })
            $(companySectorId).append(`<option value='<%= csa.Library.Constant.OTHER_NUMBER %>'>Lain-lain</option>`)
        }

        $(companySectorId).on('change', function () {
            $(companyDepartmentId).empty()
            $(companyDepartmentId).append(`<option value='0'>Select an option</option>`)
            vm.JobPositions.filter(x=>x.RefValue == $(this).val()).forEach(function (item) {
                $(companyDepartmentId).append(`<option value='${item.Value}'>${item.Text}</option>`)
            })
            if ($(this).val() != 0) {
                $(companyDepartmentId).append(`<option value='<%= csa.Library.Constant.OTHER_NUMBER %>'>Lain-lain</option>`)
            }
        })

        function loadDetails() {
            programEvent.val(vm.ProgramEventId).trigger('change')
            fullName.val(vm.FullName)
            if (vm.DateOfBirth != null) {
                dateOfBirth.data('datepicker').setDate(new Date(vm.DateOfBirth))
            }
            gender.val(vm.Gender).trigger('change')
            icNumber.val(vm.ICNumber)
            address.val(vm.Address)
            phoneNumber.val(vm.PhoneNumber)
            email.val(vm.Email)
            taxNumber.val(vm.TaxNumber)
            race.val(vm.RaceId).trigger('change')
            religion.val(vm.ReligionId).trigger('change')
            education.val(vm.HighestLevelOfEducationId).trigger('change')
            maritalStatus.val(vm.MaritalStatusId).trigger('change')
            referralName.val(vm.ReferralName)

            spouseName.val(vm.Spouse.FullName)
            spouseIC.val(vm.Spouse.ICNumber)
            spouseContactInformation.val(vm.Spouse.ContactInformation)
            spouseOccupation.val(vm.Spouse.Occupation)
            spouseCompanyAddress.val(vm.Spouse.CompanyAddress)
            spouseSalary.val(vm.Spouse.Salary)

            familyNumberOfDependent.val(vm.Family.NumberOfDependent)
            familyHasOKU.val(vm.Family.IsHasOKU).trigger('change')
            familyFatherName.val(vm.Family.FatherName)
            familyFatherContactNumber.val(vm.Family.FatherContactNumber)
            familyFatherAddress.val(vm.Family.FatherAddress)
            familyMotherName.val(vm.Family.MotherName)
            familyMotherContactNumber.val(vm.Family.MotherContactNumber)
            familyMotherAddress.val(vm.Family.MotherAddress)
            companyEmpoyerTypeId.val(vm.Company.EmployerTypeId).trigger('change')
            companyName.val(vm.Company.CompanyName)
            companyJobTitle.val(vm.Company.JobTitle)
            let sector = vm.Company.SectorId
            if (sector == null) {
                sector = "null"
            }
            companySectorId.val(sector).trigger('change')
            companySectorOther.val(vm.Company.SectorOther)
            let departement = vm.Company.DepartmentId
            if (departement == null) {
                departement = "null"
            }
            companyDepartmentId.val(departement).trigger('change')
            companyDepartmentOther.val(vm.Company.DepartmentOther)
            companyAddress.val(vm.Company.CompanyAddress)
            companyOfficeContactNumber.val(vm.Company.OfficeContactNumber)
            companyEmploymentStatusId.val(vm.Company.EmploymentStatusId).trigger('change')
            companyRetirementAge.val(vm.Company.RetirementAge)
            companyYearOfService.val(vm.Company.YearOfService).trigger('change');

            emergencyContactPerson.val(vm.Emergency.ContactPerson)
            emergencyRelationShipId.val(vm.Emergency.RelationShipId).trigger('change')
            emergencyContactNumber.val(vm.Emergency.ContactNumber)
            emergencyIC.val(vm.Emergency.ICNumber)
            emergencyOccupation.val(vm.Emergency.Occupation)
            emergencyAddress.val(vm.Emergency.Address)

            bankId.val(vm.Bank.BankId)
            bankAccountNumber.val(vm.Bank.AccountNumber)
            salaryRange.val(vm.Bank.SalaryRangeId)
            bankOther.val(vm.Bank.BankOther)

            updateSelect(bankId)
            updateSelect(salaryRange)

            otherLanguage.val(vm.Other.PreferredLanguage)
            otherHobbies.val(vm.Other.Hobbies)
            otherSocialMediaHandles.val(vm.Other.SocialMediaHandles)
            otherFbName.val(vm.Other.FBName)

            companyEmploymentStatusOther.val(vm.Company.EmploymentStatusOther)
            companyEmpoyerTypeOther.val(vm.Company.EmployerTypeOther)
            companyEmployerName.val(vm.Company.EmployerName)

            //if (vm.ICFile != null) {
            //    $('#anchorIc').removeClass('d-none').attr('href', vm.ICFile.FilePath).text(vm.ICFile.FileName)
            //    upldIc.hide()
            //}

            //if (vm.PayslipFile != null) {
            //    $('#anchorPayslip').removeClass('d-none').attr('href', vm.PayslipFile.FilePath).text(vm.PayslipFile.FileName)
            //    upldPayslip.hide()
            //}

            //if (vm.OfferLetterFile != null) {
            //    $('#anchorOfferLetter').removeClass('d-none').attr('href', vm.OfferLetterFile.FilePath).text(vm.OfferLetterFile.FileName)
            //    upldOfferLetter.hide()
            //}

            if (vm.ICFile) {
                showFile(vm.ICFile, "ic", "icFile")
            }

            if (vm.PayslipFile) {
                showFile(vm.PayslipFile, "payslip", "member")
            }

            if (vm.OfferLetterFile) {
                showFile(vm.OfferLetterFile, "offerletter", "member")
            }

            //check element if null value
            $('#card-content select').each(function () {
                var selectElement = $(this);
                if (selectElement.val() == null) {
                    selectElement.prop('selectedIndex', 0)
                }
            });
        }

        function updateSelect(el) {
            if (el.val() == null) {
                el.prop('selectedIndex', 0)
            }

            el.trigger('change')
        }

        function showFile(objFile, actionName, type) {
            let action = $('#' + actionName + "Action")
            action.removeClass("d-none")
            $('#txt' + actionName).val(objFile.Value.Text)
            $(action.children().eq(0)).data('download', `${window.location.origin}/DownloadFile.aspx?f=${objFile.Value.Value}&n=${objFile.Value.Text}&t=${type}`)
            $(action.children().eq(1)).attr('href', `${window.location.origin}/ViewFile.aspx?f=${objFile.Value.Value}&n=${objFile.Value.Text}&t=${type}`)
        }

        function openInputFile(inputId) {
            $('#' + inputId).trigger('click')
        }

        function onChangeUpload(e, inputId) {
            $('#' + inputId).val(event.target.files[0].name)
        }

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
            vm = JSON.parse($('#<%= hfModelView.ClientID %>').val())
            prepare()
            loadDetails()
        });
    </script>
</asp:Content>
