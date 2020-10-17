using DDB.HitPointManager.Domain;

namespace DDB.HitPointManager.Data
{
    public interface ICharacterRepository
    {
        Character GetCharacterByName(string name);
    }

    public class CharacterRepository : ICharacterRepository
    {
        public Character GetCharacterByName(string name)
        {
            // TODO - Make hashmap to store loaded character and use it for cache
            return new Character
            {
                Name = "Character from Repository"
            };
        }
    }
}