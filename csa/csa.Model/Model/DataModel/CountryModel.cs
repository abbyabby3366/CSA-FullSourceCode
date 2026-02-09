using System;

namespace csa.Model
{
    public class CountryModel
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Currency { get; set; }

        public string IsoNumeric { get; set; }

        public string IsoAlpha3 { get; set; }

        public string PhoneCode { get; set; }

        public int Status { get; set; }

        public int OrderSeq { get; set; }
    }

    //================================================================================================

    public class CountryGVModel
    {
        public string DT_RowId { get; set; }
        //public Guid CountryId { get; set; }

        //public string Code { get; set; }

        public string Name { get; set; }

        //public string Currency { get; set; }

        //public int? PhoneCode { get; set; }

        public int Status { get; set; }

        public int OrderSeq { get; set; }
    }
}