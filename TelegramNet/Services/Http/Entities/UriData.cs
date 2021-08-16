using System;
using System.Collections.Generic;

namespace TelegramNet.Services.Http.Entities
{
	public sealed class UriData
	{
		private readonly string _method;
		private readonly IDictionary<string, object>? _parameters;

		public UriData(string methodName, IDictionary<string, object>? parameters = null)
		{
			_method = methodName;
			_parameters = parameters;
		}

		internal Uri Build(string token)
		{
			return LinkBuilder.Build(token, _method, _parameters);
		}
	}
}