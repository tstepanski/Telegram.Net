namespace TelegramNet
{
    public class ChatId
    {
        public ChatId(string id)
        {
            Id = id;
        }

        public ChatId(int id)
        {
            Id = id.ToString();
        }

        public string Id { get; }

        public static implicit operator ChatId(string id)
        {
            return new(id);
        }

        public static implicit operator ChatId(int id)
        {
            return new(id);
        }
    }
}