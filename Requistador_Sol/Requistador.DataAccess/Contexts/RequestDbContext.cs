using LiteDB;
using Microsoft.Extensions.Options;
using Requistador.DataAccess.Extensions;
using Requistador.Domain.Base;
using Requistador.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Requistador.DataAccess.Contexts
{
    public class RequestDbContext
    {
        private readonly string _dbPath;
        private readonly string _collection;
        public readonly LiteDatabase Context;
        public RequestDbContext(IOptions<LiteDbConfig> configs)
        {
            try
            {
                //var db = new LiteDatabase(configs.Value.DatabasePath);
                //if (db != null)
                //    Context = db;
                _dbPath = configs.Value.DatabasePath;
                _collection = "clientRequests";
            }
            catch (Exception ex)
            {
                throw new Exception("Can find or create LiteDb database.", ex);
            }
        }

        public TEntity Get<TEntity>(int id) where TEntity : AppRequest<BaseEntity>
        {
            using (var db = new LiteDatabase(_dbPath))
            {
                var collection = db.GetCollection<TEntity>(_collection);
                var result = collection.FindById(id);
                
                return result;
            }
        }

        public TEntity Get<TEntity>(Query dbQuery) where TEntity : AppRequest<BaseEntity>
        {
            using (var db = new LiteDatabase(_dbPath))
            {
                var collection = db.GetCollection<TEntity>(_collection);
                var result = collection.FindOne(dbQuery);
                
                return result;
            }
        }

        public IEnumerable<TEntity> FindAll<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : AppRequest<BaseEntity>
        {
            using (var db = new LiteDatabase(_dbPath))
            {
                var collection = db.GetCollection<TEntity>(_collection);
                var result = collection.Find(predicate).ToList();
                
                return result;
            }
        }

        public Guid Insert<TEntity>(TEntity entity) where TEntity : AppRequest<BaseEntity>
        {
            BsonValue result;
            using (var db = new LiteDatabase(_dbPath))
            {
                var collection = db.GetCollection<TEntity>(_collection);
                result = collection.Insert(entity);
            }

            return result.AsGuid;
        }

        public void InsertMany<TEntity>(IEnumerable<TEntity> entity) where TEntity : AppRequest<BaseEntity>
        {
            using (var db = new LiteDatabase(_dbPath))
            {
                var collection = db.GetCollection<TEntity>(_collection);
                collection.InsertBulk(entity);
            }
        }
    }
}
