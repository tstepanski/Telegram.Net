using System.Text.Json.Serialization;

namespace TelegramNet.Types
{
	internal sealed class ApiChatLocation
	{
		[JsonPropertyName("location")] public ApiLocation? ApiLocation { get; set; }

		[JsonPropertyName("address")] public string? Address { get; set; }
	}
}