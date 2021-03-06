﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using DDB.HitPointManager.Domain;

namespace DDB.HitPointManager.Data
{
     /// <summary>
     /// Handles loading characters from a data source
     /// </summary>
    public interface ICharacterRepository
    {
        Character GetByName(string name);
    }

    public class CharacterRepository : ICharacterRepository
    {
        private readonly IEnumerable<Character> _characters;

        public CharacterRepository()
        {
            // Load all characters when object instantiated
            var rootDir = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            var json = File.ReadAllText($"{rootDir}/data/characters.json");
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            _characters = JsonSerializer.Deserialize<IEnumerable<Character>>(json, options);
        }

        public Character GetByName(string name)
        {
            return _characters.FirstOrDefault(obj =>
                obj.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}