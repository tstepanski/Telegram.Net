using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TelegramNet.Services.HTTP.Entities;

namespace TelegramNet.Services.HTTP
{
    public class HttpRequester
    {
        private readonly HttpClient _client;
        private readonly string _token;

        public HttpRequester(string token)
        {
            _client = new HttpClient();
            _token = token;
        }

        internal async Task<HttpResult> ExecuteMethodAsync(string methodName, HttpMethod method, string json = null)
        {
            if (methodName == null) throw new ArgumentNullException(nameof(methodName));
            if (method == null) throw new ArgumentNullException(nameof(method));

            try
            {
                var uri = LinkBuilder.Build(_token, methodName);
                var message = new HttpRequestMessage {Method = method, RequestUri = uri};


                if (!string.IsNullOrEmpty(json))
                {
                    var content = new StringContent(json);
                    message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    message.Content = content;
                }

                var response = await _client.SendAsync(message);

                var cont = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<HttpResult>(cont);
            }
            catch (Exception e)
            {
                return new HttpResult {Ok = false, Description = $"EXCEPTION\n{e}"};
            }
        }

        internal async Task<HttpResult> ExecuteMethodAsync(UriData data, HttpMethod method)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (method == null) throw new ArgumentNullException(nameof(method));

            try
            {
                var uri = data.Build(_token);
                var message = new HttpRequestMessage {Method = method, RequestUri = uri};

                var response = await _client.SendAsync(message);

                var cont = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<HttpResult>(cont);
            }
            catch (Exception e)
            {
                return new HttpResult {Ok = false, Description = $"EXCEPTION\n{e}"};
            }
        }

        internal HttpResult ExecuteMethod(string methodName, HttpMethod method, string json = null)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (methodName == null) throw new ArgumentNullException(nameof(methodName));

            try
            {
                var uri = LinkBuilder.Build(_token, methodName);
                var message = new HttpRequestMessage {Method = method, RequestUri = uri};

                if (!string.IsNullOrEmpty(json))
                {
                    var content = new StringContent(json);
                    message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    message.Content = content;
                }

                var response = _client.Send(message);

                using var stream = response.Content.ReadAsStream();

                var contentBytes = new byte[stream.Length];

                stream.Read(contentBytes, 0, contentBytes.Length);

                var cont = Encoding.UTF8.GetString(contentBytes);

                return JsonSerializer.Deserialize<HttpResult>(cont);
            }
            catch (Exception e)
            {
                return new HttpResult {Ok = false, Description = $"EXCEPTION\n{e}"};
            }
        }

        internal HttpResult ExecuteMethod(UriData data, HttpMethod method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (data == null) throw new ArgumentNullException(nameof(data));

            try
            {
                var uri = data.Build(_token);
                var message = new HttpRequestMessage {Method = method, RequestUri = uri};

                var response = _client.Send(message);

                using var stream = response.Content.ReadAsStream();

                var contentBytes = new byte[stream.Length];

                stream.Read(contentBytes, 0, contentBytes.Length);

                var cont = Encoding.UTF8.GetString(contentBytes);

                return JsonSerializer.Deserialize<HttpResult>(cont);
            }
            catch (Exception e)
            {
                return new HttpResult {Ok = false, Description = $"EXCEPTION\n{e}"};
            }
        }

        public RequestResult<T> ExecuteMethod<T>(string methodName, HttpMethod method, string json = null)
        {
            return new(ExecuteMethod(methodName, method, json));
        }

        public RequestResult<T> ExecuteMethod<T>(UriData data, HttpMethod method)
        {
            return new(ExecuteMethod(data, method));
        }

        /// <summary>
        /// Executes Telegram's API method.
        /// </summary>
        /// <param name="methodName">Method name</param>
        /// <param name="method">GET, POST.</param>
        /// <param name="json">JSON body</param>
        public async Task<RequestResult<T>> ExecuteMethodAsync<T>(string methodName, HttpMethod method,
            string json = null)
        {
            return new(await ExecuteMethodAsync(methodName, method, json));
        }

        public async Task<RequestResult<T>> ExecuteMethodAsync<T>(UriData data, HttpMethod method)
        {
            return new(await ExecuteMethodAsync(data, method));
        }
    }
}