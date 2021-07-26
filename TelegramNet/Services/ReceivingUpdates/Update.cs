using System.Text.Json.Serialization;
using TelegramNet.Types;

namespace TelegramNet.Services.ReceivingUpdates
{
    internal class Update
    {
        [JsonPropertyName("update_id")] public int UpdateId { get; set; }

        [JsonPropertyName("message")] public Message Message { get; set; }

        [JsonPropertyName("edited_message")] public Message EditedMessage { get; set; }

        [JsonPropertyName("channel_post")] public Message ChannelPost { get; set; }

        [JsonPropertyName("edited_channel_post")]
        public Message EditedChannelPost { get; set; }
    }
}