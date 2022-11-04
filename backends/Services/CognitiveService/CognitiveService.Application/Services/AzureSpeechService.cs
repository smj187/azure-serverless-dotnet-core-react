using Azure.Core;
using CognitiveService.Application.Services.Interfaces;
using CognitiveService.Application.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services
{
    public class AzureSpeechService : IAzureSpeechService
    {
        private readonly HttpClient _httpClient;

        public AzureSpeechService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<AzureSpeechVoiceResponse>> DiscoverAzureVoicesAsync()
        {
            var httpResponseMessage = await _httpClient.GetAsync("cognitiveservices/voices/list");
            httpResponseMessage.EnsureSuccessStatusCode();
            var response = JsonSerializer.Deserialize<IReadOnlyCollection<AzureSpeechVoiceResponse>>(await httpResponseMessage.Content.ReadAsStringAsync());

            if (response == null)
            {
                throw new NotImplementedException("response is null");
            }

            return response;
        }

        public async Task<Stream> TTS(string language, string voice, string gender, string? text = null, string? ssml = null)
        {
            var content = text != null ? text : ssml;

            var request = $@"
                    <speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xmlns:mstts='https://www.w3.org/2001/mstts' xml:lang='{language}'>
                        <voice xml:lang='{language}' xml:gender='{gender}' name='{voice}'>
                            {content}
                        </voice>
                    </speak>
            ";

            var body = new StringContent(request, Encoding.UTF8, "application/ssml+xml");

            var httpResponseMessage = await _httpClient.PostAsync("cognitiveservices/v1", body);
            httpResponseMessage.EnsureSuccessStatusCode();
            var response = await httpResponseMessage.Content.ReadAsStreamAsync();

            return response;
        }
    }
}
