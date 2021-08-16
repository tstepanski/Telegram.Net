using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
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
		public delegate Task OnUpdateHandler(TelegramUpdate[] updates);

		private static Action<ILogger> _onConstructionLog;
		private readonly UpdatingWorker _worker;
		private IServiceProvider? _services;

		protected internal TelegramExtensionClient? ExtClient;
		protected ExtensionCollection? ExtensionCollection;

		static BaseTelegramClient()
		{
			_onConstructionLog = logger =>
			{
				var reportingUrl = @"https://inlnk.ru/q6E85".Replace(@"\", string.Empty);

				var logMessage = new StringBuilder(Constants.TelegramNetLogo)
					.AppendLine(@"GitHub: https://github.com/denvot/Telegram.NET")
					.AppendLine(@"NuGet: https://www.nuget.org/packages/TelegramDotNet/")
					.AppendLine(reportingUrl)
					.AppendLine($@"You've found issue? Please describe this on GitHub: {reportingUrl}")
					.ToString();

				logger.LogDebug(logMessage);

				_onConstructionLog = _ => { };
			};
		}

		/// <summary>
		/// Constructor for an abstract class. 
		/// </summary>
		/// <param name="token">Telegram bot's token</param>
		/// <param name="logger"></param>
		protected BaseTelegramClient(string token, ILogger logger)
		{
			_worker = new UpdatingWorker(this, logger);
			TelegramApi = new TelegramApiClient(token, logger);

			_onConstructionLog(logger);
		}

		internal TelegramApiClient TelegramApi { get; }

		/// <summary>
		/// Gets <see cref="SelfUser"/> which associated with current <see cref="BaseTelegramClient"/>.
		/// </summary>
		public abstract SelfUser? Me { get; }

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
		/// <param name="extension">Extension's module</param>
		public void RegisterExtension(Extension extension)
		{
			if (ExtClient == null || _services == null)
			{
				return;
			}

			ExtensionCollection ??= new ExtensionCollection(this, ExtClient, _services);

			ExtensionCollection.AddExtension(extension);
		}

		/// <summary>
		/// Receives apiChat by id.
		/// </summary>
		/// <param name="id"></param>
		public abstract Task<TelegramChat> GetChatAsync(ChatId id);

		/// <summary>
		/// Starts pooling.
		/// </summary>
		public Task Start()
		{
			var updateConfiguration = new UpdateConfig
			{
				Timeout = 10,
				AllowedUpdates = new[] { "message" }
			};

			return _worker.StartUpdatingThreadAsync(updateConfiguration, HandOffUpdates);
		}

		/// <summary>
		/// Ends pooling.
		/// </summary>
		public void Stop()
		{
			_worker.StopUpdatingThread();
		}

		private void HandOffUpdates(Update[] updates)
		{
			var recentUpdates = updates
				.Select(update => TelegramUpdate.FromUpdate(this, update))
				.ToArray();

			OnUpdateReceived?.Invoke(recentUpdates);
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
			InlineKeyboardMarkup? markup = null,
			ReplyKeyboardMarkup? replyMarkup = null);

		/// <summary>
		/// Sends a message to the specified apiChat.
		/// </summary>
		/// <param name="id">ApiChat ID</param>
		/// <param name="text">Text of message.</param>
		/// <param name="mode">Parsing mode (Markdown or HTML).</param>
		/// <param name="keyboard">Keyboard</param>
		/// <returns>The message, which was sent.</returns>
		public abstract Task<TelegramClientMessage?> SendMessageAsync(ChatId id, string text,
			ParseMode mode = ParseMode.MarkdownV2, IKeyboard? keyboard = null);

		[Obsolete("This method is obsolete. Use method SendDocumentAsync with IKeyboard implementation.")]
		public abstract Task<TelegramClientMessage> SendDocumentAsync(ChatId idO,
			Uri fileUrl,
			Uri? thumbUri = null,
			string? caption = null,
			ParseMode parseMode = ParseMode.MarkdownV2,
			bool disableNotification = false,
			int replyToMessageId = default,
			bool allowSendingWithoutReply = false,
			InlineKeyboardMarkup? inlineMarkup = null,
			ReplyKeyboardMarkup? replyMarkup = null);

		public abstract Task<TelegramClientMessage?> SendDocumentAsync(ChatId id, Uri fileUrl, Uri? thumbUri = null,
			string? caption = null, ParseMode parseMode = ParseMode.MarkdownV2, bool disableNotification = false,
			int replyToMessageId = default, bool allowSendingWithoutReply = false, IKeyboard? keyboard = null);


		[Obsolete("This method is obsolete. Use method SendPhotoAsync with IKeyboard implementation.")]
		public abstract Task<TelegramClientMessage> SendPhotoAsync(ChatId idO,
			Uri photoUrl,
			string? caption = null,
			ParseMode parseMode = ParseMode.MarkdownV2,
			bool disableNotification = false,
			int replyToMessageId = default,
			bool allowSendingWithoutReply = false,
			InlineKeyboardMarkup? inlineMarkup = null,
			ReplyKeyboardMarkup? replyMarkup = null);

		public abstract Task<TelegramClientMessage?> SendPhotoAsync(ChatId id, Uri photoUrl, string? caption = null,
			ParseMode parseMode = ParseMode.MarkdownV2, bool disableNotification = false,
			int replyToMessageId = default, bool allowSendingWithoutReply = false, IKeyboard? keyboard = null);

		/// <summary>
		/// Fires when the client has received a new update from the server.
		/// </summary>
		public event OnUpdateHandler? OnUpdateReceived;
	}
}