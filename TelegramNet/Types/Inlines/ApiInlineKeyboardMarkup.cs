using System.Text.Json.Serialization;

namespace TelegramNet.Types.Inlines
{
    internal class ApiInlineKeyboardMarkup
    {
        [JsonPropertyName("inline_keyboard")] public ApiInlineKeyboardButton[][] InlineKeyboard { get; set; }
    }
}