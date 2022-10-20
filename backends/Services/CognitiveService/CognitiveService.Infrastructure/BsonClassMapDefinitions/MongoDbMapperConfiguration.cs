using CognitiveService.Infrastructure.BsonClassMapDefinitions.BsonClassMappings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Infrastructure.BsonClassMapDefinitions
{
    public static class MongoDbMapperConfiguration
    {
        public static IServiceCollection AddBsonClassMappings(this IServiceCollection services)
        {
            VoiceBsonClassMapping.Apply();
            return services;
        }
    }
}
