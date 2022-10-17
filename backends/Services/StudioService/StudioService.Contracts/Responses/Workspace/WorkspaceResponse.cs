using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Contracts.Responses.Workspace
{
    public class WorkspaceFolderResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid? ParentFolderId { get; set; } 

        public List<WorkspaceProjectResponse> Projects { get; set; } = new List<WorkspaceProjectResponse>();

        public DateTimeOffset ModifiedAt { get; set; }
    }
    public class WorkspaceProjectResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public DateTimeOffset ModifiedAt { get; set; }
    }
    public class WorkspaceResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int ProjectsCount { get; set; }

        public List<WorkspaceFolderResponse> Folders { get; set; } = new List<WorkspaceFolderResponse>();
        public List<WorkspaceProjectResponse> Projects { get; set; } = new List<WorkspaceProjectResponse>();

        public DateTimeOffset ModifiedAt { get; set; }
    }

}
