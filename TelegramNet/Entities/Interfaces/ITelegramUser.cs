using System;
using System.Threading.Tasks;
using TelegramNet.Entities.Keyboards.Inlines;
using TelegramNet.Entities.Keyboards.Replies;
using TelegramNet.Enums;

namespace TelegramNet.Entities.Interfaces
{
	public interface ITelegramUser
	{
		public int Id { get; }
		public bool IsBot { get; }
		public string? FirstName { get; }
		public string? LastName { get; }
		public string? Username { get; }
		public string? LanguageCode { get; }
		public bool? CanJoinGroups { get; }
		public bool? CanReadAllGroupMessages { get; }
		public bool? SupportsInlineQueries { get; }
		public UserMention Mention { get; }

		[Obsolete(@"Use overload with " + nameof(IKeyboard) + @" parameter", true)]
		public Task<TelegramClientMessage> SendMessageAsync(string text,
			ParseMode mode = ParseMode.MarkdownV2,
			InlineKeyboardMarkup? inlineMarkup = null,
			ReplyKeyboardMarkup? replyMarkup = null);

		public Task<TelegramClientMessage?> SendMessageAsync(string text, ParseMode mode = ParseMode.MarkdownV2,
			IKeyboard? keyboard = null);
	}
}