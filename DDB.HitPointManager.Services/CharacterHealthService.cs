using DDB.HitPointManager.Data;
using DDB.HitPointManager.Domain;

namespace DDB.HitPointManager.Services
{
    /// <summary>
    /// Handles loading from/saving to character health repository
    /// </summary>
    public interface ICharacterHealthService
    {
        CharacterHealth GetCharacterHealth(string name);
        void Save(CharacterHealth characterHealth);
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

        public void Save(CharacterHealth characterHealth)
        {
            _characterHealthRepository.Save(characterHealth);
        }
    }
}
