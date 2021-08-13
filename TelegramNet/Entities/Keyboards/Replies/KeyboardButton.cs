using TelegramNet.Entities.Interfaces;

namespace TelegramNet.Entities.Keyboards.Replies
{
    public class KeyboardButton : IApiFormatable
    {
        public KeyboardButton(string text,
            bool requestContact = false,
            bool requestLocation = false,
            KeyboardButtonPollType requestPoll = null)
        {
            Text = text;
            RequestContact = requestContact;
            RequestLocation = requestLocation;
            RequestPoll = requestPoll;
        }

        public string Text { get; }

        public bool RequestContact { get; }

        public bool RequestLocation { get; }

        public KeyboardButtonPollType RequestPoll { get; }

        public static implicit operator KeyboardButton(string text)
        {
            return new(text);
        }

        object IApiFormatable.GetApiFormat()
        {
            return new Types.Replies.ApiKeyboardButton()
            {
                Text = Text,
                RequestContact = RequestContact,
                RequestLocation = RequestLocation,
                RequestPoll = (RequestPoll as IApiFormatable)?.GetApiFormat() as Types.Replies.ApiKeyboardButtonPollType
            };
        }
    }
}