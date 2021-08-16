using System.Net.Http;
using System.Threading.Tasks;
using TelegramNet.Enums;
using TelegramNet.Helpers;
using TelegramNet.Types;

namespace TelegramNet.Entities
{
	public sealed class TelegramClientMessage : TelegramMessage
	{
		private readonly TelegramApiClient _client;
		private readonly ParseMode _mode;

		internal TelegramClientMessage(BaseTelegramClient client, ApiMessage? apiMessage, ParseMode mode) : base(client,
			apiMessage)
		{
			_client = client.TelegramApi;
			_mode = mode;
		}

		public async Task<bool> EditTextAsync(MessageTextEditor textEditor)
		{
			textEditor.ChatId = Chat.Id;
			textEditor.MessageId = Id;
			var json = textEditor.ToJson();

			var result = await _client.RequestAsync("editMessageText", HttpMethod.Post, json);

			return result.Ok;
		}

		public async Task<bool> RemoveKeyboardAsync()
		{
			var messageTextEditor = new MessageTextEditor()
				.WithText(Text ?? string.Empty)
				.WithParseMode(_mode);

			return await EditTextAsync(messageTextEditor);
		}
	}
}