using System.Text.Json.Serialization;

namespace DDB.HitPointManager.Domain
{
    public class Defense
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("defense")]
        public DefenseType DefenseType { get; set; }
    }
}