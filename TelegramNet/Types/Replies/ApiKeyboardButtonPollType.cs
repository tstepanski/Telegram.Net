using System.Text.Json.Serialization;

namespace TelegramNet.Types.Replies
{
	internal sealed class ApiKeyboardButtonPollType
	{
		[JsonPropertyName("type")] public string? Type { get; set; }
	}
}