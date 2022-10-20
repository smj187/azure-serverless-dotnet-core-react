using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Contracts.Requests
{
    public class PatchVoicePreviewRequest
    {
        public Guid VoiceId { get; set; }
        public IFormFile? Audio { get; set; } = null;
    }
}
