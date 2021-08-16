using System.Collections.Generic;
using System.Linq;

namespace TelegramNet.Entities.Keyboards
{
	public sealed class KeyboardBuilder<T>
	{
		internal List<T[]> Buttons { get; init; } = new();

		public KeyboardBuilder<T> AddRows(IEnumerable<T[]> buttons)
		{
			Buttons.AddRange(buttons);
			return new KeyboardBuilder<T>
			{
				Buttons = Buttons
			};
		}

		public KeyboardBuilder<T> AddRows(IEnumerable<KeyboardRowBuilder<T>> rowBuilders)
		{
			return AddRows(rowBuilders.Select(x => x.Build()));
		}

		public KeyboardBuilder<T> AddRow(T[] buttons)
		{
			Buttons.Add(buttons);
			return new KeyboardBuilder<T>
			{
				Buttons = Buttons
			};
		}

		public KeyboardBuilder<T> AddRow(KeyboardRowBuilder<T> rowBuilder)
		{
			return AddRow(rowBuilder.Build());
		}

		public static KeyboardBuilder<T> CreateWithRow(T[] buttons)
		{
			return new KeyboardBuilder<T>()
				.AddRow(buttons);
		}

		public static KeyboardBuilder<T> CreateWithRow(KeyboardRowBuilder<T> rowBuilder)
		{
			return new KeyboardBuilder<T>()
				.AddRow(rowBuilder);
		}

		public static KeyboardBuilder<T> CreateWithRows(IEnumerable<T[]> rows)
		{
			return new KeyboardBuilder<T>()
				.AddRows(rows);
		}

		public static KeyboardBuilder<T> CreateWithRows(IEnumerable<KeyboardRowBuilder<T>> rowBuilders)
		{
			return new KeyboardBuilder<T>()
				.AddRows(rowBuilders);
		}
	}
}