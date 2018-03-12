using MongoDB.Bson;
using MongoDB.Driver;

using Openwork.Persistence.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Openwork.Persistence
{
    public class MongoRepository<T> : IRepository<T> where T : LayerSupertype//, NoSqlDocument
    {
        public void Delete(T entity)
        {
            IMongoCollection<T> c = GetCollection();

            c.DeleteOne<T>(x => x.Id.Equals(entity.Id));

        }

        private static IMongoCollection<T> GetCollection()
        {
            return MongoSessionFactoryHelper.Instance.Session.GetCollection<T>(typeof(T).Name);
        }

        public IList<T> Find(string query)
        {
            IMongoCollection<T> c = GetCollection();
            return c.Find(new BsonDocument()).ToList<T>();
        }

        public T GetById(object id)
        {
            IMongoCollection<T> c = GetCollection();
            return c.Find<T>(x => x.Id.Equals(id)).FirstOrDefault<T>();
        }

        public void SaveOrUpdate(T entity)
        {
            IMongoCollection<T> c = GetCollection();


            if (entity.Id == null)
            {
                c.InsertOne(entity);
                return;
            }

            var filter = Builders<T>.Filter.Eq(s => s.Id, entity.Id);
            c.ReplaceOne(filter, entity);

        }
    }
}
