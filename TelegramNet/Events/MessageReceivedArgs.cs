using TelegramNet.Entities;

namespace TelegramNet.Events
{
    public class MessageReceivedArgs
    {
        public TelegramMessage Message { get; internal set; }

        public TelegramUser Sender { get; internal set; }
    }
}