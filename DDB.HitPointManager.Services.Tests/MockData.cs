using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using DDB.HitPointManager.Domain;

namespace DDB.HitPointManager.Services.Tests
{
    public abstract class MockData
    {
        public Character GetTestCharacter(string name)
        {
            return Characters.First(obj => obj.Name.Equals(name));
        }

        public IEnumerable<Character> Characters {
            get
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                return JsonSerializer.Deserialize<IEnumerable<Character>>(CharacterJson, options);
            }
        }

        public CharacterHealth GetBrivHealth()
        {
            return new CharacterHealth
            {
                MaxHp = 45,
                CurrentHp = 45,
                TempHp = 0,
                Name = "Briv"
            };
        }

        private readonly string CharacterJson = @"[
  {
    ""name"": ""Briv"",
    ""level"": 5,
    ""classes"": [
      {
        ""name"": ""fighter"",
        ""hitDiceValue"": 10,
        ""classLevel"": 3
      },
      {
        ""name"": ""wizard"",
        ""hitDiceValue"": 6,
        ""classLevel"": 2
      }
    ],
    ""stats"": {
      ""strength"": 15,
      ""dexterity"": 12,
      ""constitution"": 14,
      ""intelligence"": 13,
      ""wisdom"": 10,
      ""charisma"": 8
    },
    ""items"": [
      {
        ""name"": ""Ioun Stone of Fortitude"",
        ""modifier"": {
          ""affectedObject"": ""stats"",
          ""affectedValue"": ""constitution"",
          ""value"": 2
        }
      }
    ],
    ""defenses"": [
      {
        ""type"": ""fire"",
        ""defense"": ""immunity""
      },
      {
        ""type"": ""slashing"",
        ""defense"": ""resistance""
      }
    ]
  }
]";
    }
}
