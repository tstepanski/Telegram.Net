using System;
using System.Threading.Tasks;

namespace TelegramNet.Extensions.Base
{
    public abstract class Extension
    {
        public abstract Task SetupAsync(BaseTelegramClient client, TelegramExtensionClient extensionClient,
            IServiceProvider provider);
    }
}