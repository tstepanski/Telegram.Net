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
	public sealed class TelegramChat : ITelegramChat
	{
		private readonly BaseTelegramClient _client;

		internal TelegramChat(BaseTelegramClient client, ApiChat? apiChat)
		{
			if (apiChat != null)
			{
				Id = apiChat.Id;
				Type = apiChat.Type ?? string.Empty;
				Title = apiChat.Title ?? string.Empty;
				Username = apiChat.Username ?? string.Empty;
				FirstName = apiChat.FirstName ?? string.Empty;
				LastName = apiChat.LastName ?? string.Empty;
			}
			else
			{
				throw new InvalidOperationException(GitHubIssueBuilder.Message(
					$"Exception while initializing {nameof(TelegramChat)}.", nameof(InvalidOperationException),
					$"**Description:**\nDescribe your problem here.\n**StackTrace:**\n{Environment.StackTrace}"));
			}

			_client = client;
		}

		public ChatId Id { get; }
		public string Type { get; }
		public string? Title { get; }
		public string? Username { get; }
		public string? FirstName { get; }
		public string? LastName { get; }

		[Obsolete(@"Method deprecated, use overload with " + nameof(IKeyboard) + @" instead", false)]
		public async Task<TelegramClientMessage> SendMessageAsync(string text, ParseMode mode = ParseMode.MarkdownV2,
			InlineKeyboardMarkup? inlineMarkup = null, ReplyKeyboardMarkup? replyMarkup = null)
		{
			return await _client.SendMessageAsync(Id, text, mode, inlineMarkup, replyMarkup);
		}

		public async Task<TelegramClientMessage?> SendMessageAsync(string text, ParseMode mode = ParseMode.MarkdownV2,
			IKeyboard? keyboard = null)
		{
			return await _client.SendMessageAsync(Id, text, mode, keyboard);
		}

		public static implicit operator ChatId(TelegramChat chat)
		{
			return chat.Id;
		}
	}
}