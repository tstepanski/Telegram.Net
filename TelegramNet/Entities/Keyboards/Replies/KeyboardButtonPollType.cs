using System;
using TelegramNet.Entities.Interfaces;
using TelegramNet.Enums;

namespace TelegramNet.Entities.Keyboards.Replies
{
    public class KeyboardButtonPollType : IApiFormatable
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

        object IApiFormatable.GetApiFormat()
        {
            return new Types.Replies.ApiKeyboardButtonPollType()
            {
                Type = Type.ToString().ToLower()
            };
        }
    }
}