using System.Collections.Generic;
using System.Linq;

namespace TelegramNet.Entities.Keyboards.Replies
{
    public class ReplyKeyboardMarkup
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

        internal Types.Replies.ReplyKeyboardMarkup GetApiFormat()
        {
            return new()
            {
                Keyboard = Keyboard.Select(x => x.Select(z => new Types.Replies.KeyboardButton
                {
                    Text = z.Text,
                    RequestContact = z.RequestContact,
                    RequestLocation = z.RequestLocation,
                    RequestPoll = z.RequestPoll != null
                        ? new Types.Replies.KeyboardButtonPollType
                        {
                            Type = z.RequestPoll.Type.ToString().ToLower()
                        }
                        : null
                }).ToArray()).ToArray(),
                InputFieldPlaceHolder = InputFieldPlaceholder,
                OneTimeKeyboard = OneTimeKeyboard,
                ResizeKeyboard = ResizeKeyboard,
                Selective = Selective
            };
        }
    }


    public enum PollType
    {
        Quiz = 0,
        Regular = 1,
        None = 2
    }
}