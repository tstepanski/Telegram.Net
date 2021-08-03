using System.Net.Http;
using System.Threading.Tasks;
using TelegramNet.Services.Http;
using TelegramNet.Services.Http.Entities;

namespace TelegramNet
{
    internal class TelegramApiClient
    {
        public TelegramApiClient(string token)
        {
            _requester = new HttpRequester(token);
        }

        private readonly HttpRequester _requester;

        public async Task<T> RequestAsync<T>(string method, HttpMethod m, string json = null)
        {
            return await Request<T>(method, m, json);
        }

        public async Task<RequestResult<T>> BaseRequestAsync<T>(string method, HttpMethod m, string json = null)
        {
            return await _requester.ExecuteMethodAsync<T>(method, m, json);
        }

        public async Task<HttpResult> RequestAsync(string method, HttpMethod m, string json = null)
        {
            return await _requester.ExecuteMethodAsync(method, m, json);
        }

        private async Task<T> Request<T>(string method, HttpMethod httpMethod, string json = null)
        {
            return (await BaseRequestAsync<T>(method, httpMethod, json)).EnsureSuccess();
        }
    }
}