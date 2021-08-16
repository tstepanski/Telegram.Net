using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace TelegramNet.Helpers
{
	internal static class ObjectConverter
	{
		public static IReadOnlyDictionary<string, object?> ToDictionary<T>(this T entity)
		{
			return typeof(T)
				.GetProperties()
				.ToDictionary(property => property.Name, property => property.GetValue(entity));
		}

		public static string ToJson<T>(this T entity)
		{
			return JsonSerializer.Serialize(entity);
		}
	}
}