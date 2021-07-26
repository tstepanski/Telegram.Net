using System.Threading.Tasks;

namespace TelegramNet.Entities.Interfaces
{
    public interface ITelegramChat
    {
        public ChatId Id { get; }

        public string Type { get; }

        public string Title { get; }

        public string Username { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public Task<TelegramClientMessage> SendMessageAsync(string text);
    }
}