using CognitiveService.Application.Queries;
using CognitiveService.Infrastructure.ProviderResponses;
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
    public class FindGoogleVoicesQueryHandler : IRequestHandler<FindGoogleVoicesQuery, IReadOnlyCollection<GoogleVoiceResponse>>
    {
        private readonly string _apiKey = "AIzaSyA0uxOkaOxNaTQUNRUTmgnir-IPpQk3gJk";
        private readonly RestClient _client;

        public FindGoogleVoicesQueryHandler()
        {
            var client = new RestClient($"https://texttospeech.googleapis.com");
            client.AddDefaultParameter("key", _apiKey);
            _client = client;
        }

        public async Task<IReadOnlyCollection<GoogleVoiceResponse>> Handle(FindGoogleVoicesQuery request, CancellationToken cancellationToken)
        {
            var restRequest = new RestRequest("v1beta1/voices");

            var restResponse = await _client.GetAsync<GoogleVoicesResponse>(restRequest, cancellationToken);
            if (restResponse == null)
            {
                throw new NotImplementedException();
            }

            return restResponse.Voices;
        }
    }
}
