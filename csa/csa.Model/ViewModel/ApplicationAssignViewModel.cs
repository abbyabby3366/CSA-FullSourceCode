using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.ViewModel
{
    public class ApplicationAssignViewModel
    {
        public ApplicationAssignViewModel(long applicationId, ApplicationDetailsInfo info, ValueText<long> referrerMemberId, int? pFCAdminId, int? aMAdminId, int? uMAdminId, int? rMAdminId, int? pAAdminId, List<ValueText<int>> creditTeams, List<ValueText<int>> salesDirector, List<ValueText<int>> rM, List<ValueText<int>> pA, int? preparedAdminId, int? analyzedAdminId)
        {
            ApplicationId = applicationId;
            Info = info;
            ReferrerMemberId = referrerMemberId;
            PFCAdminId = pFCAdminId;
            AMAdminId = aMAdminId;
            UMAdminId = uMAdminId;
            RMAdminId = rMAdminId;
            PAAdminId = pAAdminId;
            CreditTeams = creditTeams;
            SalesDirector = salesDirector;
            RM = rM;
            PA = pA;
            PreparedAdminId = preparedAdminId;
            AnalyzedAdminId = analyzedAdminId;
        }
        public long ApplicationId { get; set; }
        public ApplicationDetailsInfo Info { get; set; }
        public List<ValueText<int>> CreditTeams { get; set; }        
        public List<ValueText<int>> SalesDirector { get; set; }        
        public List<ValueText<int>> RM { get; set; }        
        public List<ValueText<int>> PA { get; set; }        
        public ValueText<long> ReferrerMemberId { get; set; }
        public int? PFCAdminId { get; set; }
        public int? AMAdminId { get; set; }
        public int? UMAdminId { get; set; }
        public int? RMAdminId { get; set; }
        public int? PAAdminId { get; set; }
        public int? PreparedAdminId { get; set; }
        public int? AnalyzedAdminId { get; set; }
    }
}
