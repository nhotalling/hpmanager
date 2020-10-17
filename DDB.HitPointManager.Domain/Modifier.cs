using System.Text.Json.Serialization;

namespace DDB.HitPointManager.Domain
{
    public class Modifier
    {
        [JsonPropertyName("affectedObject")]
        public string AffectedObject { get; set; }

        [JsonPropertyName("affectedValue")]
        public string AffectedValue { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }
    }

}
