using System.Text.Json.Serialization;

namespace TelegramNet.Types.Replies
{
    internal class KeyboardButtonPollType
    {
        [JsonPropertyName("type")] public string Type { get; set; }
    }
}