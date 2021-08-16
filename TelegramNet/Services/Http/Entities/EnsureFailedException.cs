using System;

namespace TelegramNet.Services.Http.Entities
{
	public sealed class EnsureFailedException<T> : SystemException
	{
		public EnsureFailedException(string message) : base($"ERR\nType: {typeof(T)}\nDescription: {message}")
		{
		}
	}
}