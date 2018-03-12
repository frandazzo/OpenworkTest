using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Openwork.Persistence.Middleware
{
    public class LayerSupertype
    {
        
        [BsonIgnoreIfDefault]
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public virtual object Id { get; set; }
        public virtual string Description { get; set; }
        public virtual string Name { get; set; }
        public virtual string RepositoryId { get; set; }
        public virtual string Number { get; set; }


    }
}
