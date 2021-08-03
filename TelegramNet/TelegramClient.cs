using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TelegramNet.Entities;
using TelegramNet.Entities.Extra;
using TelegramNet.Entities.Keyboards.Inlines;
using TelegramNet.Enums;
using TelegramNet.Helpers;
using TelegramNet.Services.Http;
using TelegramNet.Types;

namespace TelegramNet
{
    /// <summary>
    /// Telegram client. Base implementation of <see cref="BaseTelegramClient"/>.
    /// </summary>
    public class TelegramClient : BaseTelegramClient
    {
        public TelegramClient(string token) : base(token)
        {
            _requester = new HttpRequester(token);
            ExtClient = new TelegramExtensionClient(this);

            OnUpdateReceived += OnUpdate;
        }

        private readonly HttpRequester _requester;

        #region ~EVENTS~

        #region ~DELEGATES~

        public delegate Task MessageActionHandler(TelegramMessage message);

        #endregion

        /// <summary>
        /// Fires when the client has received a new message. Needs pooling (<see cref="BaseTelegramClient.Start()"/>).
        /// </summary>
        public event MessageActionHandler OnMessageReceived;

        /// <summary>
        /// Fires when the message has edited. Needs pooling (<see cref="BaseTelegramClient.Start()"/>).
        /// </summary>
        public event MessageActionHandler OnMessageEdited;

        /// <summary>
        /// Fires when the client has received a new post. Needs pooling (<see cref="BaseTelegramClient.Start()"/>).
        /// </summary>
        public event MessageActionHandler OnChannelPost;

        /// <summary>
        /// Fires when the post has edited. Needs pooling (<see cref="BaseTelegramClient.Start()"/>).
        /// </summary>
        public event MessageActionHandler OnChannelPostEdited;

        #region ~EVENT WORKERS~

        private Task OnUpdate(TelegramUpdate[] updates)
        {
            foreach (var update in updates)
            {
                if (update.Message != null) OnMessageReceived?.Invoke(update.Message);

                if (update.EditedMessage != null) OnMessageEdited?.Invoke(update.EditedMessage);

                if (update.ChannelPost != null) OnChannelPost?.Invoke(update.ChannelPost);

                if (update.EditedChannelPost != null) OnChannelPostEdited?.Invoke(update.EditedChannelPost);
            }

            return Task.CompletedTask;
        }

        #endregion

        #endregion

        #region ~PROPERTIES~

        /// <inheritdoc/>
        public override SelfUser Me
        {
            get
            {
                var me = _requester.ExecuteMethod<User>("getMe", HttpMethod.Get);

                return me.Ok ? new SelfUser(this, me.Result.Value) : null;
            }
        }

        #endregion

        #region ~METHODS~

        /// <inheritdoc/>
        public override async Task<TelegramChat> GetChatAsync(ChatId chat)
        {
            return new(this, await TelegramApi.RequestAsync<Chat>("getChat", HttpMethod.Get,
                new Dictionary<string, object>
                {
                    {"chat_id", chat.Fetch()}
                }.ToJson()));
        }

        /// <inheritdoc/>
        public override async Task<TelegramClientMessage> SendMessageAsync(ChatId chat,
            string text,
            ParseMode mode = ParseMode.MarkdownV2,
            InlineKeyboardMarkup inlineMarkup = null,
            Entities.Keyboards.Replies.ReplyKeyboardMarkup replyMarkup = null)
        {
            object toSerialize = inlineMarkup == null
                ? replyMarkup == null ? new
                {
                    chat_id = chat.Fetch(),
                    text,
                    parse_mode = mode.ToApiString()
                }
                : new
                {
                    chat_id = chat.Fetch(),
                    text,
                    parse_mode = mode.ToApiString(),
                    reply_markup = (object) replyMarkup.GetApiFormat()
                }
                : new
                {
                    chat_id = chat.Fetch(),
                    text,
                    parse_mode = mode.ToApiString(),
                    reply_markup = inlineMarkup.ToApiFormat()
                };

            var message = await TelegramApi.RequestAsync<Message>("sendMessage", HttpMethod.Post, toSerialize.ToJson());

            return new TelegramClientMessage(this, message, mode);
        }

        #endregion
    }
}