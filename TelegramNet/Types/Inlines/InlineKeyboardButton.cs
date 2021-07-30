using System.Text.Json.Serialization;
using TelegramNet.Types.Extras;

namespace TelegramNet.Types.Inlines
{
    internal class InlineKeyboardButton
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("callback_data")]
        public string CallbackData { get; set; }

        [JsonPropertyName("switch_inline_query")]
        public string SwitchInlineQuery { get; set; }

        [JsonPropertyName("switch_inline_query_current_chat")]
        public string SwitchInlineQueryCurrentChat { get; set; }

        [JsonPropertyName("pay")]
        public bool Pay { get; set; }

        [JsonPropertyName("login_url")]
        public LoginUrl LoginUrl { get; set; }
    }
}