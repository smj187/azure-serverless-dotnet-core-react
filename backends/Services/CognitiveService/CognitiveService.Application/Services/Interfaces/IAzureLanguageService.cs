using CognitiveService.Application.DTOs;
using CognitiveService.Application.Services.Models;

namespace CognitiveService.Application.Services.Interfaces
{
    public interface IAzureLanguageService
    {
        Task<AzureKeyPhraseExtractionResponse> ExtractKeyPhrasesAsync(IEnumerable<KeyPhraseExtractionDocumentRequestDTO> documents);
        Task<AzureLanguageDetectionResponse> DetectLanguagesAsync(IEnumerable<LanguageDetectionDocumentRequestDTO> documents);
        Task<AzureEntityRecognitionResponse> RecognizeEntitiesAsync(IEnumerable<EntityRecognitionDocumentRequestDTO> documents);
    }
}
