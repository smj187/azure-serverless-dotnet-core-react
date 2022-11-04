using CognitiveService.Application.Services.Models;

namespace CognitiveService.Application.Services.Interfaces
{
    public interface IAzureSpeechService
    {
        Task<IReadOnlyCollection<AzureSpeechVoiceResponse>> DiscoverAzureVoicesAsync();
        Task<Stream> TTS(string language, string voice, string gender, string? text, string? ssml);
    }
}
