using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using BuildingBlocks.BlobStorage.Interfaces;
using CognitiveService.Application.Services.Interfaces;
using CognitiveService.Core;
using CognitiveService.Infrastructure.ProviderResponses;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CognitiveService.Application.Services
{
    public class AwsService : IAwsService
    {
        private readonly AmazonPollyClient _polly;

        public AwsService(IConfiguration configuration)
        {
            var credentials = new BasicAWSCredentials(configuration.GetValue<string>("ExternalServices:AwsTTS:AccessKey"), configuration.GetValue<string>("ExternalServices:AwsTTS:SecretKey"));

            var polly = new AmazonPollyClient(credentials, RegionEndpoint.EUWest3);
            _polly = polly;
        }


        public async Task<Stream> TTS(string value, string language, string voice, string engine)
        {
            var text = $@"
                <speak>
                    {value}
                </speak>
            ";

            var res = await _polly.SynthesizeSpeechAsync(new SynthesizeSpeechRequest
            {
                LanguageCode = new LanguageCode(language),
                OutputFormat = OutputFormat.Ogg_vorbis,
                SampleRate = "24000",
                Text = text,
                TextType = TextType.Ssml,
                VoiceId = new VoiceId(voice),
                Engine = new Engine(engine),
            });

            return res.AudioStream;

        }
    }
}
