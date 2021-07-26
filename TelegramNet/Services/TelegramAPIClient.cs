using System.Net.Http;
using System.Threading.Tasks;
using TelegramNet.Services.HTTP.Entities;
using TelegramNet.Services.ReceivingUpdates;

namespace TelegramNet.Services
{
    internal class TelegramApiClient
    {
        public TelegramApiClient(TelegramClient client)
        {
            _client = client;
        }

        private readonly TelegramClient _client;

        public async Task<Update[]> GetUpdatesAsync()
        {
            return await Request<Update[]>("getUpdates", HttpMethod.Get);
        }

        public async Task<T> RequestAsync<T>(string method, HttpMethod m, string json = null)
        {
            return await Request<T>(method, m, json);
        }

        public async Task<HttpResult> RequestAsync(string method, HttpMethod m, string json = null)
        {
            return await _client.ExecuteMethodAsync(method, m, json);
        }

        private async Task<T> Request<T>(string method, HttpMethod httpMethod, string json = null)
        {
            var result = await _client.ExecuteMethodAsync<T>(method, httpMethod, json);

            return result.EnsureSuccess();
        }
    }
}