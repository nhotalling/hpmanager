using System;
using System.Collections.Generic;
using DDB.HitPointManager.Domain;

namespace DDB.HitPointManager.Data
{
    /// <summary>
    /// Tracks a character's health for the life of the application
    /// </summary>
    public interface ICharacterHealthRepository
    {
        CharacterHealth GetByName(string name);
        void Save(CharacterHealth characterHealth);
    }

    public class CharacterHealthRepository : ICharacterHealthRepository
    {
        private readonly IDictionary<string, CharacterHealth> _characterDictionary;

        public CharacterHealthRepository()
        {
            _characterDictionary = new Dictionary<string, CharacterHealth>();
        }

        public CharacterHealth GetByName(string name)
        {
            _characterDictionary.TryGetValue(name, out var characterHealth);
            return characterHealth;
        }

        public void Save(CharacterHealth characterHealth)
        {
            if (characterHealth == null || string.IsNullOrEmpty(characterHealth.Name))
            {
                throw new ArgumentException("CharacterHealth must not be null and must have a valid Name");
            }

            _characterDictionary[characterHealth.Name] = characterHealth;
        }
    }
}
