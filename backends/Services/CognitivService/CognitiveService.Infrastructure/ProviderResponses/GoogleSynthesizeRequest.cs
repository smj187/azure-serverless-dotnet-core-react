using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Infrastructure.ProviderResponses
{
    // https://cloud.google.com/text-to-speech/docs/reference/rest/v1/text/synthesize#SynthesisInput
    public class GoogleSynthesizeRequest
    {
        public GoogleSynthesizeInput input { get; set; }
        public GoogleSynthesizeVoice voice { get; set; }
        public GoogleSynthesizeAudioConfig audioConfig { get; set; }
        public List<string>? enableTimePointing { get; set; }

    }

    public class GoogleSynthesizeInput
    {
        public string? text { get; set; }
        public string? ssml { get; set; }
    }

    public class GoogleSynthesizeVoice
    {
        public string languageCode { get; set; }
        public string name { get; set; }
        public string ssmlGender { get; set; }
    }

    public class GoogleSynthesizeAudioConfig
    {
        public string audioEncoding { get; set; }
        // different speaking speed [0,25 - 4,0, 1,0 = default]
        public int? speakingRate { get; set; }
        // [-20,0 - 20,0]
        public int? pitch { get; set; }
        // volumen [-96,0 - 16,0, 0,0 = default]
        public int? volumeGainDb { get; set; }
        public int? sampleRateHertz { get; set; }

        // https://cloud.google.com/text-to-speech/docs/audio-profiles
        public string? effectsProfileId { get; set; }
    }

}
