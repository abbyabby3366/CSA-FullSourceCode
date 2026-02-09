using System;

using csa.Data.EntityModel;

namespace csa.Model
{
    public class AddressModel
    {
        public Guid Id { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Postcode { get; set; }

        public state State { get; set; }

        public string StateStr { get; set; }

        public country Country { get; set; }
    }

    public class AddressDisplayModel
    {
        public Guid Id { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Postcode { get; set; }

        public IdValueModel State { get; set; }

        public string StateStr { get; set; }

        public IdValueModel Country { get; set; }
    }

    //----------------------------------------------

    public class AddEditAddressBaseModel
    {
        public string Address { get; set; }

        public string City { get; set; }

        public string Postcode { get; set; }

        public Guid? State { get; set; }

        public string StateStr { get; set; }

        public Guid Country { get; set; }
    }

    public class EditAddressModel : AddEditAddressBaseModel
    {
        public Guid Id { get; set; }
    }

    public class AddAddressModel : AddEditAddressBaseModel
    {

    }
}