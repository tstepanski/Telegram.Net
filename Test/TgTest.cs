using System.Threading.Tasks;
using NUnit.Framework;
using TelegramNet;
using TelegramNet.Entities.Keyboards.Inlines;
using TelegramNet.Entities.Keyboards.Replies;
using TelegramNet.Enums;
using TelegramNet.Helpers;

namespace Test
{
    public class TgTest
    {
        [Test]
        public async Task SendingMessageTest()
        {
            var client = new TelegramClient("1710474838:AAG5g9lWry0hNYX0i-RAmjBSbNB47_D6s3g");

            var chat = await client.GetChatAsync(640800833);

            Assert.IsNotNull(chat);

            var mess = await chat.SendMessageAsync("hello");

            Assert.IsTrue(mess.Text == "hello");
        }

        [Test]
        public async Task EditingMessageTest()
        {
            var client = new TelegramClient("1710474838:AAG5g9lWry0hNYX0i-RAmjBSbNB47_D6s3g");
            var chat = await client.GetChatAsync(640800833);

            Assert.IsNotNull(chat);

            var mess = await chat.SendMessageAsync("hello");

            var edited = await mess.EditTextAsync(new MessageTextEditor().WithText("edited"));

            Assert.IsTrue(edited);
        }

        [Test]
        public async Task InlineReplyMarkupTest()
        {
            var client = new TelegramClient("1710474838:AAG5g9lWry0hNYX0i-RAmjBSbNB47_D6s3g");
            var chat = await client.GetChatAsync(640800833);
            
            Assert.IsNotNull(chat);

            var mess = await chat.SendMessageAsync("Inline markup test", ParseMode.MarkdownV2, new InlineKeyboardMarkup(new [] { new InlineKeyboardButton("Inline test!", callbackData: "Callback") }));
            
            Assert.IsNotNull(mess);
        }

        [Test]
        public async Task RemovingInlineTest()
        {
            var client = new TelegramClient("1710474838:AAG5g9lWry0hNYX0i-RAmjBSbNB47_D6s3g");
            var chat = await client.GetChatAsync(640800833);
            
            Assert.IsNotNull(chat);

            var mess = await chat.SendMessageAsync("Inline markup test",
                ParseMode.MarkdownV2,
                new InlineKeyboardMarkup(new[]
                {
                    new InlineKeyboardButton("Inline test!",
                        callbackData: "Callback")
                }));
            
            bool res = await mess.RemoveKeyboardAsync();
            Assert.IsTrue(res);
        }

        [Test]
        public async Task ReplyKeyboardTest()
        {
            var client = new TelegramClient("1710474838:AAG5g9lWry0hNYX0i-RAmjBSbNB47_D6s3g");

            var mess = await client.SendMessageAsync(640800833,
                "Inline markup test",
                ParseMode.MarkdownV2,
                replyMarkup: new ReplyKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new KeyboardButton("Test")
                    }
                }));
            
            Assert.IsNotNull(mess);
        }

        [Test]
        public async Task BuilderTest()
        {
            var client = new TelegramClient("1710474838:AAG5g9lWry0hNYX0i-RAmjBSbNB47_D6s3g");

            var mess = await client.SendMessageAsync(640800833,
                "Reply markup test",
                ParseMode.MarkdownV2,
                replyMarkup: new ReplyKeyboardMarkup(ReplyKeyboardMarkup.Builder
                    .AddRow(
                        ReplyKeyboardMarkup.RowBuilder
                            .WithButton("Hello")
                            .WithButton("Test"))
                    .AddRow(
                        ReplyKeyboardMarkup.RowBuilder
                            .WithButton("Another row"))));
            
            Assert.IsNotNull(mess);

            var mess1 = await client.SendMessageAsync(640800833,
                "Inline markup test",
                ParseMode.MarkdownV2,
                new InlineKeyboardMarkup(InlineKeyboardMarkup.Builder
                    .AddRow(
                        InlineKeyboardMarkup.RowBuilder
                            .WithButton(new InlineKeyboardButton("First row", callbackData: "First row")))
                    .AddRow(
                        InlineKeyboardMarkup.RowBuilder
                            .WithButton(new InlineKeyboardButton("Second row", callbackData: "Second row")))));
            
            Assert.IsNotNull(mess1);
        }
    }
}