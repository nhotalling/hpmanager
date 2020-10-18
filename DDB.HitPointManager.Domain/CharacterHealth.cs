using System.Text.Json.Serialization;

namespace DDB.HitPointManager.Domain
{
    public class CharacterHealth
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("maxHp")]
        public int MaxHp { get; set; }

        [JsonPropertyName("currentHp")]
        public int CurrentHp { get; set; }

        [JsonPropertyName("tempHp")]
        public int TempHp { get; set; }
    }
}
