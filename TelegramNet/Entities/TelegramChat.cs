using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TelegramNet.Entities.Interfaces;
using TelegramNet.Helpers;
using TelegramNet.Services;
using TelegramNet.Types;

namespace TelegramNet.Entities
{
    public class TelegramChat : ITelegramChat
    {
        internal TelegramChat(BaseTelegramClient client, Chat chat)
        {
            #region ~Realization of entity

            Id = chat.Id;
            Type = chat.Type ?? string.Empty;
            Title = chat.Title ?? string.Empty;
            Username = chat.Username ?? string.Empty;
            FirstName = chat.FirstName ?? string.Empty;
            LastName = chat.LastName ?? string.Empty;

            #endregion

            _tgClient = client;
            _client = client.TelegramApi;
        }

        private readonly BaseTelegramClient _tgClient;
        private readonly TelegramApiClient _client;

        public ChatId Id { get; }
        public string Type { get; }
        public string Title { get; }
        public string Username { get; }
        public string FirstName { get; }
        public string LastName { get; }

        public async Task<TelegramClientMessage> SendMessageAsync(string text)
        {
            var parseAble = int.TryParse(Id.Id, out var id);

            var message = await _client.RequestAsync<Message>("sendMessage", HttpMethod.Post,
                new Dictionary<string, object>
                {
                    {"chat_id", parseAble ? id : Id.Id},
                    {"text", text}
                }.ToJson());

            return new TelegramClientMessage(_tgClient, message);
        }
    }
}