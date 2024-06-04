using Entities.Specification;
using ServerCore.Main;
using Utilities.ModelCollection;

namespace Entities.Characters.Collection
{
    public class CharactersCollection : ModelCollection<string, CharacterModel>
    {
        public CharacterModel AddCharacter(CharacterServerData serverData, EntitySpecification specification)
        {
            var model = new CharacterModel(serverData, specification);
            
            Add(serverData.PlayerId.Value, model);
            
            return model;
        }
    }
}