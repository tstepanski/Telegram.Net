using TelegramNet.Entities;

namespace TelegramNet
{
    /// <summary>
    /// Provides powerful interface for bot creating.
    /// </summary>
    public interface ITelegramClient
    {
        /// <summary>
        /// Get self.
        /// </summary>
        public SelfUser Me { get; }

        /// <summary>
        /// Start receiving updates.
        /// </summary>
        public void Start();

        /// <summary>
        /// Stop receiving updates.
        /// </summary>
        public void Stop();
    }
}