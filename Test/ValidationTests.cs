using NUnit.Framework;
using TelegramNet.Validators;

namespace Test
{
	public sealed class ValidationTests
	{
		[Test]
		public void ValidatableToken()
		{
			const string validToken = "123456:ABC-DEF1234ghIkl-zyx57W2v1u123ew11";
			string[] invalidTokens = { "SOME STRING", "123456:" };

			Assert.IsTrue(TokenValidator.Validate(validToken));

			foreach (var token in invalidTokens)
			{
				Assert.IsFalse(TokenValidator.Validate(token));
			}
		}
	}
}