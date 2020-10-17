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
            // Calculate CON bonus

            // Get first level HP (max)
            // TODO - Do we need an indicator on the character to show which was
            // its first character class?
            // For demo purposes, we're using the max HD value for level 1.

            // Get remaining levels - average
            // Using the average method to provide consistent testing results.
            return 10;
        }
    }
}