using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Openwork.Persistence
{
    public class MongoSessionFactoryHelper
    {
        private static MongoSessionFactoryHelper _instance;
        private IMongoClient _sessionFactory;
        private static Object _lockObject = new object();
        private string _currentDB;

        

        public static MongoSessionFactoryHelper Instance
        {
            get
            {
                lock (_lockObject)
                {
                    if (_instance == null)
                        _instance = new MongoSessionFactoryHelper();
                    return _instance;
                }
            }

        }

        private MongoSessionFactoryHelper()
        {

            //qui posso configuerare il nome del db dal database
            //prendendolo dal file di configurazione
            _currentDB = "test";
            _sessionFactory = new MongoClient();
           

        }

        public IMongoClient SessionFactory
        {
            get
            {
                return _sessionFactory;
            }
        }


        public IMongoDatabase Session
        {
            get
            {
                return _sessionFactory.GetDatabase(_currentDB);
            }
        }
    
    }
}
