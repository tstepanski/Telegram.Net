using System;
using System.Threading.Tasks;
using TelegramNet.Entities.Interfaces;
using TelegramNet.Entities.Keyboards.Inlines;
using TelegramNet.Enums;
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
        }

        private readonly BaseTelegramClient _tgClient;

        public ChatId Id { get; }
        public string Type { get; }
        public Optional<string> Title { get; }
        public Optional<string> Username { get; }
        public Optional<string> FirstName { get; }
        public Optional<string> LastName { get; }

        public async Task<TelegramClientMessage> SendMessageAsync(string text, ParseMode mode = ParseMode.MarkdownV2,
            InlineKeyboardMarkup inlineMarkup = null,
            Keyboards.Replies.ReplyKeyboardMarkup replyMarkup = null)
        {
            return await _tgClient.SendMessageAsync(Id,
                text,
                mode,
                inlineMarkup,
                replyMarkup);
        }
    }
}