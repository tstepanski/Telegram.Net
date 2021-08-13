using System.Text.Json.Serialization;

namespace TelegramNet.Types
{
    internal class ApiLocation
    {
        [JsonPropertyName("longitude")] public float Longitude { get; set; }

        [JsonPropertyName("latitude")] public float Latitude { get; set; }

        [JsonPropertyName("horizontal_accuracy")]
        public float HorizontalAccuracy { get; set; }

        [JsonPropertyName("live_period")] public int LivePeriod { get; set; }

        public int Heading { get; set; }

        [JsonPropertyName("proximity_alert_radius")]
        public int ProximityAlertRadius { get; set; }
    }
}