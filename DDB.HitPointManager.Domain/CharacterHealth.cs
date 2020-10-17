using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DDB.HitPointManager.Domain
{
    public class CharacterHealth
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("maxHp")]
        public string MaxHp { get; set; }

        [JsonPropertyName("currentHp")]
        public string CurrentHp { get; set; }

        [JsonPropertyName("tempHp")]
        public string TempHp { get; set; }

        // Improvement: Track Death Saves, Conditions, Status (Alive, Dead, Unconscious)
    }
}
