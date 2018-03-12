using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

using Openwork.Persistence;
using Openwork.Persistence.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenWork.Persistence
{
    public class PersistenceFacade
    {
        private static PersistenceFacade _instance;
        private PersistenceFacade() { }

        public static PersistenceFacade Instance {
            get
            {
                if (_instance == null)
                    _instance = new PersistenceFacade();

                return _instance;
            }
        }




        public T GetById<T>(object id) where T : LayerSupertype
        {
            IRepository<T> rep = RepositoryFactory.RepositoryOf<T>();
            return rep.GetById(id);
        }

        public void SaveOrUpdate<T>(T entity) where T : LayerSupertype
        {
            IRepository<T> rep = RepositoryFactory.RepositoryOf<T>();
            rep.SaveOrUpdate(entity);
        }
        public void Delete<T>(T entity) where T : LayerSupertype
        {
            IRepository<T> rep = RepositoryFactory.RepositoryOf<T>();
            rep.Delete(entity);
        }
        public IList<T> Find<T>(string whereClause) where T : LayerSupertype
        {
            IRepository<T> rep = RepositoryFactory.RepositoryOf<T>();
            return rep.Find(whereClause);
        }


    }
}
