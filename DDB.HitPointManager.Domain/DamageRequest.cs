using System.Text.Json.Serialization;

namespace DDB.HitPointManager.Domain
{
    public class DamageRequest
    {
        // TODO - Consider making Type an enum?
        // although leaving it as a string allows for extensibility for new damage types...

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }
    }
}