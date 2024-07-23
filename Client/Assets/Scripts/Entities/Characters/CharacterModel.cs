using Entities.Specification;
using ServerCore.Main;
using ServerManagement.Test;
using Utilities.Model;

namespace Entities.Characters
{
    public class CharacterModel : EntityModel, IModel
    {
        public readonly CharacterServerData ServerData;

        public readonly CharacterMovementState[] MovementBuffer = new CharacterMovementState[2048];

        public CharacterModel(EntitySpecification specification, IEntityModel target) : base(specification, target)
        {
        }

        public CharacterModel(CharacterServerData serverData, EntitySpecification specification) : base(specification)
        {
            ServerData = serverData;
            MovementBuffer = new CharacterMovementState[2048];
        }
    }
}