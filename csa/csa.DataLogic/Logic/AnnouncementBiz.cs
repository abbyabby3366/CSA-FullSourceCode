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
    public static class AnnouncementBiz
    {
        public static RespArgs<GridViewModel<DashboardAnnouncementByMember>> GetGVByMember(DateTime date)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var sqlSelect = new SQLSelect("announcement ann");
                sqlSelect.AddSelect("ann.Title,ann.Content");
                sqlSelect.SetOrderBY("ann.ArticleDate", SQLSelect.OrderByEnum.DESC);
                sqlSelect.AddWhere($"ann.StatusId={(int)GlobalStatus.ACTIVE}");
                sqlSelect.AddWhere($@"(
        (ann.StartDate IS NOT NULL AND ann.EndDate IS NOT NULL AND ann.StartDate >= '{date.ToString("yyyy-MM-dd")}' AND ann.EndDate < '{date.AddDays(1).ToString("yyyy-MM-dd")}')
        OR (ann.StartDate IS NOT NULL AND ann.EndDate IS NULL AND ann.StartDate >= '{date.ToString("yyyy-MM-dd")}')
        OR (ann.StartDate IS NULL AND ann.EndDate IS NOT NULL AND ann.EndDate < '{date.AddDays(1).ToString("yyyy-MM-dd")}')
    )");


                var list = db.ExecuteStoreQuery<DashboardAnnouncementByMember>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                return RespArgs<GridViewModel<DashboardAnnouncementByMember>>.CreateSuccess(new GridViewModel<DashboardAnnouncementByMember>(list, count, count, 1, int.MaxValue, null, 1));
            }
        }
    }
}
