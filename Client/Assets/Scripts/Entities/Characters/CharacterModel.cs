using Entities.Specification;
using ServerCore.Main;
using ServerCore.Main.Utilities;
using ServerManagement.Test;
using Utilities.Model;

namespace Entities.Characters
{
    public class CharacterModel : EntityModel, IModel
    {
        public string Id { get; }
        public readonly CharacterServerData ServerData;

        public CharacterMovementState LastProcessedState;
        
        public readonly CharacterMovementState[] MovementBuffer = new CharacterMovementState[2048];

        public int CurrentTick { get; set; }
        
        public CharacterModel(EntitySpecification specification, IEntityModel target) : base(specification, target)
        {
        }

        public CharacterModel(string id, EntitySpecification specification) : base(specification)
        {
            Id = id;
            ServerData = new CharacterServerData(id);
            MovementBuffer = new CharacterMovementState[2048];
        }
    }
}