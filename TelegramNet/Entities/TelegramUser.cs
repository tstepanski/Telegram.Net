using System;
using System.Threading.Tasks;
using TelegramNet.Entities.Interfaces;
using TelegramNet.Entities.Keyboards.Inlines;
using TelegramNet.Entities.Keyboards.Replies;
using TelegramNet.Enums;
using TelegramNet.Helpers;
using TelegramNet.Types;

namespace TelegramNet.Entities
{
	public class TelegramUser : ITelegramUser
	{
		private readonly BaseTelegramClient _client;

		internal TelegramUser(BaseTelegramClient client, ApiUser? user)
		{
			if (user != null)
			{
				Id = user.Id;
				IsBot = user.IsBot;
				FirstName = user.FirstName;
				LastName = user.LastName;
				Username = user.Username;
				LanguageCode = user.LanguageCode;
				CanJoinGroups = user.CanJoinGroups;
				CanReadAllGroupMessages = user.CanReadAllGroupMessages;
				SupportsInlineQueries = user.SupportsInlineQueries;
			}
			else
			{
				throw new InvalidOperationException(GitHubIssueBuilder.Message(
					$"Exception while initializing {nameof(TelegramUser)}.", nameof(InvalidOperationException),
					$"**Description:**\nDescribe your problem here.\n**StackTrace:**\n{Environment.StackTrace}"));
			}

			Mention = new UserMention(this);
			_client = client;
		}

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
		public async Task<TelegramClientMessage> SendMessageAsync(string text,
			ParseMode parseMode = ParseMode.MarkdownV2, InlineKeyboardMarkup? inlineMarkup = null,
			ReplyKeyboardMarkup? replyMarkup = null)
		{
			return await _client.SendMessageAsync(Id, text, parseMode, inlineMarkup, replyMarkup);
		}

		public async Task<TelegramClientMessage?> SendMessageAsync(string text,
			ParseMode parseMode = ParseMode.MarkdownV2, IKeyboard? keyboard = null)
		{
			return await _client.SendMessageAsync(Id, text, parseMode, keyboard);
		}

		public override string ToString()
		{
			return $"User {Username} with id {Id}";
		}
	}
}