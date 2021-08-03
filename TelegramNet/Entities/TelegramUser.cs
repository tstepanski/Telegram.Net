using System;
using System.Threading.Tasks;
using TelegramNet.Entities.Interfaces;
using TelegramNet.Entities.Keyboards;
using TelegramNet.Entities.Keyboards.Inlines;
using TelegramNet.Entities.Keyboards.Replies;
using TelegramNet.Enums;
using TelegramNet.ExtraTypes;
using TelegramNet.Helpers;
using TelegramNet.Types;

namespace TelegramNet.Entities
{
    public class TelegramUser : ITelegramUser
    {
        internal TelegramUser(BaseTelegramClient client, User user)
        {
            if (user != null)
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
            }
            else
            {
                throw new InvalidOperationException(GitHubIssueBuilder.Message(
                    $"Exception while initializing {nameof(TelegramUser)}.", nameof(InvalidOperationException),
                    $"**Description:**\nDescribe your problem here.\n**StackTrace:**\n{Environment.StackTrace}"));
            }

            Mention = new UserMention(this);
            _tgClient = client;
        }

        private readonly BaseTelegramClient _tgClient;

        public int Id { get; }
        public bool IsBot { get; }
        public string FirstName { get; }
        public Optional<string> LastName { get; }
        public Optional<string> Username { get; }
        public Optional<string> LanguageCode { get; }
        public Optional<bool> CanJoinGroups { get; }
        public Optional<bool> CanReadAllGroupMessages { get; }
        public Optional<bool> SupportsInlineQueries { get; }
        public UserMention Mention { get; }

        public async Task<TelegramClientMessage> SendMessageAsync(string text,
            ParseMode parseMode = ParseMode.MarkdownV2,
            InlineKeyboardMarkup inlineMarkup = null,
            ReplyKeyboardMarkup replyMarkup = null)
        {
            return await _tgClient.SendMessageAsync(Id,
                text,
                parseMode,
                inlineMarkup,
                replyMarkup);
        }

        public override string ToString()
        {
            return $"User {Username} with id {Id}";
        }
    }
}