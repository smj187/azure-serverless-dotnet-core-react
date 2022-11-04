using Azure.Core;
using BuildingBlocks.BlobStorage.Extensions;
using CognitiveService.Application.Services.Interfaces;
using CognitiveService.Application.Services.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services
{
    public class AzureVisionService : IAzureVisionService
    {
        private readonly HttpClient _httpClient;

        public AzureVisionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AzureVisionCaptionResponse> GetCaptionFromRemoteAsync(string url)
        {
            var request = new AzureVisionCaptionRequest(url);

            var body = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync("vision/v3.2/describe?language=en&model-version=2021-05-01", body);
            httpResponseMessage.EnsureSuccessStatusCode();
            var response = JsonSerializer.Deserialize<AzureVisionCaptionResponse>(await httpResponseMessage.Content.ReadAsStringAsync());

            if (response == null)
            {
                throw new NotImplementedException("response is null");
            }

            return response;
        }

        public async Task<AzureVisionCaptionResponse> GetCaptionFromSourceAsync(IFormFile image)
        {
            var bytes = await image.GetBytes();
            var formContent = new MultipartFormDataContent
            {
                {
                    new StreamContent(new MemoryStream(bytes))
                }
            };

            var httpResponseMessage = await _httpClient.PostAsync("vision/v3.2/describe?language=en&model-version=2021-05-01", formContent);
            httpResponseMessage.EnsureSuccessStatusCode();
            var response = JsonSerializer.Deserialize<AzureVisionCaptionResponse>(await httpResponseMessage.Content.ReadAsStringAsync());

            if (response == null)
            {
                throw new NotImplementedException("response is null");
            }

            return response;
        }

        public async Task<AzureVisionTagResponse> GetTagsFromRemoteAsync(string url)
        {
            var request = new AzureVisionTagRequest(url);

            var body = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync("vision/v3.2/tag?language=en&model-version=2021-04-01", body);
            httpResponseMessage.EnsureSuccessStatusCode();
            var response = JsonSerializer.Deserialize<AzureVisionTagResponse>(await httpResponseMessage.Content.ReadAsStringAsync());

            if (response == null)
            {
                throw new NotImplementedException("response is null");
            }

            return response;
        }

        public async Task<AzureVisionTagResponse> GetTagsFromSourceAsync(IFormFile image)
        {
            var bytes = await image.GetBytes();
            var formContent = new MultipartFormDataContent
            {
                {
                    new StreamContent(new MemoryStream(bytes))
                }
            };

            var httpResponseMessage = await _httpClient.PostAsync("vision/v3.2/tag?language=en&model-version=2021-04-01", formContent);
            httpResponseMessage.EnsureSuccessStatusCode();


            var response = JsonSerializer.Deserialize<AzureVisionTagResponse>(await httpResponseMessage.Content.ReadAsStringAsync());

            if (response == null)
            {
                throw new NotImplementedException("response is null");
            }

            return response;
        }
    }
}
