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
    public class AzureLanguageService : IAzureLanguageService
    {
        private readonly HttpClient _httpClient;

        public AzureLanguageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AzureLanguageDetectionResponse> DetectLanguagesAsync(IEnumerable<LanguageDetectionDocumentRequestDTO> documents)
        {
            var request = new AzureLanguageDetectionRequest();
            foreach (var document in documents)
            {
                request.AddDocument(document.Id, document.Text);
            }

            var body = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync("language/:analyze-text?api-version=2022-05-01&showStats=true", body);
            httpResponseMessage.EnsureSuccessStatusCode();
            var response = JsonSerializer.Deserialize<AzureLanguageDetectionResponse>(await httpResponseMessage.Content.ReadAsStringAsync());

            if (response == null)
            {
                throw new NotImplementedException("response is null");
            }

            return response;
        }

        public async Task<AzureKeyPhraseExtractionResponse> ExtractKeyPhrasesAsync(IEnumerable<KeyPhraseExtractionDocumentRequestDTO> documents)
        {
            var request = new AzureKeyPhraseExtractionRequest();
            foreach (var document in documents)
            {
                request.AddDocument(document.Id, document.Language, document.Text);
            }

            var body = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync("language/:analyze-text?api-version=2022-05-01&showStats=true", body);
            httpResponseMessage.EnsureSuccessStatusCode();
            var response = JsonSerializer.Deserialize<AzureKeyPhraseExtractionResponse>(await httpResponseMessage.Content.ReadAsStringAsync());

            if (response == null)
            {
                throw new NotImplementedException("response is null");
            }

            return response;
        }

        public async Task<AzureEntityRecognitionResponse> RecognizeEntitiesAsync(IEnumerable<EntityRecognitionDocumentRequestDTO> documents)
        {
            var request = new AzureEntityRecognitionRequest();
            foreach (var document in documents)
            {
                request.AddDocument(document.Id, document.Language, document.Text);
            }

            var body = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync("language/:analyze-text?api-version=2022-05-01&showStats=true", body);
            httpResponseMessage.EnsureSuccessStatusCode();
            var d = await httpResponseMessage.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<AzureEntityRecognitionResponse>(d);

            if (response == null)
            {
                throw new NotImplementedException("response is null");
            }

            return response;
        }
    }
}
