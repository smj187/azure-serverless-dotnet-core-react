using CognitiveService.Application.Commands;
using CognitiveService.Application.Queries;
using CognitiveService.Application.Services.Interfaces;
using CognitiveService.Application.Services.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.QueryHandlers
{
    public class FindTranslationsQueryHandler : IRequestHandler<FindTranslationsQuery, IReadOnlyCollection<AzureTranslationResponse>>
    {
        private readonly IAzureTranslationService _azureTranslationService;

        public FindTranslationsQueryHandler(IAzureTranslationService azureTranslationService)
        {
            _azureTranslationService = azureTranslationService;
        }

        public async Task<IReadOnlyCollection<AzureTranslationResponse>> Handle(FindTranslationsQuery request, CancellationToken cancellationToken)
        {
            return await _azureTranslationService.Translate(request.Locale, request.Translations);
        }
    }
}
