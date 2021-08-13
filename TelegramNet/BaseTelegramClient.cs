using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TelegramNet.Entities;
using TelegramNet.Entities.Extra;
using TelegramNet.Entities.Interfaces;
using TelegramNet.Entities.Keyboards.Inlines;
using TelegramNet.Entities.Keyboards.Replies;
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

            Console.WriteLine(Constants.TelegramNetLogo);
            Console.WriteLine("GitHub: https://github.com/denvot/Telegram.NET");
            Console.WriteLine("NuGet: https://www.nuget.org/packages/TelegramDotNet/");
            Console.WriteLine(
                "You've found issue? Please describe this on GitHub: " +
                //https://github.com/DenVot/Telegram.Net/issues/new?title=What happened?&body=<h2>My problem is...</h2>%0D%0ASomething went wrong! Please help me! There is my code:%0D%0A%0D%0A```csharp%0D%0AConsole.WriteLine(%22Write your beautiful code here%22)%3B%0D%0A```%0D%0A<h2>Stacktrace (optional):</h2>%0D%0A%0D%0A```bash%0D%0ASystem.InvalidOperationException%0D%0A%09at SomeMethod(object a, object b, int c)%0D%0A```
                //The short link provided by https://involta.ru/tools/short-links/
                "https://inlnk.ru/q6E85"
                    .Replace(@"\",
                        string.Empty));
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
        /// Receives apiChat by id.
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
        /// Sends a message to the specified apiChat.
        /// </summary>
        /// <param name="idO">ApiChat ID</param>
        /// <param name="text">Text of message.</param>
        /// <param name="mode">Parsing mode (Markdown or HTML).</param>
        /// <param name="markup">An inline markup.</param>
        /// <param name="replyMarkup">A reply markup. The markup will send if <see cref="markup"/> is null.</param>
        /// <returns>The message, which was sent.</returns>
        [Obsolete("This method is obsolete. Use method SendMessageAsync with IKeyboard implementation.")]
        public abstract Task<TelegramClientMessage> SendMessageAsync(ChatId idO,
            string text,
            ParseMode mode = ParseMode.MarkdownV2,
            InlineKeyboardMarkup markup = null,
            ReplyKeyboardMarkup replyMarkup = null);

        /// <summary>
        /// Sends a message to the specified apiChat.
        /// </summary>
        /// <param name="id">ApiChat ID</param>
        /// <param name="text">Text of message.</param>
        /// <param name="mode">Parsing mode (Markdown or HTML).</param>
        /// <param name="keyboard">Keyboard</param>
        /// <returns>The message, which was sent.</returns>
        public abstract Task<TelegramClientMessage> SendMessageAsync(ChatId id,
            string text,
            ParseMode mode = ParseMode.MarkdownV2,
            IKeyboard keyboard = null);

        [Obsolete("This method is obsolete. Use method SendDocumentAsync with IKeyboard implementation.")]
        public abstract Task<TelegramClientMessage> SendDocumentAsync(ChatId idO,
            Uri fileUrl,
            Uri thumbUri = null,
            string caption = null,
            ParseMode parseMode = ParseMode.MarkdownV2,
            bool disableNotification = false,
            int replyToMessageId = default,
            bool allowSendingWithoutReply = false,
            InlineKeyboardMarkup inlineMarkup = null,
            ReplyKeyboardMarkup replyMarkup = null);

        public abstract Task<TelegramClientMessage> SendDocumentAsync(ChatId id,
            Uri fileUrl,
            Uri thumbUri = null,
            string caption = null,
            ParseMode parseMode = ParseMode.MarkdownV2,
            bool disableNotification = false,
            int replyToMessageId = default,
            bool allowSendingWithoutReply = false,
            IKeyboard keyboard = null);


        [Obsolete("This method is obsolete. Use method SendPhotoAsync with IKeyboard implementation.")]
        public abstract Task<TelegramClientMessage> SendPhotoAsync(ChatId idO,
            Uri photoUrl,
            string caption = null,
            ParseMode parseMode = ParseMode.MarkdownV2,
            bool disableNotification = false,
            int replyToMessageId = default,
            bool allowSendingWithoutReply = false,
            InlineKeyboardMarkup inlineMarkup = null,
            ReplyKeyboardMarkup replyMarkup = null);

        public abstract Task<TelegramClientMessage> SendPhotoAsync(ChatId id,
            Uri photoUrl,
            string caption = null,
            ParseMode parseMode = ParseMode.MarkdownV2,
            bool disableNotification = false,
            int replyToMessageId = default,
            bool allowSendingWithoutReply = false,
            IKeyboard keyboard = null);

        public delegate Task OnUpdateHandler(TelegramUpdate[] updates);

        /// <summary>
        /// Fires when the client has received a new update from the server.
        /// </summary>
        public event OnUpdateHandler OnUpdateReceived;
    }
}