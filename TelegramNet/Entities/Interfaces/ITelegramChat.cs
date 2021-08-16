using System;
using System.Threading.Tasks;
using TelegramNet.Entities.Keyboards.Inlines;
using TelegramNet.Entities.Keyboards.Replies;
using TelegramNet.Enums;

namespace TelegramNet.Entities.Interfaces
{
	public interface ITelegramChat
	{
		public ChatId Id { get; }

		public string Type { get; }

		public string? Title { get; }

		public string? Username { get; }

		public string? FirstName { get; }

		public string? LastName { get; }

		[Obsolete(@"Method deprecated, use overload with " + nameof(IKeyboard) + @" instead", true)]
		public Task<TelegramClientMessage> SendMessageAsync(string text,
			ParseMode mode = ParseMode.MarkdownV2,
			InlineKeyboardMarkup? inlineMarkup = null,
			ReplyKeyboardMarkup? replyMarkup = null);

		public Task<TelegramClientMessage?> SendMessageAsync(string text, ParseMode mode = ParseMode.MarkdownV2,
			IKeyboard? keyboard = null);
	}
}