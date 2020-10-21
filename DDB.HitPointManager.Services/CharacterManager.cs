using System;
using System.Collections.Generic;
using System.Linq;
using DDB.HitPointManager.Core;
using DDB.HitPointManager.Core.Exceptions;
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
        int CalculateDamage(IEnumerable<DamageRequest> damageRequest, IEnumerable<Defense> defenses);
        CharacterHealth ApplyDamage(int damage, CharacterHealth originalHealth);
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

        public CharacterHealth DealDamage(string name, IEnumerable<DamageRequest> damageRequest)
        {
            var originalHealth = GetCharacterHealth(name);
            // GetCharacterHealth has null checks for character, so we can assume character is not null
            var character = _characterService.GetCharacter(name);

            var damageTaken = CalculateDamage(damageRequest, character.Defenses);

            if (damageTaken == 0)
            {
                return originalHealth;
            }

            var updatedHealth = ApplyDamage(damageTaken, originalHealth);
            _characterHealthService.Save(updatedHealth);

            return updatedHealth;
        }

        public int CalculateDamage(IEnumerable<DamageRequest> damageRequest, IEnumerable<Defense> defenses)
        {
            if (damageRequest == null)
            {
                return 0;
            }
            if (defenses == null)
            {
                defenses = new List<Defense>();
            }
            defenses = defenses.ToList();
            var damageTaken = 0;

            foreach (var damage in damageRequest)
            {
                // Not allowing negative damage. Could consider throwing an error.
                var damageToApply = Math.Max(0, damage.Value);

                // This if check could also check to see if no defenses match the damage type and apply all damage,
                // but that wouldn't handle the addition / subtraction portion down the road since it won't
                // necessarily be damage type specific (eg reduce all damage by 5)
                if (!defenses.Any())
                {
                    damageTaken += damageToApply;
                    continue;
                }

                // immunity
                if (defenses.Any(defense =>
                    defense.DefenseType == DefenseType.Immunity && defense.Type == damage.Type))
                {
                    continue;
                }

                var currentDamage = damageToApply;

                // apply any addition / subtraction
                // assumed this is out of scope for demo based on defense model

                // allow one resistance
                if (defenses.Any(defense =>
                    defense.DefenseType == DefenseType.Resistance && defense.Type == damage.Type))
                {
                    currentDamage = Calculations.HalfRoundDown(currentDamage);
                }

                // allow one vulnerability
                if (defenses.Any(defense =>
                    defense.DefenseType == DefenseType.Vulnerability && defense.Type == damage.Type))
                {
                    currentDamage *= 2;
                }

                damageTaken += currentDamage;
            }

            return damageTaken;
        }

        public CharacterHealth ApplyDamage(int damage, CharacterHealth originalHealth)
        {
            if (damage <= 0)
            {
                // Not allowing negative damage. Could consider throwing an error.
                return originalHealth;
            }
            var updatedHealth = originalHealth.Clone();
            var remainingDamage = damage;

            // the if checks aren't strictly necessary since the math.min would handle 0
            // but they help describe the intent.

            if (updatedHealth.TempHp > 0)
            {
                var tempDamage = Math.Min(updatedHealth.TempHp, remainingDamage);
                updatedHealth.TempHp -= tempDamage;
                remainingDamage -= tempDamage;
            }

            if (remainingDamage > 0)
            {
                var hpDamage = Math.Min(updatedHealth.CurrentHp, remainingDamage);
                updatedHealth.CurrentHp -= hpDamage;
                remainingDamage -= hpDamage;

                // if CurrentHp <= 0, character is unconscious
                // if remainingDamage >= MaxHp, character is killed
            }

            return updatedHealth;
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
            // Could clone here if we don't want to modify the original object
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