using CsaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.DataLogic
{
    public static class SurveyBiz
    {
        public static Survey Get(long surveyId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                db.ContextOptions.LazyLoadingEnabled = false;
                return db.Surveys.FirstOrDefault(x => x.SurveyId == surveyId );
            }
        }
        public static Survey Get(long memberId, int surveyTypeId)
        {
            using(CsaEntities db = new CsaEntities())
            {
                db.ContextOptions.LazyLoadingEnabled = false;
                return db.Surveys.FirstOrDefault(x => x.MemberId == memberId && x.SurveyVersionId == surveyTypeId);
            }
        }

        public static bool ExistSurvey(long memberId, int surveyTypeId,out long? surveyId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var survey = db.Surveys.FirstOrDefault(x => x.MemberId == memberId && x.SurveyVersionId == surveyTypeId);
                surveyId = survey?.SurveyId;
                return survey != null;
            }
        }
    }
}
