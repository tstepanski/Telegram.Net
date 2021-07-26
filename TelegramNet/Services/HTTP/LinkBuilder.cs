using System;

namespace TelegramNet.Services.HTTP
{
    internal static class LinkBuilder
    {
        public static Uri Build(string token, string method)
        {
            return new($"https://api.telegram.org/bot{token}/{method}");
        }
    }
}