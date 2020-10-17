using System.Text.Json.Serialization;

namespace DDB.HitPointManager.Domain
{
    public class CharacterClass
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("hitDiceValue")]
        public int HitDiceValue { get; set; }

        [JsonPropertyName("classLevel")]
        public int ClassLevel { get; set; }
    }
}