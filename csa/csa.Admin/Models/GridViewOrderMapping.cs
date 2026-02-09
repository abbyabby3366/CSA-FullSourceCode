using csa.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Admin.Models
{
    public class GridViewOrderMapping
    {
        private readonly List<GridViewOrderMappingObj> list = new List<GridViewOrderMappingObj>();
        public void Add(int index,string from,string to,bool isDefault = false, SQLSelect.OrderByEnum defaultSortDirection = SQLSelect.OrderByEnum.ASC)
        {
            list.Add(new GridViewOrderMappingObj(index,from,to,isDefault, defaultSortDirection));
        }

        public GridViewOrderMappingResult Find(GridViewDataOrderModel orderBy)
        {
            if(orderBy == null) return new GridViewOrderMappingResult(list.Find(x => x.IsDefault).To, list.Find(x => x.IsDefault).SortDirection);

            var find = list.Find(x => x.Index == orderBy.column);
            if(find == null)
            {
                return new GridViewOrderMappingResult(list.Find(x => x.IsDefault).To, orderBy.dir == "asc" ? SQLSelect.OrderByEnum.ASC : SQLSelect.OrderByEnum.DESC);
            }

            return new GridViewOrderMappingResult(find.To, orderBy.dir == "asc" ? SQLSelect.OrderByEnum.ASC : SQLSelect.OrderByEnum.DESC);
        }
    }

    public class GridViewOrderMappingObj
    {
        public GridViewOrderMappingObj(int index, string from, string to,bool isDefault, SQLSelect.OrderByEnum sortDirection)
        {
            Index = index;
            From = from;
            To = to;
            IsDefault = isDefault;
            SortDirection = sortDirection;
        }

        public int Index { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool IsDefault { get; set; }
        public SQLSelect.OrderByEnum SortDirection { get; set; }
    }

    public class GridViewOrderMappingResult
    {
        public GridViewOrderMappingResult(string sortOrder, SQLSelect.OrderByEnum sortDirection)
        {
            SortOrder = sortOrder;
            SortDirection = sortDirection;
        }

        public string SortOrder { get; set; }
        public SQLSelect.OrderByEnum SortDirection { get; set; }
    }
}
