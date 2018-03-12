using Openwork.Persistence.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Openwork.Persistence
{
    internal class RepositoryFactory
    {
        internal static IRepository<T> RepositoryOf<T>() where T : LayerSupertype
        {
            if (typeof(NoSqlDocument).IsAssignableFrom(typeof(T)))
                return new MongoRepository<T>();
            return new HibernateRepository<T>();
        }
    }
}
