using CognitiveService.Application.Services.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Queries
{
    public class FindImageTagsQuery : IRequest<AzureVisionTagResponse>
    {
        public string? Url { get; set; } = null;
        public IFormFile? Image { get; set; } = null;
    }
}
