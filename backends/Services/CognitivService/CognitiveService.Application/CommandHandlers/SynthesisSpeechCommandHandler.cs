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
        private readonly IAwsService _awsService;
        private readonly IVoiceRepository _voiceRepository;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IGoogleService _googleService;
        private readonly IAzureService _azureService;

        public SynthesisSpeechCommandHandler(IAwsService awsService, IVoiceRepository voiceRepository, IBlobStorageService blobStorageService, IGoogleService googleService, IAzureService azureService)
        {
            _awsService = awsService;
            _voiceRepository = voiceRepository;
            _blobStorageService = blobStorageService;
            _googleService = googleService;
            _azureService = azureService;
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
                stream = await _awsService.TTS(request.Value, request.Locale, voice.InternalName, voice.AwsVoiceConfig!.DefaultEngine);
            }
            else if (voice.VoiceProvider.IsGoogleProvider())
            {
                stream = await _googleService.TTS(request.Value, request.Locale, voice.InternalName, voice.Gender);
            }
            else if (voice.VoiceProvider.IsAzureProvider())
            {
                stream = await _azureService.TTS(request.Value, request.Locale, voice.InternalName, voice.Gender);
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
                + DateTimeOffset.UtcNow.ToString("yy-mm-dd")
                + fileType.Replace("/", ".");

            var url = await _blobStorageService.UploadStreamAsync(stream, fileName, fileType);

            return url;
        }
    }
}
