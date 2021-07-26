using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TelegramNet.Entities.Interfaces
{
    public interface ITelegramMessage
    {
        public int Id { get; }

        public ITelegramUser Author { get; }

        public ITelegramChat SenderChat { get; }

        public DateTime Timestamp { get; }

        public string Text { get; }

        public ITelegramChat Chat { get; }

        public ITelegramUser ForwardFrom { get; }

        public ITelegramChat ForwardFromChat { get; }

        public IEnumerable<MessageCaption> Captions { get; }

        public Task<bool> DeleteAsync();
    }
}