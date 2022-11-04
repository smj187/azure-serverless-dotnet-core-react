using BuildingBlocks.BlobStorage.Interfaces;
using CognitiveService.Application.Commands;
using CognitiveService.Application.Services.Interfaces;
using CognitiveService.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.CommandHandlers
{
    public class SynthesisSpeechCommandHandler : IRequestHandler<SynthesisSpeechCommand, string>
    {
        private readonly IAzureSpeechService _azureSpeechService;
        private readonly IAmazonPollyService _amazonPollyService;
        private readonly IVoiceRepository _voiceRepository;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IGoogleSpeechService _googleSpeechService;

        public SynthesisSpeechCommandHandler(IAzureSpeechService azureSpeechService, IAmazonPollyService amazonPollyService, IVoiceRepository voiceRepository, IBlobStorageService blobStorageService, IGoogleSpeechService googleSpeechService)
        {
            _azureSpeechService = azureSpeechService;
            _amazonPollyService = amazonPollyService;
            _voiceRepository = voiceRepository;
            _blobStorageService = blobStorageService;
            _googleSpeechService = googleSpeechService;
        }

        public async Task<string> Handle(SynthesisSpeechCommand request, CancellationToken cancellationToken)
        {
            var voice = await _voiceRepository.FindAsync(request.VoiceId);
            if (voice == null)
            {
                throw new NotImplementedException();
            }


            Stream stream;
            if (voice.VoiceProvider.IsAwsProvider())
            {
                stream = await _amazonPollyService.TTS(request.Locale, voice.InternalName, voice.AwsVoiceConfig!.DefaultEngine, request.Text, request.Ssml);
            }
            else if (voice.VoiceProvider.IsGoogleProvider())
            {
                //stream = await _googleService.TTS(request.Text, request.Locale, voice.InternalName, voice.Gender);
                stream = await _googleSpeechService.TTS(request.Locale, voice.InternalName, voice.Gender, request.Text, request.Ssml);
            }
            else if (voice.VoiceProvider.IsAzureProvider())
            {
                stream = await _azureSpeechService.TTS(request.Locale, voice.InternalName, voice.Gender, request.Text, request.Ssml);
            }
            else
            {
                throw new NotImplementedException();
            }


            var fileType = "audio/ogg";

            var fileName = voice.VoiceProvider.Description.ToLower()
                + "_"
                + Path.GetRandomFileName()
                + "_"
                + DateTimeOffset.UtcNow.ToString("yy-mm-dd");

            fileName.Replace("/", ".");
            fileName += ".ogg";

            var url = await _blobStorageService.UploadSpeechStreamAsync(stream, fileName, fileType);

            return url;
        }
    }
}
