using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TelegramNet.Entities;
using TelegramNet.Entities.Extra;
using TelegramNet.Entities.Interfaces;
using TelegramNet.Entities.Keyboards.Inlines;
using TelegramNet.Entities.Keyboards.Replies;
using TelegramNet.Enums;
using TelegramNet.Helpers;
using TelegramNet.Logging;
using TelegramNet.Services.Http;
using TelegramNet.Types;

namespace TelegramNet
{
	/// <summary>
	/// Telegram client. Base implementation of <see cref="BaseTelegramClient"/>.
	/// </summary>
	public sealed class TelegramClient : BaseTelegramClient
	{
		private readonly HttpRequester _requester;

		public TelegramClient(string token, bool loggingToConsole = false) : base(token)
		{
			_requester = new HttpRequester(token);
			ExtClient = new TelegramExtensionClient(this);

			Logger.UseConsole(loggingToConsole);
			OnUpdateReceived += OnUpdate;
		}

		#region ~PROPERTIES~

		/// <inheritdoc/>
		public override SelfUser? Me
		{
			get
			{
				var me = _requester.ExecuteMethod<ApiUser>("getMe", HttpMethod.Get);

				return me.Ok && me.Result != null
					? new SelfUser(this, me.Result)
					: null;
			}
		}

		#endregion

		#region ~EVENTS~

		#region ~DELEGATES~

		public delegate Task MessageActionHandler(TelegramMessage message);

		#endregion

		/// <summary>
		/// Fires when the client has received a new message. Needs pooling (<see cref="BaseTelegramClient.Start()"/>).
		/// </summary>
		public event MessageActionHandler? OnMessageReceived;

		/// <summary>
		/// Fires when the message has edited. Needs pooling (<see cref="BaseTelegramClient.Start()"/>).
		/// </summary>
		public event MessageActionHandler? OnMessageEdited;

		/// <summary>
		/// Fires when the client has received a new post. Needs pooling (<see cref="BaseTelegramClient.Start()"/>).
		/// </summary>
		public event MessageActionHandler? OnChannelPost;

		/// <summary>
		/// Fires when the post has edited. Needs pooling (<see cref="BaseTelegramClient.Start()"/>).
		/// </summary>
		public event MessageActionHandler? OnChannelPostEdited;

		#region ~EVENT WORKERS~

		private Task OnUpdate(TelegramUpdate[] updates)
		{
			foreach (var update in updates)
			{
				if (update.Message != null)
				{
					OnMessageReceived?.Invoke(update.Message);
				}

				if (update.EditedMessage != null)
				{
					OnMessageEdited?.Invoke(update.EditedMessage);
				}

				if (update.ChannelPost != null)
				{
					OnChannelPost?.Invoke(update.ChannelPost);
				}

				if (update.EditedChannelPost != null)
				{
					OnChannelPostEdited?.Invoke(update.EditedChannelPost);
				}
			}

			return Task.CompletedTask;
		}

		#endregion

		#endregion

		#region ~METHODS~

		/// <inheritdoc/>
		public override async Task<TelegramChat> GetChatAsync(ChatId chat)
		{
			return new TelegramChat(this, await TelegramApi.RequestAsync<ApiChat>("getChat", HttpMethod.Get,
				new Dictionary<string, object?>
				{
					{ "chat_id", chat.Fetch() }
				}.ToJson()));
		}

		/// <inheritdoc/>
		[Obsolete("This method is obsolete. Use method SendMessageAsync with IKeyboard implementation.")]
		public override async Task<TelegramClientMessage> SendMessageAsync(ChatId chat,
			string text,
			ParseMode mode = ParseMode.MarkdownV2,
			InlineKeyboardMarkup? inlineMarkup = null,
			ReplyKeyboardMarkup? replyMarkup = null)
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
					reply_markup = (replyMarkup as IProvidesApiFormat).GetApiFormat()
				}
				: new
				{
					chat_id = chat.Fetch(),
					text,
					parse_mode = mode.ToApiString(),
					reply_markup = (inlineMarkup as IProvidesApiFormat).GetApiFormat()
				};

