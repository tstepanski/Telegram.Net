using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramNet.ExtraTypes;

namespace TelegramNet.Entities.Interfaces
{
    public interface ITelegramMessage
    {
        public int Id { get; }
        public Optional<TelegramUser> Author { get; }
        public Optional<TelegramChat> SenderChat { get; }
        public Optional<DateTime> Timestamp { get; }
        public Optional<string> Text { get; }
        public TelegramChat Chat { get; }
        public Optional<TelegramUser> ForwardFrom { get; }
        public Optional<TelegramChat> ForwardFromChat { get; }
        public Optional<IEnumerable<MessageCaption>> Captions { get; }

        public Optional<int> ForwardFromMessageId { get; }

        public Optional<string> ForwardSignature { get; }

        public Optional<string> ForwardSenderName { get; }

        public Optional<DateTime> ForwardDate { get; }

        public Optional<TelegramMessage> ReplyToMessage { get; }

        public Optional<TelegramUser> ViaBot { get; }

        public Optional<DateTime> EditDate { get; }

        public Optional<string> MediaGroupId { get; }

        public Optional<string> AuthorSignature { get; }

        public Optional<IEnumerable<MessageCaption>> Entities { get; }

        public Task<bool> DeleteAsync();
    }
}