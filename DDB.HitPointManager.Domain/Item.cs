using System.Text.Json.Serialization;

namespace DDB.HitPointManager.Domain
{
    public class Item
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("modifier")]
        public Modifier Modifier { get; set; }
    }
}
