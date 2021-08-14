using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using TelegramNet.Helpers;
using TelegramNet.Logging;

namespace TelegramNet.Services.ReceivingUpdates
{
    internal class UpdatingWorker
    {
        private int _lastId = 0;

        public UpdatingWorker(BaseTelegramClient client)
        {
            _api = client.TelegramApi;
        }

        private bool _stop;
        private Thread _thr;
        private readonly TelegramApiClient _api;

        public void StartUpdatingThread(UpdateConfig config, Action<Update[]> onUpdate)
        {
            var pongReqNeed = true;
            _stop = false;
            _thr = new Thread(async () =>
            {
                while (!_stop)
                {
                    config.Offset = _lastId + 1;
                    var response =
                        await _api.RequestAsync<Update[]>("getUpdates", HttpMethod.Get, config.ToJson());

                    if (response != null)
                    {
                        if (pongReqNeed)
                        {
                            Logger.Log($"First update response got. Count: {response.Length}.",
                                LogSource.TelegramApiServer);
                            pongReqNeed = false;
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
                        pongReqNeed = false;
                    }

                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
            });

            _thr.Start();
        }

        public void StopUpdatingThread()
        {
            _stop = true;
        }
    }
}