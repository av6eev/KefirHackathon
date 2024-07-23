using System.Collections.Generic;
using System.Linq;
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

        public bool TryGetByNickname(string nickname, out CharacterModel characterModel)
        {
            var character = Collection.Values.Where(element => element.ServerData.PlayerNickname.Value == nickname).ToList();
            
            if (character.Any())
            {
                characterModel = character.First();
                return true;
            }

            characterModel = default;
            return false;
        }
    }
}