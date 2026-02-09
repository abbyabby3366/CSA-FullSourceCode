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
    public static class EmailTemplateBiz
    {
        public static Emailtemplate Get(int emailTemplateId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Emailtemplates.FirstOrDefault(x => x.EmailTemplateId == emailTemplateId);
            }
        }

        public static Emailtemplate Get(string name)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Emailtemplates.FirstOrDefault(x => x.Name == name);
            }
        }
        public static bool ExistEmail(string name)
        {
            return Get(name) != null;
        }
        public static RespArgs<GridViewModel<EmailTemplateGV>> GetGV(string search, int pageIndex, int pageSize, string sortOrder, SQLSelect.OrderByEnum sortDirection)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var sqlSelect = new SQLSelect("emailtemplate et");
                sqlSelect.AddSelect("et.EmailTemplateId,et.Name,et.TemplateTypeId,et.CreateDate,et.StatusId");
                sqlSelect.SetOrderBY(sortOrder, sortDirection);
                sqlSelect.SetLimit((pageIndex * pageSize), pageSize);

                if (!search.IsEmpty()) sqlSelect.AddWhere($"et.Name LIKE '%{search}%'");

                var list = db.ExecuteStoreQuery<EmailTemplateGV>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                //add sequence
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].SequenceId = (pageIndex * pageSize) + i;
                }

                return RespArgs<GridViewModel<EmailTemplateGV>>.CreateSuccess(new GridViewModel<EmailTemplateGV>(list, count, count, pageIndex + 1, pageSize, null, 1));
            }
        }

        public static RespArgs<int> Create(RequestNewEmailTemplate req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                if (ExistEmail(req.Name)) throw new ArgumentException("email_template_already_exist");

                var newObj = new Emailtemplate
                {
                    Name = req.Name,
                    TemplateTypeId = req.TemplateTypeId,
                    Content = req.Content,
                    CreateDate = DateTime.Now,
                    StatusId = (int)GlobalStatus.ACTIVE,
                };
                db.Emailtemplates.AddObject(newObj);
                db.SaveChangesAsync();
                return RespArgs<int>.CreateSuccess(newObj.EmailTemplateId);
            }
        }

        public static RespArgs<bool> Update(RequestUpdateEmailTemplate req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var findEmailTemplate = db.Emailtemplates.FirstOrDefault(x => x.EmailTemplateId == req.EmailTemplateId);
                if (findEmailTemplate == null) throw new ArgumentException("email_template_not_found");

                var findDuplicate = db.Emailtemplates.FirstOrDefault(x => x.Name == req.Name);
                if(findDuplicate != null && findDuplicate.EmailTemplateId != req.EmailTemplateId) throw new ArgumentException("email_template_already_exist");

                findEmailTemplate.Name = req.Name;
                findEmailTemplate.StatusId = req.StatusId;
                findEmailTemplate.Content = req.Content;
                findEmailTemplate.TemplateTypeId = req.TemplateTypeId;
                db.SaveChangesAsync();
                return RespArgs<bool>.CreateSuccess(true);
            }
        }

        public static RespArgs<bool> Delete(int id)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var findEmailTemplate = db.Emailtemplates.FirstOrDefault(x => x.EmailTemplateId == id);
                if (findEmailTemplate == null) throw new ArgumentException("email_template_not_found");

                db.Emailtemplates.DeleteObject(findEmailTemplate);
                db.SaveChanges();
                return RespArgs<bool>.CreateSuccess(true);
            }
        }

        public static List<DropdownItem> GetAllDropdown()
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Emailtemplates.OrderBy(x => x.Name).AsEnumerable().Select(s => new DropdownItem(s.EmailTemplateId.ToString(), s.Name)).ToList();
            }
        }
    }
}
