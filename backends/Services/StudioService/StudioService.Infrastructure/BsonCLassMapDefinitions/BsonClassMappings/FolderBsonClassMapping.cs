using MongoDB.Bson.Serialization;
using StudioService.Core.Domain.Workspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Infrastructure.BsonCLassMapDefinitions.BsonClassMappings
{
    public static class FolderBsonClassMapping
    {
        public static void Apply()
        {
            BsonClassMap.RegisterClassMap<Folder>(x =>
            {
                x.MapProperty(x => x.Name).SetElementName("name").SetIsRequired(true);
                x.MapProperty(x => x.Description).SetElementName("description").SetIsRequired(true);
                x.MapProperty(x => x.Subfolders).SetElementName("sub_folders").SetIsRequired(true);
                x.MapProperty(x => x.Projects).SetElementName("projects").SetIsRequired(true);
            });
        }
    }
}
