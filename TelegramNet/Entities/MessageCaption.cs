using System;
using TelegramNet.Enums;
using TelegramNet.ExtraTypes;
using TelegramNet.Types;

namespace TelegramNet.Entities
{
    public class MessageCaption
    {
        internal MessageCaption(BaseTelegramClient client, MessageEntity entity, string text)
        {
            var offset = entity.Offset;
            var length = entity.Length;

            Content = text.Substring(offset, length);

            Type = entity.Type switch
            {
                "mention" => MessageCaptionType.Mention,
                "hashtag" => MessageCaptionType.Hashtag,
                "cashtag" => MessageCaptionType.Cashtag,
                "bot_command" => MessageCaptionType.BotCommand,
                "url" => MessageCaptionType.Url,
                "email" => MessageCaptionType.Email,
                "phone_number" => MessageCaptionType.PhoneNumber,
                "bold" => MessageCaptionType.Bold,
                "italic" => MessageCaptionType.Italic,
                "underline" => MessageCaptionType.Underline,
                "strikethrough" => MessageCaptionType.Strikethrough,
                "code" => MessageCaptionType.Code,
                "pre" => MessageCaptionType.Pre,
                "text_link" => MessageCaptionType.TextLink,
                "text_mention" => MessageCaptionType.TextMention
            };

            if (entity.User != null)
                User = new TelegramUser(client, entity.User);

            if (entity.Url != null)
                Url = new Uri(entity.Url);

            if (entity.Language != null)
                Language = entity.Language;
        }

        public MessageCaptionType Type { get; }

        public string Content { get; }

        public Optional<TelegramUser> User { get; }

        public Optional<Uri> Url { get; }

        public Optional<string> Language { get; }
    }
}