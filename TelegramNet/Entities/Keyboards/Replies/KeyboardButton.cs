using TelegramNet.Entities.Interfaces;
using TelegramNet.Types.Replies;

namespace TelegramNet.Entities.Keyboards.Replies
{
	public sealed class KeyboardButton : IProvidesApiFormat
	{
		public KeyboardButton(string? text,
			bool requestContact = false,
			bool requestLocation = false,
			KeyboardButtonPollType? requestPoll = null)
		{
			Text = text;
			RequestContact = requestContact;
			RequestLocation = requestLocation;
			RequestPoll = requestPoll;
		}

		public string? Text { get; }

		public bool RequestContact { get; }

		public bool RequestLocation { get; }

		public KeyboardButtonPollType? RequestPoll { get; }

		object IProvidesApiFormat.GetApiFormat()
		{
			return new ApiKeyboardButton
			{
				Text = Text,
				RequestContact = RequestContact,
				RequestLocation = RequestLocation,
				RequestPoll = (RequestPoll as IProvidesApiFormat)?.GetApiFormat() as ApiKeyboardButtonPollType
			};
		}

		public static implicit operator KeyboardButton(string? text)
		{
			return new KeyboardButton(text);
		}
	}
}