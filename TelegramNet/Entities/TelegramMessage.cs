using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TelegramNet.Entities.Interfaces;
using TelegramNet.Entities.Keyboards.Inlines;
using TelegramNet.Entities.Keyboards.Replies;
using TelegramNet.ExtraTypes;
using TelegramNet.Helpers;
using TelegramNet.Types;
using TelegramNet.Types.Inlines;
using TelegramNet.Types.Replies;

namespace TelegramNet.Entities
{
    public class TelegramMessage : ITelegramMessage
    {
        internal TelegramMessage(BaseTelegramClient client, ApiMessage apiMessage)
        {
            if (apiMessage != null)
            {
                #region ~Realization of entity~

                Id = apiMessage.Id;
                Author = apiMessage.Author != null
                    ? new TelegramUser(client,
                        apiMessage.Author)
                    : default;
                ForwardFrom = apiMessage.ForwardFrom != null
                    ? new TelegramUser(client,
                        apiMessage.ForwardFrom)
                    : default;
                ForwardFromChat = apiMessage.ForwardFromApiChat != null
                    ? new TelegramChat(client,
                        apiMessage.ForwardFromApiChat)
                    : default;
                SenderChat = apiMessage.SenderApiChat != null
                    ? new TelegramChat(client,
                        apiMessage.SenderApiChat)
                    : default;
                Chat = new TelegramChat(client,
                    apiMessage.ApiChat); //
                Timestamp = apiMessage.Date != default
                    ? UnixParser.Parse(apiMessage.Date)
                    : default;
                Captions = apiMessage.CaptionEntities?.Select(x => new MessageCaption(client,
                        x,
                        apiMessage.Text))
                    .ToArray();
                Text = apiMessage.Text;
                ForwardFromMessageId = apiMessage.ForwardFromMessageId != default
                    ? apiMessage.ForwardFromMessageId
                    : default;
                ForwardSignature = apiMessage.ForwardSignature;
                ForwardSenderName = apiMessage.ForwardSenderName;
                ForwardDate = apiMessage.ForwardDate != default
                    ? UnixParser.Parse(apiMessage.ForwardDate)
                    : default;
                ReplyToMessage = apiMessage.ReplyToApiMessage != null
                    ? new TelegramMessage(client,
                        apiMessage.ReplyToApiMessage)
                    : default;
                ViaBot = apiMessage.ViaBot != null
                    ? new TelegramUser(client,
                        apiMessage.ViaBot)
                    : default;
                EditDate = apiMessage.EditDate != default
                    ? UnixParser.Parse(apiMessage.EditDate)
                    : default;
                MediaGroupId = apiMessage.MediaGroupId;
                AuthorSignature = apiMessage.AuthorSignature;
                Entities = apiMessage.Entities?.Select(x => new MessageCaption(client,
                        x,
                        apiMessage.Text))
                    .ToArray();

                var serializedButtons = apiMessage.ReplyMarkup.ToJson();

                try
                {
                    var but = JsonSerializer.Deserialize<InlineObject>(serializedButtons);

                    if (but != null)
                    {
	                    InlineKeyboardMarkups = but.InlineKeyboard.Select(x => x.Select(z =>
				                    new InlineKeyboardButton(z.Text,
					                    new Uri(z.Url),
					                    new LoginUri(z.ApiLoginUrl.Url == null
							                    ? new Uri(z.ApiLoginUrl.Url ?? string.Empty)
							                    : null,
						                    z.ApiLoginUrl.ForwardText,
						                    z.ApiLoginUrl.BotUsername,
						                    z.ApiLoginUrl.RequestWriteAccess)))
			                    .ToArray())
		                    .ToArray();
                    }
                }
                catch (Exception)
                {
                    // ignored
                }

                try
                {
                    var but = JsonSerializer.Deserialize<ApiReplyKeyboardMarkup>(serializedButtons);

                    if (but != null)
                    {
	                    ReplyKeyboardMarkup = new ReplyKeyboardMarkup(but.Keyboard.Select(x => x
				                    .Select(z => new KeyboardButton(z.Text,
					                    z.RequestContact,
					                    z.RequestLocation,
					                    new KeyboardButtonPollType(z.RequestPoll.Type)))
				                    .ToArray())
			                    .ToArray(), but.ResizeKeyboard, but.OneTimeKeyboard, but.InputFieldPlaceHolder,
		                    but.Selective);
                    }
                }
                catch (Exception)
                {
                    // ignored
                }

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

        public Optional<TelegramUser> Author { get; }

        public Optional<TelegramChat> SenderChat { get; }

        public Optional<DateTime> Timestamp { get; }

        public Optional<string> Text { get; }

        public TelegramChat Chat { get; }

        public Optional<TelegramUser> ForwardFrom { get; }

        public Optional<TelegramChat> ForwardFromChat { get; }

        public Optional<IEnumerable<MessageCaption>> Captions { get; }

        public Optional<int> ForwardFromMessageId { get; }

        public Optional<string> ForwardSignature { get; }

        public Optional<string> ForwardSenderName { get; }

        public Optional<DateTime> ForwardDate { get; }

        public Optional<TelegramMessage> ReplyToMessage { get; }

        public Optional<TelegramUser> ViaBot { get; }

        public Optional<DateTime> EditDate { get; }

        public Optional<string> MediaGroupId { get; }

        public Optional<string> AuthorSignature { get; }

        public Optional<IEnumerable<MessageCaption>> Entities { get; }

        public InlineKeyboardButton[][] InlineKeyboardMarkups { get; }

        public ReplyKeyboardMarkup ReplyKeyboardMarkup { get; }

        public async Task<bool> DeleteAsync()
        {
            var result = await _client.RequestAsync("deleteMessage", HttpMethod.Post, new Dictionary<string, object>
            {
                {"chat_id", Chat.Id.Fetch()},
                {"message_id", Id}
            }.ToJson());

            return result.Ok;
        }

        private class InlineObject
        {
            public ApiInlineKeyboardButton[][] InlineKeyboard { get; set; }
        }
    }
}