using MongoDB.Bson.Serialization;
using StudioService.Core.Domain.Project;
using StudioService.Core.Domain.Workspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Infrastructure.BsonCLassMapDefinitions.BsonClassMappings
{
    public static class WorkspaceBsonClassMapping
    {
        public static void Apply()
        {
            BsonClassMap.RegisterClassMap<Workspace>(x =>
            {
                x.MapProperty(x => x.UserId).SetElementName("user_id");
                x.MapProperty(x => x.Subfolders).SetElementName("folders").SetIsRequired(true);
                x.MapProperty(x => x.Projects).SetElementName("projects").SetIsRequired(true);
                x.MapProperty(x => x.Name).SetElementName("name").SetIsRequired(true);
                x.MapProperty(x => x.WorkspaceType).SetElementName("workspace_type").SetIsRequired(true);
            });
        }
    }
}
