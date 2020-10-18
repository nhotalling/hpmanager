using System;
using System.Collections.Generic;
using System.Linq;
using DDB.HitPointManager.Core;
using DDB.HitPointManager.Data;
using DDB.HitPointManager.Domain;

namespace DDB.HitPointManager.Services
{
    /// <summary>
    /// Handles loading from/saving to character repository.
    /// Also contains character-specific business logic.
    /// </summary>
    public interface ICharacterService
    {
        Character GetCharacter(string name);
        int CalculateMaxHp(Character character);
        int CalculateStatBonus(Character character, StatType statType);
    }

    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;

        public CharacterService(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        // Typically we'd use a Dto here instead of returning the entity
        public Character GetCharacter(string name)
        {
            return _characterRepository.GetByName(name);
        }

        public int CalculateMaxHp(Character character)
        {
            if (character == null || (!character.Classes?.Any() ?? true))
            {
                throw new ArgumentException("Characters should include 1 or more classes");
            }

            var hp = 0;

            // Determine CON bonus to apply at every level
            var conBonus = CalculateStatBonus(character, StatType.Constitution);

            // Get first level HP (max)
            // Improvement - Add an boolean to the CharacterClass that indicates if the class was the starting class.
            // For demo purposes, we will assume the first class in the list

            // Get remaining levels - average
            // Using the average method to provide consistent testing results.
            return hp;
        }

        public int CalculateStatBonus(Character character, StatType statType)
        {
            if (character?.Stats == null)
            {
                throw new ArgumentException("Characters should not have null Stats");
            }

            var statModifier = 0;
            var baseScore = 0;
            var affectedValues = new List<string>();

            switch (statType)
            {
                case StatType.Strength:
                    affectedValues = new List<string> { Constants.Strength, "str" };
                    baseScore = character.Stats.Strength;
                    break;
                case StatType.Dexterity:
                    affectedValues = new List<string> { Constants.Dexterity, "dex" };
                    baseScore = character.Stats.Dexterity;
                    break;
                case StatType.Constitution:
                    affectedValues = new List<string> { Constants.Constitution, "con" };
                    baseScore = character.Stats.Constitution;
                    break;
                case StatType.Intelligence:
                    affectedValues = new List<string> { Constants.Intelligence, "int" };
                    baseScore = character.Stats.Intelligence;
                    break;
                case StatType.Wisdom:
                    affectedValues = new List<string> { Constants.Wisdom, "wis" };
                    baseScore = character.Stats.Wisdom;
                    break;
                case StatType.Charisma:
                    affectedValues = new List<string> { Constants.Charisma, "cha" };
                    baseScore = character.Stats.Strength;
                    break;
            }

            if (character.Items?.Any() ?? false)
            {
                // Look for items that affect the specified stat
                character.Items.Where(item => item.Modifier != null &&
                                              Constants.Stats.Equals(item.Modifier.AffectedObject, StringComparison.InvariantCultureIgnoreCase)
                                              && affectedValues.Contains(item.Modifier.AffectedValue?.ToLower()))
                    .ToList()
                    .ForEach(item =>
                    {
                        // This assumes the character only has valid items
                        // e.g. does not have multiple copies of the same type of magic item
                        statModifier += item.Modifier.Value;
                    });
            }

            return Calculations.GetStatModifier(baseScore + statModifier);
        }
    }
}