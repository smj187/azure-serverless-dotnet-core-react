using CognitiveService.Application.DTOs;
using CognitiveService.Application.Services.Models;

namespace CognitiveService.Application.Services.Interfaces
{
    public interface IAzureTranslationService
    {
        Task<List<AzureTranslationResponse>> Translate(string locale, IEnumerable<TranslationRequestDTO> translations);
    }
}
