using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TelegramNet.Entities.Interfaces
{
	public interface ITelegramMessage
	{
		public int Id { get; }
		public TelegramUser? Author { get; }
		public TelegramChat? SenderChat { get; }
		public DateTime? Timestamp { get; }
		public string? Text { get; }
		public TelegramChat Chat { get; }
		public TelegramUser? ForwardFrom { get; }
		public TelegramChat? ForwardFromChat { get; }
		public IEnumerable<MessageCaption>? Captions { get; }

		public int? ForwardFromMessageId { get; }

		public string? ForwardSignature { get; }

		public string? ForwardSenderName { get; }

		public DateTime? ForwardDate { get; }

		public TelegramMessage? ReplyToMessage { get; }

		public TelegramUser? ViaBot { get; }

		public DateTime? EditDate { get; }

		public string? MediaGroupId { get; }

		public string? AuthorSignature { get; }

		public IEnumerable<MessageCaption>? Entities { get; }

		public Task<bool> DeleteAsync();
	}
}