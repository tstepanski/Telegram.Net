using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TelegramNet;

namespace Test
{
	public sealed class ClientTests
	{
		[Test]
		public Task TestGetMe()
		{
			var loggerMock = new Mock<ILogger>();
			var client = new TelegramClient("1710474838:AAG5g9lWry0hNYX0i-RAmjBSbNB47_D6s3g", loggerMock.Object);
			var up = client.Me;

			Assert.IsNotNull(up);
			return Task.CompletedTask;
		}
	}
}