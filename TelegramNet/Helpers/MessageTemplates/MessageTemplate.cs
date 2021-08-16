using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramNet.Entities;
using TelegramNet.Entities.Interfaces;
using TelegramNet.Enums;

namespace TelegramNet.Helpers.MessageTemplates
{
	public sealed class MessageTemplate
	{
		private readonly BaseTelegramClient _client;

		public MessageTemplate(BaseTelegramClient client)
		{
			_client = client;
		}

		public string? Text { get; private set; }
		public IKeyboard? Keyboard { get; private set; }
		public ParseMode ParseMode { get; private set; } = ParseMode.MarkdownV2;
		public Uri? DocumentUri { get; private set; }
		public Uri? ImageUri { get; private set; }

		internal async Task<TelegramClientMessage[]> ExecuteTemplateAsync(ChatId id)
		{
			var messages = new List<TelegramClientMessage>();

			if (Text != null)
			{
				var message = await _client.SendMessageAsync(id, Text, ParseMode, Keyboard);

				if (message != null)
				{
					messages.Add(message);
				}
			}

			if (DocumentUri != null)
			{
				var message = await _client.SendDocumentAsync(id: id, DocumentUri);

				if (message != null)
				{
					messages.Add(message);
				}
			}

			// ReSharper disable once InvertIf
			if (ImageUri != null)
			{
				var message = await _client.SendPhotoAsync(id: id, ImageUri);

				if (message != null)
				{
					messages.Add(message);
				}
			}

			return messages.ToArray();
		}

		public MessageTemplate WithText(string text)
		{
			Text = text;

			return this;
		}

		public MessageTemplate UseParseMode(ParseMode mode)
		{
			ParseMode = mode;

			return this;
		}

		public MessageTemplate WithKeyboard(IKeyboard? keyboard)
		{
			Keyboard = keyboard;

			return this;
		}

		public MessageTemplate WithDocument(Uri documentUri)
		{
			DocumentUri = documentUri;

			return this;
		}

		public MessageTemplate WithImage(Uri imgUri)
		{
			ImageUri = imgUri;

			return this;
		}

		public async Task<TelegramClientMessage[]> SendAsync(ChatId id)
		{
			var chat = await _client.GetChatAsync(id);

			if (chat == null)
			{
				throw new InvalidOperationException($"Can't find the apiChat associated with {id}");
			}

			return await ExecuteTemplateAsync(chat);
		}
	}
}