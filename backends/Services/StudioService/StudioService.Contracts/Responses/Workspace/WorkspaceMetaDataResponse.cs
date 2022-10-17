using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Contracts.Responses.Workspace
{
    public class WorkspaceMetaDataResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int ProjectsCount { get; set; }

        public DateTimeOffset ModifiedAt { get; set; }
    }
}
