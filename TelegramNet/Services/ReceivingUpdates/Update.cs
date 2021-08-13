using System.Text.Json.Serialization;
using TelegramNet.Types;

namespace TelegramNet.Services.ReceivingUpdates
{
    internal class Update
    {
        [JsonPropertyName("update_id")] public int UpdateId { get; set; }

        [JsonPropertyName("message")] public ApiMessage ApiMessage { get; set; }

        [JsonPropertyName("edited_message")] public ApiMessage EditedApiMessage { get; set; }

        [JsonPropertyName("channel_post")] public ApiMessage ChannelPost { get; set; }

        [JsonPropertyName("edited_channel_post")]
        public ApiMessage EditedChannelPost { get; set; }
    }
}