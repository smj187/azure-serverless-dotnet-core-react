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
    public class DetectLanguageQueryHandler : IRequestHandler<DetectLanguageQuery, AzureLanguageDetectionResponse>
    {
        private readonly IAzureLanguageService _azureLanguageService;

        public DetectLanguageQueryHandler(IAzureLanguageService azureLanguageService)
        {
            _azureLanguageService = azureLanguageService;
        }

        public async Task<AzureLanguageDetectionResponse> Handle(DetectLanguageQuery request, CancellationToken cancellationToken)
        {
            return await _azureLanguageService.DetectLanguagesAsync(request.Documents);
        }
    }
}
