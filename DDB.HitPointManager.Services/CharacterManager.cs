using System.Collections.Generic;
using DDB.HitPointManager.Core;
using DDB.HitPointManager.Domain;

namespace DDB.HitPointManager.Services
{
    /// <summary>
    /// Handles character manipulation via the various Character services.
    /// Contains business logic for damage, healing, and temp hp methods.
    /// </summary>
    public interface ICharacterManager
    {
        CharacterHealth AddTempHp(string name, int amount);
        CharacterHealth DealDamage(string name, IEnumerable<DamageRequest> damage);

        Character GetCharacter(string name);
        CharacterHealth GetStatus(string name);
        CharacterHealth Heal(string name, int amount);
    }

    public class CharacterManager : ICharacterManager
    {
        private readonly ICharacterService _characterService;
        private readonly ICharacterHealthService _characterHealthService;
        public CharacterManager(ICharacterService characterService,
                                ICharacterHealthService characterHealthService)
        {
            _characterService = characterService;
            _characterHealthService = characterHealthService;
        }

        public CharacterHealth AddTempHp(string name, int amount)
        {
            var health = GetCharacterHealth(name);
            throw new System.NotImplementedException();
        }

        public CharacterHealth DealDamage(string name, IEnumerable<DamageRequest> damage)
        {
            throw new System.NotImplementedException();
        }

        public Character GetCharacter(string name)
        {
            return _characterService.GetCharacter(name);
        }

        public CharacterHealth GetStatus(string name)
        {
            return GetCharacterHealth(name);
        }

        public CharacterHealth Heal(string name, int amount)
        {
            throw new System.NotImplementedException();
        }

        internal CharacterHealth GetCharacterHealth(string name)
        {
            var currentHealth = _characterHealthService.GetCharacterHealth(name);
            if (currentHealth != null)
            {
                return currentHealth;
            }

            var character = _characterService.GetCharacter(name);
            if (character == null)
            {
                throw new ResourceNotFoundException("Character not found.");
            }

            var maxHp = _characterService.CalculateMaxHp(character);
            currentHealth = new CharacterHealth
            {
                Name = character.Name,
                MaxHp = maxHp,
                CurrentHp = maxHp,
                TempHp = 0
            };
            _characterHealthService.Save(currentHealth);

            return currentHealth;
        }

    }
}