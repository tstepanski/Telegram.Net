using System.Net.Http;
using System.Threading.Tasks;
using TelegramNet.Enums;
using TelegramNet.Helpers;
using TelegramNet.Types;

namespace TelegramNet.Entities
{
    public class TelegramClientMessage : TelegramMessage
    {
        internal TelegramClientMessage(BaseTelegramClient client, Message message, ParseMode mode) : base(client,
            message)
        {
            _client = client.TelegramApi;
            _mode = mode;
        }

        private readonly TelegramApiClient _client;
        private readonly ParseMode _mode;

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
            return await EditTextAsync(new MessageTextEditor().WithText(Text.GetValueForce()).WithParseMode(_mode));
        }
    }
}