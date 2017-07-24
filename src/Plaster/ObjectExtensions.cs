using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DocumentTable;

namespace Plaster
{
    internal static class ObjectExtensions
    {
        public static Document GetDocument<T>(this T obj, string id)
        {
            if(!typeof(T).GetTypeInfo().IsClass)
                throw new NotSupportedException($"{typeof(T)} is not supported.");

            var fields = new List<Field>();

            if (obj != null)
            {
                foreach (var prop in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    var val = prop.GetValue(obj);
                    if (val != null) fields.Add(new Field(prop.Name, val));
                }
            }

            if (id == null)
            {
                var prop = typeof(T).GetProperties().FirstOrDefault(p => p.Name.ToLower() == "id");
                if (prop != null) id = prop.GetValue(obj).ToString();
            }

            if(string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            fields.Add(new Field("_id", id));
            
            return new Document(fields);
        }
    }
}