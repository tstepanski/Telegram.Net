using TelegramNet.Types;

namespace TelegramNet.Entities
{
    public class SelfUser : TelegramUser
    {
        internal SelfUser(BaseTelegramClient client, User user) : base(client, user)
        {
            SelfClient = client;
        }

        public BaseTelegramClient SelfClient { get; }
    }
}