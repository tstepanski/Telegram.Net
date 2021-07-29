using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TelegramNet.Entities.Interfaces;
using TelegramNet.ExtraTypes;
using TelegramNet.Helpers;
using TelegramNet.Types;

namespace TelegramNet.Entities
{
    public class TelegramMessage : ITelegramMessage
    {
        internal TelegramMessage(BaseTelegramClient client, Message message)
        {
            if (message != null)
            {
                #region ~Realization of entity~

                Id = message.Id;
                Author = message.Author != null
                    ? new TelegramUser(client,
                        message.Author)
                    : default;
                ForwardFrom = message.ForwardFrom != null
                    ? new TelegramUser(client,
                        message.ForwardFrom)
                    : default;
                ForwardFromChat = message.ForwardFromChat != null
                    ? new TelegramChat(client,
                        message.ForwardFromChat)
                    : default;
                SenderChat = message.SenderChat != null
                    ? new TelegramChat(client,
                        message.SenderChat)
                    : default;
                Chat = new TelegramChat(client,
                    message.Chat); //
                Timestamp = message.Date != default
                    ? UnixParser.Parse(message.Date)
                    : default;
                Captions = message.CaptionEntities?.Select(x => new MessageCaption(client,
                        x,
                        message.Text))
                    .ToArray();
                Text = message.Text;
                ForwardFromMessageId = message.ForwardFromMessageId != default
                    ? message.ForwardFromMessageId
                    : default;
                ForwardSignature = message.ForwardSignature;
                ForwardSenderName = message.ForwardSenderName;
                ForwardDate = message.ForwardDate != default
                    ? UnixParser.Parse(message.ForwardDate)
                    : default;
                ReplyToMessage = message.ReplyToMessage != null
                    ? new TelegramMessage(client,
                        message.ReplyToMessage)
                    : default;
                ViaBot = message.ViaBot != null
                    ? new TelegramUser(client,
                        message.ViaBot)
                    : default;
                EditDate = message.EditDate != default
                    ? UnixParser.Parse(message.EditDate)
                    : default;
                MediaGroupId = message.MediaGroupId;
                AuthorSignature = message.AuthorSignature;
                Entities = message.Entities?.Select(x => new MessageCaption(client,
                        x,
                        message.Text))
                    .ToArray();

                #endregion
            }
            else
            {
               throw new InvalidOperationException(GitHubIssueBuilder.Message(
                                   $"Exception while initializing {nameof(TelegramMessage)}.", nameof(InvalidOperationException),
                                   $"**Description:**\nDescribe your problem here.\n**StackTrace:**\n{Environment.StackTrace}"));
            }
            
            _client = client.TelegramApi;
        }

        private readonly TelegramApiClient _client;

        public int Id { get; }
        public Optional<ITelegramUser> Author { get; }
        public Optional<ITelegramChat> SenderChat { get; }
        public Optional<DateTime> Timestamp { get; }
        public Optional<string> Text { get; }
        public ITelegramChat Chat { get; }
        public Optional<ITelegramUser> ForwardFrom { get; }
        public Optional<ITelegramChat> ForwardFromChat { get; }
        public Optional<IEnumerable<MessageCaption>> Captions { get; }

        public Optional<int> ForwardFromMessageId { get; }

        public Optional<string> ForwardSignature { get; }

        public Optional<string> ForwardSenderName { get; }

        public Optional<DateTime> ForwardDate { get; }

        public Optional<ITelegramMessage> ReplyToMessage { get; }

        public Optional<ITelegramUser> ViaBot { get; }

        public Optional<DateTime> EditDate { get; }

        public Optional<string> MediaGroupId { get; }

        public Optional<string> AuthorSignature { get; }

        public Optional<IEnumerable<MessageCaption>> Entities { get; }

        public async Task<bool> DeleteAsync()
        {
            var result = await _client.RequestAsync("deleteMessage", HttpMethod.Post, new Dictionary<string, object>
            {
                {"chat_id", Chat.Id.Fetch()},
                {"message_id", Id}
            }.ToJson());

            return result.Ok;
        }
    }
}