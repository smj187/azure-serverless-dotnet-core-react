using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Contracts.Requests.Workspace
{
    public class PatchFolderDescriptionRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; } = null;

    }
}
