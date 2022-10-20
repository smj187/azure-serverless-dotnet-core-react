using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Infrastructure.ProviderResponses
{
    public sealed class AwsVoiceResponse
    {
        public AwsVoiceResponse(string voiceId, string name, string gender, string languageCode, List<string> engines, string languageName, List<string> languageCodes, List<string> specialStyles)
        {
            VoiceId = voiceId;
            Name = name;
            Gender = gender;
            LanguageCode = languageCode;
            Engines = engines;
            LanguageName = languageName;
            LanguageCodes = languageCodes;
            SpecialStyles = specialStyles;
        }

        public string VoiceId { get; init; }
        public string Name { get; init; }
        public string Gender { get; init; }
        public string LanguageCode { get; init; }
        public string LanguageName { get; init; }
        public List<string> Engines { get; init; }
        public List<string> LanguageCodes { get; init; }
        public List<string> SpecialStyles { get; init; }
    }
}
