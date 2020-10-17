using DDB.HitPointManager.Domain;

namespace DDB.HitPointManager.Services
{
    public interface ICharacterManager
    {
        CharacterHealth AddTempHp(string name);
        CharacterHealth DealDamage(string name);

        Character GetCharacter(string name);
        CharacterHealth GetStatus(string name);
        CharacterHealth Heal(string name);
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

        public CharacterHealth AddTempHp(string name)
        {
            throw new System.NotImplementedException();
        }

        public CharacterHealth DealDamage(string name)
        {
            throw new System.NotImplementedException();
        }

        public Character GetCharacter(string name)
        {
            return _characterService.GetCharacter(name);
        }

        public CharacterHealth GetStatus(string name)
        {
            throw new System.NotImplementedException();
        }

        public CharacterHealth Heal(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}