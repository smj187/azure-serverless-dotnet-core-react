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
    public class DeleteVoiceCommandHandler : IRequestHandler<DeleteVoiceCommand>
    {
        private readonly IVoiceRepository _voiceRepository;
        private readonly IBlobStorageService _blobStorageService;

        public DeleteVoiceCommandHandler(IVoiceRepository voiceRepository, IBlobStorageService blobStorageService)
        {
            _voiceRepository = voiceRepository;
            _blobStorageService = blobStorageService;
        }

        public async Task<Unit> Handle(DeleteVoiceCommand request, CancellationToken cancellationToken)
        {
            var voice = await _voiceRepository.FindAsync(request.VoiceId);
            if (voice == null)
            {
                throw new NotImplementedException();
            }

            if (voice.PreviewAudio != null)
            {
                if (!await _blobStorageService.DeleteVoiceAssetBlob(voice.PreviewAudio.Name))
                {
                    throw new NotImplementedException("failed to delete preview");
                }
            }
            
            if (voice.AvatarImage != null)
            {
                if (!await _blobStorageService.DeleteVoiceAssetBlob(voice.AvatarImage.Name))
                {
                    throw new NotImplementedException("failed to delete avatar");
                }
            }

            var result = await _voiceRepository.DeleteAsync(voice);
            if(result == false)
            {
                throw new NotImplementedException();
            }

            return Unit.Value;
        }
    }
}
