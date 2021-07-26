using System;
using System.Threading.Tasks;

namespace TelegramNet.Extensions.Base
{
    public abstract class Extension
    {
        public abstract Task SetupAsync(TelegramExtensionClient extensionClient, IServiceProvider provider);
    }
}