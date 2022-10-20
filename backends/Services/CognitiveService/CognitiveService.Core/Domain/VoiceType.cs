using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Core.Domain
{
    public class VoiceType : Enumeration
    {
        public static readonly VoiceType Legacy = new(0, "Legacy");
        public static readonly VoiceType Premium = new(1, "Premium");

        public VoiceType(int value, string description)
            : base(value, description)
        {

        }

        public static VoiceType CreateLegacyVoiceType() => new(Legacy.Value, Legacy.Description);
        public static VoiceType CreatePremiumVoiceType() => new(Premium.Value, Premium.Description);

        public static VoiceType CreateFromValue(int value)
        {
            if (value == Legacy.Value) return CreateLegacyVoiceType();
            if (value == Premium.Value) return CreatePremiumVoiceType();

            throw new NotImplementedException();
        }
    }
}
