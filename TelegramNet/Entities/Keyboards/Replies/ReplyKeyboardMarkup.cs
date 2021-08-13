using System.Collections.Generic;
using System.Linq;
using TelegramNet.Entities.Interfaces;

namespace TelegramNet.Entities.Keyboards.Replies
{
    public class ReplyKeyboardMarkup : IKeyboard, IApiFormatable
    {
        public static KeyboardBuilder<KeyboardButton> Builder => new();
        public static KeyboardRowBuilder<KeyboardButton> RowBuilder => new();

        public ReplyKeyboardMarkup(IEnumerable<KeyboardButton[]> keyboard,
            bool resizeKeyboard = false,
            bool oneTimeKeyboard = false,
            string inputFieldPlaceholder = null,
            bool selective = false)
        {
            Keyboard = keyboard;
            ResizeKeyboard = resizeKeyboard;
            OneTimeKeyboard = oneTimeKeyboard;
            InputFieldPlaceholder = inputFieldPlaceholder;
            Selective = selective;
        }

        public ReplyKeyboardMarkup(KeyboardBuilder<KeyboardButton> keyboard,
            bool resizeKeyboard = false,
            bool oneTimeKeyboard = false,
            string inputFieldPlaceholder = null,
            bool selective = false)
        {
            Keyboard = keyboard.Buttons;
            ResizeKeyboard = resizeKeyboard;
            OneTimeKeyboard = oneTimeKeyboard;
            InputFieldPlaceholder = inputFieldPlaceholder;
            Selective = selective;
        }

        public IEnumerable<KeyboardButton[]> Keyboard { get; }

        public bool ResizeKeyboard { get; }

        public bool OneTimeKeyboard { get; }

        public string InputFieldPlaceholder { get; }

        public bool Selective { get; }

        object IApiFormatable.GetApiFormat()
        {
            return new Types.Replies.ApiReplyKeyboardMarkup()
            {
                Keyboard = Keyboard.Select(x => x
                        .Select(z => (z as IApiFormatable).GetApiFormat() as Types.Replies.ApiKeyboardButton)
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