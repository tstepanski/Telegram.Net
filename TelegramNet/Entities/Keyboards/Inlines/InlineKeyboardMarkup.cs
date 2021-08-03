using System;
using System.Collections.Generic;
using System.Linq;
using TelegramNet.ExtraTypes;

namespace TelegramNet.Entities.Keyboards.Inlines
{
    public class InlineKeyboardMarkup
    {
        public static KeyboardBuilder<InlineKeyboardButton> Builder => new();
        public static KeyboardRowBuilder<InlineKeyboardButton> RowBuilder => new();

        public InlineKeyboardMarkup(IEnumerable<InlineKeyboardButton[]> buttons)
        {
            Buttons = buttons;
        }

        public InlineKeyboardMarkup(InlineKeyboardButton button)
        {
            Buttons = new[] {new[] {button}};
        }

        public InlineKeyboardMarkup(InlineKeyboardButton[] buttons)
        {
            Buttons = new[] {buttons};
        }

        public InlineKeyboardMarkup(KeyboardBuilder<InlineKeyboardButton> builder)
        {
            Buttons = builder.Buttons;
        }

        public IEnumerable<InlineKeyboardButton[]> Buttons { get; }

        internal object ToApiFormat()
        {
            return new {inline_keyboard = Buttons.Select(x => x.Select(z => z.ToApiFormat()).ToArray()).ToArray()};
        }
    }
}