using System;
using System.Text.Json.Serialization;
using TelegramNet.Enums;


namespace TelegramNet.Helpers
{
    public class MessageEditor
    {
        [JsonPropertyName("chat_id")] public int ChatId { get; set; }

        [JsonPropertyName("message_id")] public int MessageId { get; set; }

        [JsonPropertyName("inline_message_id")]
        public string InlineMessageId { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("parse_mode")]
        public string ParseMode { get; set; } = "MarkdownV2";

        // [JsonPropertyName("entities")]
        // public MessageCaption[] Entities { get; set; }

        [JsonPropertyName("disable_web_page_preview")]
        public bool DisableWebPagePreview { get; set; }

        public MessageEditor WithText(string text)
        {
            Text = text;
            return this;
        }

        public MessageEditor WithParseMode(ParseMode mode)
        {
            ParseMode = mode switch
            {
                Enums.ParseMode.MarkdownV2 => "MarkdownV2",
                Enums.ParseMode.Html => "HTML",
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
            };
            return this;
        }

        //TODO Add markups
    }
}