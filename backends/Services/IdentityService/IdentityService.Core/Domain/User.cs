using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Core.Domain
{
    public class User : AggregateBase
    {
        private string _b2cObjectId;
        private AccountTier _accoutTier;
        private DateTimeOffset _historyStartDate;
        private DateTimeOffset _historyResetDate;
        private List<Credit> _creditHistory;

        public User(string b2cObjectId)
        {
            Id = Guid.NewGuid();
            ModifiedAt = null;
            CreatedAt = DateTimeOffset.UtcNow;

            _accoutTier = AccountTier.CreateFreeTier();

            _b2cObjectId = b2cObjectId;

            _historyStartDate = DateTimeOffset.UtcNow.Date;
            _historyResetDate = DateTimeOffset.UtcNow.Date.AddDays(30);
            _creditHistory = new List<Credit>();
        }

        public string B2cObjectId { get => _b2cObjectId; private set => _b2cObjectId = value; }
        public AccountTier AccountTier { get => _accoutTier; private set => _accoutTier = value; }


        public DateTimeOffset HistoryStartDate { get => _historyStartDate; private set => _historyStartDate = value; }
        public DateTimeOffset HistoryResetDate { get => _historyResetDate; private set => _historyResetDate = value; }
        public List<Credit> CreditHistory { get => _creditHistory; private set => _creditHistory = new List<Credit>(value); }


        public int TotalAvailableCredits
        {
            get => _accoutTier.GetAvailableCredits();
        }

        public int RemainingAvailableCredits
        {
            get
            {
                // find credits in current history duration (start - reset)
                var startDateIndex = CreditHistory.FindIndex(c => c.Date == HistoryStartDate.Date);
                var resetDateIndex = CreditHistory.FindIndex(c => c.Date == HistoryResetDate.Date);

                var credits = CreditHistory.Where(c => c.Date >= HistoryStartDate.Date && c.Date <= HistoryResetDate.Date);
                var usedCreditsSum = credits.Sum(x => x.GetUsage());

                return TotalAvailableCredits - usedCreditsSum;
            }
        }

        public DateTimeOffset LastActive
        {
            get
            {
                // if there was no action yet
                if (!CreditHistory.Any())
                {
                    return CreatedAt;
                }

                // get latest used credit
                return CreditHistory.FirstOrDefault().Entries.FirstOrDefault().Time;
            }
        }



        public void AddEntryToCredit(string type, string value, int credits = 1)
        {
            // find today
            var today = CreditHistory.FirstOrDefault(c => c.Date == DateTimeOffset.UtcNow.Date);

            // create today if it doesn't already exist
            if (today == null)
            {
                var newDay = new Credit();
                CreditHistory.Add(newDay);
                today = newDay;
            }

            // add new entry to credit
            today.PushCredit(type, value, credits);
        }

   


        public void ChangeAccountTier(int value)
        {
            _accoutTier = AccountTier.CreateFromValue(value);
        }
    }
}
