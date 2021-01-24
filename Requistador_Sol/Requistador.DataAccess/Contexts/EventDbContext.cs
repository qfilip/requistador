using LiteDB;
using Requistador.Domain.Base;
using Requistador.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Requistador.DataAccess.Contexts
{
    public class EventDbContext
    {
        private readonly string _dbPath;
        private readonly string _collection;
        public EventDbContext(string dbPath)
        {
            _collection = "client-requests";
            _dbPath = dbPath;
        }

        public TEntity Get<TEntity>(int id) where TEntity : ClientRequest<BaseEntity>
        {
            using (var db = new LiteDatabase(_dbPath))
            {
                var collection = db.GetCollection<TEntity>(_collection);
                var result = collection.FindById(id);
                
                return result;
            }
        }

        public TEntity Get<TEntity>(Query dbQuery) where TEntity : ClientRequest<BaseEntity>
        {
            using (var db = new LiteDatabase(_dbPath))
            {
                var collection = db.GetCollection<TEntity>(_collection);
                var result = collection.FindOne(dbQuery);
                
                return result;
            }
        }

        public IEnumerable<TEntity> FindAll<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : ClientRequest<BaseEntity>
        {
            using (var db = new LiteDatabase(_dbPath))
            {
                var collection = db.GetCollection<TEntity>(_collection);
                var result = collection.Find(predicate).ToList();
                
                return result;
            }
        }

        public void Insert<TEntity>(TEntity entity) where TEntity : ClientRequest<BaseEntity>
        {
            using (var db = new LiteDatabase(_dbPath))
            {
                var collection = db.GetCollection<TEntity>(_collection);
                collection.Insert(entity);
            }
        }
    }
}
