using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TelegramNet.Entities;
using TelegramNet.Entities.Interfaces;
using TelegramNet.Events;
using TelegramNet.Helpers;
using TelegramNet.Services;
using TelegramNet.Services.HTTP;
using TelegramNet.Services.HTTP.Entities;
using TelegramNet.Services.ReceivingUpdates;
using TelegramNet.Types;

namespace TelegramNet
{
    ///<inheritdoc/>
    public class TelegramClient : BaseTelegramClient
    {
        private readonly UpdatingWorker _worker;
        private readonly HttpRequester _requester;

        #region ~EVENTS~

        #region ~DELEGATES~

        public delegate Task MessageActionHandler(ITelegramUser sender, TelegramMessage message);

        #endregion

        public event MessageActionHandler OnMessageReceived;
        public event MessageActionHandler OnMessageEdited;
        public event MessageActionHandler OnChannelPost;
        public event MessageActionHandler OnChannelPostEdited;

        #region ~WORKERS~

        private void OnUpdate(Update[] updates)
        {
            foreach (var update in updates)
            {
                if (update.Message != null)
                {
                    var mess = new TelegramMessage(this, update.Message);
                    OnMessageReceived?.Invoke(mess.Author, mess);
                }

                if (update.EditedMessage != null)
                {
                    var mess = new TelegramMessage(this, update.EditedMessage);
                    OnMessageEdited?.Invoke(mess.Author, mess);
                }

                if (update.ChannelPost != null)
                {
                    var mess = new TelegramMessage(this, update.ChannelPost);
                    OnChannelPost?.Invoke(mess.Author, mess);
                }

                if (update.EditedChannelPost != null)
                {
                    var mess = new TelegramMessage(this, update.EditedChannelPost);
                    OnChannelPostEdited?.Invoke(mess.Author, mess);
                }
            }
        }

        #endregion

        #endregion

        /// <inheritdoc/>
        internal override TelegramApiClient TelegramApi { get; }

        public TelegramClient(string token)
        {
            _worker = new UpdatingWorker(this);
            _requester = new HttpRequester(token);
            TelegramApi = new TelegramApiClient(this);
            ExtClient = new TelegramExtensionClient(this);
        }

        /// <inheritdoc/>
        public override SelfUser Me
        {
            get
            {
                var me = _requester.ExecuteMethod<User>("getMe", HttpMethod.Get);

                return me.Ok ? new SelfUser(this, me.Result.Value) : null;
            }
        }

        /// <inheritdoc/>
        public override void Start()
        {
            _worker.StartUpdatingThread(new UpdateConfig
            {
                Timeout = 10,
                AllowedUpdates = new[] {"message"}
            }, OnUpdate);
        }

        /// <inheritdoc/>
        public override void Stop()
        {
            _worker.StopUpdatingThread();
        }

        internal async Task<RequestResult<T>> ExecuteMethodAsync<T>(string methodName, HttpMethod method,
            string json = null)
        {
            return await _requester.ExecuteMethodAsync<T>(methodName, method, json);
        }

        internal async Task<HttpResult> ExecuteMethodAsync(string methodName, HttpMethod method, string json = null)
        {
            return await _requester.ExecuteMethodAsync(methodName, method, json);
        }

        public async Task<TelegramChat> GetChatAsync(ChatId chat)
        {
            var parseAble = int.TryParse(chat.Id, out var id);

            return new TelegramChat(this, await TelegramApi.RequestAsync<Chat>("getChat", HttpMethod.Get,
                new Dictionary<string, object>
                {
                    {"chat_id", parseAble ? id : chat.Id}
                }.ToJson()));
        }
    }
}