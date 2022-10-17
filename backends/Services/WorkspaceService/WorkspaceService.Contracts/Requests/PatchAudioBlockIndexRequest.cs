using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkspaceService.Contracts.Requests
{
    public class PatchAudioBlockIndexRequest
    {
        public Guid AudioBlockId { get; set; }
        public int Index { get; set; }
    }
}
