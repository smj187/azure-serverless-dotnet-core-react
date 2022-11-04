using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services.Models
{
    public class GoogleSpeechSynthesisRequest
    {
        [JsonPropertyName("input")]
        public GoogleSpeechSynthesizeInput Input { get; set; }

        [JsonPropertyName("voice")]
        public GoogleSpeechSynthesizeVoice Voice { get; set; }

        [JsonPropertyName("audioConfig")]
        public GoogleSpeechSynthesizeAudioConfig AudioConfig { get; set; }

        [JsonPropertyName("enableTimePointing")]
        public List<string>? EnableTimePointing { get; set; }

        public GoogleSpeechSynthesisRequest(string locale, string voiceName, string gender, string? text = null, string? ssml = null)
        {
            AudioConfig = new GoogleSpeechSynthesizeAudioConfig
            {
                AudioEncoding = "ogg_opus"
            };

            Voice = new GoogleSpeechSynthesizeVoice(locale, voiceName, gender);
            Input = new GoogleSpeechSynthesizeInput(text, ssml);
        }
    }

    public class GoogleSpeechSynthesizeVoice
    {
        [JsonPropertyName("languageCode")]
        public string LanguageCode { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("ssmlGender")]
        public string SsmlGender { get; set; }

        public GoogleSpeechSynthesizeVoice(string languageCode, string name, string ssmlGender)
        {
            LanguageCode = languageCode;
            Name = name;
            SsmlGender = ssmlGender;
        }
    }

    public class GoogleSpeechSynthesizeInput
    {
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("ssml")]
        public string? Ssml { get; set; }

        public GoogleSpeechSynthesizeInput(string? text = null,string? ssml = null)
        {
            if (text == null && ssml == null)
            {
                throw new NotImplementedException("pls provide text or ssml");
            }

            if (text == null)
            {
                Ssml = ssml;
            }
            else
            {
                Text = text;
            }
        }
    }

    public class GoogleSpeechSynthesizeAudioConfig
    {
        [JsonPropertyName("audioEncoding")]
        public string AudioEncoding { get; set; }

        // different speaking speed [0,25 - 4,0, 1,0 = default]
        [JsonPropertyName("speakingRate")]
        public int? SpeakingRate { get; set; }

        // [-20,0 - 20,0]
        [JsonPropertyName("pitch")]
        public int? Pitch { get; set; }

        // volumen [-96,0 - 16,0, 0,0 = default]
        [JsonPropertyName("volumeGainDb")]
        public int? VolumeGainDb { get; set; }

        [JsonPropertyName("sampleRateHertz")]
        public int? SampleRateHertz { get; set; }

        // https://cloud.google.com/text-to-speech/docs/audio-profiles
        [JsonPropertyName("effectsProfileId")]
        public string? EffectsProfileId { get; set; }
    }
}
