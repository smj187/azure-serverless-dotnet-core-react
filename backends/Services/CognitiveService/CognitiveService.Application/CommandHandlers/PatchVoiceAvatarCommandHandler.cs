using BuildingBlocks.BlobStorage.Interfaces;
using CognitiveService.Application.Commands;
using CognitiveService.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.CommandHandlers
{
    public class PatchVoiceAvatarCommandHandler : IRequestHandler<PatchVoiceAvatarCommand, Voice>
    {
        private readonly IVoiceRepository _voiceRepository;
        private readonly IBlobStorageService _blobStorageService;

        public PatchVoiceAvatarCommandHandler(IVoiceRepository voiceRepository, IBlobStorageService blobStorageService)
        {
            _voiceRepository = voiceRepository;
            _blobStorageService = blobStorageService;
        }

        public async Task<Voice> Handle(PatchVoiceAvatarCommand request, CancellationToken cancellationToken)
        {
            var voice = await _voiceRepository.FindAsync(request.VoiceId);
            if (voice == null)
            {
                throw new NotImplementedException();
            }

            // add/update avatar
            if (request.Image != null)
            {
                var name = $"voice_avatar_${voice.Id}.{request.Image.ContentType.Split("/").LastOrDefault()}";
                var url = await _blobStorageService.UploadVoiceAvatarAsset(name, request.Image);

                voice.CreateVoiceAvatar(url, name, request.Image);
            }
            // remove avatar
            else
            {
                if (voice.AvatarImage == null)
                {
                    throw new NotImplementedException("no avatar set yet");
                }

                if (!await _blobStorageService.DeleteVoiceAssetBlob(voice.AvatarImage.Name))
                {
                    throw new NotImplementedException("failed to delete avatar");
                }

                voice.ResetVoiceAvatar();
            }

            await _voiceRepository.PatchAsync(voice);

            return voice;
        }
    }
}
