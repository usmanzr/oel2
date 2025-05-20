using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace oel2
{
    public class FieldResult
    {
        public string FieldName { get; set; }
        public string Status { get; set; }
    }

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class CriminalCaseService : System.Web.Services.WebService
    {
        private static readonly List<string> SensitivePatterns = new List<string>
        {
            "INF-", "BADGE-", "CASE-SECRET-", "123 Confidential St"
        };

        [WebMethod]
        public List<FieldResult> CheckSensitiveData(string caseNumber, string informantID, string badgeID, string address)
        {
            return new List<FieldResult>
            {
                new FieldResult { FieldName = "CaseNumber", Status = IsSensitive(caseNumber) ? "Sensitive" : "Safe" },
                new FieldResult { FieldName = "InformantID", Status = IsSensitive(informantID) ? "Sensitive" : "Safe" },
                new FieldResult { FieldName = "BadgeID", Status = IsSensitive(badgeID) ? "Sensitive" : "Safe" },
                new FieldResult { FieldName = "Address", Status = IsSensitive(address) ? "Sensitive" : "Safe" }
            };
        }

        private bool IsSensitive(string input)
        {
            foreach (var pattern in SensitivePatterns)
            {
                if (input.StartsWith(pattern))
                    return true;
            }
            return false;
        }
    }
}
