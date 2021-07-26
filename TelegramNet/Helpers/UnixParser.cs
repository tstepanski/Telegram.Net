using System;

namespace TelegramNet.Helpers
{
    public static class UnixParser
    {
        public static DateTime Parse(int time)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            start = start.AddSeconds(time);
            return start;
        }
    }
}