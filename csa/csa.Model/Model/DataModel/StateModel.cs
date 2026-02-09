using System;

namespace csa.Model
{
    public class StateModel
    {
        public Guid Id { get; set; }

        //public string Code { get; set; }

        public string Name { get; set; }

        public Guid CountryId { get; set; }
    }

    //================================================================================================

    public class StateGVModel
    {
        public string DT_RowId { get; set; }

        //public Guid StateId { get; set; }

        //public string Code { get; set; }

        public string Name { get; set; }

        public Guid CountryId { get; set; }

        public string CountryName { get; set; }

        public int OrderSeq { get; set; }
    }
}