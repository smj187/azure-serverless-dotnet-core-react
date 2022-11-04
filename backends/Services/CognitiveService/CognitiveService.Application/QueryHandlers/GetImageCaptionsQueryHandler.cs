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
    public class GetImageCaptionsQueryHandler : IRequestHandler<FindImageCaptionsQuery, AzureVisionCaptionResponse>
    {
        private readonly IAzureVisionService _azureVisionService;

        public GetImageCaptionsQueryHandler(IAzureVisionService azureVisionService)
        {
            _azureVisionService = azureVisionService;
        }

        public async Task<AzureVisionCaptionResponse> Handle(FindImageCaptionsQuery request, CancellationToken cancellationToken)
        {
            if (request.Url != null)
            {
                return await _azureVisionService.GetCaptionFromRemoteAsync(request.Url);
            }

            if (request.Image != null)
            {
                return await _azureVisionService.GetCaptionFromSourceAsync(request.Image);
            }

            throw new NotImplementedException();
        }
    }
}
