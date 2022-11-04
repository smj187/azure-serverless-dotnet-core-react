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
    public class GetImageTagsQueryHandler : IRequestHandler<FindImageTagsQuery, AzureVisionTagResponse>
    {
        private readonly IAzureVisionService _azureVisionService;

        public GetImageTagsQueryHandler(IAzureVisionService azureVisionService)
        {
            _azureVisionService = azureVisionService;
        }

        public async Task<AzureVisionTagResponse> Handle(FindImageTagsQuery request, CancellationToken cancellationToken)
        {
            if (request.Url != null)
            {
                return await _azureVisionService.GetTagsFromRemoteAsync(request.Url);
            }

            if (request.Image != null)
            {
                return await _azureVisionService.GetTagsFromSourceAsync(request.Image);
            }

            throw new NotImplementedException($"neither url nor image was provided");
        }
    }
}
