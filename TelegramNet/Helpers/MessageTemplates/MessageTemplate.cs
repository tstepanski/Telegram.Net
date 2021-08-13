using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramNet.Entities;
using TelegramNet.Entities.Interfaces;
using TelegramNet.Entities.Keyboards.Inlines;
using TelegramNet.Entities.Keyboards.Replies;
using TelegramNet.Enums;

namespace TelegramNet.Helpers.MessageTemplates
{
    public class MessageTemplate
    {
        public MessageTemplate(BaseTelegramClient client)
        {
            this.client = client;
        }

        private readonly BaseTelegramClient client;

        public string Text { get; private set; }
        public IKeyboard Keyboard { get; private set; }
        public ParseMode ParseMode { get; private set; } = ParseMode.MarkdownV2;
        public Uri DocumentUri { get; private set; }
        public Uri ImageUri { get; private set; }

        internal async Task<TelegramClientMessage[]> ExecuteTemplateAsync(ChatId id)
        {
            var msgs = new List<TelegramClientMessage>();
            if (Text != null)
            {
                var msg = await client.SendMessageAsync(id, Text, ParseMode, Keyboard);
                if (msg != null)
                    msgs.Add(msg);
            }

            if (DocumentUri != null)
            {
                var msg = await client.SendDocumentAsync(id: id, DocumentUri);
                if (msg != null)
                    msgs.Add(msg);
            }

            if (ImageUri != null)
            {
                var msg = await client.SendPhotoAsync(id: id, ImageUri);
                if (msg != null)
                    msgs.Add(msg);
            }

            return msgs.ToArray();
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

        public MessageTemplate WithKeyboard(IKeyboard keyboard)
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
            var chat = await client.GetChatAsync(id);

            if (chat == null) throw new InvalidOperationException($"Can't find the apiChat associated with {id}");

            var mess = await ExecuteTemplateAsync(chat);
            return mess;
        }
    }
}