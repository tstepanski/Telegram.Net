using System.Text.Json.Serialization;

namespace TelegramNet.Services.Http.Entities
{
    internal class HttpResult
    {
        [JsonPropertyName("ok")] public bool Ok { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; }
        [JsonPropertyName("error_code")] public int ErrorCode { get; set; }
        [JsonPropertyName("result")] public object Result { get; set; }
    }
}