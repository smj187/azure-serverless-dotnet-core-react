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
    public class ListVoicesQueryHandler : IRequestHandler<ListVoicesQuery, IReadOnlyCollection<Voice>>
    {
        private readonly IVoiceRepository _voiceRepository;

        public ListVoicesQueryHandler(IVoiceRepository voiceRepository)
        {
            _voiceRepository = voiceRepository;
        }

        public async Task<IReadOnlyCollection<Voice>> Handle(ListVoicesQuery request, CancellationToken cancellationToken)
        {
            return await _voiceRepository.ListAsync();
        }
    }
}
