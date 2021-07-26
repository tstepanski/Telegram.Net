using System;

namespace TelegramNet.Services.HTTP.Entities
{
    public class EnsureFailedException<T> : SystemException
    {
        public EnsureFailedException(string message) : base($"ERR\nType: {typeof(T)}\nDescription: {message}")
        {
        }
    }
}