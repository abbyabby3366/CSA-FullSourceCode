using System;
using System.Collections.Generic;

namespace csa.Model
{
    public class MiscModel
    {

    }

    //================================================================================================

    public class IdValueModel
    {
        public Guid Id { get; set; }

        public string Value { get; set; }
    }

    public class IdPairsModel
    {
        public Guid? Id1 { get; set; }

        public Guid? Id2 { get; set; }
    }

    public class IdDualStringModel
    {
        public Guid Id { get; set; }

        public string Value1 { get; set; }

        public string Value2 { get; set; }
    }

    public class IdIntValModel
    {
        public Guid Id { get; set; }

        public int Value { get; set; }
    }

    public class IdLongValModel
    {
        public Guid Id { get; set; }

        public long Value { get; set; }
    }

    public class IdListModel<T>
    {
        public Guid Id { get; set; }

        public List<T> ValList { get; set; }
    }

    public class DropDownListModel<T>
    {
        public T Id { get; set; }

        public string Name { get; set; }
    }

    //================================================================================================

    [Serializable]
    public class BaseModel
    {

    }

    [Serializable]
    public class GridViewModel<T> where T : new()
    {
        public GridViewModel()
        {
            data = new List<T>();
        }

        public GridViewModel(IEnumerable<T> data, int recordsTotal, int recordsFiltered, int pageIndex, int pageSize, string sortExpression, int sortDirection)
        {
            this.data = data;
            this.recordsTotal = recordsTotal;
            this.recordsFiltered = recordsFiltered;
            PageIndex = pageIndex;
            PageSize = pageSize;
            SortExpression = sortExpression;
            SortDirection = sortDirection;
        }

        public IEnumerable<T> data { get; set; }

        public int recordsTotal { get; set; }

        public int recordsFiltered { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string SortExpression { get; set; }

        public int SortDirection { get; set; }

        //public System.Linq.Expressions.Expression<Func<T, bool>> Predicate { get; set; }
    }

    public class ExportFileModel
    {
        public byte[] FileContent { get; set; }

        public string Filename { get; set; }
    }

    public class DownloadFileModel
    {
        public byte[] FileContent { get; set; }

        public string Filename { get; set; }
    }
}
