using CognitiveService.Application.Queries;
using CognitiveService.Application.Services.Interfaces;
using CognitiveService.Application.Services.Models;
using MediatR;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.QueryHandlers
{
    public class FindAzureVoicesQueryHandler : IRequestHandler<FindAzureVoicesQuery, IReadOnlyCollection<AzureSpeechVoiceResponse>>
    {
        private readonly IAzureSpeechService _azureSpeechService;

        public FindAzureVoicesQueryHandler(IAzureSpeechService azureSpeechService)
        {
            _azureSpeechService = azureSpeechService;
        }

        public async Task<IReadOnlyCollection<AzureSpeechVoiceResponse>> Handle(FindAzureVoicesQuery request, CancellationToken cancellationToken)
        {
            return await _azureSpeechService.DiscoverAzureVoicesAsync();
        }
    }
}
