using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramNet.Services.HTTP.Entities
{
    public class UriData
    {
        public UriData(string methodName, IDictionary<string, object> parameters = null)
        {
            _method = methodName;
            _parameters = parameters;
        }

        private readonly string _method;
        private readonly IDictionary<string, object> _parameters;

        internal Uri Build(string token)
        {
            var url = LinkBuilder.Build(token, _method).ToString();
            var sb = new StringBuilder();

            sb.Append("?");

            foreach (var parameter in _parameters) sb.Append($"{parameter.Key}={parameter.Value}&");

            return new Uri(url + (_parameters.Count > 0 ? sb.ToString() : string.Empty));
        }
    }
}