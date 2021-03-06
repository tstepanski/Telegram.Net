using System.Text.Json.Serialization;

namespace TelegramNet.Types
{
	internal sealed class ApiChatPhoto
	{
		[JsonPropertyName("small_file_id")] public string? SmallFileId { get; set; }

		[JsonPropertyName("small_file_unique_id")]
		public string? SmallFileUniqueId { get; set; }

		[JsonPropertyName("big_file_id")] public string? BigFileId { get; set; }

		[JsonPropertyName("big_file_unique_id")]
		public string? BigFileUniqueId { get; set; }
	}
}