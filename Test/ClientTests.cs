using System.Threading.Tasks;
using NUnit.Framework;
using TelegramNet;

namespace Test
{
    public class ClientTests
    {
        [Test]
        public async Task TestGetMe()
        {
            var client = new TelegramClient("1710474838:AAG5g9lWry0hNYX0i-RAmjBSbNB47_D6s3g");
            var up = client.Me;

            Assert.IsNotNull(up);
        }
    }
}