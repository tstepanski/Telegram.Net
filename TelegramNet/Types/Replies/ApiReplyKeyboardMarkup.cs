using System.Text.Json.Serialization;

namespace TelegramNet.Types.Replies
{
	internal sealed class ApiReplyKeyboardMarkup
	{
		[JsonPropertyName("keyboard")] public ApiKeyboardButton[][]? Keyboard { get; set; }

		[JsonPropertyName("resize_keyboard")] public bool ResizeKeyboard { get; set; }

		[JsonPropertyName("one_time_keyboard")]
		public bool OneTimeKeyboard { get; set; }

		[JsonPropertyName("input_field_place_holder")]
		public string? InputFieldPlaceHolder { get; set; }

		[JsonPropertyName("selective")] public bool Selective { get; set; }
	}
}