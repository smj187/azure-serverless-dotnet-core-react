using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Contracts.Requests.Project
{
    public class CreateProjectRequest
    {
        public Guid WorkspaceId { get; set; }
        public Guid? FolderId { get; set; } = null;

        public string Name { get; set; }
        public string? Description { get; set; } = null;
    }
}
