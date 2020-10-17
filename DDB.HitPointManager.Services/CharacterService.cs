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


        // TODO - Add CharacterDto

        public Character GetCharacter(string name)
        {
            // if character == null, throw 404
            return _characterRepository.GetCharacterByName(name);
        }
    }
}