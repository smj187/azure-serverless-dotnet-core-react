using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services.Models
{
    public class GoogleVoiceResponse
    {
        [JsonPropertyName("voices")]
        public List<GoogleVoiceData> Voices { get; set; }
    }

    public class GoogleVoiceData
    {
        [JsonPropertyName("languageCodes")]
        public List<string> LanguageCodes { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("ssmlGender")]
        public string SsmlGender { get; set; }

        [JsonPropertyName("naturalSampleRateHertz")]
        public int NaturalSampleRateHertz { get; set; }
    }
}
