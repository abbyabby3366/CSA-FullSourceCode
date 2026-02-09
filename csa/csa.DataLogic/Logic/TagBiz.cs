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
    public static class TagBiz
    {
        public static Tag Get(int tagId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Tags.FirstOrDefault(x => x.TagId == tagId);
            }
        }

        public static Tag Get(string label)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Tags.FirstOrDefault(x => x.Label == label);
            }
        }
        public static bool ExistTag(string label)
        {
            return Get(label) != null;
        }
        public static RespArgs<GridViewModel<TagGV>> GetGV(string search, int pageIndex, int pageSize, string sortOrder, SQLSelect.OrderByEnum sortDirection)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var sqlSelect = new SQLSelect("tag t");
                sqlSelect.AddSelect("t.TagId,t.Label,t.StatusId,t.CreateDate");
                sqlSelect.SetOrderBY(sortOrder, sortDirection);
                sqlSelect.SetLimit((pageIndex * pageSize), pageSize);

                if (!search.IsEmpty()) sqlSelect.AddWhere($"t.Label LIKE '%{search}%'");

                var list = db.ExecuteStoreQuery<TagGV>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                //add sequence
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].SequenceId = (pageIndex * pageSize) + i;
                }

                return RespArgs<GridViewModel<TagGV>>.CreateSuccess(new GridViewModel<TagGV>(list, count, count, pageIndex + 1, pageSize, null, 1));
            }
        }

        public static RespArgs<int> CreateTag(RequestNewTag req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                if (ExistTag(req.Label)) throw new ArgumentException("tag_already_exist");

                var newTag = new Tag
                {
                    Label = req.Label,
                    CreateDate = DateTime.Now,
                    StatusID = (int)GlobalStatus.ACTIVE
                };
                db.Tags.AddObject(newTag);
                db.SaveChangesAsync();
                return RespArgs<int>.CreateSuccess(newTag.TagId);
            }
        }

        public static RespArgs<bool> UpdateTag(RequestUpdateTag req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var findTag = db.Tags.FirstOrDefault(x => x.TagId == req.TagId);
                if (findTag == null) throw new ArgumentException("tag_not_found");

                var findDuplicate = db.Tags.FirstOrDefault(x => x.Label == req.Label);
                if (findDuplicate != null && findDuplicate.TagId != req.TagId) throw new ArgumentException("tag_already_exist");

                findTag.Label = req.Label;
                findTag.StatusID = req.StatusId;
                db.SaveChangesAsync();
                return RespArgs<bool>.CreateSuccess(true);
            }
        }

        public static RespArgs<bool> DeleteTag(RequestDeleteTag req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var findTag = db.Tags.FirstOrDefault(x => x.TagId == req.TagId);
                if (findTag == null) throw new ArgumentException("tag_not_found");

                var admin = AdminBiz.Get(req.AdminId);
                if(admin == null) throw new ArgumentException("access_failed");

                db.Tags.DeleteObject(findTag);
                db.SaveChanges();
                return RespArgs<bool>.CreateSuccess(true);
            }
        }
    }
}
