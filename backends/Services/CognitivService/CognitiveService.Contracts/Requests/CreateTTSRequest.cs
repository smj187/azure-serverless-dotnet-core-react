using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Contracts.Requests
{
    public class CreateTTSRequest
    {
        public string Value { get; set; }
        public string Locale { get; set; }
        public Guid VoiceId { get; set; }
    }

}
