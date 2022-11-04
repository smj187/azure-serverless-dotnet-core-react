using CognitiveService.Application.Services.Models;
using Microsoft.AspNetCore.Http;

namespace CognitiveService.Application.Services.Interfaces
{
    public interface IAzureVisionService
    {
        Task<AzureVisionTagResponse> GetTagsFromRemoteAsync(string url);
        Task<AzureVisionTagResponse> GetTagsFromSourceAsync(IFormFile image);

        Task<AzureVisionCaptionResponse> GetCaptionFromRemoteAsync(string url);
        Task<AzureVisionCaptionResponse> GetCaptionFromSourceAsync(IFormFile image);
    }
}
