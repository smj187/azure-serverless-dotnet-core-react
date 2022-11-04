using CognitiveService.Application.Services.Models;

namespace CognitiveService.Application.Services.Interfaces
{
    public interface IGoogleSpeechService
    {
        Task<GoogleVoiceResponse> DiscoverGoogleVoicesAsync();
        Task<Stream> TTS(string language, string voice, string gender, string? text, string? ssml);
    }
}
