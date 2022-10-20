using BuildingBlocks.BlobStorage.Interfaces;
using CognitiveService.Application.Commands;
using CognitiveService.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.CommandHandlers
{
    public class PatchVoicePreviewCommandHandler : IRequestHandler<PatchVoicePreviewCommand, Voice>
    {
        private readonly IVoiceRepository _voiceRepository;
        private readonly IBlobStorageService _blobStorageService;

        public PatchVoicePreviewCommandHandler(IVoiceRepository voiceRepository, IBlobStorageService blobStorageService)
        {
            _voiceRepository = voiceRepository;
            _blobStorageService = blobStorageService;
        }

        public async Task<Voice> Handle(PatchVoicePreviewCommand request, CancellationToken cancellationToken)
        {
            var voice = await _voiceRepository.FindAsync(request.VoiceId);
            if (voice == null)
            {
                throw new NotImplementedException();
            }

            // add/update preview
            if (request.Audio != null)
            {
                var name = $"voice_preview_{voice.Id}.{request.Audio.ContentType.Split("/").LastOrDefault()}";
                var url = await _blobStorageService.UploadVoicePreviewAsset(name, request.Audio);

                voice.CreateVoicePreview(url, name, request.Audio);
            }
            // remove preview
            else
            {
                if (voice.PreviewAudio == null)
                {
                    throw new NotImplementedException("no preview set yet");
                }

                if (!await _blobStorageService.DeleteVoiceAssetBlob(voice.PreviewAudio.Name))
                {
                    throw new NotImplementedException("failed to delete preview");
                }

                voice.ResetVoicePreview();
            }


            await _voiceRepository.PatchAsync(voice);

            return voice;
        }
    }
}
