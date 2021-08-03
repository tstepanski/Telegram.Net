namespace TelegramNet.Validators
{
    public static class TokenValidator
    {
        public static bool Validate(string token)
        {
            var parts = token.Split(':');

            if (parts.Length != 2)
                return false;

            var isInteger = int.TryParse(parts[0], out var _);

            if (!isInteger)
                return false;

            return parts[1].Length != 0;
        }
    }
}