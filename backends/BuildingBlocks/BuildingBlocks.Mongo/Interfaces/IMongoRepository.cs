using BuildingBlocks.Domain.Interfaces;
using BuildingBlocks.Domain.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Mongo.Interfaces
{
    public interface IMongoRepository<T> : IMongoCommandRepository<T>, IMongoQueryRepository<T> where T : IAggregateBase
    {

    }
}
