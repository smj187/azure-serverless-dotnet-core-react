using CognitiveService.Application.Queries;
using CognitiveService.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.QueryHandlers
{
    public class FindVoiceQueryHandler : IRequestHandler<FindVoiceQuery, Voice?>
    {
        private readonly IVoiceRepository _voiceRepository;

        public FindVoiceQueryHandler(IVoiceRepository voiceRepository)
        {
            _voiceRepository = voiceRepository;
        }

        public async Task<Voice?> Handle(FindVoiceQuery request, CancellationToken cancellationToken)
        {
            return await _voiceRepository.FindAsync(request.VoiceId);
        }
    }
}
