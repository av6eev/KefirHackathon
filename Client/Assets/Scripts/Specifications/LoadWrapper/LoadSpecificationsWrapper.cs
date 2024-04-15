using Awaiter;
using Loader.Object;
using Specification;
using Specifications.Collection;

namespace Specifications.LoadWrapper
{
    public class LoadSpecificationsWrapper<T> where T : ISpecification, new()
    {
        private readonly IPrimitiveSpecificationsCollection _specificationsCollection;
        public readonly CustomAwaiter LoadAwaiter = new();
    
        public LoadSpecificationsWrapper(ILoadObjectsModel loadObjectsModel, string key, IPrimitiveSpecificationsCollection specificationsCollection)
        {
            _specificationsCollection = specificationsCollection;

            LoadFromAsset(loadObjectsModel, key);
        }

        private async void LoadFromAsset(ILoadObjectsModel loadObjectsModel, string key)
        {
            var objectModel = loadObjectsModel.Load<SpecificationCollectionScrObj<T>>(key);
            await objectModel.LoadAwaiter;

            foreach (var element in objectModel.Result.Collection)
            {
                _specificationsCollection.Add(element.Specification.Id, element.Specification);
            }
            
            LoadAwaiter.Complete();
        }
    }
}