namespace TelegramNet.Enums
{
    internal static class ParseModeInterpreter
    {
        public static string ToApiString(this ParseMode mode)
        {
            return mode switch
            {
                ParseMode.MarkdownV2 => "MarkdownV2",
                ParseMode.Html => "HTML",
                _ => "MarkdownV2"
            };
        }
    }
}