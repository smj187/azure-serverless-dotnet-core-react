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
    public class PatchVoiceTypeCommandHandler : IRequestHandler<PatchVoiceTypeCommand, Voice>
    {
        private readonly IVoiceRepository _voiceRepository;

        public PatchVoiceTypeCommandHandler(IVoiceRepository voiceRepository)
        {
            _voiceRepository = voiceRepository;
        }

        public async Task<Voice> Handle(PatchVoiceTypeCommand request, CancellationToken cancellationToken)
        {
            var voice = await _voiceRepository.FindAsync(request.VoiceId);
            if (voice == null)
            {
                throw new NotImplementedException();
            }

            voice.PatchVoiceType(request.VoiceType);

            await _voiceRepository.PatchAsync(voice);

            return voice;
        }
    }
}
