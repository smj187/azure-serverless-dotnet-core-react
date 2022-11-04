using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services.Models
{
    public class AzureSpeechVoiceResponse
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("DisplayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("LocalName")]
        public string LocalName { get; set; }

        [JsonPropertyName("ShortName")]
        public string ShortName { get; set; }

        [JsonPropertyName("Gender")]
        public string Gender { get; set; }

        [JsonPropertyName("Locale")]
        public string Locale { get; set; }

        [JsonPropertyName("LocaleName")]
        public string LocaleName { get; set; }

        [JsonPropertyName("SampleRateHertz")]
        public string SampleRateHertz { get; set; }

        [JsonPropertyName("VoiceType")]
        public string VoiceType { get; set; }

        [JsonPropertyName("Status")]
        public string Status { get; set; }

        [JsonPropertyName("WordsPerMinute")]
        public string WordsPerMinute { get; set; }

        [JsonPropertyName("StyleList")]
        public List<string> StyleList { get; set; }

        [JsonPropertyName("SecondaryLocaleList")]
        public List<string> SecondaryLocaleList { get; set; }

        [JsonPropertyName("RolePlayList")]
        public List<string> RolePlayList { get; set; }
    }
}
