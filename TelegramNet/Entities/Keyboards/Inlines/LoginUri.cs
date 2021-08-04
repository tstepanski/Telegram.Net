using System;
using TelegramNet.ExtraTypes;

namespace TelegramNet.Entities.Keyboards.Inlines
{
    public class LoginUri
    {
        public LoginUri(Uri url, string text = null, string botUsername = null, bool requestWriteAccess = false)
        {
            Url = url;
            Text = text;
            BotUsername = botUsername;
            RequestWriteAccess = requestWriteAccess;
        }

        public Uri Url { get; }

        public Optional<string> Text { get; }

        public Optional<string> BotUsername { get; }

        public Optional<bool> RequestWriteAccess { get; }

        internal object ToApiFormat()
        {
            return new
            {
                url = Url.ToString(),
                forward_text = Text.GetValueForce(),
                bot_username = BotUsername.GetValueForce(),
                request_write_access = RequestWriteAccess.GetValueForce()
            };
        }
    }
}