using System;
using TelegramNet.Enums;
using TelegramNet.Types;

namespace TelegramNet.Entities
{
	public sealed class MessageCaption
	{
		internal MessageCaption(BaseTelegramClient client, ApiMessageEntity entity, string? text)
		{
			var offset = entity.Offset;
			var length = entity.Length;

			Content = text?.Substring(offset, length) ?? string.Empty;

			Type = entity.Type switch
			{
				"mention" => MessageCaptionType.Mention,
				"hashtag" => MessageCaptionType.Hashtag,
				"cashtag" => MessageCaptionType.Cashtag,
				"bot_command" => MessageCaptionType.BotCommand,
				"url" => MessageCaptionType.Url,
				"email" => MessageCaptionType.Email,
				"phone_number" => MessageCaptionType.PhoneNumber,
				"bold" => MessageCaptionType.Bold,
				"italic" => MessageCaptionType.Italic,
				"underline" => MessageCaptionType.Underline,
				"strikethrough" => MessageCaptionType.Strikethrough,
				"code" => MessageCaptionType.Code,
				"pre" => MessageCaptionType.Pre,
				"text_link" => MessageCaptionType.TextLink,
				"text_mention" => MessageCaptionType.TextMention,
				_ => MessageCaptionType.Unknown
			};

			if (entity.User != null)
			{
				User = new TelegramUser(client, entity.User);
			}

			if (entity.Url != null)
			{
				Url = new Uri(entity.Url);
			}

			if (entity.Language != null)
			{
				Language = entity.Language;
			}
		}

		public MessageCaptionType Type { get; }

		public string Content { get; }

		public TelegramUser? User { get; }

		public Uri? Url { get; }

		public string? Language { get; }
	}
}