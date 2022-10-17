using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using CognitiveService.Application.Queries;
using CognitiveService.Infrastructure.ProviderResponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.QueryHandlers
{
    public class FindAwsVoicesQueryHandler : IRequestHandler<FindAwsVoicesQuery, IReadOnlyCollection<AwsVoiceResponse>>
    {
        private readonly AmazonPollyClient _polly;

        public FindAwsVoicesQueryHandler()
        {
            var credentials = new BasicAWSCredentials("AKIATBG2M4KPIDGN7AEY", "/UqrLlp8afVSY/eboZgvArjenLdwcLiqV2IHHyRX");

            var polly = new AmazonPollyClient(credentials, RegionEndpoint.EUWest3);
            _polly = polly;
        }

        public async Task<IReadOnlyCollection<AwsVoiceResponse>> Handle(FindAwsVoicesQuery request, CancellationToken cancellationToken)
        {
            var awsResponse = await _polly.DescribeVoicesAsync(new DescribeVoicesRequest());

            var voices = new List<AwsVoiceResponse>();

            var specialVoices = new List<string>
            {
                "Amy", "Joanna", "Matthew", "Lupe"
            };

            foreach (var v in awsResponse.Voices)
            {
                var specialStyles = new List<string>();
                if (specialVoices.Contains(v.Name))
                {
                    specialVoices.Add("news");
                }
                voices.Add(new AwsVoiceResponse(v.Id, v.Name, v.Gender, v.LanguageCode, v.SupportedEngines, v.LanguageName, v.AdditionalLanguageCodes, specialStyles));
            }


            return voices;

        }
    }
}
