using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkspaceService.Contracts.Requests
{
    public class PatchAudioBlockValueRequest
    {
        public Guid AudioBlockId { get; set; }
        public string? Value { get; set; }
    }
}
