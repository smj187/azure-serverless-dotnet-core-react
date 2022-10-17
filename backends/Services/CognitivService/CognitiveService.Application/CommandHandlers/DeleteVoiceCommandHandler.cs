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

        public DeleteVoiceCommandHandler(IVoiceRepository voiceRepository)
        {
            _voiceRepository = voiceRepository;
        }

        public async Task<Unit> Handle(DeleteVoiceCommand request, CancellationToken cancellationToken)
        {
            var voice = await _voiceRepository.FindAsync(request.VoiceId);
            if (voice == null)
            {
                throw new NotImplementedException();
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
