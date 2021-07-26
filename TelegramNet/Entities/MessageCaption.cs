using TelegramNet.Enums;
using TelegramNet.Types;

namespace TelegramNet.Entities
{
    public class MessageCaption
    {
        internal MessageCaption(MessageEntity entity, string text)
        {
            var offset = entity.Offset;
            var length = entity.Length;

            Content = text.Substring(offset, length);
        }

        public MessageCaptionType Type { get; }

        public string Content { get; }
    }
}