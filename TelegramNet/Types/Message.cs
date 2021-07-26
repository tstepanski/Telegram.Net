using System.Text.Json.Serialization;
using TelegramNet.ExtraTypes;

namespace TelegramNet.Types
{
    internal class Message
    {
        [JsonPropertyName("message_id")] public int Id { get; set; }
        [JsonPropertyName("from")] public User Author { get; set; }
        [JsonPropertyName("sender_chat")] public Chat SenderChat { get; set; }
        [JsonPropertyName("date")] public int Date { get; set; }
        [JsonPropertyName("chat")] public Chat Chat { get; set; }
        [JsonPropertyName("forward_from")] public User ForwardFrom { get; set; }

        [JsonPropertyName("forward_from_chat")]
        public Chat ForwardFromChat { get; set; }

        [JsonPropertyName("caption_entities")] public MessageEntity[] CaptionEntities { get; set; }
        [JsonPropertyName("text")] public string Text { get; set; }
    }
}