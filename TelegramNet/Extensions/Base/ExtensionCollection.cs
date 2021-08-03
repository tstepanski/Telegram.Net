using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TelegramNet.Extensions.Base
{
    public class ExtensionCollection : IEnumerable<Extension>
    {
        internal ExtensionCollection(BaseTelegramClient bClient, TelegramExtensionClient client,
            IServiceProvider services)
        {
            _client = client;
            _services = services;
            _bClient = bClient;
        }

        private readonly TelegramExtensionClient _client;
        private readonly IServiceProvider _services;
        private readonly BaseTelegramClient _bClient;

        private readonly List<Extension> _extensions = new();

        public void AddExtension(Extension extension)
        {
            _extensions.Add(extension);
        }

        internal async Task RunAllExtensionsAsync()
        {
            foreach (var extension in _extensions) await extension.SetupAsync(_bClient, _client, _services);
        }

        public IEnumerator<Extension> GetEnumerator()
        {
            return _extensions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _extensions.GetEnumerator();
        }
    }
}