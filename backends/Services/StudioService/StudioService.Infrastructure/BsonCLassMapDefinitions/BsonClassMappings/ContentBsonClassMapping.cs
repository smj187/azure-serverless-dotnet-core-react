using MongoDB.Bson.Serialization;
using StudioService.Core.Domain.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Infrastructure.BsonCLassMapDefinitions.BsonClassMappings
{
    public static class AudioContentBsonClassMapping
    {
        public static void Apply()
        {
            BsonClassMap.RegisterClassMap<AudioContent>(x =>
            {
                x.MapProperty(x => x.Value).SetElementName("value");
                x.MapProperty(x => x.Index).SetElementName("index");
            });
        }
    }
}
