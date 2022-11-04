using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using CognitiveService.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services
{
    public class AmazonPollyService : IAmazonPollyService
    {
        private readonly AmazonPollyClient _polly;

        public AmazonPollyService()
        {
            var credentials = new BasicAWSCredentials("AKIATBG2M4KPIDGN7AEY", "/UqrLlp8afVSY/eboZgvArjenLdwcLiqV2IHHyRX");
            var polly = new AmazonPollyClient(credentials, RegionEndpoint.EUWest3);
            _polly = polly;
        }

        public async Task<DescribeVoicesResponse> DiscoverAmazonVoicesAsync()
        {
            DescribeVoicesResponse response = await _polly.DescribeVoicesAsync(new DescribeVoicesRequest());

            return response;
        }

        public async Task<Stream> TTS(string language, string voice, string engine, string? text = null, string? ssml = null)
        {
            var isSsml = text == null;
            var content = isSsml ? $@"
                <speak>
                    {ssml}
                </speak>
            " : $@"
                <speak>
                    {text}
                </speak>
            ";


            SynthesizeSpeechResponse response = await _polly.SynthesizeSpeechAsync(new SynthesizeSpeechRequest
            {
                LanguageCode = new LanguageCode(language),
                OutputFormat = OutputFormat.Ogg_vorbis,
                SampleRate = "24000",
                Text = content,
                TextType = isSsml ? TextType.Ssml : TextType.Text,
                VoiceId = new VoiceId(voice),
                Engine = new Engine(engine),
            });

            return response.AudioStream;
        }
    }
}
