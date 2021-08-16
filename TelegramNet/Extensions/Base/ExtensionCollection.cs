using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TelegramNet.Extensions.Base
{
	public sealed class ExtensionCollection : IEnumerable<Extension>
	{
		private readonly BaseTelegramClient _bClient;

		private readonly TelegramExtensionClient _client;

		private readonly List<Extension> _extensions = new();
		private readonly IServiceProvider _services;

		internal ExtensionCollection(BaseTelegramClient bClient, TelegramExtensionClient client,
			IServiceProvider services)
		{
			_client = client;
			_services = services;
			_bClient = bClient;
		}

		public IEnumerator<Extension> GetEnumerator()
		{
			return _extensions.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _extensions.GetEnumerator();
		}

		public void AddExtension(Extension extension)
		{
			_extensions.Add(extension);
		}

		internal async Task RunAllExtensionsAsync()
		{
			foreach (var extension in _extensions)
			{
				await extension.SetupAsync(_bClient, _client, _services);
			}
		}
	}
}