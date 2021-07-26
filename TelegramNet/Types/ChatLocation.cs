using System.Text.Json.Serialization;

namespace TelegramNet.Types
{
    internal class ChatLocation
    {
        [JsonPropertyName("location")] public Location Location { get; set; }

        [JsonPropertyName("address")] public string Address { get; set; }
    }
}