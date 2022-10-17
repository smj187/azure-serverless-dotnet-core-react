using MongoDB.Bson.Serialization;
using StudioService.Core.Domain.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Infrastructure.BsonCLassMapDefinitions.BsonClassMappings
{
    public static class ProjectBsonClassMapping
    {
        public static void Apply()
        {
            BsonClassMap.RegisterClassMap<Project>(x =>
            {
                x.MapProperty(x => x.Name).SetElementName("name");
                x.MapProperty(x => x.Description).SetElementName("description");
                x.MapProperty(x => x.ImageUrl).SetElementName("image_url");
                x.MapProperty(x => x.WorkspaceId).SetElementName("workspace_id");
                x.MapProperty(x => x.FolderId).SetElementName("folder_id");
                x.MapProperty(x => x.AudioContent).SetElementName("audio_content").SetIsRequired(true);
            });
        }
    }
}
