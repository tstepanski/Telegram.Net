using System.Text.Json.Serialization;
using TelegramNet.Types.Extras;

namespace TelegramNet.Types.Inlines
{
    internal class ApiInlineKeyboardButton
    {
        [JsonPropertyName("text")] public string Text { get; set; }

        [JsonPropertyName("url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Url { get; set; }

        [JsonPropertyName("callback_data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CallbackData { get; set; }

        [JsonPropertyName("switch_inline_query")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string SwitchInlineQuery { get; set; }

        [JsonPropertyName("switch_inline_query_current_chat")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string SwitchInlineQueryCurrentChat { get; set; }

        [JsonPropertyName("pay")] public bool Pay { get; set; }

        [JsonPropertyName("login_url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ApiLoginUrl ApiLoginUrl { get; set; }
    }
}