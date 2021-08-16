using System;
using TelegramNet.Entities.Interfaces;
using TelegramNet.Types.Inlines;

namespace TelegramNet.Entities.Keyboards.Inlines
{
	public sealed class InlineKeyboardButton : IProvidesApiFormat
	{
		public InlineKeyboardButton(string? text,
			Uri? url = null,
			LoginUri? loginUrl = null,
			string? callbackData = null,
			string? switchInlineQuery = null,
			string? switchInlineQueryCurrentChat = null,
			bool pay = false)
		{
			var urlIsNull = url == null;
			var loginUrlIsNull = loginUrl == null;
			var callbackDataIsNull = callbackData == null;

			if (urlIsNull && loginUrlIsNull && callbackDataIsNull)
			{
				throw new InvalidOperationException(
					$"{nameof(InlineKeyboardButton)} must have url or loginUrl or callbackData.");
			}

			Text = text;
			Url = url;
			LoginUrl = loginUrl;
			CallbackData = callbackData;
			SwitchInlineQuery = switchInlineQuery;
			SwitchInlineQueryCurrentChat = switchInlineQueryCurrentChat;
			Pay = pay;
		}

		public string? Text { get; }

		public Uri? Url { get; }

		public LoginUri? LoginUrl { get; }

		public string? CallbackData { get; }

		public string? SwitchInlineQuery { get; }

		public string? SwitchInlineQueryCurrentChat { get; }

		public bool? Pay { get; }

		object IProvidesApiFormat.GetApiFormat()
		{
			return new ApiInlineKeyboardButton
			{
				Text = Text,
				Url = Url?.ToString(),
				CallbackData = CallbackData,
				SwitchInlineQuery = SwitchInlineQuery,
				SwitchInlineQueryCurrentChat = SwitchInlineQueryCurrentChat,
				Pay = Pay
			};
		}

		public static InlineKeyboardButton WithCallbackData(string? text, string callbackData)
		{
			return new InlineKeyboardButton(text, callbackData: callbackData);
		}

		public static InlineKeyboardButton WithUrl(string? text, Uri url)
		{
			return new InlineKeyboardButton(text, url);
		}

		public static InlineKeyboardButton WithLoginUrl(string? text, LoginUri url)
		{
			return new InlineKeyboardButton(text, loginUrl: url);
		}
	}
}