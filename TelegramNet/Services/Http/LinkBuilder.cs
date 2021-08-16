using System;
using System.Collections.Generic;
using System.Linq;

namespace TelegramNet.Services.Http
{
	internal static class LinkBuilder
	{
		public static Uri Build(string token, string method, IDictionary<string, object>? queryParameters)
		{
			var enumerableOfTupleParameters = queryParameters
				?.Select(pair => (pair.Key, pair.Value.ToString() ?? string.Empty));

			return Build(token, method, enumerableOfTupleParameters);
		}

		public static Uri Build(string token, string method, IReadOnlyDictionary<string, string>? queryParameters)
		{
			var enumerableOfTupleParameters = queryParameters?.Select(pair => (pair.Key, pair.Value));

			return Build(token, method, enumerableOfTupleParameters);
		}

		public static Uri Build(string token, string method, IEnumerable<(string Key, string Value)>? queryParameters)
		{
			var queryString = ConcatenateQueryStringParameters(queryParameters);

			return Build(token, method, queryString);
		}

		public static Uri Build(string token, string method, string? queryString = null)
		{
			queryString = FormatQueryString(queryString);

			return new Uri($"https://api.telegram.org/bot{token}/{method}{queryString}");
		}

		private static string ConcatenateQueryStringParameters(IEnumerable<(string Key, string Value)>? parameters)
		{
			if (parameters == null)
			{
				return string.Empty;
			}

			var parameterParts = parameters.Select(parameter => $@"{parameter.Key}={parameter.Value}");

			return string.Join('&', parameterParts);
		}

		private static string FormatQueryString(string? queryString)
		{
			if (string.IsNullOrWhiteSpace(queryString))
			{
				return string.Empty;
			}

			return queryString.StartsWith('?')
				? queryString
				: $@"?{queryString}";
		}
	}
}