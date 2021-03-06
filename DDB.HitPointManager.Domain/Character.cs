﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DDB.HitPointManager.Domain
{
    public class Character
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("classes")]
        public IEnumerable<CharacterClass> Classes { get; set; }

        [JsonPropertyName("stats")]
        public Stats Stats { get; set; }

        [JsonPropertyName("items")]
        public IEnumerable<Item> Items { get; set; }

        [JsonPropertyName("defenses")]
        public IEnumerable<Defense> Defenses { get; set; }
    }
}
