using CognitiveService.Application.Queries;
using CognitiveService.Infrastructure.ProviderResponses;
using MediatR;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.QueryHandlers
{
    public class FindAzureVoicesQueryHandler : IRequestHandler<FindAzureVoicesQuery, IReadOnlyCollection<AzureVoiceResponse>>
    {
        private readonly string _region = "westeurope";
        private readonly RestClient _client;
        private readonly string _apiKey = "f67126b813c14c2a96fcadc61adfa867";

        public FindAzureVoicesQueryHandler()
        {
            var client = new RestClient($"https://{_region}.tts.speech.microsoft.com");
            client.AddDefaultParameter("Ocp-Apim-Subscription-Key", _apiKey);
            _client = client;
        }

        public async Task<IReadOnlyCollection<AzureVoiceResponse>> Handle(FindAzureVoicesQuery request, CancellationToken cancellationToken)
        {
            var restRequest = new RestRequest("cognitiveservices/voices/list", Method.Get);

            var restResponse = await _client.GetAsync<IReadOnlyCollection<AzureVoiceResponse>>(restRequest, cancellationToken);
            if (restResponse == null)
            {
                throw new NotImplementedException();
            }


            return restResponse;
        }
    }
}
