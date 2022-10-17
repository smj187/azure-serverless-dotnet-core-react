using BuildingBlocks.Domain;
using BuildingBlocks.Mongo.Helpers;
using BuildingBlocks.Mongo.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Mongo.Repositories
{
    public class MongoRepository<T> : IMongoRepository<T> where T : AggregateBase
    {
        private readonly IMongoCommandRepository<T> _commandRepository;
        private readonly IMongoQueryRepository<T> _queryRepository;

        public MongoRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("MongoConnectionString");
            var databaseName = configuration.GetValue<string>("MongoDatabase");

            _commandRepository = new MongoCommandRepository<T>(connectionString, databaseName);
            _queryRepository = new MongoQueryRepository<T>(connectionString, databaseName);
        }

        public async Task<T> AddAsync(T entity)
        {
            return await _commandRepository.AddAsync(entity);
        }

        public async Task<long> CountAsync()
        {
            return await _queryRepository.CountAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _commandRepository.DeleteAsync(id);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            return await _commandRepository.DeleteAsync(entity);
        }

        public async Task<bool> DeleteManyAsync(IEnumerable<Guid> ids)
        {
            return await _commandRepository.DeleteManyAsync(ids);
        }

        public async Task<T?> FindAsync(Guid id)
        {
            return await _queryRepository.FindAsync(id);
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _queryRepository.FindAsync(expression);
        }

        public async Task<IReadOnlyCollection<T>> ListAsync()
        {
            return await _queryRepository.ListAsync();
        }

        public async Task<IReadOnlyCollection<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            return await _queryRepository.ListAsync(expression);
        }

        public async Task<(MongoPaginationResult mongoPaginationResult, IReadOnlyCollection<T>)> ListAsync(int page, int pageSize)
        {
            return await _queryRepository.ListAsync(page, pageSize);
        }

        public async Task<IReadOnlyCollection<T>> ListAsync(List<Guid> includes)
        {
            return await _queryRepository.ListAsync(includes);
        }

        public async Task<T> PatchAsync(T entity)
        {
            return await _commandRepository.PatchAsync(entity);
        }
    }
}
