using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using BuildingBlocks.Domain;

namespace BuildingBlocks.Mongo.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMongoDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            BsonClassMap.RegisterClassMap<Enumeration>(x =>
            {
                x.MapProperty(x => x.Value).SetElementName("value").SetIsRequired(true);
                x.MapProperty(x => x.Description).SetElementName("description").SetIsRequired(true);
            });

            //BsonClassMap.RegisterClassMap<AggregateBase>(x =>
            //{
            //    x.MapProperty(x => x.Id).SetElementName("id").SetIsRequired(true);
            //    x.MapProperty(x => x.CreatedAt).SetElementName("created_at").SetIsRequired(true);
            //    x.MapProperty(x => x.ModifiedAt).SetElementName("modified_at").SetIsRequired(true);
            //});

            return services;
        }


    }
}
