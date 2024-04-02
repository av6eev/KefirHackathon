using Awaiter;
using Loader.Object;
using Specification;
using Specifications.Collection;

namespace Specifications.LoadWrapper
{
    public class LoadAssetsSpecificationsWrapper<TAsset> where TAsset : BaseSpecification
    {
        private readonly IPrimitiveSpecificationsCollection _specificationsCollection;
        public readonly CustomAwaiter LoadAwaiter = new();
    
        public LoadAssetsSpecificationsWrapper(ILoadObjectsModel loadObjectsModel, string key, IPrimitiveSpecificationsCollection specificationsCollection)
        {
            _specificationsCollection = specificationsCollection;

            LoadFromAsset(loadObjectsModel, key);
        }

        private async void LoadFromAsset(ILoadObjectsModel loadObjectsModel, string key)
        {
            var objectModel = loadObjectsModel.Load<SpecificationAssetCollectionScrObj>(key);
            await objectModel.LoadAwaiter;

            foreach (var element in objectModel.Result.Collection)
            {
                var specification = element.GetSpecification();
                
                _specificationsCollection.Add(specification.Id, specification);
            }
            
            LoadAwaiter.Complete();
        }
    }
    
    
}