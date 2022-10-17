using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkspaceService.Contracts.Requests
{
    public class RemoveAudioBlockFromProjectRequest
    {
        public Guid AudioBlockId { get; set; }

    }
}
