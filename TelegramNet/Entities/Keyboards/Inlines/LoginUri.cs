using System;

namespace TelegramNet.Entities.Keyboards.Inlines
{
	public sealed class LoginUri
	{
		public LoginUri(Uri? url, string? text = null, string? botUsername = null, bool requestWriteAccess = false)
		{
			Url = url;
			Text = text;
			BotUsername = botUsername;
			RequestWriteAccess = requestWriteAccess;
		}

		public Uri? Url { get; }

		public string? Text { get; }

		public string? BotUsername { get; }

		public bool? RequestWriteAccess { get; }

		internal object ToApiFormat()
		{
			return new
			{
				url = Url?.ToString(),
				forward_text = Text,
				bot_username = BotUsername,
				request_write_access = RequestWriteAccess
			};
		}
	}
}