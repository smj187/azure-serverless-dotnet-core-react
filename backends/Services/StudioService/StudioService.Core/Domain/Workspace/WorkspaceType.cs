using BuildingBlocks.Domain;
using StudioService.Core.Domain.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Core.Domain.Workspace
{
    public class WorkspaceType : Enumeration
    {
        public static readonly WorkspaceType ScriptWorkspace = new(0, "Script_Workspace");
        public static readonly WorkspaceType BookWorkspace = new(1, "Book_Workspace");

        public WorkspaceType(int value, string description)
            : base(value, description)
        {

        }
    }
}
