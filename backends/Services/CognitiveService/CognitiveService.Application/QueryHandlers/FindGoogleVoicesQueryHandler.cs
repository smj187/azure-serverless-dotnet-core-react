using CognitiveService.Application.Queries;
using CognitiveService.Application.Services.Interfaces;
using CognitiveService.Application.Services.Models;
using MediatR;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.QueryHandlers
{
    public class FindGoogleVoicesQueryHandler : IRequestHandler<FindGoogleVoicesQuery, GoogleVoiceResponse>
    {
        private readonly IGoogleSpeechService _googleSpeechService;

        public FindGoogleVoicesQueryHandler(IGoogleSpeechService googleSpeechService)
        {
            _googleSpeechService = googleSpeechService;
        }

        public async Task<GoogleVoiceResponse> Handle(FindGoogleVoicesQuery request, CancellationToken cancellationToken)
        {
            return await _googleSpeechService.DiscoverGoogleVoicesAsync();
        }
    }
}
