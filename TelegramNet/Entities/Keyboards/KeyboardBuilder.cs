using System.Collections.Generic;
using System.Linq;

namespace TelegramNet.Entities.Keyboards
{
    public class KeyboardBuilder<T>
    {
        public KeyboardBuilder()
        {
        }

        internal List<T[]> Buttons { get; init; } = new();

        public KeyboardBuilder<T> AddRows(IEnumerable<T[]> buttons)
        {
            Buttons.AddRange(buttons);
            return new KeyboardBuilder<T>
            {
                Buttons = Buttons
            };
        }

        public KeyboardBuilder<T> AddRows(IEnumerable<KeyboardRowBuilder<T>> RowBuilders)
        {
            return AddRows(RowBuilders.Select(x => x.Build()));
        }

        public KeyboardBuilder<T> AddRow(T[] buttons)
        {
            Buttons.Add(buttons);
            return new KeyboardBuilder<T>
            {
                Buttons = Buttons
            };
        }

        public KeyboardBuilder<T> AddRow(KeyboardRowBuilder<T> RowBuilder)
        {
            return AddRow(RowBuilder.Build());
        }

        public static KeyboardBuilder<T> CreateWithRow(T[] buttons)
        {
            return new KeyboardBuilder<T>()
                .AddRow(buttons);
        }

        public static KeyboardBuilder<T> CreateWithRow(KeyboardRowBuilder<T> RowBuilder)
        {
            return new KeyboardBuilder<T>()
                .AddRow(RowBuilder);
        }

        public static KeyboardBuilder<T> CreateWithRows(IEnumerable<T[]> Rows)
        {
            return new KeyboardBuilder<T>()
                .AddRows(Rows);
        }

        public static KeyboardBuilder<T> CreateWithRows(IEnumerable<KeyboardRowBuilder<T>> RowBuilders)
        {
            return new KeyboardBuilder<T>()
                .AddRows(RowBuilders);
        }
    }
}