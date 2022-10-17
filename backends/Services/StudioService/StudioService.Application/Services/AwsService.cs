using Amazon;
using Amazon.Auth.AccessControlPolicy;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Application.Services
{
    public class AwsService : IAwsService
    {
        private readonly IBlobService _blobService;
        private readonly AmazonPollyClient _client;

        public AwsService(IBlobService blobService)
        {
            _blobService = blobService;

            var credentials = new BasicAWSCredentials("AKIATBG2M4KPIDGN7AEY", "/UqrLlp8afVSY/eboZgvArjenLdwcLiqV2IHHyRX");

            var polly = new AmazonPollyClient(credentials, RegionEndpoint.EUWest3);
            _client = polly;
        }

        public async Task<string> GernateAudio(string value)
        {

            var withSaml = $@"
                <speak>
                    We&apos;re using the lawyer at Peabody &amp; Chambers, attorneys-at-law.
                </speak>
            ";

            var res = await _client.SynthesizeSpeechAsync(new Amazon.Polly.Model.SynthesizeSpeechRequest
            {
                LanguageCode = "en-US",
                OutputFormat = OutputFormat.Ogg_vorbis,
                SampleRate = "24000",
                Text = withSaml,
                TextType = TextType.Ssml,
                VoiceId = VoiceId.Matthew,
                Engine = Engine.Neural,
            });


          

            var name = DateTimeOffset.UtcNow.ToString("aws_yy-MM-dd-HH-mm-ss").Replace("/", "_") + ".mp3";

            var url = await _blobService.UploadFileAsStream("files", name, res.AudioStream, res.ContentType);

        

            return url;
        }
    }
}
