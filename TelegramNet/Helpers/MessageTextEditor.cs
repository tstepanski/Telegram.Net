using System;
using System.Text.Json.Serialization;
using TelegramNet.Enums;

namespace TelegramNet.Helpers
{
    public class MessageTextEditor
    {
        [JsonIgnore]
        internal ChatId ChatId
        {
            get => ChatId.FromObject(ChatIndeficator);
            set => ChatIndeficator = value.Fetch();
        }

        [JsonPropertyName("chat_id")] public object ChatIndeficator { get; internal set; }

        [JsonPropertyName("message_id")] public int MessageId { get; internal set; }

        [JsonPropertyName("inline_message_id")]
        public string InlineMessageId { get; internal set; }

        [JsonPropertyName("text")] public string Text { get; set; }

        [JsonPropertyName("parse_mode")] public string ParseMode { get; set; } = "MarkdownV2";

        [JsonPropertyName("disable_web_page_preview")]
        public bool DisableWebPagePreview { get; set; }

        public MessageTextEditor WithText(string text)
        {
            Text = text;
            return this;
        }

        public MessageTextEditor WithParseMode(ParseMode mode)
        {
            ParseMode = mode switch
            {
                Enums.ParseMode.MarkdownV2 => "MarkdownV2",
                Enums.ParseMode.Html => "HTML",
                _ => "MarkdownV2"
            };
            return this;
        }
    }
}