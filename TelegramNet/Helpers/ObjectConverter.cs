using System.Collections.Generic;
using System.Text.Json;

namespace TelegramNet.Helpers
{
    internal static class ObjectConverter
    {
        public static Dictionary<string, object> ToDictionary<T>(this T entity)
        {
            var type = typeof(T);

            var props = type.GetProperties();

            var vals = new Dictionary<string, object>();

            foreach (var t in props)
            {
	            vals.Add(t.Name, t.GetValue(entity));
            }

            return vals;
        }

        public static string ToJson<T>(this T entity)
        {
            return JsonSerializer.Serialize(entity);
        }
    }
}