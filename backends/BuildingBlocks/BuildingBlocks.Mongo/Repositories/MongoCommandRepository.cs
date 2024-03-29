﻿using BuildingBlocks.Domain;
using BuildingBlocks.Mongo.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Mongo.Repositories
{
    public class MongoCommandRepository<T> : IMongoCommandRepository<T> where T : AggregateBase
    {
        private readonly IMongoCollection<T> _mongoCollection;
        private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;

        public MongoCommandRepository(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            //var pl = new EnglishPluralizationService();
            //var collectionName = pl.Pluralize(typeof(T).Name.ToLower());

            _mongoCollection = database.GetCollection<T>(typeof(T).Name.ToLower());
        }

        public async Task<T> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _mongoCollection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var filter = _filterBuilder.Eq(x => x.Id, id);
            var res = await _mongoCollection.DeleteOneAsync(filter);
            return res.IsAcknowledged;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            var filter = _filterBuilder.Eq(x => x.Id, entity.Id);
            var res = await _mongoCollection.DeleteOneAsync(filter);
            return res.IsAcknowledged;
        }

        public async Task<bool> DeleteManyAsync(IEnumerable<Guid> ids)
        {
            var filter = _filterBuilder.In(x => x.Id, ids);
            var result = await _mongoCollection.DeleteManyAsync(filter);
            return result.IsAcknowledged;
        }

        public async Task<T> PatchAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var filter = _filterBuilder.Eq(x => x.Id, entity.Id);
            await _mongoCollection.ReplaceOneAsync(filter, entity);
            return entity;
        }

    }
}
