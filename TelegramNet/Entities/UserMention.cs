using TelegramNet.Entities.Interfaces;

namespace TelegramNet.Entities
{
	public sealed class UserMention
	{
		private readonly ITelegramUser _user;

		internal UserMention(ITelegramUser user)
		{
			_user = user;
		}

		public override string ToString()
		{
			return _user.Username ?? $"tg://user?id={_user.Id}";
		}
	}
}