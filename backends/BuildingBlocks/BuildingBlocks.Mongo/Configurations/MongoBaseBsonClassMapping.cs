using BuildingBlocks.Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Mongo.Configurations
{
    public static class MongoBaseBsonClassMapping
    {
        public static void Apply()
        {
            BsonClassMap.RegisterClassMap<EntityBase>(x =>
            {
                x.MapProperty(x => x.Id)
                    .SetElementName("_id")
                    .SetIsRequired(true)
                    .SetSerializer(new GuidSerializer(BsonType.String));

                x.MapProperty(x => x.CreatedAt).SetElementName("created_at");
                x.MapProperty(x => x.ModifiedAt).SetElementName("modified_at");
            });
        }
    }
}
