using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TelegramNet.Entities.Interfaces;
using TelegramNet.Helpers;
using TelegramNet.Services;
using TelegramNet.Types;

namespace TelegramNet.Entities
{
    public class TelegramMessage : ITelegramMessage
    {
        internal TelegramMessage(BaseTelegramClient client, Message message)
        {
            #region ~Realization of entity~

            Id = message.Id;
            Author = message.Author != null ? new TelegramUser(client, message.Author) : default;
            ForwardFrom = message.ForwardFrom != null ? new TelegramUser(client, message.ForwardFrom) : default;
            ForwardFromChat = message.ForwardFromChat != null
                ? new TelegramChat(client, message.ForwardFromChat)
                : default;
            SenderChat = message.SenderChat != null ? new TelegramChat(client, message.SenderChat) : default;
            Chat = message.Chat != null ? new TelegramChat(client, message.Chat) : default;
            Timestamp = message.Date != 0 ? UnixParser.Parse(message.Date) : default;
            Captions = message.CaptionEntities != null
                ? message.CaptionEntities.Select(x => new MessageCaption(x, message.Text))
                : new List<MessageCaption>();
            Text = !string.IsNullOrEmpty(message.Text) ? message.Text : default;

            #endregion

            _client = client.TelegramApi;
        }

        private readonly TelegramApiClient _client;

        public int Id { get; }
        public ITelegramUser Author { get; }
        public ITelegramChat SenderChat { get; }
        public DateTime Timestamp { get; }
        public string Text { get; }
        public ITelegramChat Chat { get; }
        public ITelegramUser ForwardFrom { get; }
        public ITelegramChat ForwardFromChat { get; }
        public IEnumerable<MessageCaption> Captions { get; }

        public async Task<bool> DeleteAsync()
        {
            var result = await _client.RequestAsync("deleteMessage", HttpMethod.Post, new Dictionary<string, object>
            {
                {"chat_id", Chat.Id.Id},
                {"message_id", Id}
            }.ToJson());

            return result.Ok;
        }
    }
}