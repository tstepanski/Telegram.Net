using System.Collections.Generic;
using System.Linq;
using TelegramNet.Entities.Interfaces;
using TelegramNet.Types.Replies;

namespace TelegramNet.Entities.Keyboards.Replies
{
	public sealed class ReplyKeyboardMarkup : IKeyboard, IProvidesApiFormat
	{
		public ReplyKeyboardMarkup(IEnumerable<KeyboardButton[]> keyboard, bool resizeKeyboard = false,
			bool oneTimeKeyboard = false, string? inputFieldPlaceholder = null, bool selective = false)
		{
			Keyboard = keyboard;
			ResizeKeyboard = resizeKeyboard;
			OneTimeKeyboard = oneTimeKeyboard;
			InputFieldPlaceholder = inputFieldPlaceholder;
			Selective = selective;
		}

		public ReplyKeyboardMarkup(KeyboardBuilder<KeyboardButton> keyboard, bool resizeKeyboard = false,
			bool oneTimeKeyboard = false, string? inputFieldPlaceholder = null,
			bool selective = false) : this(keyboard.Buttons, resizeKeyboard, oneTimeKeyboard, inputFieldPlaceholder,
			selective)
		{
		}

		public static KeyboardBuilder<KeyboardButton> Builder => new();
		public static KeyboardRowBuilder<KeyboardButton> RowBuilder => new();

		public IEnumerable<KeyboardButton[]> Keyboard { get; }

		public bool ResizeKeyboard { get; }

		public bool OneTimeKeyboard { get; }

		public string? InputFieldPlaceholder { get; }

		public bool Selective { get; }

		object IProvidesApiFormat.GetApiFormat()
		{
			return new ApiReplyKeyboardMarkup
			{
				Keyboard = Keyboard
					.Select(buttons => buttons
						.OfType<IProvidesApiFormat>()
						.Select(button => button.GetApiFormat())
						.OfType<ApiKeyboardButton>()
						.ToArray())
					.ToArray(),
				ResizeKeyboard = ResizeKeyboard,
				OneTimeKeyboard = OneTimeKeyboard,
				InputFieldPlaceHolder = InputFieldPlaceholder,
				Selective = Selective
			};
		}
	}
}