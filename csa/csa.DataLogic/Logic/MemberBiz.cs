using csa.Library;
using csa.Model;
using csa.Model.DataObject;
using CsaModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace csa.DataLogic
{
    public static class MemberBiz
    {
        public static Member Get(long memberId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Members.FirstOrDefault(x => x.MemberId == memberId);
            }
        }

        public static List<Member> GetAllActiveWithSearch(string search)
        {
            using (CsaEntities db = new CsaEntities())
            {
                int statusId = (int)GlobalStatus.ACTIVE;
                var members = from member in db.Members
                          where member.StatusId == statusId &&
                          (member.FirstName.Contains(search) ||
                          member.LastName.Contains(search) ||
                          member.PhoneNumber.Contains(search))
                          select member;
                return members.ToList();
            }
        }

        public static List<Member> GetAllActiveWithSearch(string search,int? exceptMemberId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                int statusId = (int)GlobalStatus.ACTIVE;
                var members = from member in db.Members
                              where member.MemberId != exceptMemberId &&
                              member.StatusId == statusId
                              && (member.FirstName.Contains(search) ||
                              member.LastName.Contains(search) ||
                              member.PhoneNumber.Contains(search))
                              select member;
                return members.ToList();
            }
        }

        public static Member Get(string phoneNumber)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Members.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
            }
        }

        public static bool ExistMember(int memberId)
        {
            return Get(memberId) != null;
        }        

        public static RespArgs<LoginMember> Login(RequestLoginMember req)
        {
            var member = Get(req.PhoneNumber);
            if (member == null) throw new ArgumentException("Invalid phone number or password");
            if (member.StatusId == (int)MemberStatus.INACTIVE) throw new ArgumentException("Invalid phone number or password");

            string shaPassword = SecurityLibrary.SHA512Hash(req.Password + member.Salt);
            if (shaPassword != member.PasswordSalted) throw new ArgumentException("Invalid phone number or password");

            return RespArgs<LoginMember>.CreateSuccess(member.Convert());
        }

        public static async Task<RespArgs<string>> RegisterFromMember(RequestRegisterMember req)
        {
            using(var tscope = new TransactionScope())
            {
                using (CsaEntities db = new CsaEntities())
                {
                    //if (req.Password != req.ConfirmPassword) throw new ArgumentException("password_not_same");

                    if (ExistPhone(req.PhoneNumber,null)) throw new ArgumentException("Phone already exist");

                    var salt = SecurityLibrary.GenerateSalt();
                    var passwordSalted = SecurityLibrary.SHA512Hash(req.Password + salt);

                    var newMember = new Member
                    {
                        PhoneNumber = req.PhoneNumber,
                        PasswordSalted = passwordSalted,
                        Salt = salt,
                        MemberTypeId = (int)MemberType.AGENT,
                        CreateDate = DateTime.Now,
                        StatusId = (int)MemberStatus.ACTIVE
                    };
                    db.Members.AddObject(newMember);
                    await db.SaveChangesAsync();

                    var resCalculate = await RecalculateFileNumber(db, newMember.MemberId);

                    EmailBiz.NewClientRegistration(db, new EmailNewClientRegistrationData(newMember.FullName, newMember.CreateDate, newMember.PhoneNumber, resCalculate.ObjVal));

                    tscope.Complete();
                    return RespArgs<string>.CreateSuccess(newMember.PhoneNumber);
                }
            }
        }

        public static async Task<RespArgs<bool>> CreateByAdmin(RequestNewMemberByAdmin req,File icFile,File profileFile, File payslipFile)
        {
            using(var tscope = new TransactionScope())
            {
                using (CsaEntities db = new CsaEntities())
                {
                    if (ExistPhone(req.PhoneNumber,null)) throw new ArgumentException("Phone already exist");

                    if(req.ICNumber.IsNotEmpty())
                    {
                        if (ExistICNumber(req.ICNumber, null)) throw new ArgumentException("ICNumber already exist");
                    }

                    if (req.Email.IsNotEmpty())
                    {
                        if (ExistEmailAddress(req.Email, null)) throw new ArgumentException("Email already exist");
                    }

                    var salt = SecurityLibrary.GenerateSalt();
                    var passwordSalted = SecurityLibrary.SHA512Hash(req.Password + salt);

                    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

                    var newMember = new Member
                    {
                        MemberCode = req.MemberCode,
                        FirstName = textInfo.ToTitleCase(req.FirstName ?? ""),
                        LastName = textInfo.ToTitleCase(req.LastName ?? ""),
                        ReferrerMemberId = req.ReferrerMemberId.ToInt().IfZeroToNull(),
                        Email = req.Email.IsEmpty() ? null : req.Email,
                        PhoneNumber = req.PhoneNumber,
                        PasswordSalted = passwordSalted,
                        Salt = salt,
                        CreateDate = DateTime.Now,
                        StatusId = req.StatusId,
                        Salary = req.Salary,
                        GenderId = req.GenderId,
                        StreetAddress1 = req.Address,
                        StateId = req.StateId.ToInt().IfZeroToNull(),
                        CountryId = req.CountryId,
                        CompanyName = req.CompanyName,
                        Occupation = req.Occupation,
                        BankId = req.Bank.BankId.ToInt().IfZeroToNull(),
                        BankAccountName = req.Bank.BankAccountName,
                        BankAccountNumber = req.Bank.BankAccountNumber,
                        ICNumber = req.ICNumber,
                        CompanySectorId = req.CompanySectorId.ToInt().IfZeroToNull(),
                        CompanyEmploymentStatusId = req.CompanyEmploymentStatusId.ToInt().IfZeroToNull(),
                        RetirementAge = req.RetirementAge,
                        CompanyYearOfService = req.YearOfService,
                        CompanyTypeId = req.CompanyTypeId.ToInt(),
                        SalaryRangeId = req.SalaryRangeId.ToInt().IfZeroToNull()
                    };
                    if (icFile != null)
                    {
                        db.Files.AddObject(icFile);
                        await db.SaveChangesAsync();

                        newMember.ICFileId = icFile.FileId;
                    }
                    if (profileFile != null)
                    {
                        db.Files.AddObject(profileFile);
                        await db.SaveChangesAsync();

                        newMember.ProfileFileId = profileFile.FileId;
                    }
                    if (payslipFile != null)
                    {
                        db.Files.AddObject(payslipFile);
                        await db.SaveChangesAsync();

                        newMember.PayslipFileId = payslipFile.FileId;
                    }
                    db.Members.AddObject(newMember);
                    await db.SaveChangesAsync();

                    var resCalculate = await RecalculateFileNumber(db, newMember.MemberId);

                    EmailBiz.NewClientRegistration(db, new EmailNewClientRegistrationData(newMember.FullName, newMember.CreateDate, newMember.PhoneNumber, resCalculate.ObjVal));

                    tscope.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }

        public static async Task<RespArgs<bool>> UpdateByAdmin(RequestUpdateMemberByAdminData req, File icFile,File payslipFile,File offerLetterFile)
        {
            using(var tscope = new TransactionScope())
            {
                using (CsaEntities db = new CsaEntities())
                {
                    if (req.Email.IsNotEmpty())
                    {
                        if (ExistEmailAddress(req.Email, req.MemberId)) throw new ArgumentException("Email already exist");
                    }

                    if (req.ICNumber.IsNotEmpty())
                    {
                        if (ExistICNumber(req.ICNumber, req.MemberId)) throw new ArgumentException("ICNumber already exist");
                    }

                    var member = db.Members.FirstOrDefault(x => x.MemberId == req.MemberId);
                    if (member == null) throw new ArgumentException("Member not found");

                    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

                    member.StateId = req.StateId.ToInt().IfZeroToNull();
                    member.CountryId = req.CountryId;
                    member.ReferrerMemberId = req.ReferrerMemberId.ToInt().IfZeroToNull();
                    member.StatusId = req.StatusId;
                    member.FirstName = textInfo.ToTitleCase(req.FirstName ?? "");
                    member.LastName = textInfo.ToTitleCase(req.LastName ?? "");
                    member.ICNumber = req.ICNumber.IsEmpty() ? null : req.ICNumber;
                    member.Birthdate = req.DateOfBirth;
                    member.GenderId = req.Gender;
                    member.Email = req.Email.IsEmpty() ? null : req.Email;

                    member.ProgramEventId = req.ProgramEventId.ToInt().IfZeroToNull();
                    member.StreetAddress1 = req.Address;
                    member.TaxNumber = req.TaxNumber;
                    member.RaceId = req.RaceId.ToInt().IfZeroToNull();
                    member.ReligionId = req.ReligionId.ToInt().IfZeroToNull();
                    member.HighestLevelOfEducationId = req.HighestLevelOfEducationId.ToInt().IfZeroToNull();
                    member.MaritalStatusId = req.MaritalStatusId.ToInt().IfZeroToNull();

                    member.SpouseFullName = req.Spouse.FullName;
                    member.SpouseIdentificationNumber = req.Spouse.ICNumber;
                    member.SpouseContactInformation = req.Spouse.ContactInformation;
                    member.SpouseOccupation = req.Spouse.Occupation;
                    member.SpouseCompanyAddress = req.Spouse.CompanyAddress;
                    member.SpouseSalary = req.Spouse.Salary;

                    member.NumberOfDependent = req.Family.NumberOfDependent;
                    member.IsHaveOKU = req.Family.IsHasOKU.HasValue ? (sbyte?)req.Family.IsHasOKU : null;
                    member.FatherName = req.Family.FatherName;
                    member.FatherContactNumber = req.Family.FatherContactNumber;
                    member.FatherAddress = req.Family.FatherAddress;
                    member.MotherName = req.Family.MotherName;
                    member.MotherConcatNumber = req.Family.MotherContactNumber;
                    member.MotherAddress = req.Family.MotherAddress;

                    member.CompanyEmployerTypeId = req.Company.EmployerTypeId.ToInt().IfZeroToNull();
                    member.CompanyName = req.Company.CompanyName;
                    member.CompanyJobTitle = req.Company.JobTitle;
                    member.CompanySectorId = req.Company.SectorId.ToInt().IfZeroToNull();
                    member.CompanySectorOther = req.Company.SectorOther;
                    member.CompanyDepartmentId = req.Company.DepartmentId.ToInt().IfZeroToNull();
                    member.CompanyDepartementOther = req.Company.DepartmentOther;
                    member.CompanyAddress = req.Company.CompanyAddress;
                    member.CompanyOfficeContactNumber = req.Company.OfficeContactNumber;
                    member.CompanyEmploymentStatusId = req.Company.EmploymentStatusId.ToInt().IfZeroToNull();
                    member.RetirementAge = req.Company.RetirementAge;
                    member.CompanyYearOfService = req.Company.YearOfService;
                    member.CompanyEmployerTypeOther = req.Company.EmployerTypeOther;
                    member.CompanyEmploymentStatusOther = req.Company.EmploymentStatusOther;

                    member.EmergencyConcatPerson = req.Emergency.ContactPerson;
                    member.EmergencyRelationId = req.Emergency.RelationShipId.ToInt().IfZeroToNull();
                    member.EmergencyConcatNumber = req.Emergency.ContactNumber;
                    member.EmergencyPersonICNumber = req.Emergency.ICNumber;
                    member.EmergencyPersonOccupation = req.Emergency.Occupation;
                    member.EmergencyPersonAddress = req.Emergency.Address;

                    member.OtherPreferredLanguage = req.Other.PreferredLanguage;
                    member.OtherHobbies = req.Other.Hobbies;
                    member.OtherSocialMediaHandles = req.Other.SocialMediaHandles;
                    member.OtherFBName = req.Other.FBName;
                    member.SalaryRangeId = req.Bank.SalaryRangeId.ToInt().IfZeroToNull();

                    if (icFile != null)
                    {
                        db.Files.AddObject(icFile);
                        await db.SaveChangesAsync();

                        member.ICFileId = icFile.FileId;
                    }
                    if (payslipFile != null)
                    {
                        db.Files.AddObject(payslipFile);
                        await db.SaveChangesAsync();

                        member.PayslipFileId = payslipFile.FileId;
                    }
                    if (offerLetterFile != null)
                    {
                        db.Files.AddObject(offerLetterFile);
                        await db.SaveChangesAsync();

                        member.OfferLetterFileId = offerLetterFile.FileId;
                    }
                    await db.SaveChangesAsync();
                    await RecalculateFileNumber(db, member.MemberId);
                    tscope.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }

        public static RespArgs<bool> ForgotPassword(RequestForgotPasswordMember req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var member = db.Members.FirstOrDefault(x => x.PhoneNumber == req.PhoneNumber);
                if (member == null) throw new ArgumentException("Phone number not found");

                member.ForgotPasswordOTP = "GenerateOTP";
                //to do
                return RespArgs<bool>.CreateSuccess(true);
            }
        }

        public static async Task<RespArgs<BankDetails>> ChangeBankDetails(RequestChangeBankDetails req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var member = db.Members.FirstOrDefault(x => x.MemberId == req.MemberId);
                if (member == null) throw new ArgumentException("Member not found");

                member.BankId = req.BankId;
                member.BankOther = member.BankId == Constant.OTHER_NUMBER ? req.BankOther : "" ;
                member.BankAccountName = req.BankAccountName;
                member.BankAccountNumber = req.BankAccountNumber;
                await db.SaveChangesAsync();
                return RespArgs<BankDetails>.CreateSuccess(new BankDetails(member.BankId,member.BankAccountName,member.BankAccountNumber,member.BankOther));
            }
        }

        public static RespArgs<MemberPersonalDetails> ChangePersonalDetails(RequestChangePersonalDetails req,CsaModel.File file, CsaModel.File payslipFile)
        {
            using (CsaEntities db = new CsaEntities())
            {
                if(req.Email.IsNotEmpty())
                {
                    if(ExistEmailAddress(req.Email,req.MemberId)) throw new ArgumentException("Email already exist");
                }

                var member = db.Members.FirstOrDefault(x => x.MemberId == req.MemberId);
                if (member == null) throw new ArgumentException("Member not found");

                if(member.StatusId == (int)MemberStatus.REMOVED) throw new ArgumentException("Member not found");

                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

                member.FirstName = textInfo.ToTitleCase(req.FirstName ?? "");
                member.LastName = textInfo.ToTitleCase(req.LastName ?? "");
                member.Email = req.Email.IsEmpty() ? null : req.Email;

                if(file != null)
                {
                    db.Files.AddObject(file);
                    db.SaveChangesAsync();

                    member.ICFileId = file.FileId;
                }
                if(payslipFile != null)
                {
                    db.Files.AddObject(payslipFile);
                    db.SaveChangesAsync();

                    member.PayslipFileId = payslipFile.FileId;
                }
                var fileProfile = FileBiz.Get(member.ProfileFileId);
                var filePayslip = FileBiz.Get(member.PayslipFileId);
                var fileIc = FileBiz.Get(member.ICFileId);
                db.SaveChangesAsync();
                return RespArgs<MemberPersonalDetails>.CreateSuccess(new MemberPersonalDetails(member.MemberId,member.FirstName,member.LastName,member.Email,member.PhoneNumber,BuildFile(fileIc), fileProfile?.ToDisplay(), BuildFile(filePayslip)));
            }
        }

        private static FileUploadedBy<ValueText<string>> BuildFile(File file)
        {
            if (file == null) return null;
            return new FileUploadedBy<ValueText<string>>(new ValueText<string>(file?.FileId + file?.Extension, file?.Filename), "", null);
        }

        public static RespArgs<bool> ChangePassword(RequestChangePassword req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var member = db.Members.FirstOrDefault(x => x.MemberId == req.MemberId);
                if (member == null) throw new ArgumentException("Member not found");

                string shaPassword = SecurityLibrary.SHA512Hash(req.CurrentPassword + member.Salt);
                if (shaPassword != member.PasswordSalted) throw new ArgumentException("Invalid current password");

                var salt = SecurityLibrary.GenerateSalt();
                var passwordSalted = SecurityLibrary.SHA512Hash(req.NewPassword + salt);

                member.Salt = salt;
                member.PasswordSalted = passwordSalted;
                db.SaveChangesAsync();
                return RespArgs<bool>.CreateSuccess(true);
            }
        }

        public static RespArgs<bool> ChangePasswordByAdmin(RequestChangePasswordByAdmin req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var member = db.Members.FirstOrDefault(x => x.MemberId == req.MemberId);
                if (member == null) throw new ArgumentException("Member not found");

                var admin = db.Admins.FirstOrDefault(x => x.AdminId == req.AdminId);
                if (admin == null) throw new ArgumentException("Cant access");

                string shaPassword = SecurityLibrary.SHA512Hash(req.CurrentPassword + admin.Salt);
                if (shaPassword != admin.PasswordSalted) throw new ArgumentException("Invalid current admin password");

                var salt = SecurityLibrary.GenerateSalt();
                var passwordSalted = SecurityLibrary.SHA512Hash(req.NewPassword + salt);

                member.Salt = salt;
                member.PasswordSalted = passwordSalted;
                db.SaveChangesAsync();
                return RespArgs<bool>.CreateSuccess(true);
            }
        }

        public static RespArgs<FileDisplay> ChangeProfilePicture(RequestChangeProfilePicture req, CsaModel.File file)
        {
            using (CsaEntities db = new CsaEntities())
            {
                FileDisplay fdProfile = null;
                var member = db.Members.FirstOrDefault(x => x.MemberId == req.MemberId);
                if (member == null) throw new ArgumentException("Member not found");

                if (file != null)
                {
                    db.Files.AddObject(file);
                    db.SaveChangesAsync();

                    member.ProfileFileId = file.FileId;

                    fdProfile = file.ToDisplay();
                }
                db.SaveChangesAsync();
                return RespArgs<FileDisplay>.CreateSuccess(fdProfile);
            }
        }

        public static int CountReferral(long memberId)
        {
            using(CsaEntities db = new CsaEntities())
            {
                return db.Members.Count(x => x.ReferrerMemberId == memberId);
            }
        }

        public static int CountReferralYesterday(long memberId,DateTime now)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Members.Count(x => x.ReferrerMemberId == memberId && x.CreateDate < now);
            }
        }

        public static RespArgs<GridViewModel<ReferralGVByMember>> GetReferralGVByMember(long memberId,string search,int pageIndex, int pageSize,string sortOrder,SQLSelect.OrderByEnum sortDirection)
        {
            using (CsaEntities db = new CsaEntities())
            {
                search = search.ToLiteral();
                var sqlSelect = new SQLSelect("member m");
                sqlSelect.AddSelect("m.MemberId,m.MemberCode,m.FirstName,m.LastName,m.ReferralTypeId,m.ReferralAmount,m.CreateDate,m.StatusId");
                sqlSelect.AddWhere($"m.ReferrerMemberId = {memberId}");
                sqlSelect.SetOrderBY(sortOrder, sortDirection);
                sqlSelect.SetLimit((pageIndex * pageSize), pageSize);

                if (!search.IsEmpty()) sqlSelect.AddWhere($"CONCAT(IFNULL(m.FirstName,''),IFNULL(m.LastName,'')) LIKE '%{search}%'");

                var list = db.ExecuteStoreQuery<ReferralGVByMember>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                //add sequence
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].SequenceId = (pageIndex * pageSize) + i;
                }

                return RespArgs<GridViewModel<ReferralGVByMember>>.CreateSuccess(new GridViewModel<ReferralGVByMember>(list, count, count, pageIndex + 1, pageSize, null, 1));
            }
        }

        public static RespArgs<bool> CreateAgentByMember(RequestAgentByMember req, CsaModel.File file)
        {
            using (CsaEntities db = new CsaEntities())
            {
                if (ExistPhone(req.PhoneNumber,null)) throw new ArgumentException("Phone number already exist");

                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

                var member = new Member();
                member.FirstName = textInfo.ToTitleCase(req.FirstName ?? "");
                member.LastName = textInfo.ToTitleCase(req.LastName ?? "");
                member.Email = req.Email.IsEmpty() ? null : req.Email;
                member.PhoneNumber = req.PhoneNumber;
                member.ICNumber = req.ICNumber;
                string randomPassword = SecurityLibrary.GenRandomPwd();
                member.Salt = SecurityLibrary.GenerateSalt();
                member.PasswordSalted = SecurityLibrary.SHA512Hash(randomPassword + member.Salt);
                member.ReferralTypeId = (int)AgentType.TYPE_A;
                member.ReferrerMemberId = req.CreatorMemberId;
                member.CreateDate = DateTime.Now;
                member.StatusId = (int)MemberStatus.ACTIVE;

                if (file != null)
                {
                    db.Files.AddObject(file);
                    db.SaveChangesAsync();

                    member.ICFileId = file.FileId;
                }
                db.Members.AddObject(member);
                db.SaveChangesAsync();
                return RespArgs<bool>.CreateSuccess(true);
            }
        }

        public static RespArgs<GridViewModel<DashboardReferralGVByMember>> GetDashboardReferralGVByMember(long memberId, int pageSize)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var sqlSelect = new SQLSelect("member m");
                sqlSelect.AddSelect("m.MemberId,m.MemberCode,m.FirstName,m.LastName,m.PhoneNumber,m.StatusId");
                sqlSelect.AddWhere($"m.ReferrerMemberId = {memberId}");
                sqlSelect.SetOrderBY("m.CreateDate", SQLSelect.OrderByEnum.DESC);
                sqlSelect.SetLimit(0, pageSize);

                var list = db.ExecuteStoreQuery<DashboardReferralGVByMember>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                //add sequence
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].SequenceId = 1 + i;
                }

                return RespArgs<GridViewModel<DashboardReferralGVByMember>>.CreateSuccess(new GridViewModel<DashboardReferralGVByMember>(list, count, count, 1, pageSize, null, 1));
            }
        }

        public static RespArgs<GridViewModel<MemberGVByAdmin>> GetMemberGVByAdmin(string search, int pageIndex, int pageSize, string sortOrder, SQLSelect.OrderByEnum sortDirection)
        {
            using (CsaEntities db = new CsaEntities())
            {
                search = search.ToLiteral();
                var sqlSelect = new SQLSelect("member m");
                sqlSelect.AddSelect("m.CreateDate,m.MemberId,m.FirstName,m.LastName,m.ICNumber,m.GenderId,m.PhoneNumber,m.CompanyName,m.Occupation,CONCAT(IFNULL(refm.FirstName,''),' ',IFNULL(refm.LastName,'')) as ReferrerName,m.MemberTypeId,m.CompanyTypeId,m.FileNumber");
                sqlSelect.AddSelect("app.ApplicationId,app.ApplicationStatusId,app.RejectedDate,app.CustomerStatusId");
                sqlSelect.AddLeftJoin("member refm", "m.ReferrerMemberId", "refm.MemberId");
                sqlSelect.AddLeftJoin("(SELECT ApplicationId,ApplicationStatusId,MemberId,RejectedDate,CustomerStatusId,ROW_NUMBER() OVER (PARTITION BY MemberId ORDER BY CreateDate DESC) AS rn from application) app", "m.MemberId", "app.MemberId AND app.rn = 1");
                sqlSelect.AddWhere($"m.StatusId IN ({string.Join(",", new int[] { (int)MemberStatus.ACTIVE, (int)MemberStatus.INACTIVE })})");
                sqlSelect.SetOrderBY(sortOrder, sortDirection);
                sqlSelect.SetLimit((pageIndex * pageSize), pageSize);

                if (!search.IsEmpty()) sqlSelect.AddWhere($"(CONCAT(IFNULL(m.FirstName,''),IFNULL(m.LastName,'')) LIKE '%{search}%' OR m.PhoneNumber LIKE '%{search}%' OR m.CompanyName LIKE '%{search}%' OR m.Occupation LIKE '%{search}%' OR m.ICNumber LIKE '%{search}%')");

                var list = db.ExecuteStoreQuery<MemberGVByAdmin>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                return RespArgs<GridViewModel<MemberGVByAdmin>>.CreateSuccess(new GridViewModel<MemberGVByAdmin>(list, count, count, pageIndex + 1, pageSize, null, 1));
            }
        }

        public static async Task<RespArgs<string>> RecalculateFileNumber(CsaEntities db,long memberId)
        {
            var member = db.Members.FirstOrDefault(x => x.MemberId == memberId);
            if (member == null) return RespArgs<string>.CreateError("Member not found");

            var lastApplication = db.Applications.Where(x => x.MemberId == memberId).OrderByDescending(x => x.CreateDate).FirstOrDefault();
            int caseCount = 0;
            if (lastApplication != null)
            {
                caseCount = db.Caseupdates.Count(x => x.ApplicationId == lastApplication.ApplicationId);
            }

            member.FileNumber = BuildFileNumber(member.MemberId, member.CompanyTypeId, lastApplication?.ApplicationId, lastApplication?.RejectedDate, lastApplication?.CustomerStatusId, caseCount, lastApplication?.ApplicationStatusId, member.MemberTypeId,member.NotEntitled);
            await db.SaveChangesAsync();

            return RespArgs<string>.CreateSuccess(member.FileNumber);
        }

        public static RespArgs<GridViewModel<MemberGVByAdmin>> GetMemberNeedApprovalGVByAdmin(string search, int pageIndex, int pageSize, string sortOrder, SQLSelect.OrderByEnum sortDirection)
        {
            using (CsaEntities db = new CsaEntities())
            {
                search = search.ToLiteral();
                var sqlSelect = new SQLSelect("member m");
                sqlSelect.AddSelect("m.CreateDate,m.MemberId,m.FirstName,m.LastName,m.ICNumber,m.GenderId,m.PhoneNumber,m.CompanyName,m.Occupation,CONCAT(IFNULL(refm.FirstName,''),' ',IFNULL(refm.LastName,'')) as ReferrerName");
                sqlSelect.AddLeftJoin("member refm", "m.ReferrerMemberId", "refm.MemberId");
                sqlSelect.AddWhere($"m.StatusId IN ({string.Join(",", new int[] { (int)MemberStatus.UNVERIFIED })})");
                sqlSelect.SetOrderBY(sortOrder, sortDirection);
                sqlSelect.SetLimit((pageIndex * pageSize), pageSize);

                if (!search.IsEmpty()) sqlSelect.AddWhere($"(CONCAT(IFNULL(m.FirstName,''),IFNULL(m.LastName,'')) LIKE '%{search}%' OR m.PhoneNumber LIKE '%{search}%' OR m.CompanyName LIKE '%{search}%' OR m.Occupation LIKE '%{search}%' OR m.ICNumber LIKE '%{search}%')");

                var list = db.ExecuteStoreQuery<MemberGVByAdmin>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                return RespArgs<GridViewModel<MemberGVByAdmin>>.CreateSuccess(new GridViewModel<MemberGVByAdmin>(list, count, count, pageIndex + 1, pageSize, null, 1));
            }
        }

        public static async Task<RespArgs<bool>> ApproveMember(RequestApproveMember req)
        {
            using(var tscope = new TransactionScope())
            {
                using (CsaEntities db = new CsaEntities())
                {
                    var member = db.Members.FirstOrDefault(x => x.MemberId == req.MemberId);
                    if (member == null) throw new ArgumentException("Member not found");

                    //decimal initAmount = member.WalletCash ?? 0;
                    member.StatusId = (int)MemberStatus.ACTIVE; 
                    //member.WalletCash = initAmount + CommissionBiz.CommissionRegisterNewMember;

                    //await WithdrawalBiz.ProcessingWithdrawalByMemberIdWhenAdminApprove(db, new RequestProcessingWithdrawal(member.MemberId));

                    //db.Histories.AddObject(HistoryBiz.Factory(new HistoryCommissionRegisterNewMember(CommissionBiz.CommissionRegisterNewMember, initAmount, member.MemberId, req.AdminId)));

                    //if (member.ReferrerMemberId.HasValue)
                    //{
                    //    var refMember = db.Members.FirstOrDefault(x => x.MemberId == member.ReferrerMemberId.Value);
                    //    initAmount = refMember.WalletCash ?? 0;
                    //    refMember.WalletCash = (refMember.WalletCash ?? 0) + CommissionBiz.CommissionRegisterUpline;

                    //    db.Histories.AddObject(HistoryBiz.Factory(new HistoryCommissionRegisterReferrer(CommissionBiz.CommissionRegisterUpline, initAmount, refMember.MemberId, req.AdminId)));
                    //}

                    db.SaveChanges();
                    tscope.Complete();
                    return await Task.FromResult(RespArgs<bool>.CreateSuccess(true));
                }
            }
        }

        public static async Task<RespArgs<bool>> RejectMember(RequestRejectMember req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                using (var trans = new TransactionScope())
                {
                    var member = db.Members.FirstOrDefault(x => x.MemberId == req.MemberId);
                    if (member == null) throw new ArgumentException("Member not found");

                    //long uniqueTime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
                    //member.StatusId = (int)MemberStatus.REMOVED;
                    //member.Email = member.Email + "_REMOVED_" + uniqueTime;
                    //member.PhoneNumber = member.PhoneNumber + "_REMOVED_" + uniqueTime;
                    //member.ICNumber = member.ICNumber + "_REMOVED_" + uniqueTime;

                    member.NotEntitled = 1;
                    member.StatusId = (int)MemberStatus.ACTIVE;
                    await db.SaveChangesAsync();

                    await RecalculateFileNumber(db, member.MemberId);

                    //write another code
                    trans.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }

        public static async Task<RespArgs<bool>> ApproveMemberHero(RequestApproveMember req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                using (var trans = new TransactionScope())
                {
                    var member = db.Members.FirstOrDefault(x => x.MemberId == req.MemberId);
                    if (member == null) throw new ArgumentException("Member not found");

                    member.StatusId = (int)MemberStatus.ACTIVE;
                    member.MemberTypeId = (int)MemberType.HERO;
                    await db.SaveChangesAsync();

                    await RecalculateFileNumber(db, member.MemberId);

                    //write another code
                    trans.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }

        public static async Task<RespArgs<bool>> WalletChangesByAdmin(RequestWalletChangesByAdmin req)
        {
            using(var tscope = new TransactionScope())
            {
                using (CsaEntities db = new CsaEntities())
                {
                    var member = db.Members.FirstOrDefault(x => x.MemberId == req.MemberId);
                    if (member == null) throw new ArgumentException("Member not found");

                    decimal currentWallet = member.WalletCash.HasValue ? member.WalletCash.Value : 0;

                    decimal changesAmount = 0;
                    if (currentWallet > req.Amount)
                    {
                        changesAmount = (currentWallet - req.Amount) * -1;
                    }
                    else if (currentWallet < req.Amount)
                    {
                        changesAmount = req.Amount - currentWallet;
                    }

                    member.WalletCash = req.Amount;
                    await db.SaveChangesAsync();

                    var newhistory = HistoryBiz.Factory(new HistoryWalletMemberChangesByAdmin(changesAmount,currentWallet, member.MemberId, req.AdminId));
                    db.Histories.AddObject(newhistory);
                    await db.SaveChangesAsync();

                    //write another code
                    tscope.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }
        
        public static async Task<RespArgs<bool>> WalletSavingsChangesByAdmin(RequestWalletChangesByAdmin req)
        {
            using(var tscope = new TransactionScope())
            {
                using (CsaEntities db = new CsaEntities())
                {
                    var member = db.Members.FirstOrDefault(x => x.MemberId == req.MemberId);
                    if (member == null) throw new ArgumentException("Member not found");
                   
                    member.WalletSavings = req.Amount;
                    await db.SaveChangesAsync();
                    
                    //write another code
                    tscope.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }

        //public static string BuildFileNumberByMemberId(long memberId)
        //{
        //    var member = Get(memberId);
        //    if (member == null) return "";

        //    var lastApplication = ApplicationBiz.GetLastByMemberId(memberId);
        //    int caseCount = 0;
        //    if(lastApplication != null)
        //    {
        //        caseCount = CaseUpdateBiz.ListByApplicationId(lastApplication.ApplicationId).Count;
        //    }
        //    return BuildFileNumber(member.MemberId, member.CompanyTypeId, lastApplication?.ApplicationId, lastApplication?.RejectedDate, lastApplication?.CustomerStatusId, caseCount,lastApplication?.ApplicationStatusId, member.MemberTypeId);
        //}

        public static string BuildFileNumber(long memberId,int companyTypeId,long? applicationId,DateTime? rejectedDate,int? customerStatusId,int caseCount,int? applicationStatusId,int? memberTypeId,int? notEntitled)
        {
            string prefix = "YABAM";
            StringBuilder sb = new StringBuilder(prefix);
            sb.Append("-");
            switch (companyTypeId)
            {
                case 1:
                    sb.Append("G");
                    break;
                case 2:
                    sb.Append("P");
                    break;
                default:
                    break;
            }
            sb.Append(memberId.ToString().PadLeft(4, '0'));
            sb.Append("*");

            //when hero not changing again
            if(memberTypeId == (int)MemberType.HERO)
            {
                sb.Append("S");
            }
            else
            {
                if (applicationId.HasValue)
                {
                    if (customerStatusId == (int)CustomerStatus.Drop_Case) sb.Append("X");
                    else if (rejectedDate.HasValue || customerStatusId == (int)CustomerStatus.Burst) sb.Append("ME");
                    else if (applicationStatusId > (int)ApplicationStatus.SETTLEMENT && caseCount > 1) sb.Append("R");
                    else if (applicationStatusId > (int)ApplicationStatus.SETTLEMENT && caseCount == 1) sb.Append("S");
                    else
                    {
                        if (memberTypeId == (int)MemberType.AGENT) sb.Append("AGT");
                        else if (memberTypeId == (int)MemberType.HERO) sb.Append("S");
                        else if (notEntitled == 1) sb.Append("ME");
                        else sb.Append("C");
                    }
                }
                else
                {
                    if (memberTypeId == (int)MemberType.AGENT) sb.Append("AGT");
                    else if (memberTypeId == (int)MemberType.HERO) sb.Append("S");
                    else if (notEntitled == 1) sb.Append("ME");
                    else sb.Append("C");
                }
            }            
            
            //if (applicationId.HasValue)
            //{
            //    if (!rejectedDate.HasValue)
            //    {
            //        sb.Append("R");
            //    }
            //    else
            //    {
            //        if (customerStatusId == (int)CustomerStatus.Burst)
            //        {
            //            sb.Append("M");
            //        }
            //        else
            //        {
            //            sb.Append("X");
            //        }
            //    }
            //}

            //string prefix = "CSA";
            //StringBuilder sb = new StringBuilder(prefix);
            //sb.Append("-");
            //switch (companyTypeId)
            //{
            //    case 1:
            //        sb.Append("G");
            //        break;
            //    case 2:
            //        sb.Append("P");
            //        break;
            //    default:
            //        break;
            //}
            //sb.Append(memberId.ToString().PadLeft(4, '0'));
            //sb.Append("*");
            //if (applicationId.HasValue)
            //{
            //    if (!rejectedDate.HasValue)
            //    {
            //        sb.Append("R");
            //    }
            //    else
            //    {
            //        if (customerStatusId == (int)CustomerStatus.Burst)
            //        {
            //            sb.Append("M");
            //        }
            //        else
            //        {
            //            sb.Append("X");
            //        }
            //    }
            //}

            return sb.ToString();
        }

        public static List<TopReferrals> GetTopReferrals(int take)
        {
            using(CsaEntities db = new CsaEntities())
            {
                var query = from members in db.Members
                            where members.ReferrerMemberId.HasValue
                            group members by members.ReferrerMemberId into referGroup
                            join referrer in db.Members on referGroup.Key equals referrer.MemberId
                            select new TopReferrals
                            {
                                MemberId = referrer.MemberId,
                                Name = referrer.FirstName + " " + referrer.LastName,
                                Count = referGroup.Count()
                            };

                var topReferral = query.OrderByDescending(o => o.Count).Take(take);

                var list = topReferral.ToList();

                return list;
            }
        }

        static List<(DateTime, DateTime)> GetWeeksInCurrentMonth(int year, int month)
        {
            List<(DateTime, DateTime)> weeks = new List<(DateTime, DateTime)>();

            // Get the first and last day of the month
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            DateTime currentStart = firstDayOfMonth;

            while (currentStart <= lastDayOfMonth)
            {
                DateTime currentEnd;
                // Calculate the end of the week
                int dayOfWeek = (int)currentStart.DayOfWeek;
                if(dayOfWeek == 0)
                {
                    currentEnd = currentStart.AddDays(6);
                }
                else
                {
                    currentEnd = currentStart.AddDays(6 - dayOfWeek);
                }

                // Ensure the end date does not exceed the last day of the month
                if (currentEnd > lastDayOfMonth)
                {
                    currentEnd = lastDayOfMonth;
                }

                weeks.Add((currentStart, currentEnd));

                // Move to the next week
                currentStart = currentStart.AddDays((6 - dayOfWeek) + 1);
            }

            return weeks;
        }

        public static RespArgs<Dictionary<string,List<string>>> NewUserDashboard(string type)
        {
            var result = new Dictionary<string, List<string>>();
            using (CsaEntities db = new CsaEntities())
            {
                if(type == "today")
                {
                    var startDate = DateTime.Now;
                    var endDate = startDate.AddDays(1);

                    int total = db.ExecuteStoreQuery<int>($"SELECT COUNT(*) FROM member WHERE CreateDate >= '{startDate.ToString("yyyy-MM-dd")}' AND CreateDate < '{endDate.ToString("yyyy-MM-dd")}'").FirstOrDefault();

                    result.Add("categories", new List<string> { startDate.ToString("dd MMM yyyy") });
                    result.Add("data", new List<string> { total.ToString() });
                }
                else if (type == "last week")
                {
                    var weekNo = new List<string>();
                    var data = new List<string>();
                    var startDate = new DateTime(DateTime.Now.Year,DateTime.Now.Month, 1);
                    var endDate = startDate.AddMonths(1).AddDays(-1);

                    var weeks = GetWeeksInCurrentMonth(DateTime.Now.Year,DateTime.Now.Month);

                    for (int i = 0; i < weeks.Count; i++)
                    {
                        var week = weeks[i];
                        weekNo.Add("Week "+(i + 1));
                        int total = db.ExecuteStoreQuery<int>($"SELECT COUNT(*) FROM member WHERE CreateDate >= '{week.Item1.ToString("yyyy-MM-dd")}' AND CreateDate < '{week.Item2.ToString("yyyy-MM-dd")}'").FirstOrDefault();
                        data.Add(total.ToString());
                    }

                    result.Add("categories", weekNo);
                    result.Add("data", data);
                }
                else if (type == "last month")
                {
                    var day = new List<string>();
                    var data = new List<string>();                    
                    for (int i = 0; i < new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).Day; i++)
                    {
                        var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i + 1);
                        var endDate = startDate.AddDays(1);

                        day.Add((i + 1).ToString());
                        int total = db.ExecuteStoreQuery<int>($"SELECT COUNT(*) FROM member WHERE CreateDate >= '{startDate.ToString("yyyy-MM-dd")}' AND CreateDate < '{endDate.ToString("yyyy-MM-dd")}'").FirstOrDefault();
                        data.Add(total.ToString());
                    }
                    result.Add("categories", day);
                    result.Add("data", data);
                }
                else if (type == "current year")
                {
                    var data = new List<string>();
                    result.Add("categories", new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" });
                    for (int i = 0; i < 12; i++)
                    {
                        var startDate = new DateTime(DateTime.Now.Year, i + 1, 1);
                        var endDate = startDate.AddMonths(1);
                        int total = db.ExecuteStoreQuery<int>($"SELECT COUNT(*) FROM member WHERE CreateDate >= '{startDate.ToString("yyyy-MM-dd")}' AND CreateDate < '{endDate.ToString("yyyy-MM-dd")}'").FirstOrDefault();
                        data.Add(total.ToString());
                    }
                    result.Add("data", data);
                }

                return RespArgs<Dictionary<string, List<string>>>.CreateSuccess(result);
            }
        }

        public static int TotalCountNewMember()
        {
            using(CsaEntities db = new CsaEntities())
            {
                int statusNonActivated = (int)MemberStatus.UNVERIFIED;
                return db.Members.Count(x => x.StatusId == statusNonActivated);
            }
        }

        public static bool ExistICNumber(string icNumber,long? memberId)
        {
            using(CsaEntities db = new CsaEntities())
            {
                var find = db.Members.FirstOrDefault(x => x.ICNumber == icNumber);
                return find != null && find.MemberId != memberId;
            }
        }

        public static bool ExistEmailAddress(string email, long? memberId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var find = db.Members.FirstOrDefault(x => x.Email == email);
                return find != null && find.MemberId != memberId;
            }
        }

        public static bool ExistPhone(string phone, long? memberId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var find = db.Members.FirstOrDefault(x => x.PhoneNumber == phone);
                return find != null && find.MemberId != memberId;
            }
        }

        public static async Task<RespArgs<bool>> UpdateMemberApplicationCreatae(RequestSaveMemberBeforeApplicationCreateData req, File icFile, File payslipFile, File offerLetterFile)
        {
            using (var tscope = new TransactionScope())
            {
                using (CsaEntities db = new CsaEntities())
                {
                    var member = db.Members.FirstOrDefault(x => x.MemberId == req.MemberId);
                    if (member == null) throw new ArgumentException("Member not found");
                    
                    if(req.Email.IsNotEmpty())
                    {
                        if(ExistEmailAddress(req.Email,req.MemberId)) throw new ArgumentException("Email already exist");
                    }

                    if (ExistICNumber(req.ICNumber, req.MemberId)) throw new ArgumentException("ICNumber already exist");

                    if (member.ICFileId.IsEmpty() && icFile == null) throw new ArgumentException("IC diperlukan");
                    if (member.PayslipFileId.IsEmpty() && payslipFile == null) throw new ArgumentException("Slip gaji diperlukan");

                    var arrName = req.FullName?.Trim().Split(' ');
                    string firstName = arrName[0];
                    string lastName = "";

                    if (arrName.Count() > 1)
                    {
                        lastName = string.Join(" ", arrName.Skip(1));
                    }

                    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

                    var dt = DateTime.Now;
                    member.FirstName = textInfo.ToTitleCase(firstName ?? "");
                    member.LastName = textInfo.ToTitleCase(lastName ?? "");
                    member.ICNumber = req.ICNumber;
                    member.Birthdate = req.DateOfBirth;
                    member.GenderId = req.Gender;
                    member.Email = req.Email.IsEmpty() ? null : req.Email;

                    member.ProgramEventId = req.ProgramEventId.ToInt().IfZeroToNull();
                    member.StreetAddress1 = req.Address;
                    member.TaxNumber = req.TaxNumber;
                    member.RaceId = req.RaceId.ToInt().IfZeroToNull();
                    member.ReligionId = req.ReligionId.ToInt().IfZeroToNull();
                    member.HighestLevelOfEducationId = req.HighestLevelOfEducationId.ToInt().IfZeroToNull();
                    member.MaritalStatusId = req.MaritalStatusId.ToInt().IfZeroToNull();

                    member.SpouseFullName = req.Spouse.FullName;
                    member.SpouseIdentificationNumber = req.Spouse.ICNumber;
                    member.SpouseContactInformation = req.Spouse.ContactInformation;
                    member.SpouseOccupation = req.Spouse.Occupation;
                    member.SpouseCompanyAddress = req.Spouse.CompanyAddress;
                    member.SpouseSalary = req.Spouse.Salary;

                    member.NumberOfDependent = req.Family.NumberOfDependent;
                    member.IsHaveOKU = req.Family.IsHasOKU.HasValue ? (sbyte?)req.Family.IsHasOKU : null;
                    member.FatherName = req.Family.FatherName;
                    member.FatherContactNumber = req.Family.FatherContactNumber;
                    member.FatherAddress = req.Family.FatherAddress;
                    member.MotherName = req.Family.MotherName;
                    member.MotherConcatNumber = req.Family.MotherContactNumber;
                    member.MotherAddress = req.Family.MotherAddress;

                    member.CompanyEmployerTypeId = req.Company.EmployerTypeId.ToInt().IfZeroToNull();
                    member.CompanyName = req.Company.CompanyName;
                    member.CompanyJobTitle = req.Company.JobTitle;
                    member.CompanySectorId = req.Company.SectorId.ToInt().IfZeroToNull();
                    member.CompanySectorOther = req.Company.SectorOther;
                    member.CompanyDepartmentId = req.Company.DepartmentId.ToInt().IfZeroToNull();
                    member.CompanyDepartementOther = req.Company.DepartmentOther;
                    member.CompanyAddress = req.Company.CompanyAddress;
                    member.CompanyOfficeContactNumber = req.Company.OfficeContactNumber;
                    member.CompanyEmploymentStatusId = req.Company.EmploymentStatusId.ToInt().IfZeroToNull();
                    member.RetirementAge = req.Company.RetirementAge;
                    member.CompanyYearOfService = req.Company.YearOfService;
                    member.CompanyEmployerTypeOther = req.Company.EmployerTypeOther;
                    member.CompanyEmploymentStatusOther = req.Company.EmploymentStatusOther;
                    member.CompanyEmployerName = req.Company.EmployerName;

                    member.EmergencyConcatPerson = req.Emergency.ContactPerson;
                    member.EmergencyRelationId = req.Emergency.RelationShipId.ToInt().IfZeroToNull();
                    member.EmergencyConcatNumber = req.Emergency.ContactNumber;
                    member.EmergencyPersonICNumber = req.Emergency.ICNumber;
                    member.EmergencyPersonOccupation = req.Emergency.Occupation;
                    member.EmergencyPersonAddress = req.Emergency.Address;

                    member.BankId = req.Bank.BankId.ToInt().IfZeroToNull();
                    member.BankAccountNumber = req.Bank.AccountNumber;
                    member.BankOther = req.Bank.BankOther;
                    member.Salary = req.Bank.GrossSalary;

                    member.OtherPreferredLanguage = req.Other.PreferredLanguage;
                    member.OtherHobbies = req.Other.Hobbies;
                    member.OtherSocialMediaHandles = req.Other.SocialMediaHandles;
                    member.OtherFBName = req.Other.FBName;
                    member.SalaryRangeId = req.Bank.SalaryRangeId.ToInt().IfZeroToNull();

                    if (icFile != null)
                    {
                        db.Files.AddObject(icFile);
                        await db.SaveChangesAsync();

                        member.ICFileId = icFile.FileId;
                    }
                    if (payslipFile != null)
                    {
                        db.Files.AddObject(payslipFile);
                        await db.SaveChangesAsync();

                        member.PayslipFileId = payslipFile.FileId;
                    }
                    if (offerLetterFile != null)
                    {
                        db.Files.AddObject(offerLetterFile);
                        await db.SaveChangesAsync();

                        member.OfferLetterFileId = offerLetterFile.FileId;
                    }
                    await db.SaveChangesAsync();

                    await RecalculateFileNumber(db, member.MemberId);

                    tscope.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }

        //public static async Task<RespArgs<long>> SurveyAsync(RequestNewSurveyDataByMember req,File icFile,File payslipFile,File offerLetterFile) 
        //{
        //   using(var tscope = new TransactionScope())
        //    {
        //        using (CsaEntities db = new CsaEntities())
        //        {
        //            var member = db.Members.FirstOrDefault(x => x.MemberId == req.MemberId);
        //            if (member == null) throw new ArgumentException("Member not found");

        //            var dt = DateTime.Now;
        //            member.ICNumber = req.Profile.ICNumber;
        //            member.Birthdate = req.Profile.DateOfBirth;
        //            member.GenderId = req.Profile.GenderId;
        //            member.Email = req.Profile.EmailAddress.IsEmpty() ? null : req.Profile.EmailAddress;
        //            if(!req.Login.IsLogged)
        //            {
        //                var salt = SecurityLibrary.GenerateSalt();
        //                var passwordSalted = SecurityLibrary.SHA512Hash(req.Login.Password + salt);

        //                member.PasswordSalted = passwordSalted;
        //                member.Salt = salt;
        //            }

        //            if(member.StatusId == 0) member.StatusId = (int)MemberStatus.ACTIVE;

        //            member.ReferrerMemberId = req.ReferralMemberId;

        //            member.ProgramEventId = req.Profile.ProgramEventId.ToInt().IfZeroToNull();
        //            member.StreetAddress1 = req.Profile.Address;
        //            member.StreetAddress1 = req.Profile.TaxNumber;
        //            member.RaceId = req.Profile.RaceId.ToInt().IfZeroToNull();
        //            member.ReligionId = req.Profile.ReligionId.ToInt().IfZeroToNull();
        //            member.HighestLevelOfEducationId = req.Profile.HighLevelOfEducationId.ToInt().IfZeroToNull();
        //            member.MaritalStatusId = req.Profile.MaritalStatusId.ToInt().IfZeroToNull();

        //            member.SpouseFullName = req.Spouse.FullName;
        //            member.SpouseIdentificationNumber = req.Spouse.ICNumber;
        //            member.SpouseContactInformation = req.Spouse.ContactInformation;
        //            member.SpouseOccupation = req.Spouse.Occupation;
        //            member.SpouseCompanyAddress = req.Spouse.CompanyAddress;
        //            member.SpouseSalary = req.Spouse.Salary;

        //            member.NumberOfDependent = req.Family.NumberOfDependent;
        //            member.IsHaveOKU = req.Family.IsHasOKU.HasValue ? (sbyte?)req.Family.IsHasOKU : null;
        //            member.FatherName = req.Family.FatherName;
        //            member.FatherContactNumber = req.Family.FatherContactNumber;
        //            member.FatherAddress = req.Family.FatherAddress;
        //            member.MotherName = req.Family.MotherName;
        //            member.MotherConcatNumber = req.Family.MotherContactNumber;
        //            member.MotherAddress = req.Family.MotherAddress;

        //            member.CompanyEmployerTypeId = req.Company.EmployerTypeId.ToInt().IfZeroToNull();
        //            member.CompanyName = req.Company.CompanyName;
        //            member.CompanyJobTitle = req.Company.JobTitle;
        //            member.CompanySectorId = req.Company.SectorId.ToInt().IfZeroToNull();
        //            member.CompanyDepartmentId = req.Company.DepartmentId.ToInt().IfZeroToNull();
        //            member.CompanyAddress = req.Company.CompanyAddress;
        //            member.CompanyOfficeContactNumber = req.Company.OfficeContactNumber;
        //            member.CompanyEmploymentStatusId = req.Company.EmploymentStatusId.ToInt().IfZeroToNull();
        //            member.RetirementAge = req.Company.RetirementAge;
        //            member.CompanyYearOfService = req.Company.YearOfService;

        //            member.EmergencyConcatPerson = req.Emergency.ContactPerson;
        //            member.EmergencyRelationId = req.Emergency.RelationShipId.ToInt().IfZeroToNull();
        //            member.EmergencyConcatNumber = req.Emergency.ContactNumber;
        //            member.EmergencyPersonICNumber = req.Emergency.ICNumber;
        //            member.EmergencyPersonOccupation = req.Emergency.Occupation;
        //            member.EmergencyPersonAddress = req.Emergency.Address;

        //            member.BankId = req.Bank.BankId.ToInt().IfZeroToNull();
        //            member.BankAccountNumber = req.Bank.AccountNumber;
        //            member.Salary = req.Bank.GrossSalary;

        //            member.OtherPreferredLanguage = req.Other.PreferredLanguage;
        //            member.OtherHobbies = req.Other.Hobbies;
        //            member.OtherSocialMediaHandles = req.Other.SocialMediaHandles;
        //            member.OtherFBName = req.Other.FBName;
        //            member.SurveyStatusId = (int)SurveyStatus.New;
        //            member.CreateDate = DateTime.Now;
        //            member.SalaryRangeId = req.Bank.SalaryRangeId.ToInt().IfZeroToNull();

        //            if (icFile != null)
        //            {
        //                db.Files.AddObject(icFile);
        //                await db.SaveChangesAsync();

        //                member.ICFileId = icFile.FileId;
        //            }
        //            if (payslipFile != null)
        //            {
        //                db.Files.AddObject(payslipFile);
        //                await db.SaveChangesAsync();

        //                member.PayslipFileId = payslipFile.FileId;
        //            }
        //            if (offerLetterFile != null)
        //            {
        //                db.Files.AddObject(offerLetterFile);
        //                await db.SaveChangesAsync();

        //                member.OfferLetterFileId = offerLetterFile.FileId;
        //            }
        //            await db.SaveChangesAsync();

        //            var newSurvey = new Survey
        //            {
        //                MemberId = member.MemberId,
        //                Answer = req.SurveyJson,
        //                SurveyVersionId = 1,
        //                CreateDate = dt
        //            };
        //            db.Surveys.AddObject(newSurvey);
        //            await db.SaveChangesAsync();

        //            EmailBiz.NewClientRegistration(db, new EmailNewClientRegistrationData(member.FullName, member.CreateDate, member.PhoneNumber, BuildFileNumber(member.MemberId, member.CompanyTypeId, null, null, null)));
        //            if(member.Email.IsNotEmpty()) EmailBiz.YabamCompleteSurvey(db, new EmailNewData(member.Email,member.FullName));

        //            tscope.Complete();
        //            return RespArgs<long>.CreateSuccess(1);
        //        }
        //    }
        //}

        public static async Task<RespArgs<string>> SurveyAsync(RequestNewSurvey1DataByMember req, File icFile, File payslipFile,string linkLogin)
        {
            using (var tscope = new TransactionScope())
            {
                using (CsaEntities db = new CsaEntities())
                {
                    var member = db.Members.FirstOrDefault(x => x.MemberId == req.MemberId);
                    if (member == null) throw new ArgumentException("Member not found");
                    //var survey = db.Surveys.FirstOrDefault(x => x.MemberId == member.MemberId);
                    //if(survey != null) throw new ArgumentException("Duplicate execute survey");
                    //if(member.StatusId != (int)MemberStatus.DRAFT) throw new ArgumentException("Duplicate execute survey");

                    if (req.Email.IsNotEmpty())
                    {
                        if(ExistEmailAddress(req.Email, req.MemberId)) throw new ArgumentException("Email already exist");
                    }

                    if (ExistICNumber(req.ICNumber, req.MemberId)) throw new ArgumentException("ICNumber already exist");

                    var dt = DateTime.Now;
                    member.ICNumber = req.ICNumber;
                    string defaultPassword = "peluangkedua";
                    if (!req.IsLogged)
                    {
                        var salt = SecurityLibrary.GenerateSalt();
                        var passwordSalted = SecurityLibrary.SHA512Hash(defaultPassword + salt);

                        member.PasswordSalted = passwordSalted;
                        member.Salt = salt;
                    }

                    member.CompanyEmployerTypeId = req.EmployerTypeId;
                    member.CompanyEmployerTypeOther = req.EmployerTypeOther;
                    member.CompanyEmploymentStatusId = req.EmploymentStatusId;
                    member.CompanyEmploymentStatusOther = req.EmploymentStatusOther;
                    member.RetirementAge = req.RetirementAge;
                    member.CompanyYearOfService = req.YearOfService;
                    member.ReferrerMemberId = req.ReferralMemberId;
                    member.BankId = req.BankId.ToInt().IfZeroToNull();
                    member.BankOther = member.BankId == Constant.OTHER_NUMBER ? req.BankOther : "" ;
                    member.BankAccountNumber = req.BankAccountNumber;
                    member.SurveyStatusId = (int)SurveyStatus.New;
                    member.ProgramEventId = (int)MemberProgramEvent.YABAM;//yabam
                    member.CountryId = 1;//malaysia default                    
                    member.StateId = req.StateId.ToInt().IfZeroToNull();
                    member.CompanyEmployerName = req.CompanyEmployerName;
                    member.CompanySectorId = req.CompanySectorId;
                    member.CompanySectorOther = req.CompanySectorOther;
                    member.CompanyDepartmentId = req.CompanyDepartmentId;
                    member.CompanyDepartementOther = req.CompanyDepartmentOther;
                    member.GenderId = req.GenderId;
                    member.Email = req.Email.IsEmpty() ? null : req.Email;
                    member.StatusId = (int)MemberStatus.UNVERIFIED;

                    if (!member.ReferrerMemberId.HasValue)
                    {
                        member.AdminRemark = $"Referrer: {req.ReferrerName}";
                    }

                    if (member.StatusId == (int)MemberStatus.DRAFT)
                    {
                        member.CreateDate = dt;
                    }

                    if (icFile != null)
                    {
                        db.Files.AddObject(icFile);
                        await db.SaveChangesAsync();

                        member.ICFileId = icFile.FileId;
                    }
                    if (payslipFile != null)
                    {
                        db.Files.AddObject(payslipFile);
                        await db.SaveChangesAsync();

                        member.PayslipFileId = payslipFile.FileId;
                    }
                    await db.SaveChangesAsync();

                    var newSurvey = new Survey
                    {
                        MemberId = member.MemberId,
                        Answer = req.SurveyJson,
                        SurveyVersionId = 1,
                        CreateDate = dt
                    };
                    db.Surveys.AddObject(newSurvey);                    

                    //auto withdraw
                    db.Withdrawals.AddObject(WithdrawalBiz.Factory(new RequestNewWithdrawal(member.MemberId, null, CommissionBiz.CommissionRegisterNewMember, member.BankId, member.BankAccountName, member.BankAccountNumber,parentMemberId: null,dt)));
                    if (member.ReferrerMemberId.HasValue)
                    {
                        var refMember = db.Members.FirstOrDefault(x => x.MemberId == member.ReferrerMemberId.Value);
                        db.Withdrawals.AddObject(WithdrawalBiz.Factory(new RequestNewWithdrawal(refMember.MemberId, null, CommissionBiz.CommissionRegisterUpline, refMember.BankId, refMember.BankAccountName, refMember.BankAccountNumber,parentMemberId: member.MemberId,dt)));
                    }
                    await db.SaveChangesAsync();

                    var resCalculate = await RecalculateFileNumber(db, member.MemberId);

                    EmailBiz.NewClientRegistration(db, new EmailNewClientRegistrationData(member.FullName, member.CreateDate, member.PhoneNumber, resCalculate.ObjVal));
                    if (member.Email.IsNotEmpty()) EmailBiz.YabamCompleteSurvey(db, new EmailNewData(member.Email, member.FullName,defaultPassword,linkLogin,member.PhoneNumber));

                    tscope.Complete();
                    return RespArgs<string>.CreateSuccess(member.PhoneNumber);
                }
            }
        }

        public static async Task<RespArgs<Member>> RegisterMemberBySurvey(RequestNewMemberSurvey req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                if (ExistPhone(req.PhoneNumber,null)) throw new ArgumentException("Phone already exist");

                var arrName = req.FullName?.Trim().Split(' ');
                string firstName = arrName[0];
                string lastName = "";
                
                if (arrName.Count() > 1)
                {
                    lastName = string.Join(" ", arrName.Skip(1));
                }
                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                var newMember = new Member
                {
                    FirstName = textInfo.ToTitleCase(firstName),
                    LastName = textInfo.ToTitleCase(lastName),
                    PhoneNumber = req.PhoneNumber,
                    CreateDate = DateTime.Now,
                    StatusId = (int)MemberStatus.DRAFT,
                };
                db.Members.AddObject(newMember);
                await db.SaveChangesAsync();
                return RespArgs<Member>.CreateSuccess(newMember);
            }
        }

        public static async Task UpdateMemberBySurvey(RequestNewMemberSurvey req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var member = db.Members.FirstOrDefault(x => x.PhoneNumber == req.PhoneNumber);
                if (member == null) throw new ArgumentException("Member not found");
                var arrName = req.FullName?.Trim().Split(' ');
                string firstName = arrName[0];
                string lastName = "";

                if (arrName.Count() > 1)
                {
                    lastName = string.Join(" ", arrName.Skip(1));
                }

                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                member.FirstName = textInfo.ToTitleCase(firstName);
                member.LastName = textInfo.ToTitleCase(lastName);                    
                await db.SaveChangesAsync();
            }
        }

        public static RespArgs<GridViewModel<MemberGVByAdmin>> GetMemberGVByAdmin(MemberGVType memberGVType, string search, int pageIndex, int pageSize, string sortOrder, SQLSelect.OrderByEnum sortDirection)
        {
            using (CsaEntities db = new CsaEntities())
            {
                search = search.ToLiteral();
                var sqlSelect = new SQLSelect("member m");
                sqlSelect.AddSelect("m.CreateDate,m.MemberId,m.FirstName,m.LastName,m.ICNumber,m.PhoneNumber,m.CompanyEmployerName as EmployerName,m.Occupation,CONCAT(IFNULL(refm.FirstName,''),' ',IFNULL(refm.LastName,'')) as ReferrerName,m.MemberTypeId,m.CompanyTypeId,m.FileNumber,IF(m.CompanySectorId='999',m.CompanySectorOther,sec.Name) as Sector,st.Name as State,m.BankAccountNumber,m.BankAccountName,bank.Name as Bank,m.SalaryRangeId");
                sqlSelect.AddLeftJoin("member refm", "m.ReferrerMemberId", "refm.MemberId");
                sqlSelect.AddLeftJoin("state st", "m.StateId", "st.StateId");
                sqlSelect.AddLeftJoin("sector sec", "m.CompanySectorId", "sec.SectorId");
                sqlSelect.AddLeftJoin("bank bank", "m.BankId", "bank.BankId");
                sqlSelect.AddWhere($"m.StatusId IN ({string.Join(",", new int[] { (int)MemberStatus.ACTIVE, (int)MemberStatus.INACTIVE })})");
                sqlSelect.SetOrderBY(sortOrder, sortDirection);
                sqlSelect.SetLimit((pageIndex * pageSize), pageSize);

                if (!search.IsEmpty()) sqlSelect.AddWhere($"(CONCAT(IFNULL(m.FirstName,''),IFNULL(m.LastName,'')) LIKE '%{search}%' OR m.PhoneNumber LIKE '%{search}%' OR m.CompanyEmployerName LIKE '%{search}%' OR m.Occupation LIKE '%{search}%' OR m.ICNumber LIKE '%{search}%')");

                switch (memberGVType)
                {
                    case MemberGVType.Client:
                        sqlSelect.AddWhere($"(m.FileNumber LIKE '%*C' AND (m.ProgramEventId != {(int)MemberProgramEvent.YABAM} OR m.ProgramEventId IS NULL))");
                        break;
                    case MemberGVType.Hero_Wira:
                        sqlSelect.AddWhere($"(m.FileNumber LIKE '%*R' OR m.FileNumber LIKE '%*S')");
                        break;
                    case MemberGVType.Member:
                        sqlSelect.AddWhere($"(m.FileNumber LIKE '%*ME')");
                        break;
                    case MemberGVType.Agent:
                        sqlSelect.AddWhere($"(m.FileNumber LIKE '%*AGT')");
                        break;
                    case MemberGVType.Drop_Mia:
                        sqlSelect.AddWhere($"(m.FileNumber LIKE '%*X')");
                        break;
                    case MemberGVType.Yabam:
                        sqlSelect.AddWhere($"(m.FileNumber LIKE '%*C' AND m.ProgramEventId = {(int)MemberProgramEvent.YABAM})");
                        break;
                    default:
                        break;
                }

                var list = db.ExecuteStoreQuery<MemberGVByAdmin>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                return RespArgs<GridViewModel<MemberGVByAdmin>>.CreateSuccess(new GridViewModel<MemberGVByAdmin>(list, count, count, pageIndex + 1, pageSize, null, 1));
            }
        }

        public static RespArgs<GridViewModel<AgentGVByAdmin>> GetAgentGVByAdmin(string search, int pageIndex, int pageSize, string sortOrder, SQLSelect.OrderByEnum sortDirection)
        {
            using (CsaEntities db = new CsaEntities())
            {
                search = search.ToLiteral();
                var sqlSelect = new SQLSelect("member m");
                sqlSelect.AddSelect("m.CreateDate,m.MemberId,m.FirstName,m.LastName,m.ICNumber,m.PhoneNumber,m.CompanyEmployerName as EmployerName,m.FileNumber,IF(m.CompanySectorId='999',m.CompanySectorOther,sec.Name) as Sector,st.Name as State,IF(m.CompanyDepartmentId='999',m.CompanyDepartementOther,job.Name) as Position,IF(m.PayslipFileId IS NOT NULL,'YES','NO') as Payslip");
                sqlSelect.AddLeftJoin("state st", "m.StateId", "st.StateId");
                sqlSelect.AddLeftJoin("sector sec", "m.CompanySectorId", "sec.SectorId");
                sqlSelect.AddLeftJoin("job_position job", "m.CompanyDepartmentId", "job.JobPositionId");
                sqlSelect.AddWhere($"m.StatusId IN ({string.Join(",", new int[] { (int)MemberStatus.ACTIVE, (int)MemberStatus.INACTIVE })})");
                sqlSelect.SetOrderBY(sortOrder, sortDirection);
                sqlSelect.SetLimit((pageIndex * pageSize), pageSize);

                if (!search.IsEmpty()) sqlSelect.AddWhere($"(CONCAT(IFNULL(m.FirstName,''),IFNULL(m.LastName,'')) LIKE '%{search}%' OR m.PhoneNumber LIKE '%{search}%' OR m.ICNumber LIKE '%{search}%')");
                sqlSelect.AddWhere($"(m.FileNumber LIKE '%*AGT')");

                var list = db.ExecuteStoreQuery<AgentGVByAdmin>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                return RespArgs<GridViewModel<AgentGVByAdmin>>.CreateSuccess(new GridViewModel<AgentGVByAdmin>(list, count, count, pageIndex + 1, pageSize, null, 1));
            }
        }

        public static async Task RecalculateAllMember()
        {
            using(CsaEntities db = new CsaEntities())
            {
                using(var tscope = new TransactionScope())
                {
                    var members = db.Members.ToList();
                    foreach (var item in members)
                    {
                        await RecalculateFileNumber(db, item.MemberId);
                    }
                    
                    tscope.Complete();
                }
            }
        }

        public static RespArgs<GridViewModel<ExportSurvey>> ExportSurvey(RequestExportSurvey req,int pageIndex,int pageSize = 10)
        {
            using(CsaEntities db = new CsaEntities())
            {
                var sqlSelect = new SQLSelect("member m");
                sqlSelect.AddSelect("m.FirstName,m.LastName,m.ICNumber,b.Name as Bank,m.BankAccountNumber,m.ICFileId,m.PayslipFileId,surv.Answer");
                sqlSelect.AddJoin("survey surv", "m.MemberId", "surv.MemberId");
                sqlSelect.AddLeftJoin("bank b", "m.BankId", "b.BankId");
                if (req.StartDate.HasValue) sqlSelect.AddWhere($"surv.CreateDate >= '{req.StartDate.Value.ToString("yyyy-MM-dd")}'");
                if (req.EndDate.HasValue) sqlSelect.AddWhere($"surv.CreateDate < '{req.StartDate.Value.AddDays(1).ToString("yyyy-MM-dd")}'");
                sqlSelect.SetOrderBY("surv.CreateDate", SQLSelect.OrderByEnum.DESC);
                sqlSelect.SetLimit((pageIndex * pageSize), pageSize);
                sqlSelect.AddWhere($"m.StatusId IN ({string.Join(",", new int[] { (int)MemberStatus.ACTIVE, (int)MemberStatus.INACTIVE, (int)MemberStatus.UNVERIFIED })})");

                var list = db.ExecuteStoreQuery<ExportSurvey>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                return RespArgs<GridViewModel<ExportSurvey>>.CreateSuccess(new GridViewModel<ExportSurvey>(list, count, count, pageIndex + 1, pageSize, null, 1));
            }
        }

        public static RespArgs<GridViewModel<ExportFinance>> ExportFinance(RequestExportFinance req, int pageIndex, int pageSize = 10)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var sqlSelect = new SQLSelect("withdrawal w");
                sqlSelect.AddSelect("m.FirstName,m.LastName,b.Name as Bank,w.BankAccountNumber,w.StatusId,m.ICNumber,w.Amount");
                sqlSelect.AddJoin("member m", "w.MemberId", "m.MemberId");
                sqlSelect.AddLeftJoin("bank b", "w.BankId", "b.BankId");
                if (req.StartDate.HasValue) sqlSelect.AddWhere($"w.CreateDate >= '{req.StartDate.Value.ToString("yyyy-MM-dd")}'");
                if (req.EndDate.HasValue) sqlSelect.AddWhere($"w.CreateDate < '{req.StartDate.Value.AddDays(1).ToString("yyyy-MM-dd")}'");
                sqlSelect.AddWhere($"m.StatusId IN ({string.Join(",", new int[] { (int)MemberStatus.ACTIVE, (int)MemberStatus.INACTIVE, (int)MemberStatus.UNVERIFIED })})");
                sqlSelect.SetOrderBY("w.CreateDate", SQLSelect.OrderByEnum.DESC);
                sqlSelect.SetLimit((pageIndex * pageSize), pageSize);

                var list = db.ExecuteStoreQuery<ExportFinance>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                return RespArgs<GridViewModel<ExportFinance>>.CreateSuccess(new GridViewModel<ExportFinance>(list, count, count, pageIndex + 1, pageSize, null, 1));
            }
        }

        public static RespArgs<GridViewModel<ExportDemographicMarketAnalysis>> ExportDemographicMarketAnalysis(RequestExportDemographicMarketAnalysis req, int pageIndex, int pageSize = 10)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var sqlSelect = new SQLSelect("member m");
                sqlSelect.AddSelect("m.ProgramEventId,m.RaceId,m.Birthdate,m.GenderId,m.Salary,m.SalaryRangeId,st.Name as State,sector.Name as Sector");
                sqlSelect.AddLeftJoin("state st", "m.StateId", "st.StateId");
                sqlSelect.AddLeftJoin("sector sector", "m.CompanySectorId", "sector.SectorId");
                if (req.StartDate.HasValue) sqlSelect.AddWhere($"m.CreateDate >= '{req.StartDate.Value.ToString("yyyy-MM-dd")}'");
                if (req.EndDate.HasValue) sqlSelect.AddWhere($"m.CreateDate < '{req.StartDate.Value.AddDays(1).ToString("yyyy-MM-dd")}'");
                sqlSelect.AddWhere($"m.StatusId IN ({string.Join(",", new int[] { (int)MemberStatus.ACTIVE, (int)MemberStatus.INACTIVE, (int)MemberStatus.UNVERIFIED })})");
                sqlSelect.SetOrderBY("m.CreateDate", SQLSelect.OrderByEnum.DESC);
                sqlSelect.SetLimit((pageIndex * pageSize), pageSize);

                var list = db.ExecuteStoreQuery<ExportDemographicMarketAnalysis>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                return RespArgs<GridViewModel<ExportDemographicMarketAnalysis>>.CreateSuccess(new GridViewModel<ExportDemographicMarketAnalysis>(list, count, count, pageIndex + 1, pageSize, null, 1));
            }
        }

        public static RespArgs<GridViewModel<ExportRM>> ExportRM(RequestExportRM req, int pageIndex, int pageSize = 10)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var sqlSelect = new SQLSelect("member m");
                sqlSelect.AddSelect("m.FirstName,m.LastName,COUNT(refm.MemberId) as ReferralCount");
                sqlSelect.AddLeftJoin("member refm", "m.MemberId", "refm.ReferrerMemberId");
                if (req.StartDate.HasValue) sqlSelect.AddWhere($"m.CreateDate >= '{req.StartDate.Value.ToString("yyyy-MM-dd")}'");
                if (req.EndDate.HasValue) sqlSelect.AddWhere($"m.CreateDate < '{req.StartDate.Value.AddDays(1).ToString("yyyy-MM-dd")}'");
                sqlSelect.AddWhere($"m.StatusId IN ({string.Join(",", new int[] { (int)MemberStatus.ACTIVE, (int)MemberStatus.INACTIVE, (int)MemberStatus.UNVERIFIED })})");
                sqlSelect.AddWhere($"ReferralCount > 0");
                sqlSelect.SetOrderBY("ReferralCount", SQLSelect.OrderByEnum.DESC);
                sqlSelect.SetGroupBY("m.MemberId");
                sqlSelect.SetLimit((pageIndex * pageSize), pageSize);

                var list = db.ExecuteStoreQuery<ExportRM>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                return RespArgs<GridViewModel<ExportRM>>.CreateSuccess(new GridViewModel<ExportRM>(list, count, count, pageIndex + 1, pageSize, null, 1));
            }
        }
        
        public static RespArgs<GridViewModel<ExportOperation>> ExportOperation(RequestExportOperation req, int pageIndex, int pageSize = 10)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var sqlSelect = new SQLSelect("settlement settl");
                sqlSelect.AddSelect("m.FirstName,m.LastName,m.FileNumber,settl.Amount,settl.AmountFacilities");
                sqlSelect.AddJoin("application app", "settl.ApplicationId", "app.ApplicationId");
                sqlSelect.AddJoin("member m", "app.MemberId", "m.MemberId");
                //if (req.StartDate.HasValue) sqlSelect.AddWhere($"m.CreateDate >= '{req.StartDate.Value.ToString("yyyy-MM-dd")}'");
                //if (req.EndDate.HasValue) sqlSelect.AddWhere($"m.CreateDate < '{req.StartDate.Value.AddDays(1).ToString("yyyy-MM-dd")}'");
                sqlSelect.AddWhere($"m.StatusId IN ({string.Join(",", new int[] { (int)MemberStatus.ACTIVE, (int)MemberStatus.INACTIVE, (int)MemberStatus.UNVERIFIED })})");
                sqlSelect.SetLimit((pageIndex * pageSize), pageSize);

                var list = db.ExecuteStoreQuery<ExportOperation>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                return RespArgs<GridViewModel<ExportOperation>>.CreateSuccess(new GridViewModel<ExportOperation>(list, count, count, pageIndex + 1, pageSize, null, 1));
            }
        }

        public static RespArgs<ExportCredit> ExportCredit()
        {
            using(CsaEntities db = new CsaEntities())
            {
                var exportCredit = new ExportCredit();

                int precheckingStatus = (int)ApplicationStatus.PRE_CHECKING;
                int preparationStatus = (int)ApplicationStatus.PREPARATION;
                int presentationStatus = (int)ApplicationStatus.PRESENTATION;
                int queueForReloanStatus = (int)ApplicationStatus.QUEUEFORLOAN;
                int reloanStatus = (int)ApplicationStatus.RELOAN;
                exportCredit.PreCheckingStage = db.Applications.Count(x => x.ApplicationStatusId == precheckingStatus);
                exportCredit.ProposalPreparationStage = db.Applications.Count(x => x.ApplicationStatusId == preparationStatus);
                exportCredit.ProposalPresentationStage = db.Applications.Count(x => x.ApplicationStatusId == presentationStatus);
                exportCredit.QueueForReloanStage = db.Applications.Count(x => x.ApplicationStatusId == queueForReloanStatus);
                exportCredit.ReloanSubmissionStage = db.Applications.Count(x => x.ApplicationStatusId == reloanStatus);

                int eligibleStatus = (int)CustomerStatus.Eligible;
                int burstStatus = (int)CustomerStatus.Burst;
                int dropStatus = (int)CustomerStatus.Drop_Case;
                int miaStatus = (int)CustomerStatus.MIA;
                exportCredit.TotalApprovesCases = db.Applications.Count(x => x.CustomerStatusId == eligibleStatus);
                exportCredit.TotalBurstCases = db.Applications.Count(x => x.CustomerStatusId == burstStatus);
                exportCredit.TotalDroppedCases = db.Applications.Count(x => x.CustomerStatusId == dropStatus);
                exportCredit.TotalMIACases = db.Applications.Count(x => x.CustomerStatusId == miaStatus);

                int singleCreditStatus = (int)CreditStatus.SINGLE;
                int rnrStatus = (int)CreditStatus.RANCANG_REZEKI_RNR;
                exportCredit.TotalSingleCases = db.Applications.Count(x => x.CreditStatusId == singleCreditStatus);
                exportCredit.TotalRNRCases = db.Applications.Count(x => x.CreditStatusId == rnrStatus);

                return RespArgs<ExportCredit>.CreateSuccess(exportCredit);
            }
        }
    }
}
