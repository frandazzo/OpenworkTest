using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Openwork.Persistence
{
    internal class HibernateSessionFactoryHelper
    {
        private static HibernateSessionFactoryHelper _instance;
        private ISessionFactory _sessionFactory;
        private static Object _lockObject = new object();


        private Configuration _configuration;

        public static HibernateSessionFactoryHelper Instance
        {
            get {
                lock (_lockObject)
                {
                    if (_instance == null)
                        _instance = new HibernateSessionFactoryHelper();
                    return _instance;
                }
            }
           
        }

        private HibernateSessionFactoryHelper() {

            _configuration = new Configuration();
            _configuration.Configure();
            //portare in un file di configurazione 
            _configuration.AddAssembly("Openwork.Domain");
            _configuration.AddAssembly(Assembly.GetExecutingAssembly());

            _sessionFactory = _configuration.BuildSessionFactory();


        }


        //public ISession CurrentSession
        //{
        //    get
        //    {


        //        if (!CurrentSessionContext.HasBind(_sessionFactory))
        //            CurrentSessionContext.Bind(_sessionFactory.OpenSession());

        //        return _sessionFactory.GetCurrentSession();
        //    }
        //}

        public ISession Session
        {
            get
            {




                return _sessionFactory.OpenSession();
            }
        }

        //public  void DisposeCurrentSession()
        //{
        //    ISession currentSession = CurrentSessionContext.Unbind(_sessionFactory);

        //    currentSession.Close();
        //    currentSession.Dispose();
        //}







    }
}
