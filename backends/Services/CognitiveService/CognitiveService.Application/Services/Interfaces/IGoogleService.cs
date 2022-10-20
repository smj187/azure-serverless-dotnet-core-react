using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services.Interfaces
{
    public interface IGoogleService
    {
        Task<Stream> TTS(string value, string language, string voice, string gender);
    }
}
