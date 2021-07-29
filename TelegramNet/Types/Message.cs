using System.Text.Json.Serialization;
using TelegramNet.Types.Inlines;

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

        [JsonPropertyName("forward_from_message_id")]
        public int ForwardFromMessageId { get; set; }

        [JsonPropertyName("forward_signature")]
        public string ForwardSignature { get; set; }

        [JsonPropertyName("forward_sender_name")]
        public string ForwardSenderName { get; set; }

        [JsonPropertyName("forward_date")]
        public int ForwardDate { get; set; }

        [JsonPropertyName("reply_to_message")]
        public Message ReplyToMessage { get; set; }

        [JsonPropertyName("via_bot")]
        public User ViaBot { get; set; }

        [JsonPropertyName("edit_date")]
        public int EditDate { get; set; }

        [JsonPropertyName("media_group_id")]
        public string MediaGroupId { get; set; }

        [JsonPropertyName("author_signature")]
        public string AuthorSignature { get; set; }

        [JsonPropertyName("text")] public string Text { get; set; }

        [JsonPropertyName("entities")]
        public MessageEntity[] Entities { get; set; }

        [JsonPropertyName("caption_entities")] public MessageEntity[] CaptionEntities { get; set; }

        [JsonPropertyName("reply_markup")]
        public InlineKeyboardButton[][] InlineKeyboardMarkup { get; set; }
    }
}