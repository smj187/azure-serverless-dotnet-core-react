using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Contracts.Requests
{
    public class PatchVoiceTypeRequest
    {
        public Guid VoiceId { get; set; }
        public int VoiceType { get; set; }
    }
}
