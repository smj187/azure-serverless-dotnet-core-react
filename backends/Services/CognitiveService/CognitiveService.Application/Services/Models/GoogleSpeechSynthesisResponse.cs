using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services.Models
{
    public class GoogleSpeechSynthesisResponse
    {
        [JsonPropertyName("audioContent")]
        public string AudioContent { get; set; }

        [JsonPropertyName("timepoints")]
        public List<object> Timepoints { get; set; }

        [JsonPropertyName("audioConfig")]
        public GoogleSpeechAudioConfigResponse AudioConfig { get; set; }
    }

    public class GoogleSpeechAudioConfigResponse
    {
        [JsonPropertyName("audioEncoding")]
        public string AudioEncoding { get; set; }

        [JsonPropertyName("speakingRate")]
        public int SpeakingRate { get; set; }

        [JsonPropertyName("pitch")]
        public int Pitch { get; set; }

        [JsonPropertyName("volumeGainDb")]
        public int VolumeGainDb { get; set; }

        [JsonPropertyName("sampleRateHertz")]
        public int SampleRateHertz { get; set; }

        [JsonPropertyName("effectsProfileId")]
        public List<object> EffectsProfileId { get; set; }
    }
}
