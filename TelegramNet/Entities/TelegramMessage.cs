using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TelegramNet.Entities.Interfaces;
using TelegramNet.Entities.Keyboards.Inlines;
using TelegramNet.Entities.Keyboards.Replies;
using TelegramNet.Helpers;
using TelegramNet.Types;
using TelegramNet.Types.Inlines;
using TelegramNet.Types.Replies;

namespace TelegramNet.Entities
{
	public class TelegramMessage : ITelegramMessage
	{
		private readonly TelegramApiClient _client;

		internal TelegramMessage(BaseTelegramClient client, ApiMessage? apiMessage)
		{
			if (apiMessage == null)
			{
				throw new InvalidOperationException(GitHubIssueBuilder.Message(
					$"Exception while initializing {nameof(TelegramMessage)}.", nameof(InvalidOperationException),
					$"**Description:**\nDescribe your problem here.\n**StackTrace:**\n{Environment.StackTrace}"));
			}

			TelegramUser? CreateUser(ApiUser? user)
			{
				return user == null ? null : new TelegramUser(client, user);
			}

			TelegramChat? CreateChat(ApiChat? chat)
			{
				return chat == null ? null : new TelegramChat(client, chat);
			}

			DateTime? ParseDateTime(int unixTime)
			{
				return unixTime == default ? default : UnixParser.Parse(unixTime);
			}

			IEnumerable<MessageCaption>? ConvertEntities(IEnumerable<ApiMessageEntity>? entities)
			{
				return entities
					?.Select(messageEntity => new MessageCaption(client, messageEntity, apiMessage.Text))
					.ToArray();
			}

			Id = apiMessage.Id;
			Text = apiMessage.Text;
			ForwardFromMessageId = apiMessage.ForwardFromMessageId;
			ForwardSignature = apiMessage.ForwardSignature;
			ForwardSenderName = apiMessage.ForwardSenderName;
			MediaGroupId = apiMessage.MediaGroupId;
			AuthorSignature = apiMessage.AuthorSignature;

			Author = CreateUser(apiMessage.Author);
			ForwardFrom = CreateUser(apiMessage.ForwardFrom);
			ViaBot = CreateUser(apiMessage.ViaBot);
			ForwardFromChat = CreateChat(apiMessage.ForwardFromApiChat);
			SenderChat = CreateChat(apiMessage.SenderApiChat);
			Chat = new TelegramChat(client, apiMessage.ApiChat);
			Timestamp = ParseDateTime(apiMessage.Date);
			ForwardDate = ParseDateTime(apiMessage.ForwardDate);
			EditDate = ParseDateTime(apiMessage.EditDate);
			Captions = ConvertEntities(apiMessage.CaptionEntities);
			Entities = ConvertEntities(apiMessage.Entities);

			ReplyToMessage = apiMessage.ReplyToApiMessage != null
				? new TelegramMessage(client, apiMessage.ReplyToApiMessage)
				: default;

			var serializedButtons = apiMessage.ReplyMarkup.ToJson();

			try
			{
				var button = JsonSerializer.Deserialize<InlineObject>(serializedButtons);

				if (button != null)
				{
					InlineKeyboardMarkups = ConvertInlineObjectButton(button);
				}
			}
			catch (Exception)
			{
				// ignored
			}

			try
			{
				var button = JsonSerializer.Deserialize<ApiReplyKeyboardMarkup>(serializedButtons);

				if (button != null)
				{
					ReplyKeyboardMarkup = ConvertReplyKeyboardMarkup(button);
				}
			}
			catch (Exception)
			{
				// ignored
			}

			_client = client.TelegramApi;
		}

		public InlineKeyboardButton[][]? InlineKeyboardMarkups { get; }

		public ReplyKeyboardMarkup? ReplyKeyboardMarkup { get; }

		public int Id { get; }

		public TelegramUser? Author { get; }

		public TelegramChat? SenderChat { get; }

		public DateTime? Timestamp { get; }

		public string? Text { get; }

		public TelegramChat Chat { get; }

		public TelegramUser? ForwardFrom { get; }

		public TelegramChat? ForwardFromChat { get; }

		public IEnumerable<MessageCaption>? Captions { get; }

		public int? ForwardFromMessageId { get; }

		public string? ForwardSignature { get; }

		public string? ForwardSenderName { get; }

		public DateTime? ForwardDate { get; }

		public TelegramMessage? ReplyToMessage { get; }

		public TelegramUser? ViaBot { get; }

		public DateTime? EditDate { get; }

		public string? MediaGroupId { get; }

		public string? AuthorSignature { get; }

		public IEnumerable<MessageCaption>? Entities { get; }

		public async Task<bool> DeleteAsync()
		{
			var result = await _client.RequestAsync("deleteMessage", HttpMethod.Post, new Dictionary<string, object?>
			{
				{ "chat_id", Chat.Id.Fetch() },
				{ "message_id", Id }
			}.ToJson());

			return result.Ok;
		}

		internal static TelegramMessage? CreateIfMessageNotNull(BaseTelegramClient client, ApiMessage? apiMessage)
		{
			return apiMessage == null ? null : new TelegramMessage(client, apiMessage);
		}

		private static ReplyKeyboardMarkup ConvertReplyKeyboardMarkup(ApiReplyKeyboardMarkup button)
		{
			var keyboardButtons = button
				.Keyboard
				?.Select(buttons => buttons
					.Select(ConvertKeyboardButton)
					.ToArray())
				.ToArray() ?? Array.Empty<KeyboardButton[]>();

			return new ReplyKeyboardMarkup(keyboardButtons, button.ResizeKeyboard, button.OneTimeKeyboard,
				button.InputFieldPlaceHolder, button.Selective);
		}

		private static KeyboardButton ConvertKeyboardButton(ApiKeyboardButton button)
		{
			var buttonPollType = new KeyboardButtonPollType(button.RequestPoll?.Type);

			return new KeyboardButton(button.Text, button.RequestContact, button.RequestLocation, buttonPollType);
		}

		private static InlineKeyboardButton[][]? ConvertInlineObjectButton(InlineObject button)
		{
			return button
				.InlineKeyboard
				?.Select(keyboardButtons => keyboardButtons
					.Select(ConvertInlineKeyboardButton)
					.ToArray())
				.ToArray();
		}

		private static InlineKeyboardButton ConvertInlineKeyboardButton(ApiInlineKeyboardButton button)
		{
			var apiLoginUrl = button.ApiLoginUrl;
			var url = new Uri(button.Url ?? string.Empty);
			var loginUri = new Uri(apiLoginUrl?.Url ?? string.Empty);

			var loginUrl = new LoginUri(loginUri, apiLoginUrl?.ForwardText, apiLoginUrl?.BotUsername,
				apiLoginUrl?.RequestWriteAccess ?? false);

			return new InlineKeyboardButton(button.Text, url, loginUrl);
		}

		private class InlineObject
		{
			public ApiInlineKeyboardButton[][]? InlineKeyboard { get; set; }
		}
	}
}