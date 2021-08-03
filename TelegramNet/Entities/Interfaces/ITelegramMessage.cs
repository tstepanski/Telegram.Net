using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramNet.ExtraTypes;

namespace TelegramNet.Entities.Interfaces
{
    public interface ITelegramMessage
    {
        public int Id { get; }

        public Optional<ITelegramUser> Author { get; }
        public Optional<ITelegramChat> SenderChat { get; }
        public Optional<DateTime> Timestamp { get; }
        public Optional<string> Text { get; }
        public ITelegramChat Chat { get; }
        public Optional<ITelegramUser> ForwardFrom { get; }
        public Optional<ITelegramChat> ForwardFromChat { get; }
        public Optional<IEnumerable<MessageCaption>> Captions { get; }

        public Optional<int> ForwardFromMessageId { get; }

        public Optional<string> ForwardSignature { get; }

        public Optional<string> ForwardSenderName { get; }

        public Optional<DateTime> ForwardDate { get; }

        public Optional<ITelegramMessage> ReplyToMessage { get; }

        public Optional<ITelegramUser> ViaBot { get; }

        public Optional<DateTime> EditDate { get; }

        public Optional<string> MediaGroupId { get; }

        public Optional<string> AuthorSignature { get; }

        public Optional<IEnumerable<MessageCaption>> Entities { get; }

        public Task<bool> DeleteAsync();
    }
}