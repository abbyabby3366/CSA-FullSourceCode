using System;

namespace csa.Model
{
    public class UserModel
    {

    }

    //================================================================================================

    public class UserRegistrationModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNo { get; set; }

        public Guid? CountryId { get; set; }
    }

    public class CustomerRegistrationModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNo { get; set; }

        //agent

        public Guid? RefAgentId { get; set; }

        //address

        public string Address { get; set; }

        public string City { get; set; }

        public string Postcode { get; set; }

        public Guid? StateId { get; set; }

        public string State { get; set; }

        public Guid? CountryId { get; set; }

        public int Status { get; set; }
    }

    //================================================================================================

    public class UserGVModel
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        //add-on

        public DateTime? CreatedDateLocal
        {
            get
            {
                if (this.CreatedDate != null) { return this.CreatedDate.Value.ToLocalTime(); }
                else { return null; }
            }
        }
    }
}