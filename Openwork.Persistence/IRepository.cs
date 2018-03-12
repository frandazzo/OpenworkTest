using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Openwork.Persistence
{
    public interface IRepository<T>
    {
        T GetById(object id);
        void SaveOrUpdate(T entity);
        void Delete(T entity);
        IList<T> Find(string whereClause);

    }
}
