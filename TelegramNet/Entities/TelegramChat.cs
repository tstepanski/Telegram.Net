using System;
using System.Threading.Tasks;
using TelegramNet.Entities.Interfaces;
using TelegramNet.Entities.Keyboards.Inlines;
using TelegramNet.Entities.Keyboards.Replies;
using TelegramNet.Enums;
using TelegramNet.ExtraTypes;
using TelegramNet.Helpers;
using TelegramNet.Types;

namespace TelegramNet.Entities
{
    public class TelegramChat : ITelegramChat
    {
        internal TelegramChat(BaseTelegramClient client, ApiChat apiChat)
        {
            if (apiChat != null)
            {
                #region ~Realization of entity

                Id = apiChat.Id;
                Type = apiChat.Type ?? string.Empty;
                Title = apiChat.Title ?? string.Empty;
                Username = apiChat.Username ?? string.Empty;
                FirstName = apiChat.FirstName ?? string.Empty;
                LastName = apiChat.LastName ?? string.Empty;

                #endregion
            }
            else
            {
                throw new InvalidOperationException(GitHubIssueBuilder.Message(
                    $"Exception while initializing {nameof(TelegramChat)}.", nameof(InvalidOperationException),
                    $"**Description:**\nDescribe your problem here.\n**StackTrace:**\n{Environment.StackTrace}"));
            }

            _tgClient = client;
        }

        private readonly BaseTelegramClient _tgClient;

        public ChatId Id { get; }
        public string Type { get; }
        public Optional<string> Title { get; }
        public Optional<string> Username { get; }
        public Optional<string> FirstName { get; }
        public Optional<string> LastName { get; }

        [Obsolete]
        public async Task<TelegramClientMessage> SendMessageAsync(string textO, ParseMode mode = ParseMode.MarkdownV2,
            InlineKeyboardMarkup inlineMarkup = null,
            ReplyKeyboardMarkup replyMarkup = null)
        {
            return await _tgClient.SendMessageAsync(Id,
                textO,
                mode,
                inlineMarkup,
                replyMarkup);
        }

        public async Task<TelegramClientMessage> SendMessageAsync(string text, ParseMode mode = ParseMode.MarkdownV2,
            IKeyboard keyboard = null)
        {
            return await _tgClient.SendMessageAsync(Id,
                text,
                mode,
                keyboard);
        }

        public static implicit operator ChatId(TelegramChat chat)
        {
            return chat.Id;
        }
    }
}