using System.Text.Json.Serialization;

namespace DDB.HitPointManager.Domain
{
    public class Defense
    {
        [JsonPropertyName("type")]
        public DamageType Type { get; set; }

        [JsonPropertyName("defense")]
        public DefenseType DefenseType { get; set; }
    }
}