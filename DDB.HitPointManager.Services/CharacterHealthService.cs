using DDB.HitPointManager.Data;
using DDB.HitPointManager.Domain;

namespace DDB.HitPointManager.Services
{
    public interface ICharacterHealthService
    {
        CharacterHealth GetCharacterHealth(string name);
    }

    public class CharacterHealthService : ICharacterHealthService
    {
        private readonly ICharacterHealthRepository _characterHealthRepository;

        public CharacterHealthService(ICharacterHealthRepository characterHealthRepository)
        {
            _characterHealthRepository = characterHealthRepository;
        }


        public CharacterHealth GetCharacterHealth(string name)
        {
            return _characterHealthRepository.GetByName(name);
        }
    }
}
