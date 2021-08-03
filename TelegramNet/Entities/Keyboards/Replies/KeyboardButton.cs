using System;

namespace TelegramNet.Entities.Keyboards.Replies
{
    public class KeyboardButton
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
    }

    public class KeyboardButtonPollType
    {
        public KeyboardButtonPollType(PollType poll)
        {
            Type = poll;
        }

        internal KeyboardButtonPollType(string expression)
        {
            Type = expression switch
            {
                "quiz" => PollType.Quiz,
                "regular" => PollType.Regular,
                _ => throw new ArgumentOutOfRangeException(nameof(expression), expression, null)
            };
        }
        
        public PollType Type { get; }
    }
}