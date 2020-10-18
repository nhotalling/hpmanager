using System.Text.Json.Serialization;

namespace DDB.HitPointManager.Domain
{
    public class DamageRequest
    {
        [JsonPropertyName("type")]
        public DamageType Type { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }
    }
}