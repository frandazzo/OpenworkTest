using NHibernate;
using NHibernate.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using System.Data.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Openwork.Persistence
{

    internal class JsonMappableType : IUserType
    {
        public object Assemble(object cached, object owner)
        {
            //Used for casching, as our object is immutable we can just return it as is
            return cached;
        }
        public object DeepCopy(object value)
        {
            //We deep copy the Translation by creating a new instance with the same contents
            if (value == null)
                return null;
            if (value.GetType() != typeof(Dictionary<string, object>))
                return null;
            return FromJson(ToJson((Dictionary<string, object>)value));
        }


        public object Disassemble(object value)
        {
            //Used for casching, as our object is immutable we can just return it as is
            return value;
        }
        public new bool Equals(object x, object y)
        {
            //Use json-query-string to see if their equal 
            // on value so we use this implementation
            if (x == null || y == null)
                return false;
            if (x.GetType() != typeof(Dictionary<string, object>) || y.GetType() != typeof(Dictionary<string, object>))
                return false;
            return ToJson((Dictionary<string, object>)x).Equals(ToJson((Dictionary<string, object>)y));
        }
        public int GetHashCode(object x)
        {
            if (x != null && x.GetType() == typeof(Dictionary<string, object>))
                return ToJson((Dictionary<string, object>)x).GetHashCode();
            return x.GetHashCode();
        }
        public bool IsMutable
        {
            get { return false; }
        }

        public void NullSafeSet(DbCommand cmd, object value, int index,  ISessionImplementor session)
        {
            //Set the value using the NullSafeSet implementation for string from NHibernateUtil
            if (value == null)
            {
                NHibernateUtil.String.NullSafeSet(cmd, null, index, session);
                return;
            }
            value = ToJson((Dictionary<string, object>)value);
            NHibernateUtil.String.NullSafeSet(cmd, value, index, session);
        }
        public object Replace(object original, object target, object owner)
        {
            //As our object is immutable we can just return the original
            return original;
        }
        public Type ReturnedType
        {
            get { return typeof(Dictionary<string, object>); }
        }

        public SqlType[] SqlTypes
        {
            get
            {
                //We store our translation in a single column in the database that can contain a string
                SqlType[] types = new SqlType[1];
                types[0] =  new StringClobSqlType();// new SqlType(System.Data.DbType.String);
                return types;
            }
        }
        private static JsonSerializerSettings jsonSetting = new JsonSerializerSettings
        {
            ContractResolver = new OrderedContractResolver(),
            Formatting = Formatting.None,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate

        };
        public static Dictionary<string, object> FromJson(string jsonString)
        {
            if (!string.IsNullOrWhiteSpace(jsonString))
                return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString, jsonSetting);
            return Activator.CreateInstance<Dictionary<string, object>>();
        }

        public static string ToJson(Dictionary<string, object> obj)
        {
            return JsonConvert.SerializeObject(obj, jsonSetting);
        }


        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            //We get the string from the database using the NullSafeGet used to get strings 
            string jsonString = (string)NHibernateUtil.String.NullSafeGet(rs, names[0], session);
            //And save it in the T object. This would be the place to make sure that your string 
            //is valid for use with the T class
            return FromJson(jsonString);
        }



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
