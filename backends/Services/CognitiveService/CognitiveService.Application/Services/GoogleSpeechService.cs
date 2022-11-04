using CognitiveService.Application.Services.Interfaces;
using CognitiveService.Application.Services.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services
{
    public class GoogleSpeechService : IGoogleSpeechService
    {
        private readonly HttpClient _httpClient;

        public GoogleSpeechService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GoogleVoiceResponse> DiscoverGoogleVoicesAsync()
        {
            var httpResponseMessage = await _httpClient.GetAsync("v1beta1/voices?key=AIzaSyA0uxOkaOxNaTQUNRUTmgnir-IPpQk3gJk");
            httpResponseMessage.EnsureSuccessStatusCode();
            var response = JsonSerializer.Deserialize<GoogleVoiceResponse>(await httpResponseMessage.Content.ReadAsStringAsync());

            if (response == null)
            {
                throw new NotImplementedException("response is null");
            }

            return response;
        }

        public async Task<Stream> TTS(string language, string voice, string gender, string? text, string? ssml)
        {
            var request = new GoogleSpeechSynthesisRequest(language, voice, gender, text, ssml);

            var body = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync("v1beta1/text:synthesize?key=AIzaSyA0uxOkaOxNaTQUNRUTmgnir-IPpQk3gJk", body);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonSerializer.Deserialize<GoogleSpeechSynthesisResponse>(await httpResponseMessage.Content.ReadAsStringAsync());
            if (response == null)
            {
                throw new NotImplementedException("response is null");
            }

            return new MemoryStream(Convert.FromBase64String(response.AudioContent));

        }
    }
}
