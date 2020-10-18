using System;
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
            int newTempHp;

            if (amount < 0)
            {
                // Allowing user to send negative temp hp
                // as a means to correct an error
                newTempHp = health.TempHp + amount;
                newTempHp = Math.Max(0, newTempHp);
            }
            else
            {
                // Temp HP do not stack
                newTempHp = Math.Max(amount, health.TempHp);
            }

            health.TempHp = newTempHp;
            _characterHealthService.Save(health);

            return health;
        }

        public CharacterHealth DealDamage(string name, IEnumerable<DamageRequest> damage)
        {
            var health = GetCharacterHealth(name);
            // GetCharacterHealth has null checks for character, so we can assume character is not null
            var character = _characterService.GetCharacter(name);

            // TODO - If defenses are null, make them empty list

            // Improvement - check items for defenses

            if (damage != null)
            {
                int damageTaken;
                foreach (var damageRequest in damage)
                {
                    // immunity

                    // any addition / subtraction
                    // assumed this is not relevant for demo

                    // one resistance

                    // one vulnerability
                }

                // apply temp hp

                // apply current hp
            }

            _characterHealthService.Save(health);
            return health;
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
            var health = GetCharacterHealth(name);

            if (amount < 0)
            {
                throw new ArgumentException("Amount healed should be 0 or greater");
            }

            // Improvement - disallow healing if character is dead
            health.CurrentHp = Math.Min(amount + health.CurrentHp, health.MaxHp);
            _characterHealthService.Save(health);

            return health;
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