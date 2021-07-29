using System.Text.Json.Serialization;

namespace TelegramNet.Types.Extras
{
    public class LoginUrl
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("forward_text")]
        public string ForwardText { get; set; }

        [JsonPropertyName("bot_username")]
        public string BotUsername { get; set; }

        [JsonPropertyName("request_write_access")]
        public bool RequestWriteAccess { get; set; }
    }
}