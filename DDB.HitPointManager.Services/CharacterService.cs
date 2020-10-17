using DDB.HitPointManager.Domain;

namespace DDB.HitPointManager.Services
{
    public interface ICharacterService
    {
        Character GetCharacter(string name);
    }

    public class CharacterService : ICharacterService
    {
        // TODO - Add CharacterDto

        public Character GetCharacter(string name)
        {
            return new Character
            {
                Name = "Test from Service"
            };
        }
    }
}