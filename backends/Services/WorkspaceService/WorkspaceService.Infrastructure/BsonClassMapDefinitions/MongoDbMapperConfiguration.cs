using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkspaceService.Infrastructure.BsonClassMapDefinitions.BsonClassMappings;

namespace WorkspaceService.Infrastructure.BsonClassMapDefinitions
{
    public static class MongoDbMapperConfiguration
    {
        public static IServiceCollection AddBsonClassMappings(this IServiceCollection services)
        {
            ProjectCLassMapping.Apply();
            return services;
        }
    }
}
