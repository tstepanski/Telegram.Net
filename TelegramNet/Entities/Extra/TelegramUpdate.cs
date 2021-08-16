using TelegramNet.Services.ReceivingUpdates;
using TelegramNet.Types;

namespace TelegramNet.Entities.Extra
{
	public sealed class TelegramUpdate
	{
		private TelegramUpdate(int updateId, TelegramMessage? message, TelegramMessage? editedMessage,
			TelegramMessage? channelPost, TelegramMessage? editedChannelPost)
		{
			Id = updateId;
			Message = message;
			EditedMessage = editedMessage;
			ChannelPost = channelPost;
			EditedChannelPost = editedChannelPost;
		}

		public int Id { get; }

		public TelegramMessage? Message { get; }

		public TelegramMessage? EditedMessage { get; }

		public TelegramMessage? ChannelPost { get; }

		public TelegramMessage? EditedChannelPost { get; }

		internal static TelegramUpdate FromUpdate(BaseTelegramClient client, Update update)
		{
			TelegramMessage? CreateIfMessageNotNull(ApiMessage? message)
			{
				return TelegramMessage.CreateIfMessageNotNull(client, message);
			}

			var apiMessage = CreateIfMessageNotNull(update.ApiMessage);
			var editedApiMessage = CreateIfMessageNotNull(update.EditedApiMessage);
			var channelPost = CreateIfMessageNotNull(update.ChannelPost);
			var editedChannelPost = CreateIfMessageNotNull(update.EditedChannelPost);

			return new TelegramUpdate(update.UpdateId, apiMessage, editedApiMessage, channelPost, editedChannelPost);
		}
	}
}