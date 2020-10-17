using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using DDB.HitPointManager.Domain;

namespace DDB.HitPointManager.Data
{
    public interface ICharacterRepository
    {
        Character GetByName(string name);
    }

    public class CharacterRepository : ICharacterRepository
    {
        private readonly IList<Character> _characters;

        public CharacterRepository()
        {
            // Load all characters when object instantiated
            var rootDir = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            var json = File.ReadAllText($"{rootDir}/data/characters.json");
            _characters = JsonSerializer.Deserialize<List<Character>>(json);
        }

        public Character GetByName(string name)
        {
            return _characters.FirstOrDefault(obj =>
                obj.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}