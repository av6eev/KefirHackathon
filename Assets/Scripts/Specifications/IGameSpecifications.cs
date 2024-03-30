using Cameras.Specification;
using Entities.Specification;
using Specification.Scene;
using Specifications.Collection;

namespace Specifications
{
    public interface IGameSpecifications
    {
        ISpecificationsCollection<SceneSpecification> SceneSpecifications { get; }
        ISpecificationsCollection<CameraSpecification> CameraSpecifications { get; }
        ISpecificationsCollection<EntitySpecification> EntitySpecifications { get; }
    }
}