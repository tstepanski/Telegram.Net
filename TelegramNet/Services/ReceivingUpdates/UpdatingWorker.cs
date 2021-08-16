using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TelegramNet.Helpers;

namespace TelegramNet.Services.ReceivingUpdates
{
	internal sealed class UpdatingWorker
	{
		private static readonly TimeSpan OneSecond = TimeSpan.FromSeconds(1);
		private readonly TelegramApiClient _api;
		private readonly ILogger _logger;
		private int _lastId;
		private bool _stopped;

		public UpdatingWorker(BaseTelegramClient client, ILogger logger)
		{
			_logger = logger;
			_api = client.TelegramApi;
		}

		public async Task StartUpdatingThreadAsync(UpdateConfig config, Action<Update[]> onUpdate)
		{
			var pingReqNeed = true;

			_stopped = false;

			while (!_stopped)
			{
				config.Offset = _lastId + 1;

				var response =
					await _api.RequestAsync<Update[]?>("getUpdates", HttpMethod.Get, config.ToJson());

				if (response != null)
				{
					if (pingReqNeed)
					{
						_logger.LogDebug($@"First update response got. Count: {response.Length}");

						pingReqNeed = false;
					}

					if (response.Length > 0)
					{
						_lastId = response.Select(x => x.UpdateId).OrderByDescending(x => x).First();
					}

					onUpdate?.Invoke(response);
				}
				else
				{
					_logger.LogWarning(@"Telegram API server is unavailable now.");

					pingReqNeed = false;
				}

				await Task.Delay(OneSecond);
			}
		}

		public void StopUpdatingThread()
		{
			_stopped = true;
		}
	}
}