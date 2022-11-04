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
    public class FindKeyPhrasesQueryHandler : IRequestHandler<FindKeyPhrasesQuery, AzureKeyPhraseExtractionResponse>
    {
        private readonly IAzureLanguageService _azureLanguageService;

        public FindKeyPhrasesQueryHandler(IAzureLanguageService azureLanguageService)
        {
            _azureLanguageService = azureLanguageService;
        }

        public async Task<AzureKeyPhraseExtractionResponse> Handle(FindKeyPhrasesQuery request, CancellationToken cancellationToken)
        {
            return await _azureLanguageService.ExtractKeyPhrasesAsync(request.Documents);
        }
    }
}
