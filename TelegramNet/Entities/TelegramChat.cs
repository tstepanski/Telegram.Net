using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TelegramNet.Entities.Interfaces;
using TelegramNet.ExtraTypes;
using TelegramNet.Helpers;
using TelegramNet.Types;

namespace TelegramNet.Entities
{
    public class TelegramChat : ITelegramChat
    {
        internal TelegramChat(BaseTelegramClient client, Chat chat)
        {
            if (chat != null)
            {
                #region ~Realization of entity

                Id = chat.Id;
                Type = chat.Type ?? string.Empty;
                Title = chat.Title ?? string.Empty;
                Username = chat.Username ?? string.Empty;
                FirstName = chat.FirstName ?? string.Empty;
                LastName = chat.LastName ?? string.Empty;

                #endregion    
            }
            else
            {
                throw new InvalidOperationException(GitHubIssueBuilder.Message(
                    $"Exception while initializing {nameof(TelegramChat)}.", nameof(InvalidOperationException),
                    $"**Description:**\nDescribe your problem here.\n**StackTrace:**\n{Environment.StackTrace}"));
            }

            _tgClient = client;
            _client = client.TelegramApi;
        }

        private readonly BaseTelegramClient _tgClient;
        private readonly TelegramApiClient _client;

        public ChatId Id { get; }
        public string Type { get; }
        public Optional<string> Title { get; }
        public Optional<string> Username { get; }
        public Optional<string> FirstName { get; }
        public Optional<string> LastName { get; }

        public async Task<TelegramClientMessage> SendMessageAsync(string text)
        {
            var message = await _client.RequestAsync<Message>("sendMessage", HttpMethod.Post,
                new Dictionary<string, object>
                {
                    {"chat_id", Id.Fetch()},
                    {"text", text}
                }.ToJson());

            return new TelegramClientMessage(_tgClient, message);
        }
    }
}