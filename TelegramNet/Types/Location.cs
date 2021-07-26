using System.Text.Json.Serialization;

namespace TelegramNet.Types
{
    internal class Location
    {
        public float Longitude { get; set; }

        public float Latitude { get; set; }

        [JsonPropertyName("horizontal_accuracy")]
        public float HorizontalAccuracy { get; set; }

        [JsonPropertyName("live_period")] public int LivePeriod { get; set; }

        public int Heading { get; set; }

        [JsonPropertyName("proximity_alert_radius")]
        public int ProximityAlertRadius { get; set; }
    }
}