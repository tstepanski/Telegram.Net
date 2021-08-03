using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using TelegramNet.Helpers;

namespace TelegramNet.Services.ReceivingUpdates
{
    internal class UpdatingWorker
    {
        private int lastId = 0;

        public UpdatingWorker(BaseTelegramClient client)
        {
            _api = client.TelegramApi;
        }

        private bool _stop;
        private Thread _thr;
        private readonly TelegramApiClient _api;

        public void StartUpdatingThread(UpdateConfig config, Action<Update[]> onUpdate)
        {
            _stop = false;
            _thr = new Thread(async () =>
            {
                while (!_stop)
                {
                    config.Offset = lastId + 1;
                    var response =
                        await _api.RequestAsync<Update[]>("getUpdates", HttpMethod.Get, config.ToJson());

                    if (response != null)
                    {
                        if (response.Length > 0)
                            lastId = response.Select(x => x.UpdateId).OrderByDescending(x => x).First();

                        onUpdate?.Invoke(response);
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