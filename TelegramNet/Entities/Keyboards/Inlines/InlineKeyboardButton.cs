using System;
using TelegramNet.ExtraTypes;

namespace TelegramNet.Entities.Keyboards.Inlines
{
    public class InlineKeyboardButton
    {
        public InlineKeyboardButton(string text,
            Uri url = null,
            LoginUri loginUrl = null,
            string callbackData = null,
            string switchInlineQuery = null,
            string switchInlineQueryCurrentChat = null,
            bool pay = false)
        {
            var urlIsNull = url == null;
            var loginUrlIsNull = loginUrl == null;
            var callbackDataIsNull = callbackData == null;

            if (urlIsNull && loginUrlIsNull && callbackDataIsNull)
                throw new InvalidOperationException(
                    $"{nameof(InlineKeyboardButton)} must have url or loginUrl or callbackData.");

            Text = text;
            Url = url;
            LoginUrl = loginUrl;
            CallbackData = callbackData;
            SwitchInlineQuery = switchInlineQuery;
            SwitchInlineQueryCurrentChat = switchInlineQueryCurrentChat;
            Pay = pay;
        }

        public string Text { get; }

        public Optional<Uri> Url { get; }

        public Optional<LoginUri> LoginUrl { get; }

        public Optional<string> CallbackData { get; }

        public Optional<string> SwitchInlineQuery { get; }

        public Optional<string> SwitchInlineQueryCurrentChat { get; }

        public Optional<bool> Pay { get; }

        internal object ToApiFormat()
        {
            return new
            {
                text = Text,
                url = Url.HasValue
                    ? Url.Value.ToString()
                    : string.Empty,
                login_url = LoginUrl.HasValue
                    ? LoginUrl.Value.ToApiFormat()
                    : null,
                callback_data = CallbackData.GetValueForce(),
                switch_inline_query = SwitchInlineQuery.GetValueForce(),
                switch_inline_query_current_chat = SwitchInlineQueryCurrentChat.GetValueForce(),
                pay = Pay.GetValueForce()
            };
        }

        public static InlineKeyboardButton WithCallbackData(string text, string callbackData)
        {
            return new(text, callbackData: callbackData);
        }

        public static InlineKeyboardButton WithUrl(string text, Uri url)
        {
            return new(text, url);
        }

        public static InlineKeyboardButton WithLoginUrl(string text, LoginUri url)
        {
            return new(text, loginUrl: url);
        }
    }
}