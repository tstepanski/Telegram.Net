using System.Net.Http;
using System.Threading.Tasks;
using TelegramNet.Services.Http;
using TelegramNet.Services.Http.Entities;

namespace TelegramNet
{
    public class TelegramExtensionClient
    {
        private readonly TelegramApiClient _api;

        internal TelegramExtensionClient(BaseTelegramClient client)
        {
            _api = client.TelegramApi;
        }

        public async Task<RequestResult<T>> GetAsync<T>(string url, string json = null)
        {
            return await _api.BaseRequestAsync<T>(url, HttpMethod.Get, json);
        }

        public async Task<bool> PostAsync(string url, string json = null)
        {
            var req = await _api.RequestAsync(url, HttpMethod.Post, json);

            return req.Ok;
        }
    }
}