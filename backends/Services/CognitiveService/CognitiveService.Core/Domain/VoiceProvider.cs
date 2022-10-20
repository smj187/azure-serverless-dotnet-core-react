using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Core.Domain
{
    public class VoiceProvider : Enumeration
    {
        public static readonly VoiceProvider Custom = new(0, "Custom");
        public static readonly VoiceProvider Aws = new(1, "Aws");
        public static readonly VoiceProvider Google = new(2, "Google");
        public static readonly VoiceProvider Azure = new(3, "Azure");

        public VoiceProvider(int value, string description)
            : base(value, description)
        {

        }

        public bool IsCustomProvider() => Value == Custom.Value;
        public bool IsAwsProvider() => Value == Aws.Value;
        public bool IsGoogleProvider() => Value == Google.Value;
        public bool IsAzureProvider() => Value == Azure.Value;


        public static VoiceProvider CreateCustomProvider() => new(Custom.Value, Custom.Description);
        public static VoiceProvider CreateAwsProvider() => new(Aws.Value, Aws.Description);
        public static VoiceProvider CreateGoogleProvider() => new(Google.Value, Google.Description);
        public static VoiceProvider CreateAzureProvider() => new(Azure.Value, Azure.Description);
    }
}
