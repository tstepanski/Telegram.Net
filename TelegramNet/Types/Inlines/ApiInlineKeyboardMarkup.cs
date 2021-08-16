using System.Text.Json.Serialization;

namespace TelegramNet.Types.Inlines
{
	internal sealed class ApiInlineKeyboardMarkup
	{
		[JsonPropertyName("inline_keyboard")] public ApiInlineKeyboardButton?[][]? InlineKeyboard { get; set; }
	}
}