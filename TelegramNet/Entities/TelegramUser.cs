using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TelegramNet.Entities.Interfaces;
using TelegramNet.Helpers;
using TelegramNet.Services;
using TelegramNet.Types;

namespace TelegramNet.Entities
{
    public class TelegramUser : ITelegramUser
    {
        internal TelegramUser(BaseTelegramClient client, User user)
        {
            #region ~Realisation of entity~

            Id = user.Id;
            IsBot = user.IsBot;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            LanguageCode = user.LanguageCode;
            CanJoinGroups = user.CanJoinGroups;
            CanReadAllGroupMessages = user.CanReadAllGroupMessages;
            SupportsInlineQueries = user.SupportsInlineQueries;

            #endregion

            Mention = new UserMention(this);
            _tgClient = client;
            _client = client.TelegramApi;
        }

        private readonly BaseTelegramClient _tgClient;
        private readonly TelegramApiClient _client;

        public int Id { get; }
        public bool IsBot { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Username { get; }
        public string LanguageCode { get; }
        public bool CanJoinGroups { get; }
        public bool CanReadAllGroupMessages { get; }
        public bool SupportsInlineQueries { get; }
        public UserMention Mention { get; }

        public async Task<ITelegramMessage> SendPrivateMessageAsync(string text)
        {
            var message = await _client.RequestAsync<Message>("sendMessage", HttpMethod.Post,
                new Dictionary<string, object>
                {
                    {"chat_id", Id},
                    {"text", text}
                }.ToJson());

            return new TelegramMessage(_tgClient, message);
        }

        public override string ToString()
        {
            return $"User {Username} with id {Id}";
        }
    }
}