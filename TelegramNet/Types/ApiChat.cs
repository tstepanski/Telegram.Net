using System.Text.Json.Serialization;

namespace TelegramNet.Types
{
	internal sealed class ApiChat
	{
		[JsonPropertyName("id")] public int Id { get; set; }

		[JsonPropertyName("type")] public string? Type { get; set; }

		[JsonPropertyName("title")] public string? Title { get; set; }

		[JsonPropertyName("username")] public string? Username { get; set; }

		[JsonPropertyName("first_name")] public string? FirstName { get; set; }

		[JsonPropertyName("last_name")] public string? LastName { get; set; }

		[JsonPropertyName("photo")] public ApiChatPhoto? Photo { get; set; }

		[JsonPropertyName("bio")] public string? Bio { get; set; }

		[JsonPropertyName("description")] public string? Description { get; set; }

		[JsonPropertyName("invite_link")] public string? InviteLink { get; set; }

		[JsonPropertyName("pinned_message")] public ApiMessage? PinnedApiMessage { get; set; }

		[JsonPropertyName("permissions")] public ApiChatPermissions? Permissions { get; set; }

		[JsonPropertyName("slow_mode_delay")] public int? SlowModeDelay { get; set; }

		[JsonPropertyName("message_auto_delete_timer")]
		public int? MessageAutoDeleteTimer { get; set; }

		[JsonPropertyName("sticker_set_name")] public string? StickerSetName { get; set; }

		[JsonPropertyName("can_set_sticker_set")]
		public bool? CanSetStickerSet { get; set; }

		[JsonPropertyName("linked_chat_id")] public int? LinkedChatId { get; set; }

		[JsonPropertyName("location")] public ApiChatLocation? Location { get; set; }
	}
}