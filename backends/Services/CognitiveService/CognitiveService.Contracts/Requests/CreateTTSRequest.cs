using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Contracts.Requests
{
    public class CreateTTSRequest
    {
        public string? Ssml { get; set; } = null;
        public string? Text { get; set; } = null;
        public string Locale { get; set; } = default!;
        public Guid VoiceId { get; set; } = default!;
    }

}
