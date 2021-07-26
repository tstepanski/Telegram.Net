using System.Threading.Tasks;

namespace TelegramNet.Entities.Interfaces
{
    public interface ITelegramUser
    {
        public int Id { get; }
        public bool IsBot { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Username { get; }
        public string LanguageCode { get; }
        public bool CanJoinGroups { get; }
        public bool CanReadAllGroupMessages { get; }
        public bool SupportsInlineQueries { get; }

        public UserMention Mention { get; }

        public Task<ITelegramMessage> SendPrivateMessageAsync(string text);
    }
}