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
    public class EntityRecognitionQueryHandler : IRequestHandler<EntityRecognitionQuery, AzureEntityRecognitionResponse>
    {
        private readonly IAzureLanguageService _azureLanguageService;

        public EntityRecognitionQueryHandler(IAzureLanguageService azureLanguageService)
        {
            _azureLanguageService = azureLanguageService;
        }

        public async Task<AzureEntityRecognitionResponse> Handle(EntityRecognitionQuery request, CancellationToken cancellationToken)
        {
            return await _azureLanguageService.RecognizeEntitiesAsync(request.Documents);
        }
    }
}
