using Amazon.Polly.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services.Interfaces
{
    public interface IAmazonPollyService
    {
        Task<DescribeVoicesResponse> DiscoverAmazonVoicesAsync();
        Task<Stream> TTS(string language, string voice, string engine, string? text, string? ssml);
    }
}
