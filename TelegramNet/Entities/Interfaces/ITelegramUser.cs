using System.Threading.Tasks;
using TelegramNet.Entities.Keyboards;
using TelegramNet.Entities.Keyboards.Inlines;
using TelegramNet.Entities.Keyboards.Replies;
using TelegramNet.Enums;
using TelegramNet.ExtraTypes;

namespace TelegramNet.Entities.Interfaces
{
    public interface ITelegramUser
    {
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

        public Task<TelegramClientMessage> SendMessageAsync(string text,
            ParseMode mode = ParseMode.MarkdownV2,
            InlineKeyboardMarkup inlineMarkup = null,
            ReplyKeyboardMarkup replyMarkup = null);
    }
}