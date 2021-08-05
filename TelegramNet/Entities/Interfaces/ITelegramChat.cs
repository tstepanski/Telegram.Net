using System.Threading.Tasks;
using TelegramNet.Entities.Keyboards.Inlines;
using TelegramNet.Enums;
using TelegramNet.ExtraTypes;

namespace TelegramNet.Entities.Interfaces
{
    public interface ITelegramChat
    {
        public ChatId Id { get; }

        public string Type { get; }

        public Optional<string> Title { get; }

        public Optional<string> Username { get; }

        public Optional<string> FirstName { get; }

        public Optional<string> LastName { get; }

        public Task<TelegramClientMessage> SendMessageAsync(string text,
            ParseMode mode = ParseMode.MarkdownV2,
            InlineKeyboardMarkup inlineMarkup = null,
            Keyboards.Replies.ReplyKeyboardMarkup replyMarkup = null);
    }
}