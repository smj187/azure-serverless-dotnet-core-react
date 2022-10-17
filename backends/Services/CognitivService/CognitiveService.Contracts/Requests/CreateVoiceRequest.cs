using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Contracts.Requests
{
    public class CreateVoiceAwsConfigRequest
    {
        public List<string> SpecialStyles { get; set; }
        public List<string> Engines { get; set; }
    }

    public class CreateVoiceGoogleConfigRequest
    {
        public List<string> LanguageCodes { get; set; }
        public int NaturalSampleRateHertz { get; set; }
    }

    public class CreateAzureConfigRequest
    {
        public int SampleRateHertz { get; set; }
        public string VoiceType { get; set; }
        public int WordsPerMinute { get; set; }
        public List<string>? StyleList { get; set; }
        public List<string>? RolePlayList { get; set; }
        public bool IsHighQuality48K { get; set; }
    }

    public class CreateVoiceRequest
    {
        public string Provider { get; set; }

        public string DisplayName { get; set; }
        public string InternalName { get; set; } // important
        public string Gender { get; set; } // important
        public string Locale { get; set; } // important
        public string? AvatarUrl { get; set; } = null;

        public CreateVoiceAwsConfigRequest? AwsConfig { get; set; }
        public CreateVoiceGoogleConfigRequest? GoogleConfig { get; set; }
        public CreateAzureConfigRequest? AzureConfig { get; set; }
    }
}
