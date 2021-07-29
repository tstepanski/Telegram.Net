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
            if(_user.Username.HasValue)
                return $"@{_user.Username.Value}";
            return $"@{_user.FirstName}";
        }
    }
}