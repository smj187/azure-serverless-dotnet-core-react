using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Contracts.Requests
{
    public class PatchVoiceNameRequest
    {
        public Guid VoiceId { get; set; }
        public string Name { get; set; } = default!;
    }
}
