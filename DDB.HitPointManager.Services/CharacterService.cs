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
            return 10;
        }
    }
}