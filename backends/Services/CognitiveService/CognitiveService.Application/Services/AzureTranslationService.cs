using CognitiveService.Application.DTOs;
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
    public class AzureTranslationService : IAzureTranslationService
    {
        private readonly HttpClient _httpClient;

        public AzureTranslationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AzureTranslationResponse>> Translate(string locale, IEnumerable<TranslationRequestDTO> translations)
        {
            var request = new List<AzureTranslationRequest>();
            foreach (var translation in translations)
            {
                request.Add(new AzureTranslationRequest(translation.Text));
            }
            var body = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
           
            var httpResponseMessage = await _httpClient.PostAsync($"translate?api-version=3.0&to={locale}", body);
            httpResponseMessage.EnsureSuccessStatusCode();
            var response = JsonSerializer.Deserialize<List<AzureTranslationResponse>>(await httpResponseMessage.Content.ReadAsStringAsync());
            
            if (response == null)
            {
                throw new NotImplementedException("response is null");
            }

            return response;
        }
    }
}
