using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Contracts.Responses
{
    public class CreditEntryResponse
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public int Credits { get; set; }
        public DateTimeOffset Time { get; set; }
    }
    public class CreditResponse
    {
        public DateTimeOffset Date { get; set; }
        public int Usage { get; set; }
        public IEnumerable<CreditEntryResponse> Credits { get; set; }
    }

    public class UserProfileResponse
    {
        public Guid UserId { get; set; }
        public string AccountTier { get; set; }
        public DateTimeOffset CreditHistoryStartDate { get; set; }
        public DateTimeOffset CreditHistoryResetDate { get; set; }
        public IEnumerable<CreditResponse> CreditHistory { get; set; }

        public int TotalAvailableCredits { get; set; }
        public int RemainingAvailableCredits { get; set; }

    }
}
