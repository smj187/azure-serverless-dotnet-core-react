using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Contracts.Requests
{
    public class PatchVoiceDisplayRequest
    {
        public string DisplayName { get; set; } = default!;
    }
}
