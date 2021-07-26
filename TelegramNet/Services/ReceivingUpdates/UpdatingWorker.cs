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

        public UpdatingWorker(TelegramClient client)
        {
            _client = client;
        }

        private readonly TelegramClient _client;

        private bool _stop;
        private Thread _thr;

        public void StartUpdatingThread(UpdateConfig config, Action<Update[]> onUpdate)
        {
            _stop = false;
            _thr = new Thread(async () =>
            {
                while (!_stop)
                {
                    config.Offset = lastId + 1;
                    var response =
                        await _client.ExecuteMethodAsync<Update[]>("getUpdates", HttpMethod.Get, config.ToJson());

                    if (response.Ok)
                    {
                        if (response.Result.Value.Length > 0)
                            lastId = response.Result.Value.Select(x => x.UpdateId).OrderByDescending(x => x).First();

                        onUpdate?.Invoke(response.Result.Value);
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