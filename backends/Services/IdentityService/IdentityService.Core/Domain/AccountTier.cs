using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Core.Domain
{
    public class AccountTier : Enumeration
    {
        public static readonly AccountTier T0_Free = new(0, "t0-free");
        public static readonly AccountTier T1_Basic = new(1, "t0-basic");
        public static readonly AccountTier T2_Premium = new(2, "t0-premium");

        public AccountTier(int value, string description)
            : base(value, description)
        {

        }

        public bool IsFreeTier() => Value == T0_Free.Value;
        public bool IsBasicTier() => Value == T1_Basic.Value;
        public bool IsPremiumTier() => Value == T2_Premium.Value;

        public static AccountTier CreateFreeTier() => new(T0_Free.Value, T0_Free.Description);
        public static AccountTier CreateBasicTier() => new(T1_Basic.Value, T1_Basic.Description);
        public static AccountTier CreatePremiumTier() => new(T2_Premium.Value, T2_Premium.Description);

        public static AccountTier CreateFromValue(int value)
        {
            if (value == T0_Free.Value)
            {
                return new(T0_Free.Value, T0_Free.Description);
            }

            if (value == T1_Basic.Value)
            {
                return new(T1_Basic.Value, T1_Basic.Description);
            }

            if (value == T2_Premium.Value)
            {
                return new(T2_Premium.Value, T2_Premium.Description);
            }

            throw new NotImplementedException();
        }

        public int GetAvailableCredits()
        {
            if (IsFreeTier())
            {
                return 1000;
            }

            if (IsBasicTier())
            {
                return 10000;
            }

            if (IsPremiumTier())
            {
                return 50000;
            }

            throw new NotImplementedException();
        }

        public int GetRemainingCredits(int usedCredits)
        {
            if (IsFreeTier())
            {
                return 1000 - usedCredits;
            }

            if (IsBasicTier())
            {
                return 10000 - usedCredits;
            }

            if (IsPremiumTier())
            {
                return 50000 - usedCredits;
            }

            throw new NotImplementedException();
        }
    }
}
