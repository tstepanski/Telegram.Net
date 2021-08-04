using System;
using TelegramNet.Enums;

namespace TelegramNet.Entities.Keyboards.Replies
{
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