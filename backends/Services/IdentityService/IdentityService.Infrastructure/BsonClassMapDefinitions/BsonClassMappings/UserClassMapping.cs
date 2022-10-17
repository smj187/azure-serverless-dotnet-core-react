using IdentityService.Core.Domain;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Infrastructure.BsonClassMapDefinitions.BsonClassMappings
{
    public class UserClassMapping
    {
        public static void Apply()
        {
            BsonClassMap.RegisterClassMap<User>(x =>
            {
                x.MapProperty(x => x.B2cObjectId).SetElementName("b2c_object_id").SetIsRequired(true);
                x.MapProperty(x => x.AccountTier).SetElementName("account_tier").SetIsRequired(true);
                x.MapProperty(x => x.HistoryStartDate).SetElementName("credit_history_start_date").SetIsRequired(true);
                x.MapProperty(x => x.HistoryResetDate).SetElementName("credit_history_reset_date").SetIsRequired(true);
                x.MapProperty(x => x.CreditHistory).SetElementName("credit_history").SetIsRequired(true);
                //x.MapProperty(x => x.TotalAvailableCredits).SetElementName("TotalAvailableCredits").SetIsRequired(true);
                //x.MapProperty(x => x.RemainingAvailableCredits).SetElementName("RemainingAvailableCredits").SetIsRequired(true);
            });

            BsonClassMap.RegisterClassMap<Credit>(x =>
            {
                x.MapProperty(x => x.Date).SetElementName("Date").SetIsRequired(true);
                x.MapProperty(x => x.Entries).SetElementName("Entries").SetIsRequired(true);
            });
            
            BsonClassMap.RegisterClassMap<Entry>(x =>
            {
                x.MapProperty(x => x.Type).SetElementName("Type").SetIsRequired(true);
                x.MapProperty(x => x.Value).SetElementName("Value").SetIsRequired(true);
                x.MapProperty(x => x.Credits).SetElementName("Credits").SetIsRequired(true);
                x.MapProperty(x => x.Time).SetElementName("Time").SetIsRequired(true);
            });

        }
    }
}
