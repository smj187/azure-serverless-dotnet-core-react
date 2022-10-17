using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkspaceService.Core.Domain;

namespace WorkspaceService.Infrastructure.BsonClassMapDefinitions.BsonClassMappings
{
    public class ProjectCLassMapping
    {
        public static void Apply()
        {
            BsonClassMap.RegisterClassMap<Project>(x =>
            {
                x.MapProperty(x => x.ProjectType).SetElementName("project_type").SetIsRequired(true);
                x.MapProperty(x => x.Name).SetElementName("name").SetIsRequired(true);
                x.MapProperty(x => x.Description).SetElementName("description").SetIsRequired(true);
                x.MapProperty(x => x.ImageUrl).SetElementName("image_url").SetIsRequired(true);
                x.MapProperty(x => x.AudioBlocks).SetElementName("audio_blocks").SetIsRequired(true);
            });

            BsonClassMap.RegisterClassMap<AudioBlock>(x =>
            {
                x.MapProperty(x => x.Index).SetElementName("index").SetIsRequired(true);
                x.MapProperty(x => x.VoiceId).SetElementName("voice_id").SetIsRequired(true);
                x.MapProperty(x => x.Value).SetElementName("value").SetIsRequired(true);
                x.MapProperty(x => x.AudioFile).SetElementName("audio_file").SetIsRequired(true);
            });

            BsonClassMap.RegisterClassMap<AudioFile>(x =>
            {
                x.MapProperty(x => x.VoideId).SetElementName("voice_id").SetIsRequired(true);
                x.MapProperty(x => x.AudioUrl).SetElementName("audio_url").SetIsRequired(true);
                x.MapProperty(x => x.Value).SetElementName("value").SetIsRequired(true);
            });
        }
    }
}
