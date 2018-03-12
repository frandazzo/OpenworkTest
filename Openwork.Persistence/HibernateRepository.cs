using NHibernate;
using NHibernate.Persister.Entity;
using Openwork.Persistence.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Openwork.Persistence
{
    public class HibernateRepository<T> : IRepository<T> where T : LayerSupertype
    {
        public void Delete(T entity)
        {
            using (ISession session = HibernateSessionFactoryHelper.Instance.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(entity);
                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public IList<T> Find(string query)
        {

            using (ISession session = HibernateSessionFactoryHelper.Instance.Session)
            {
                //ottengo il nome della tabella 
                string table = ((ILockable)session.GetSessionImplementation()
                   .GetEntityPersister(null, Activator.CreateInstance<T>())).RootTableName;

                string q = "select * from " + table;
                if (!string.IsNullOrEmpty(query))
                    q = q + " where " + query;


                return session.CreateSQLQuery(q).AddEntity(typeof(T)).List<T>();
            }
        }

        public T GetById(object id)
        {
            using (ISession session = HibernateSessionFactoryHelper.Instance.Session)
            {
                return session.Get<T>(id);
            }
        }

        public void SaveOrUpdate(T entity)
        {
           
            using (ISession session = HibernateSessionFactoryHelper.Instance.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate(entity);
                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
    }
}
