using System;
using System.Collections.Generic;

namespace csa.Member.Models
{
    [Serializable]
    public class GridViewDataModel
    {
        public int draw { set; get; }
        public List<GridViewDataColumnModel> columns { get; set; }
        public int start { set; get; }
        public int length { set; get; }
        public GridViewDataOrderModel order { get; set; }
        public GridViewDataSearchModel search { get; set; }
    }

    [Serializable]
    public class GridViewDataColumnModel
    {
        public string data { get; set; }
        //public string name { get; set; }
        public string type { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public GridViewDataSearchModel search { get; set; }

        public GridViewDataColumnModel()
        {
            search = new GridViewDataSearchModel();
        }
    }

    [Serializable]
    public class GridViewDataOrderModel
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

    [Serializable]
    public class GridViewDataSearchModel
    {
        public string value { get; set; }
        public bool regex { get; set; }
    }

    //================================================================================================

    //[Serializable]
    //public class GridViewDataMultiSearchModel
    //{
    //    public string column { get; set; }
    //    public string value { get; set; }
    //    public bool regex { get; set; }
    //}
}