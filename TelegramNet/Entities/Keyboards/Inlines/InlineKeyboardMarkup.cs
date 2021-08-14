using System.Collections.Generic;
using System.Linq;
using TelegramNet.Entities.Interfaces;
using TelegramNet.Types.Inlines;

namespace TelegramNet.Entities.Keyboards.Inlines
{
    public class InlineKeyboardMarkup : IKeyboard, IApiFormatable
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

        object IApiFormatable.GetApiFormat()
        {
            return new ApiInlineKeyboardMarkup
            {
                InlineKeyboard = Buttons.Select(x => x
                        .Select(z => (z as IApiFormatable).GetApiFormat() as ApiInlineKeyboardButton)
                        .ToArray())
                    .ToArray()
            };
        }
    }
}