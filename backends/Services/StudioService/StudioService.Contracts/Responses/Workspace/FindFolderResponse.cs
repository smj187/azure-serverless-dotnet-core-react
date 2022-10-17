using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Contracts.Responses.Workspace
{
    public class FindFolderSubfolderResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ProjectCount { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
    }

    public class FindFolderResponse
    {
        public Guid Id { get; set; }
        public Guid ParentFolderId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<FindFolderSubfolderResponse> Subfolders { get; set; } = new List<FindFolderSubfolderResponse>();
        public List<WorkspaceProjectResponse> Projects { get; set; } = new List<WorkspaceProjectResponse>();
        public DateTimeOffset ModifiedAt { get; set; }
    }
}
