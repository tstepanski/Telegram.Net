using System.Collections.Generic;

namespace TelegramNet.Entities.Keyboards
{
	public sealed class KeyboardRowBuilder<T>
	{
		internal List<T> Buttons { get; init; } = new();

		public static KeyboardRowBuilder<T> CreateWithButton(T button)
		{
			return new KeyboardRowBuilder<T>()
				.WithButton(button);
		}

		public static KeyboardRowBuilder<T> CreateWithButtons(IEnumerable<T> buttons)
		{
			return new KeyboardRowBuilder<T>()
				.WithButtons(buttons);
		}

		public KeyboardRowBuilder<T> WithButton(T button)
		{
			Buttons.Add(button);
			return new KeyboardRowBuilder<T>
			{
				Buttons = Buttons
			};
		}

		public KeyboardRowBuilder<T> WithButtons(IEnumerable<T> buttons)
		{
			Buttons.AddRange(buttons);

			return new KeyboardRowBuilder<T>
			{
				Buttons = Buttons
			};
		}

		internal T[] Build()
		{
			return Buttons.ToArray();
		}
	}
}