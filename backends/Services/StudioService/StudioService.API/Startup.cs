using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using StudioService.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudioService.Infrastructure.Repositories;
using StudioService.Infrastructure.BsonCLassMapDefinitions.BsonClassMappings;
using StudioService.Core.Domain.Workspace;
using BuildingBlocks.Mongo.Configurations;
using StudioService.Core.Domain.Project;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StudioService.Application.Services;

[assembly: FunctionsStartup(typeof(Startup))]
namespace StudioService.API
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", true)
                .AddEnvironmentVariables()
                .Build();


            builder.Services.AddSingleton<IConfiguration>(config);

            //var str = config.GetValue<string>("Azure:CosmosDBConnection");
            //MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(str));
            //settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            //builder.Services.AddSingleton((s) => new MongoClient(settings));
            builder.Services.AddSingleton<IWorkspaceRepository, WorkspaceRepository>();
            builder.Services.AddSingleton<IProjectRepository, ProjectRepository>();

            builder.Services.AddSingleton<IAzureService, AzureService>();
            builder.Services.AddSingleton<IGoogleService, GoogleService>();
            builder.Services.AddSingleton<IAwsService, AwsService>();
            builder.Services.AddSingleton<IBlobService, BlobService>();

            MongoBaseBsonClassMapping.Apply();
            FolderBsonClassMapping.Apply();
            ProjectBsonClassMapping.Apply();
            AudioContentBsonClassMapping.Apply();
            WorkspaceBsonClassMapping.Apply();


            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));


            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //builder.Services.AddAutoMapper(typeof(Startup).Assembly);

            //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //builder.Services.AddSingleton(s =>
            //{
            //    var str = Configuration.GetValue<string>("Azure:CosmosDBConnection");
            //    if (string.IsNullOrEmpty(str))
            //    {
            //        throw new InvalidOperationException(
            //            "Please specify a valid CosmosDBConnection in the appSettings.json file or your Azure Functions Settings.");
            //    }

            //    return new CosmosClientBuilder(str)
            //        .Build();
            //});
        }
    }
}
