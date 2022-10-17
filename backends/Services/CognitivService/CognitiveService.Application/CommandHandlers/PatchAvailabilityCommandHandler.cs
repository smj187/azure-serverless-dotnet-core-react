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
    public class PatchAvailabilityCommandHandler : IRequestHandler<PatchAvailabilityCommand, Voice>
    {
        private readonly IVoiceRepository _voiceRepository;

        public PatchAvailabilityCommandHandler(IVoiceRepository voiceRepository)
        {
            _voiceRepository = voiceRepository;
        }

        public async Task<Voice> Handle(PatchAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var voice = await _voiceRepository.FindAsync(request.VoiceId);
            if (voice == null)
            {
                throw new NotImplementedException();
            }

            voice.PatchAvailability();

            await _voiceRepository.PatchAsync(voice);
            return voice;
        }
    }
}
