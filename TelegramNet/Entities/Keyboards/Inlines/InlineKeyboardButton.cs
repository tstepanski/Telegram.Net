using System;
using TelegramNet.Entities.Interfaces;
using TelegramNet.ExtraTypes;
using TelegramNet.Types.Inlines;

namespace TelegramNet.Entities.Keyboards.Inlines
{
    public class InlineKeyboardButton : IApiFormatable
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
            {
	            throw new InvalidOperationException(
		            $"{nameof(InlineKeyboardButton)} must have url or loginUrl or callbackData.");
            }

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

        object IApiFormatable.GetApiFormat()
        {
            return new ApiInlineKeyboardButton
            {
                Text = Text,
                Url = Url.GetValueForce()?.ToString(),
                CallbackData = CallbackData.GetValueForce(),
                SwitchInlineQuery = SwitchInlineQuery.GetValueForce(),
                SwitchInlineQueryCurrentChat = SwitchInlineQueryCurrentChat.GetValueForce(),
                Pay = Pay.GetValueForce()
            };
        }
    }
}