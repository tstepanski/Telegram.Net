using System.Collections.Generic;
using System.Linq;
using TelegramNet.Entities.Interfaces;
using TelegramNet.Types.Inlines;

namespace TelegramNet.Entities.Keyboards.Inlines
{
	public sealed class InlineKeyboardMarkup : IKeyboard, IProvidesApiFormat
	{
		public InlineKeyboardMarkup(IEnumerable<InlineKeyboardButton[]> buttons)
		{
			Buttons = buttons;
		}

		public InlineKeyboardMarkup(InlineKeyboardButton button)
		{
			Buttons = new[] { new[] { button } };
		}

		public InlineKeyboardMarkup(InlineKeyboardButton[] buttons)
		{
			Buttons = new[] { buttons };
		}

		public InlineKeyboardMarkup(KeyboardBuilder<InlineKeyboardButton> builder)
		{
			Buttons = builder.Buttons;
		}

		public static KeyboardBuilder<InlineKeyboardButton> Builder => new();
		public static KeyboardRowBuilder<InlineKeyboardButton> RowBuilder => new();

		public IEnumerable<InlineKeyboardButton[]> Buttons { get; }

		object IProvidesApiFormat.GetApiFormat()
		{
			return new ApiInlineKeyboardMarkup
			{
				InlineKeyboard = Buttons
					.Select(buttons => buttons
						.OfType<IProvidesApiFormat>()
						.Select(button => button.GetApiFormat() as ApiInlineKeyboardButton)
						.ToArray())
					.ToArray()
			};
		}
	}
}