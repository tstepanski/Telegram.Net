using System.Text.Json.Serialization;

namespace TelegramNet.Types
{
	internal sealed class ApiChatPermissions
	{
		[JsonPropertyName("can_send_messages")]
		public bool? CanSendMessages { get; set; }

		[JsonPropertyName("can_send_media_messages")]
		public bool? CanSendMediaMessages { get; set; }

		[JsonPropertyName("can_send_polls")] public bool? CanSendPolls { get; set; }

		[JsonPropertyName("can_send_other_messages")]
		public bool? CanSendOtherMessages { get; set; }

		[JsonPropertyName("can_send_web_page_previews")]
		public bool? CanSendWebPagePreviews { get; set; }

		[JsonPropertyName("can_change_info")] public bool? CanChangeInfo { get; set; }

		[JsonPropertyName("can_invite_users")] public bool? CanInviteUsers { get; set; }

		[JsonPropertyName("can_pin_messages")] public bool? CanPinMessages { get; set; }
	}
}