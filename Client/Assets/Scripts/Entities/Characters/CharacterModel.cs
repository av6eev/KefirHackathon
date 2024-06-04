using Entities.Specification;
using ServerCore.Main;
using ServerCore.Main.Utilities;
using ServerManagement.Test;
using Utilities.Model;

namespace Entities.Characters
{
    public class CharacterModel : EntityModel, IModel
    {
        public readonly CharacterServerData ServerData;

        public CharacterMovementState LastProcessedState;
        
        public readonly CharacterMovementState[] MovementBuffer = new CharacterMovementState[2048];

        public int CurrentTick { get; set; }
        
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