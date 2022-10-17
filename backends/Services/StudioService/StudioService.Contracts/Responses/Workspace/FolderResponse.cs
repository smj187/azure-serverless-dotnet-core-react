using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Contracts.Responses.Workspace
{
    public class FolderResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid? ParentFolderId { get; set; }

        public DateTimeOffset ModifiedAt { get; set; }
    }
}
