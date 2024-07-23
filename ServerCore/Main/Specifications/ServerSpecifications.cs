using System;
using ServerCore.Main.Specifications.Collection;
using ServerCore.Main.Utilities.Awaiter;
using ServerCore.Main.Utilities.LoadWrapper;
using ServerCore.Main.Utilities.LoadWrapper.Object;

namespace ServerCore.Main.Specifications
{
    public class ServerSpecifications : IServerSpecifications
    {
        public INewSpecificationsCollection<LocationSpecification> LocationSpecifications { get; } = new NewSpecificationsCollection<LocationSpecification>();
        public INewSpecificationsCollection<InteractObjectStateSpecification> InteractObjectStateSpecifications { get; } = new NewSpecificationsCollection<InteractObjectStateSpecification>();
        
        public readonly CustomAwaiter LoadAwaiter = new();
        
        public ServerSpecifications(ILoadObjectsModel loadObjectsModel)
        {
            Load(loadObjectsModel);
        }

        private async void Load(ILoadObjectsModel loadObjectsModel)
        {
            await new LoadSpecificationsWrapper<LocationSpecification>(loadObjectsModel, "locations", LocationSpecifications).Load();
            await new LoadSpecificationsWrapper<InteractObjectStateSpecification>(loadObjectsModel, "interact_objects_state", InteractObjectStateSpecifications).Load();

            LoadAwaiter.Complete();
        }
    }

    public interface IServerSpecifications
    {
        INewSpecificationsCollection<LocationSpecification> LocationSpecifications { get; }
        INewSpecificationsCollection<InteractObjectStateSpecification> InteractObjectStateSpecifications { get; }
    }
}