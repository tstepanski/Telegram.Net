using TelegramNet.Types;

namespace TelegramNet.Entities
{
	public sealed class SelfUser : TelegramUser
	{
		internal SelfUser(BaseTelegramClient client, ApiUser user) : base(client, user)
		{
			SelfClient = client;
		}

		public BaseTelegramClient SelfClient { get; }
	}
}