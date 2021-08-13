using TelegramNet.Services.ReceivingUpdates;

namespace TelegramNet.Entities.Extra
{
    public class TelegramUpdate
    {
        private TelegramUpdate(int updateId,
            TelegramMessage message,
            TelegramMessage editedMessage,
            TelegramMessage channelPost,
            TelegramMessage editedChannelPost)
        {
            Id = updateId;
            Message = message;
            EditedMessage = editedMessage;
            ChannelPost = channelPost;
            EditedChannelPost = editedChannelPost;
        }

        public int Id { get; }

        public TelegramMessage Message { get; }

        public TelegramMessage EditedMessage { get; }

        public TelegramMessage ChannelPost { get; }

        public TelegramMessage EditedChannelPost { get; }

        internal static TelegramUpdate FromUpdate(BaseTelegramClient client, Update update)
        {
            return new(
                update.UpdateId,
                update.ApiMessage != null ? new TelegramMessage(client, update.ApiMessage) : null,
                update.EditedApiMessage != null ? new TelegramMessage(client, update.EditedApiMessage) : null,
                update.ChannelPost != null ? new TelegramMessage(client, update.ChannelPost) : null,
                update.EditedChannelPost != null ? new TelegramMessage(client, update.EditedChannelPost) : null);
        }
    }
}