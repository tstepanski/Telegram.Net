using System;
using System.Threading.Tasks;
using TelegramNet.Entities;
using TelegramNet.Entities.Interfaces;
using TelegramNet.Entities.Keyboards.Inlines;
using TelegramNet.Entities.Keyboards.Replies;
using TelegramNet.Enums;
using TelegramNet.Extensions.Base;

// ReSharper disable EventNeverSubscribedTo.Global

namespace TelegramNet
{
	public interface ITelegramClient
	{
		SelfUser? Me { get; }

		/// <summary>
		/// Fires when the client has received a new message. Needs pooling (<see cref="BaseTelegramClient.Start()"/>).
		/// </summary>
		event TelegramClient.MessageActionHandler? OnMessageReceived;

		/// <summary>
		/// Fires when the message has edited. Needs pooling (<see cref="BaseTelegramClient.Start()"/>).
		/// </summary>
		event TelegramClient.MessageActionHandler? OnMessageEdited;

		/// <summary>
		/// Fires when the client has received a new post. Needs pooling (<see cref="BaseTelegramClient.Start()"/>).
		/// </summary>
		event TelegramClient.MessageActionHandler? OnChannelPost;

		/// <summary>
		/// Fires when the post has edited. Needs pooling (<see cref="BaseTelegramClient.Start()"/>).
		/// </summary>
		event TelegramClient.MessageActionHandler? OnChannelPostEdited;

		Task<TelegramChat> GetChatAsync(ChatId chat);

		Task<TelegramClientMessage> SendMessageAsync(ChatId chat,
			string text,
			ParseMode mode = ParseMode.MarkdownV2,
			InlineKeyboardMarkup? inlineMarkup = null,
			ReplyKeyboardMarkup? replyMarkup = null);

		Task<TelegramClientMessage?> SendMessageAsync(ChatId id, string text,
			ParseMode mode = ParseMode.MarkdownV2, IKeyboard? keyboard = null);

		Task<TelegramClientMessage> SendDocumentAsync(ChatId chat,
			Uri fileUrl,
			Uri? thumbUri = null,
			string? caption = null,
			ParseMode mode = ParseMode.MarkdownV2,
			bool disableNotification = false,
			int replyToMessageId = default,
			bool allowSendingWithoutReply = false,
			InlineKeyboardMarkup? inlineMarkup = null,
			ReplyKeyboardMarkup? replyMarkup = null);

		Task<TelegramClientMessage?> SendDocumentAsync(ChatId id, Uri fileUrl,
			Uri? thumbUri = null, string? caption = null, ParseMode parseMode = ParseMode.MarkdownV2,
			bool disableNotification = false, int replyToMessageId = default, bool allowSendingWithoutReply = false,
			IKeyboard? keyboard = null);

		Task<TelegramClientMessage> SendPhotoAsync(ChatId chat,
			Uri photoUrl,
			string? caption = null,
			ParseMode parseMode = ParseMode.MarkdownV2,
			bool disableNotification = false,
			int replyToMessageId = default,
			bool allowSendingWithoutReply = false,
			InlineKeyboardMarkup? inlineMarkup = null,
			ReplyKeyboardMarkup? replyMarkup = null);

		Task<TelegramClientMessage?> SendPhotoAsync(ChatId id, Uri photoUrl,
			string? caption = null, ParseMode parseMode = ParseMode.MarkdownV2,
			bool disableNotification = false, int replyToMessageId = default, bool allowSendingWithoutReply = false,
			IKeyboard? keyboard = null);

		/// <summary>
		/// Configures services for bot's extensions
		/// </summary>
		/// <param name="services">Services</param>
		void ConfigureServices(IServiceProvider services);

		/// <summary>
		/// Registers new extension.
		/// </summary>
		/// <param name="extension">Extension's module</param>
		void RegisterExtension(Extension extension);

		/// <summary>
		/// Starts pooling.
		/// </summary>
		Task Start();

		/// <summary>
		/// Ends pooling.
		/// </summary>
		void Stop();

		/// <summary>
		/// Fires when the client has received a new update from the server.
		/// </summary>
		event BaseTelegramClient.OnUpdateHandler? OnUpdateReceived;
	}
}