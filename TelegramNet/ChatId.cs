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

        public bool TryFetchInteger(out int id)
        {
            var able = int.TryParse(Id, out id);

            return able;
        }

        internal object Fetch()
        {
            var isInteger = TryFetchInteger(out var id);

            if (isInteger)
                return id;
            return Id;
        }

        public static implicit operator ChatId(string id)
        {
            return new(id);
        }

        public static implicit operator ChatId(int id)
        {
            return new(id);
        }

        internal static ChatId FromObject(object obj)
        {
            var value = obj.ToString();
            return new ChatId(value);
        }
    }
}