using System.Net.Http;
using System.Threading.Tasks;
using TelegramNet.Services.Http.Entities;

namespace TelegramNet
{
    public class TelegramExtensionClient
    {
        private readonly TelegramClient _client;

        internal TelegramExtensionClient(TelegramClient client)
        {
            _client = client;
        }

        public async Task<RequestResult<T>> GetAsync<T>(string url, string json = null)
        {
            return await _client.ExecuteMethodAsync<T>(url, HttpMethod.Get, json);
        }

        public async Task<bool> PostAsync(string url, string json = null)
        {
            var req = await _client.ExecuteMethodAsync(url, HttpMethod.Post, json);

            return req.Ok;
        }
    }
}