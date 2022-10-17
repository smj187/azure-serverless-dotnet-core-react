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
    public class PatchDisplayNameCommandHandler : IRequestHandler<PatchDisplayNameCommand, Voice>
    {
        private readonly IVoiceRepository _voiceRepository;

        public PatchDisplayNameCommandHandler(IVoiceRepository voiceRepository)
        {
            _voiceRepository = voiceRepository;
        }

        public async Task<Voice> Handle(PatchDisplayNameCommand request, CancellationToken cancellationToken)
        {
            var voice = await _voiceRepository.FindAsync(request.VoiceId);
            if (voice == null)
            {
                throw new NotImplementedException();
            }

            voice.PatchDisplayName(request.NewDisplayName);

            await _voiceRepository.PatchAsync(voice);

            return voice;
        }
    }
}
