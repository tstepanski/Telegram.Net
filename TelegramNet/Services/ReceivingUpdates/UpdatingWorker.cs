using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TelegramNet.Helpers;
using TelegramNet.Logging;

namespace TelegramNet.Services.ReceivingUpdates
{
	internal sealed class UpdatingWorker
	{
		private static readonly TimeSpan OneSecond = TimeSpan.FromSeconds(1);
		private readonly TelegramApiClient _api;
		private int _lastId;
		private bool _stopped;

		public UpdatingWorker(BaseTelegramClient client)
		{
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
						Logger.Log($"First update response got. Count: {response.Length}.",
							LogSource.TelegramApiServer);

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
					Logger.Log("Telegram API server is unavailable now.", LogSource.Warn);

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