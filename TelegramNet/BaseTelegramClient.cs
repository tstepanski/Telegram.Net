using System;
using System.Linq;
using System.Threading.Tasks;
using TelegramNet.Entities;
using TelegramNet.Entities.Extra;
using TelegramNet.Entities.Keyboards;
using TelegramNet.Entities.Keyboards.Inlines;
using TelegramNet.Enums;
using TelegramNet.Extensions.Base;
using TelegramNet.Services.ReceivingUpdates;

namespace TelegramNet
{
    /// <summary>
    /// The bot's Telegram client.
    /// </summary>
    public abstract class BaseTelegramClient
    {
        /// <summary>
        /// Constructor for an abstract class. 
        /// </summary>
        /// <param name="token">Telegram bot's token</param>
        public BaseTelegramClient(string token)
        {
            TelegramApi = new TelegramApiClient(token);
            _worker = new UpdatingWorker(this);
        }

        internal TelegramApiClient TelegramApi { get; }

        private IServiceProvider _services;
        private readonly UpdatingWorker _worker;

        protected internal TelegramExtensionClient ExtClient;
        protected ExtensionCollection ExtensionCollection;

        /// <summary>
        /// Configures services for bot's extensions
        /// </summary>
        /// <param name="services">Services</param>
        public void ConfigureServices(IServiceProvider services)
        {
            _services = services;
        }

        /// <summary>
        /// Registers new extension.
        /// </summary>
        /// <param name="ext">Extension's module</param>
        public void RegisterExtension(Extension ext)
        {
            ExtensionCollection ??= new ExtensionCollection(this, ExtClient, _services);

            ExtensionCollection.AddExtension(ext);
        }

        /// <summary>
        /// Gets <see cref="SelfUser"/> which associated with current <see cref="BaseTelegramClient"/>.
        /// </summary>
        public abstract SelfUser Me { get; }

        /// <summary>
        /// Receives chat by id.
        /// </summary>
        /// <param name="id"></param>
        public abstract Task<TelegramChat> GetChatAsync(ChatId id);

        /// <summary>
        /// Starts pooling.
        /// </summary>
        public void Start()
        {
            _worker.StartUpdatingThread(new UpdateConfig
            {
                Timeout = 10,
                AllowedUpdates = new[] {"message"}
            }, x => OnUpdateReceived?.Invoke(x.Select(update => TelegramUpdate.FromUpdate(this, update)).ToArray()));
        }

        /// <summary>
        /// Ends pooling.
        /// </summary>
        public void Stop()
        {
            _worker.StopUpdatingThread();
        }

        /// <summary>
        /// Sends a message to the specified chat.
        /// </summary>
        /// <param name="chat">Chat ID</param>
        /// <param name="text">Text of message.</param>
        /// <param name="mode">Parsing mode (Markdown or HTML).</param>
        /// <param name="markup">An inline markup.</param>
        /// <param name="replyMarkup">A reply markup. The markup will send if <see cref="markup"/> is null.</param>
        /// <returns>The message, which was sent.</returns>
        public abstract Task<TelegramClientMessage> SendMessageAsync(ChatId chat, string text,
            ParseMode mode = ParseMode.MarkdownV2, InlineKeyboardMarkup markup = null,
            Entities.Keyboards.Replies.ReplyKeyboardMarkup replyMarkup = null);

        public delegate Task OnUpdateHandler(TelegramUpdate[] updates);

        /// <summary>
        /// Fires when the client has received a new update from the server.
        /// </summary>
        public event OnUpdateHandler OnUpdateReceived;
    }
}