using DDB.HitPointManager.Data;
using DDB.HitPointManager.Domain;

namespace DDB.HitPointManager.Services
{
    public interface ICharacterService
    {
        Character GetCharacter(string name);
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
    }
}