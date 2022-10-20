using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Contracts.Responses
{
    public class UserVoicesResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }
        public string VoiceType { get; set; }

        public string? AvatarUrl { get; set; }
        public string? AvatarContentType { get; set; }

        public string? PreviewUrl { get; set; }
        public string? PreviewContentType { get; set; }

        public List<string> SpecialStyles { get; set; } = new List<string>();
    }
}
