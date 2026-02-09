using csa.Library;
using csa.Model;
using csa.Model.DataObject;
using CsaModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.DataLogic
{
    public static class RoleBiz
    {
        public enum RoleAccess
        {
            [Description("User Management")]
            User_Management = 1,
            [Description("A Management")]
            A_Management,
            [Description("B Management")]
            B_Management,
            [Description("C Management")]
            C_Management
        }

        public static List<ValueText<int>> GetListAccess()
        {
            List<ValueText<int>> list = new List<ValueText<int>>();
            foreach (RoleAccess item in Enum.GetValues(typeof(RoleAccess)))
            {
                list.Add(new ValueText<int>((int)item, EnumHelper.GetDescription(item)));
            }

            return list;
        }
        public static Role Get(int? roleId)
        {
            using(CsaEntities db = new CsaEntities())
            {
                return db.Roles.FirstOrDefault(x => x.RoleId == roleId);
            }
        }

        public static List<Role> Gets()
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Roles.ToList();
            }
        }

        public static async Task<RespArgs<ValueText<int>>> Create(RequestNewRole req)
        {
            using(CsaEntities db = new CsaEntities())
            {
                var find = db.Roles.FirstOrDefault(x => x.Name == req.Name);
                if (find != null) throw new ArgumentException("role_already_exist");

                var newObj = new Role
                {
                    Name = req.Name,
                    AccessList = string.Join(",",req.AccessIds)
                };
                db.Roles.AddObject(newObj);
                await db.SaveChangesAsync();

                return RespArgs<ValueText<int>>.CreateSuccess(new ValueText<int>(newObj.RoleId,newObj.Name));
            }
        }

        public static async Task<RespArgs<bool>> Update(RequestUpdateRole req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var find = db.Roles.FirstOrDefault(x => x.Name == req.Name);
                if (find != null && find.RoleId != req.RoleId) throw new ArgumentException("role_already_exist");

                find.Name = req.Name;
                find.AccessList = string.Join(",", req.AccessIds);
                await db.SaveChangesAsync();

                return RespArgs<bool>.CreateSuccess(true);
            }
        }
    }
}
