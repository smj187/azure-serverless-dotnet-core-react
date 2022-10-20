using BuildingBlocks.Domain.Interfaces;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.Mongo.Helpers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Mongo.Interfaces
{
    public interface IMongoQueryRepository<T> : IQueryRepository<T> where T : IAggregateBase
    {
        Task<long> CountAsync();
        Task<(MongoPaginationResult mongoPaginationResult, IReadOnlyCollection<T>)> ListAsync(int page, int pageSize);

        Task<bool> Exists(Expression<Func<T, bool>> expression);
        

    }
}
