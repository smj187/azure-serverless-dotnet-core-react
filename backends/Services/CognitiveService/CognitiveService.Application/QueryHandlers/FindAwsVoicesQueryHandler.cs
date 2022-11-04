using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using CognitiveService.Application.Queries;
using CognitiveService.Application.Services;
using CognitiveService.Application.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.QueryHandlers
{
    public class FindAwsVoicesQueryHandler : IRequestHandler<FindAwsVoicesQuery, DescribeVoicesResponse>
    {
        private readonly IAmazonPollyService _amazonPollyService;

        public FindAwsVoicesQueryHandler(IAmazonPollyService amazonPollyService)
        {
            _amazonPollyService = amazonPollyService;
        }

        public async Task<DescribeVoicesResponse> Handle(FindAwsVoicesQuery request, CancellationToken cancellationToken)
        {
            return await _amazonPollyService.DiscoverAmazonVoicesAsync();
        }
    }
}
