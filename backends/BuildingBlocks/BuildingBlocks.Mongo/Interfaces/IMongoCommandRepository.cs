using BuildingBlocks.Domain.Interfaces;
using BuildingBlocks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Mongo.Interfaces
{
    public interface IMongoCommandRepository<T> : ICommandRepository<T> where T : IAggregateBase
    {
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteManyAsync(IEnumerable<Guid> ids);
    }
}
