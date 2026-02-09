using csa.Library;
using csa.Model;
using csa.Model.DataObject;
using CsaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.DataLogic
{
    public static class AdminBiz
    {
        public static Admin Get(int adminId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Admins.FirstOrDefault(x => x.AdminId == adminId);
            }
        }

        public static List<Admin> GetAllAdminActive()
        {
            using (CsaEntities db = new CsaEntities())
            {
                int statusId = (int)GlobalStatus.ACTIVE;
                return db.Admins.Where(x => x.StatusId == statusId).ToList();
            }
        }

        public static Admin Get(string email)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Admins.FirstOrDefault(x => x.Email == email);
            }
        }

        public static void SetLastLogin(int adminId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var admin = db.Admins.FirstOrDefault(x => x.AdminId == adminId);
                if (admin == null) return;
                admin.LastLogin = DateTime.Now;
                db.SaveChangesAsync();
            }
        }
        public static RespArgs<LoginAdmin> Login(RequestLoginAdmin req)
        {
            var admin = Get(req.Email);
            if (admin == null) throw new ArgumentException("Invalid email or password");

            string shaPassword = SecurityLibrary.SHA512Hash(req.Password + admin.Salt);
            if (shaPassword != admin.PasswordSalted) throw new ArgumentException("Invalid email or password");
            SetLastLogin(admin.AdminId);
            Role findRole = RoleBiz.Get(admin.RoleId);
            return RespArgs<LoginAdmin>.CreateSuccess(admin.Convert(findRole?.AccessList));
        }

        public static RespArgs<bool> ChangePassword(RequestAdminChangePassword req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var admin = db.Admins.FirstOrDefault(x => x.AdminId == req.AdminId);
                if (admin == null) throw new ArgumentException("Admin notfound");

                string shaPassword = SecurityLibrary.SHA512Hash(req.CurrentPassword + admin.Salt);
                if (shaPassword != admin.PasswordSalted) throw new ArgumentException("Invalid current password");

                var salt = SecurityLibrary.GenerateSalt();
                var passwordSalted = SecurityLibrary.SHA512Hash(req.NewPassword + salt);

                admin.Salt = salt;
                admin.PasswordSalted = passwordSalted;
                db.SaveChangesAsync();
                return RespArgs<bool>.CreateSuccess(true);
            }
        }

        public static async Task<RespArgs<bool>> ChangeRole(RequestAdminChangeRole req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var admin = db.Admins.FirstOrDefault(x => x.AdminId == req.AdminId);
                if (admin == null) throw new ArgumentException("Admin not found");

                admin.RoleId = req.RoleId.ToInt().IfZeroToNull();
                await db.SaveChangesAsync();
                return RespArgs<bool>.CreateSuccess(true);
            }
        }

        public static RespArgs<GridViewModel<AdminRoleRequestGV>> GetRequestGVByAdmin(string search, int pageIndex, int pageSize, string sortOrder, SQLSelect.OrderByEnum sortDirection)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var sqlSelect = new SQLSelect("admin a");
                sqlSelect.AddSelect("a.AdminId,a.Name,a.CreateDate,r.Name as RoleName,a.RoleId");
                sqlSelect.SetOrderBY(sortOrder, sortDirection);
                sqlSelect.SetLimit((pageIndex * pageSize), pageSize);
                sqlSelect.AddLeftJoin("`role` r", "a.RoleId", "r.RoleId");
                if (!search.IsEmpty()) sqlSelect.AddWhere($"a.Name LIKE '%{search}%'");

                var list = db.ExecuteStoreQuery<AdminRoleRequestGV>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                return RespArgs<GridViewModel<AdminRoleRequestGV>>.CreateSuccess(new GridViewModel<AdminRoleRequestGV>(list, count, count, pageIndex + 1, pageSize, null, 1));
            }
        }

        public static async Task<RespArgs<int>> Create(RequestNewAdmin req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var admin = db.Admins.FirstOrDefault(x => x.Email == req.Email);
                if (admin != null) throw new ArgumentException("Admin already exist");

                var salt = SecurityLibrary.GenerateSalt();
                var passwordSalted = SecurityLibrary.SHA512Hash(req.Password + salt);

                var newObj = new Admin
                {
                    Name = req.Name,
                    Email = req.Email,
                    PasswordSalted = passwordSalted,
                    Salt = salt,
                    IsSuperAdmin = req.AdminTypeId ?? 0,
                    RoleId = req.TeamId.ToInt().IfZeroToNull(),
                    StatusId = (int)GlobalStatus.ACTIVE,
                    CreateDate = DateTime.Now
                };
                db.Admins.AddObject(newObj);
                await db.SaveChangesAsync();
                return RespArgs<int>.CreateSuccess(newObj.AdminId);
            }
        }

        public static async Task<RespArgs<bool>> Update(RequestUpdateAdmin req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var admin = db.Admins.FirstOrDefault(x => x.Email == req.Email);
                if (admin != null && admin.AdminId != req.AdminId) throw new ArgumentException("Admin already exist");

                var adminToUpdate = db.Admins.FirstOrDefault(x => x.AdminId == req.AdminId);
                if (adminToUpdate == null) throw new ArgumentException("Admin not found");

                adminToUpdate.Name = req.Name;
                adminToUpdate.Email = req.Email;
                adminToUpdate.RoleId = req.RoleId.ToInt().IfZeroToNull();
                adminToUpdate.StatusId = req.StatusId;
                await db.SaveChangesAsync();
                return RespArgs<bool>.CreateSuccess(true);
            }
        }

        public static RespArgs<bool> ChangePasswordByAdmin(RequestAdminChangePasswordByAdmin req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var adminToChanged = db.Admins.FirstOrDefault(x => x.AdminId == req.AdminToChangePasswordId);
                if (adminToChanged == null) throw new ArgumentException("Admin not found");

                var admin = db.Admins.FirstOrDefault(x => x.AdminId == req.AdminId);
                if (admin == null) throw new ArgumentException("cant_access");

                string shaPassword = SecurityLibrary.SHA512Hash(req.CurrentPassword + admin.Salt);
                if (shaPassword != admin.PasswordSalted) throw new ArgumentException("Invalid current admin password");

                var salt = SecurityLibrary.GenerateSalt();
                var passwordSalted = SecurityLibrary.SHA512Hash(req.NewPassword + salt);

                adminToChanged.Salt = salt;
                adminToChanged.PasswordSalted = passwordSalted;
                db.SaveChangesAsync();
                return RespArgs<bool>.CreateSuccess(true);
            }
        }

        public static RespArgs<GridViewModel<AdminRequestGV>> GetGVByAdmin(string search, int pageIndex, int pageSize, string sortOrder, SQLSelect.OrderByEnum sortDirection)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var sqlSelect = new SQLSelect("admin a");
                sqlSelect.AddSelect("a.AdminId,a.Name,a.CreateDate,a.StatusId,a.IsSuperAdmin,a.Email");
                sqlSelect.SetOrderBY(sortOrder, sortDirection);
                sqlSelect.SetLimit((pageIndex * pageSize), pageSize);
                if (!search.IsEmpty()) sqlSelect.AddWhere($"a.Name LIKE '%{search}%'");

                var list = db.ExecuteStoreQuery<AdminRequestGV>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                return RespArgs<GridViewModel<AdminRequestGV>>.CreateSuccess(new GridViewModel<AdminRequestGV>(list, count, count, pageIndex + 1, pageSize, null, 1));
            }
        }

        public static List<Admin> GetAllWithSearch(string search, int? exceptId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var admins = from admin in db.Admins
                              where (exceptId == null || admin.AdminId != exceptId)
                              && (admin.Name.Contains(search) ||
                              admin.Email.Contains(search))
                              select admin;
                return admins.ToList();
            }
        }
    }
}
