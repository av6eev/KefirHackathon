using Entities.Specification;
using Utilities.ModelCollection;

namespace Entities.Characters.Collection
{
    public class CharactersCollection : ModelCollection<string, CharacterModel>
    {
        public CharacterModel AddCharacter(string id, EntitySpecification specification)
        {
            var model = new CharacterModel(id, specification);
            
            Add(id, model);
            
            return model;
        }
    }
}