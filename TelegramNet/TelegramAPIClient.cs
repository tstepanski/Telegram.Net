using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TelegramNet.Services.Http;
using TelegramNet.Services.Http.Entities;

namespace TelegramNet
{
	internal sealed class TelegramApiClient
	{
		private readonly HttpRequester _requester;

		public TelegramApiClient(string token, ILogger logger)
		{
			_requester = new HttpRequester(token, logger);
		}

		public async Task<T> RequestAsync<T>(string route, HttpMethod method, string? json = null)
		{
			return await Request<T>(route, method, json);
		}

		public async Task<RequestResult<T>> BaseRequestAsync<T>(string route, HttpMethod method, string? json = null)
		{
			return await _requester.ExecuteMethodAsync<T>(route, method, json);
		}

		public async Task<HttpResult> RequestAsync(string route, HttpMethod method, string? json = null)
		{
			return await _requester.ExecuteMethodAsync(route, method, json);
		}

		private async Task<T> Request<T>(string method, HttpMethod httpMethod, string? json = null)
		{
			return (await BaseRequestAsync<T>(method, httpMethod, json)).EnsureSuccess();
		}
	}
}