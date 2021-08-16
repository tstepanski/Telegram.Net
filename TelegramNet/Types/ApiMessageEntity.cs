using System.Text.Json.Serialization;

namespace TelegramNet.Types
{
	internal sealed class ApiMessageEntity
	{
		[JsonPropertyName("type")] public string? Type { get; set; }

		[JsonPropertyName("offset")] public int Offset { get; set; }

		[JsonPropertyName("length")] public int Length { get; set; }

		[JsonPropertyName("url")] public string? Url { get; set; }

		[JsonPropertyName("user")] public ApiUser? User { get; set; }

		[JsonPropertyName("language")] public string? Language { get; set; }
	}
}