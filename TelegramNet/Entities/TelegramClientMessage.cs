using System.Net.Http;
using System.Threading.Tasks;
using TelegramNet.Helpers;
using TelegramNet.Services;
using TelegramNet.Types;

namespace TelegramNet.Entities
{
    public class TelegramClientMessage : TelegramMessage
    {
        internal TelegramClientMessage(BaseTelegramClient client, Message message) : base(client, message)
        {
            _client = client.TelegramApi;
        }

        private readonly TelegramApiClient _client;

        public async Task<bool> EditAsync(MessageEditor editor)
        {
            // editor.Validation();
            editor.ChatId = int.Parse(Chat.Id.Id);
            editor.MessageId = Id;
            var json = editor.ToJson();

            var result = await _client.RequestAsync("editMessageText", HttpMethod.Post, json);

            return result.Ok;
        }
    }
}