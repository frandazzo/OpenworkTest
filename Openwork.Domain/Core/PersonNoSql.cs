using Openwork.Persistence.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Openwork.Domain.Core
{
    public class PersonNoSql : LayerSupertype, NoSqlDocument
    {
        public virtual Dictionary<string, object> Fields { get; set; }
    }
}
