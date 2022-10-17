using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkspaceService.Core.Domain
{
    public class ProjectType : Enumeration
    {
        private static readonly ProjectType Script = new(0, "Script");

        public ProjectType(int value, string description)
            : base(value, description)
        {

        }

        public static ProjectType CreateScriptProjectType() => Script;
    }
}
