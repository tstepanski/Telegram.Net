using System.Text.Json.Serialization;
using TelegramNet.Types.Inlines;

namespace TelegramNet.Types
{
    internal class ApiMessage
    {
        [JsonPropertyName("message_id")] public int Id { get; set; }
        [JsonPropertyName("from")] public ApiUser Author { get; set; }
        [JsonPropertyName("sender_chat")] public ApiChat SenderApiChat { get; set; }
        [JsonPropertyName("date")] public int Date { get; set; }
        [JsonPropertyName("chat")] public ApiChat ApiChat { get; set; }
        [JsonPropertyName("forward_from")] public ApiUser ForwardFrom { get; set; }

        [JsonPropertyName("forward_from_chat")]
        public ApiChat ForwardFromApiChat { get; set; }

        [JsonPropertyName("forward_from_message_id")]
        public int ForwardFromMessageId { get; set; }

        [JsonPropertyName("forward_signature")]
        public string ForwardSignature { get; set; }

        [JsonPropertyName("forward_sender_name")]
        public string ForwardSenderName { get; set; }

        [JsonPropertyName("forward_date")] public int ForwardDate { get; set; }

        [JsonPropertyName("reply_to_message")] public ApiMessage ReplyToApiMessage { get; set; }

        [JsonPropertyName("via_bot")] public ApiUser ViaBot { get; set; }

        [JsonPropertyName("edit_date")] public int EditDate { get; set; }

        [JsonPropertyName("media_group_id")] public string MediaGroupId { get; set; }

        [JsonPropertyName("author_signature")] public string AuthorSignature { get; set; }

        [JsonPropertyName("text")] public string Text { get; set; }

        [JsonPropertyName("entities")] public ApiMessageEntity[] Entities { get; set; }

        [JsonPropertyName("caption_entities")] public ApiMessageEntity[] CaptionEntities { get; set; }

        [JsonPropertyName("reply_markup")] public object ReplyMarkup { get; set; }
    }
}