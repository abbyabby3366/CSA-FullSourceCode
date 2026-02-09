using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using csa.Model;
using csa.Model.DataObject;

namespace csa.Member.Helpers
{
    public static class SessionManager
    {
        private static LoginMember _loginMember
        {
            get
            {
                return HttpContext.Current.Session["LoginMember"] as LoginMember;
            }
            set
            {
                HttpContext.Current.Session["LoginMember"] = value;
            }
        }

        private static ResponseNewMemberSurvey _accountMemberSurvey
        {
            get
            {
                return HttpContext.Current.Session["AccountMemberSurvey"] as ResponseNewMemberSurvey;
            }
            set
            {
                HttpContext.Current.Session["AccountMemberSurvey"] = value;
            }
        }

        public static LoginMember SetLoginMember(LoginMember loginMember) => _loginMember = loginMember;
        public static ResponseNewMemberSurvey SetAccountMemberSurvey(ResponseNewMemberSurvey accountSurvey) => _accountMemberSurvey = accountSurvey;
        public static LoginMember CurrentLoginMember => _loginMember;
        public static ResponseNewMemberSurvey AccountMemberSurvey => _accountMemberSurvey;
    }
}