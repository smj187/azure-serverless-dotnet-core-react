using CognitiveService.Application.Services.Interfaces;
using CognitiveService.Infrastructure.ProviderResponses;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services
{
    public class GoogleService : IGoogleService
    {
        private readonly RestClient _client;

        public GoogleService(IConfiguration configuration)
        {
            var client = new RestClient($"https://texttospeech.googleapis.com");
            client.AddDefaultParameter("key", configuration.GetValue<string>("ExternalServices:GoogleTTS:ApiKey"));

            _client = client;
        }

        public async Task<Stream> TTS(string value, string language, string voice, string gender)
        {
            var text = $@"
                <speak>
                    {value}
                </speak>";


            var config = new GoogleSynthesizeRequest
            {
                input = new GoogleSynthesizeInput
                {
                    ssml = text,
                },
                voice = new GoogleSynthesizeVoice
                {
                    languageCode = language,
                    name = voice,
                    ssmlGender = gender
                },
                audioConfig = new GoogleSynthesizeAudioConfig
                {
                    audioEncoding = "OGG_OPUS"
                }
            };

            var json = JsonSerializer.Serialize(config);

            var restRequest = new RestRequest("v1beta1/text:synthesize", Method.Post)
                .AddBody(json);

            var restResponse = await _client.PostAsync<GoogleSynthesizeResponseModel>(restRequest);
            if (restResponse == null)
            {
                throw new NotImplementedException();
            }

            var stream = new MemoryStream(Convert.FromBase64String(restResponse.audioContent));

            return stream;
        }
    }
}
