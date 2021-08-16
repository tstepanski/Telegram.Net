using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TelegramNet.Services.Http.Entities;

namespace TelegramNet.Services.Http
{
	public sealed class HttpRequester
	{
		private readonly HttpClient _client;
		private readonly ILogger _logger;
		private readonly string _token;

		public HttpRequester(string token, ILogger logger)
		{
			_client = new HttpClient();
			_token = token;
			_logger = logger;
		}

		internal async Task<HttpResult> ExecuteMethodAsync(string methodName, HttpMethod method, string? json = null)
		{
			if (methodName == null)
			{
				throw new ArgumentNullException(nameof(methodName));
			}

			if (method == null)
			{
				throw new ArgumentNullException(nameof(method));
			}

			try
			{
				var uri = LinkBuilder.Build(_token, methodName);
				var message = new HttpRequestMessage { Method = method, RequestUri = uri };


				if (!string.IsNullOrEmpty(json))
				{
					var messageContent = new StringContent(json);

					message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					messageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
					message.Content = messageContent;
				}

				var response = await _client.SendAsync(message);
				var responseContent = await response.Content.ReadAsStringAsync();

				return DeserializeHttpResult(responseContent);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, $@"Failed executing {methodName}");

				return new HttpResult
				{
					Ok = false,
					Description = $@"EXCEPTION\n{exception}"
				};
			}
		}

		internal async Task<HttpResult> ExecuteMethodAsync(UriData data, HttpMethod method)
		{
			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}

			if (method == null)
			{
				throw new ArgumentNullException(nameof(method));
			}

			try
			{
				var uri = data.Build(_token);
				var message = new HttpRequestMessage { Method = method, RequestUri = uri };
				var response = await _client.SendAsync(message);
				var responseContent = await response.Content.ReadAsStringAsync();

				return DeserializeHttpResult(responseContent);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, $@"Failed executing method by uri {data.Build(_token)}.");

				return new HttpResult
				{
					Ok = false,
					Description = $@"EXCEPTION\n{exception}"
				};
			}
		}

		internal HttpResult ExecuteMethod(string methodName, HttpMethod method, string? json = null)
		{
			if (method == null)
			{
				throw new ArgumentNullException(nameof(method));
			}

			if (methodName == null)
			{
				throw new ArgumentNullException(nameof(methodName));
			}

			try
			{
				var uri = LinkBuilder.Build(_token, methodName);
				var message = new HttpRequestMessage { Method = method, RequestUri = uri };

				if (!string.IsNullOrEmpty(json))
				{
					var messageContent = new StringContent(json);

					message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					messageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
					message.Content = messageContent;
				}

				var response = _client.Send(message);
				byte[] contentBytes;

				using (var stream = response.Content.ReadAsStream())
				{
					contentBytes = new byte[stream.Length];

					stream.Read(contentBytes, 0, contentBytes.Length);
				}

				var responseContent = Encoding.UTF8.GetString(contentBytes);

				return DeserializeHttpResult(responseContent);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, $@"Failed executing {methodName}");

				return new HttpResult
				{
					Ok = false,
					Description = $"EXCEPTION\n{exception}"
				};
			}
		}

		internal HttpResult ExecuteMethod(UriData data, HttpMethod method)
		{
			if (method == null)
			{
				throw new ArgumentNullException(nameof(method));
			}

			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}

			try
			{
				var uri = data.Build(_token);
				var message = new HttpRequestMessage { Method = method, RequestUri = uri };
				var response = _client.Send(message);
				byte[] contentBytes;

				using (var stream = response.Content.ReadAsStream())
				{
					contentBytes = new byte[stream.Length];

					stream.Read(contentBytes, 0, contentBytes.Length);
				}

				var responseContent = Encoding.UTF8.GetString(contentBytes);

				return DeserializeHttpResult(responseContent);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, $@"Failed executing method by uri {data.Build(_token)}");

				return new HttpResult
				{
					Ok = false,
					Description = $"EXCEPTION\n{exception}"
				};
			}
		}

		private static HttpResult DeserializeHttpResult(string responseContent)
		{
			return JsonSerializer.Deserialize<HttpResult>(responseContent) ??
			       throw new InvalidOperationException($@"Failed to deserialize {nameof(HttpResult)}");
		}

		public RequestResult<T> ExecuteMethod<T>(string methodName, HttpMethod method, string? json = null)
		{
			var result = ExecuteMethod(methodName, method, json);

			return new RequestResult<T>(result);
		}

		public RequestResult<T> ExecuteMethod<T>(UriData data, HttpMethod method)
		{
			return new RequestResult<T>(ExecuteMethod(data, method));
		}

		/// <summary>
		/// Executes Telegram's API method.
		/// </summary>
		/// <param name="methodName">Method name</param>
		/// <param name="method">GET, POST.</param>
		/// <param name="json">JSON body</param>
		public async Task<RequestResult<T>> ExecuteMethodAsync<T>(string methodName, HttpMethod method,
			string? json = null)
		{
			return new RequestResult<T>(await ExecuteMethodAsync(methodName, method, json));
		}

		public async Task<RequestResult<T>> ExecuteMethodAsync<T>(UriData data, HttpMethod method)
		{
			return new RequestResult<T>(await ExecuteMethodAsync(data, method));
		}
	}
}