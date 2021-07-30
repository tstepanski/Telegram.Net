using System.Text.Json.Serialization;

namespace TelegramNet.Types.Replies
{
    public class KeyboardButton
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("request_contact")]
        public bool RequestContact { get; set; }

        [JsonPropertyName("request_location")]
        public bool RequestLocation { get; set; }

        [JsonPropertyName("request_poll")]
        public KeyboardButtonPollType RequestPoll { get; set; }
    }

    public class KeyboardButtonPollType
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}