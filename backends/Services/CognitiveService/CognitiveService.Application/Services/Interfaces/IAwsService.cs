using CognitiveService.Core;
using CognitiveService.Infrastructure.ProviderResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services.Interfaces
{
    public interface IAwsService
    {
        Task<Stream> TTS(string value, string language, string voice, string engine);
    }
}
