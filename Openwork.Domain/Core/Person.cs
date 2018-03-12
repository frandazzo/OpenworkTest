using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Openwork.Persistence.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Openwork.Domain.Core
{
    public class Person : LayerSupertype//, NoSqlDocument
    {
      

        public virtual Dictionary<string, object> Fields { get; set; }


        public virtual PersonNoSql CloneNoSqlPerson()
        {
            PersonNoSql p = new PersonNoSql();
            p.Description = this.Description;
            p.Name = this.Name;
            p.Number = this.Number;
            p.RepositoryId = this.RepositoryId;
            p.Fields = this.Fields;//CloneFields(this.Fields);


            return p;
        }

        private Dictionary<string, object> CloneFields(Dictionary<string, object> fields)
        {
            
            string json = JsonConvert.SerializeObject(fields, jsonSetting);
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json, jsonSetting);

        }

        private static JsonSerializerSettings jsonSetting = new JsonSerializerSettings
        {
            ContractResolver = new OrderedContractResolver(),
            Formatting = Formatting.None,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate

        };

        /// <summary>
        /// Order the json-properties to create a simple equals case for JsonMappableType.Equals
        /// </summary>
        private class OrderedContractResolver : DefaultContractResolver
        {
            protected override System.Collections.Generic.IList<JsonProperty> CreateProperties(System.Type type, MemberSerialization memberSerialization)
            {
                return base.CreateProperties(type, memberSerialization).OrderBy(p => p.PropertyName).ToList();
            }
        }
    }
}
