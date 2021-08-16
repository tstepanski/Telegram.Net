using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TelegramNet;
using TelegramNet.Entities.Keyboards.Inlines;
using TelegramNet.Entities.Keyboards.Replies;
using TelegramNet.Enums;
using TelegramNet.Helpers;

namespace Test
{
	public sealed class TgTest
	{
		[Test]
		public async Task SendingMessageTest()
		{
			var loggerMock = new Mock<ILogger>();
			var client = new TelegramClient("1710474838:AAG5g9lWry0hNYX0i-RAmjBSbNB47_D6s3g", loggerMock.Object);

			var chat = await client.GetChatAsync(640800833);

			Assert.IsNotNull(chat);

			var message = await chat.SendMessageAsync("hello", keyboard: null);

			Assert.IsTrue(message?.Text == "hello");
		}

		[Test]
		public async Task EditingMessageTest()
		{
			var loggerMock = new Mock<ILogger>();
			var client = new TelegramClient("1710474838:AAG5g9lWry0hNYX0i-RAmjBSbNB47_D6s3g", loggerMock.Object);
			var chat = await client.GetChatAsync(640800833);

			Assert.IsNotNull(chat);

			var message = await chat.SendMessageAsync("hello", keyboard: null);
			var edited = await message!.EditTextAsync(new MessageTextEditor().WithText("edited"));

			Assert.IsTrue(edited);
		}

		[Test]
		public async Task InlineReplyMarkupTest()
		{
			var loggerMock = new Mock<ILogger>();
			var client = new TelegramClient("1710474838:AAG5g9lWry0hNYX0i-RAmjBSbNB47_D6s3g", loggerMock.Object);
			var chat = await client.GetChatAsync(640800833);

			Assert.IsNotNull(chat);

#pragma warning disable 618
			var mess = await chat.SendMessageAsync("Inline markup test",
				ParseMode.MarkdownV2,
				new InlineKeyboardMarkup(new[]
				{
					new InlineKeyboardButton("Inline test!",
						callbackData: "Callback")
				}));
#pragma warning restore 618

			Assert.IsNotNull(mess);
		}

		[Test]
		public async Task RemovingInlineTest()
		{
			var loggerMock = new Mock<ILogger>();
			var client = new TelegramClient("1710474838:AAG5g9lWry0hNYX0i-RAmjBSbNB47_D6s3g", loggerMock.Object);
			var chat = await client.GetChatAsync(640800833);

			Assert.IsNotNull(chat);

#pragma warning disable 618
			var message = await chat.SendMessageAsync("Inline markup test",
				ParseMode.MarkdownV2,
				new InlineKeyboardMarkup(new[]
				{
					new InlineKeyboardButton("Inline test!",
						callbackData: "Callback")
				}));
#pragma warning restore 618

			var res = await message.RemoveKeyboardAsync();
			Assert.IsTrue(res);
		}

		[Test]
		public async Task ReplyKeyboardTest()
		{
			var loggerMock = new Mock<ILogger>();
			var client = new TelegramClient("1710474838:AAG5g9lWry0hNYX0i-RAmjBSbNB47_D6s3g", loggerMock.Object);

			var mess = await client.SendMessageAsync(640800833,
				"Inline markup test",
				ParseMode.MarkdownV2,
				new ReplyKeyboardMarkup(new[]
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
			var loggerMock = new Mock<ILogger>();
			var client = new TelegramClient("1710474838:AAG5g9lWry0hNYX0i-RAmjBSbNB47_D6s3g", loggerMock.Object);

			var mess = await client.SendMessageAsync(640800833,
				"Reply markup test",
				ParseMode.MarkdownV2,
				new ReplyKeyboardMarkup(ReplyKeyboardMarkup.Builder
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
				keyboard: new InlineKeyboardMarkup(InlineKeyboardMarkup.Builder
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