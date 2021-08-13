using System.Text.Json.Serialization;

namespace TelegramNet.Types.Replies
{
    internal class ApiKeyboardButtonPollType
    {
        [JsonPropertyName("type")] public string Type { get; set; }
    }
}