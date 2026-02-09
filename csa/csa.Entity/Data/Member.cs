using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsaModel
{
    partial class Member
    {
        public LoginMember Convert()
        {
            return new LoginMember(this.MemberId,this.MemberCode,this.FirstName,this.LastName,this.PhoneNumber,this.ProfileFileId);
        }

        public string FullName => $"{this.FirstName} {this.LastName}";
    }
}
