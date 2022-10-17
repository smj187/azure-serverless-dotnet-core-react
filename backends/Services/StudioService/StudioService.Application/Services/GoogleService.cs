using RestSharp;
using StudioService.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudioService.Application.Services
{
    public class GoogleService : IGoogleService
    {
        private readonly string _apiKey = "AIzaSyA0uxOkaOxNaTQUNRUTmgnir-IPpQk3gJk";
        private readonly IBlobService _blobService;
        private readonly RestClient _client;

        public GoogleService(IBlobService blobService)
        {
            _blobService = blobService;


            var client = new RestClient($"https://texttospeech.googleapis.com");
            //client.AddDefaultHeader("Ocp-Apim-Subscription-Key", KEY);
            //client.AddDefaultHeader("User-Agent", "Backend");
            client.AddDefaultParameter("key", _apiKey);

            _client = client;
        }

        public async Task<string> GernateAudio(string value)
        {

            var withBreak = $@"
                <speak>
                        After further delays<break time=""1000ms"" />, the countdown began on January 21, 1968, with launch the following day.
                </speak>";

            var withMark = $@"
                <speak>
                      Go from <mark name=""here""/> here, to <mark name=""there""/> there!
                </speak>";

            // does not support time pointing with neural2 voices
            var req = new GoogleSynthesizeRequest
            {
                input = new GoogleSynthesizeInput
                {
                    ssml = withMark,
                    //text = "After further delays, the countdown began on January 21, 1968, with launch the following day."
                },
                voice = new GoogleSynthesizeVoice
                {
                    languageCode = "en-gb",
                    name = "en-GB-Neural2-A",
                    ssmlGender = "FEMALE"
                },
                audioConfig = new GoogleSynthesizeAudioConfig
                {
                    audioEncoding = "OGG_OPUS"
                },
                enableTimePointing = new List<string>
                {
                    "SSML_MARK"
                }
            };

            var json = JsonSerializer.Serialize(req);
            var request = new RestRequest("v1beta1/text:synthesize", Method.Post)
                .AddBody(json)
                .AddQueryParameter("key", _apiKey);

            var res = await _client.PostAsync<GoogleSynthesizeResponseModel>(request);

            var stream = Convert.FromBase64String(res.audioContent);

            var name = DateTimeOffset.UtcNow.ToString("yy-MM-dd-HH-mm-ss").Replace("/", "_") + ".mp3";

            var url = await _blobService.UploadAudio("files", name, stream);

            return url;
        }

        public async Task<GoogleVoiceModel> ListGoogleVoicesAsync()
        {
            var url = "https://texttospeech.googleapis.com/v1/voices";

            var request = new RestRequest("v1beta1/voices");

            var res = await _client.GetAsync<GoogleVoiceModel>(request);
            return res;
        }
    }
}
