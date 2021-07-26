using System;
using TelegramNet.Entities;
using TelegramNet.Extensions.Base;
using TelegramNet.Services;
using TelegramNet.Types;

namespace TelegramNet
{
    public abstract class BaseTelegramClient : ITelegramClient
    {
        internal abstract TelegramApiClient TelegramApi { get; }

        protected internal TelegramExtensionClient ExtClient;

        private IServiceProvider _services;

        protected ExtensionCollection ExtensionCollection;

        public void AddServices(IServiceProvider services)
        {
            _services = services;
        }

        public void RegisterExtension(Extension ext)
        {
            if (ExtensionCollection == null)
                ExtensionCollection = new ExtensionCollection(ExtClient, _services);

            ExtensionCollection.AddExtension(ext);
        }

        public abstract SelfUser Me { get; }
        public abstract void Start();
        public abstract void Stop();
    }
}