using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkspaceService.Contracts.Requests
{
    public class PatchAudioBlockVoiceRequest
    {
        public Guid ProjectId { get; set; }
        public Guid AudioBlockId { get; set; }
        public string? VoiceId { get; set; }
    }
}
