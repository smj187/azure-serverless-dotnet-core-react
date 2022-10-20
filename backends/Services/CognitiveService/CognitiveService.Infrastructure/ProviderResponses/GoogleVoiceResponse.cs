using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Infrastructure.ProviderResponses
{
    public class GoogleVoiceResponse
    {
        public GoogleVoiceResponse(List<string> languageCodes, string name, string ssmlGender, int naturalSampleRateHertz)
        {
            LanguageCodes = languageCodes;
            Name = name;
            SsmlGender = ssmlGender;
            NaturalSampleRateHertz = naturalSampleRateHertz;
        }

        public List<string> LanguageCodes { get; init; }
        public string Name { get; init; }
        public string SsmlGender { get; init; }
        public int NaturalSampleRateHertz { get; init; }
    }

    public class GoogleVoicesResponse
    {
        public List<GoogleVoiceResponse> Voices { get; set; } = new();
    }
}