			var message =
				await TelegramApi.RequestAsync<ApiMessage>("sendMessage", HttpMethod.Post, toSerialize.ToJson());

			return new TelegramClientMessage(this, message, mode);
		}

		public override async Task<TelegramClientMessage?> SendMessageAsync(ChatId id, string text,
			ParseMode mode = ParseMode.MarkdownV2, IKeyboard? keyboard = null)
		{
			object toSerialize = keyboard == null
				? new
				{
					chat_id = id.Fetch(),
					text,
					parse_mode = mode.ToApiString()
				}
				: new
				{
					chat_id = id.Fetch(),
					text,
					parse_mode = mode.ToApiString(),
					reply_markup = (keyboard as IProvidesApiFormat)?.GetApiFormat()
				};

			var message =
				await TelegramApi.RequestAsync<ApiMessage>("sendMessage", HttpMethod.Post, toSerialize.ToJson());

			return new TelegramClientMessage(this, message, mode);
		}

		[Obsolete("This method is obsolete. Use method SendDocumentAsync with IKeyboard implementation.")]
		public override async Task<TelegramClientMessage> SendDocumentAsync(ChatId chat,
			Uri fileUrl,
			Uri? thumbUri = null,
			string? caption = null,
			ParseMode mode = ParseMode.MarkdownV2,
			bool disableNotification = false,
			int replyToMessageId = default,
			bool allowSendingWithoutReply = false,
			InlineKeyboardMarkup? inlineMarkup = null,
			ReplyKeyboardMarkup? replyMarkup = null)
		{
			object toSerialize = inlineMarkup == null
				? replyMarkup == null ? new
				{
					chat_id = chat.Fetch(),
					document = fileUrl.ToString(),
					thumb = thumbUri?.ToString(),
					parse_mode = mode.ToApiString(),
					caption,
					disable_notification = disableNotification,
					reply_to_message_id = replyToMessageId,
					allow_sending_without_reply = allowSendingWithoutReply
				}
				: new
				{
					chat_id = chat.Fetch(),
					document = fileUrl.ToString(),
					thumb = thumbUri?.ToString(),
					parse_mode = mode.ToApiString(),
					caption,
					disable_notification = disableNotification,
					reply_to_message_id = replyToMessageId,
					allow_sending_without_reply = allowSendingWithoutReply,
					reply_markup = (replyMarkup as IProvidesApiFormat).GetApiFormat()
				}
				: new
				{
					chat_id = chat.Fetch(),
					document = fileUrl.ToString(),
					thumb = thumbUri?.ToString(),
					parse_mode = mode.ToApiString(),
					caption,
					disable_notification = disableNotification,
					reply_to_message_id = replyToMessageId,
					allow_sending_without_reply = allowSendingWithoutReply,
					reply_markup = (inlineMarkup as IProvidesApiFormat).GetApiFormat()
				};

			var message =
				await TelegramApi.RequestAsync<ApiMessage>("sendDocument", HttpMethod.Post, toSerialize.ToJson());

			return new TelegramClientMessage(this, message, mode);
		}

		public override async Task<TelegramClientMessage?> SendDocumentAsync(ChatId id, Uri fileUrl,
			Uri? thumbUri = null, string? caption = null, ParseMode parseMode = ParseMode.MarkdownV2,
			bool disableNotification = false, int replyToMessageId = default, bool allowSendingWithoutReply = false,
			IKeyboard? keyboard = null)
		{
			object toSerialize = keyboard == null
				? new
				{
					chat_id = id.Fetch(),
					document = fileUrl.ToString(),
					thumb = thumbUri?.ToString(),
					parse_mode = parseMode.ToApiString(),
					caption,
					disable_notification = disableNotification,
					reply_to_message_id = replyToMessageId,
					allow_sending_without_reply = allowSendingWithoutReply
				}
				: new
				{
					chat_id = id.Fetch(),
					document = fileUrl.ToString(),
					thumb = thumbUri?.ToString(),
					parse_mode = parseMode.ToApiString(),
					caption,
					disable_notification = disableNotification,
					reply_to_message_id = replyToMessageId,
					allow_sending_without_reply = allowSendingWithoutReply,
					reply_markup = (keyboard as IProvidesApiFormat)?.GetApiFormat()
				};

			var message =
				await TelegramApi.RequestAsync<ApiMessage>("sendDocument", HttpMethod.Post, toSerialize.ToJson());

			return new TelegramClientMessage(this, message, parseMode);
		}

		[Obsolete("This method is obsolete. Use method SendPhotoAsync with IKeyboard implementation.")]
		public override async Task<TelegramClientMessage> SendPhotoAsync(ChatId chat,
			Uri photoUrl,
			string? caption = null,
			ParseMode parseMode = ParseMode.MarkdownV2,
			bool disableNotification = false,
			int replyToMessageId = default,
			bool allowSendingWithoutReply = false,
			InlineKeyboardMarkup? inlineMarkup = null,
			ReplyKeyboardMarkup? replyMarkup = null)
		{
			object toSerialize = inlineMarkup == null
				? replyMarkup == null ? new
				{
					chat_id = chat.Fetch(),
					photo = photoUrl.ToString(),
					parse_mode = parseMode.ToApiString(),
					caption,
					disable_notification = disableNotification,
					reply_to_message_id = replyToMessageId,
					allow_sending_without_reply = allowSendingWithoutReply
				}
				: new
				{
					chat_id = chat.Fetch(),
					photo = photoUrl.ToString(),
					parse_mode = parseMode.ToApiString(),
					caption,
					disable_notification = disableNotification,
					reply_to_message_id = replyToMessageId,
					allow_sending_without_reply = allowSendingWithoutReply,
					reply_markup = (replyMarkup as IProvidesApiFormat).GetApiFormat()
				}
				: new
				{
					chat_id = chat.Fetch(),
					photo = photoUrl.ToString(),
					parse_mode = parseMode.ToApiString(),
					caption,
					disable_notification = disableNotification,
					reply_to_message_id = replyToMessageId,
					allow_sending_without_reply = allowSendingWithoutReply,
					reply_markup = (inlineMarkup as IProvidesApiFormat).GetApiFormat()
				};

			var message =
				await TelegramApi.RequestAsync<ApiMessage>("sendDocument", HttpMethod.Post, toSerialize.ToJson());

			return new TelegramClientMessage(this, message, parseMode);
		}

		public override async Task<TelegramClientMessage?> SendPhotoAsync(ChatId id, Uri photoUrl,
			string? caption = null, ParseMode parseMode = ParseMode.MarkdownV2,
			bool disableNotification = false, int replyToMessageId = default, bool allowSendingWithoutReply = false,
			IKeyboard? keyboard = null)
		{
			object toSerialize = keyboard == null
				? new
				{
					chat_id = id.Fetch(),
					photo = photoUrl.ToString(),
					parse_mode = parseMode.ToApiString(),
					caption,
					disable_notification = disableNotification,
					reply_to_message_id = replyToMessageId,
					allow_sending_without_reply = allowSendingWithoutReply
				}
				: new
				{
					chat_id = id.Fetch(),
					photo = photoUrl.ToString(),
					parse_mode = parseMode.ToApiString(),
					caption,
					disable_notification = disableNotification,
					reply_to_message_id = replyToMessageId,
					allow_sending_without_reply = allowSendingWithoutReply,
					reply_markup = (keyboard as IProvidesApiFormat)?.GetApiFormat()
				};

			var message =
				await TelegramApi.RequestAsync<ApiMessage>("sendDocument", HttpMethod.Post, toSerialize.ToJson());

			return new TelegramClientMessage(this, message, parseMode);
		}

		#endregion
	}
}