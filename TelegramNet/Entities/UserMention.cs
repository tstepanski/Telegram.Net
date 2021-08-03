using TelegramNet.Entities.Interfaces;

namespace TelegramNet.Entities
{
    public class UserMention
    {
        internal UserMention(ITelegramUser user)
        {
            _user = user;
        }

        private readonly ITelegramUser _user;

        public override string ToString()
        {
            return _user.Username.HasValue ? $"@{_user.Username.Value}" : $"tg://user?id={_user.Id}";
        }
    }
}