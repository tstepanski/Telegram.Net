using System.Threading.Tasks;
using NUnit.Framework;
using TelegramNet;
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
    }
}