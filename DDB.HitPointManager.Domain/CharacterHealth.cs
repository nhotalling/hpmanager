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
        public int MaxHp { get; set; }

        [JsonPropertyName("currentHp")]
        public int CurrentHp { get; set; }

        [JsonPropertyName("tempHp")]
        public int TempHp { get; set; }

        // Improvements: Track Death Saves, Conditions, Status (Alive, Dead, Unconscious)
    }
}
