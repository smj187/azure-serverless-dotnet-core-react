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
    public class PatchVoiceNameCommandHandler : IRequestHandler<PatchVoiceNameCommand, Voice>
    {
        private readonly IVoiceRepository _voiceRepository;

        public PatchVoiceNameCommandHandler(IVoiceRepository voiceRepository)
        {
            _voiceRepository = voiceRepository;
        }

        public async Task<Voice> Handle(PatchVoiceNameCommand request, CancellationToken cancellationToken)
        {
            var voice = await _voiceRepository.FindAsync(request.VoiceId);
            if (voice == null)
            {
                throw new NotImplementedException();
            }

            voice.PatchVoiceNameName(request.NewDisplayName);

            await _voiceRepository.PatchAsync(voice);

            return voice;
        }
    }
}
