using ServerCore.Main.Specifications;
using ServerCore.Main.Specifications.Collection;
using ServerCore.Main.Utilities.Awaiter;
using ServerCore.Main.Utilities.LoadWrapper.Object;
using ServerCore.Main.Utilities.SimpleJson;

namespace ServerCore.Main.Utilities.LoadWrapper
{
    public class LoadSpecificationsWrapper<T> where T : ISpecification, new()
    {
        private readonly IPrimitiveSpecificationsCollection _specificationsCollection;
        public readonly CustomAwaiter LoadAwaiter = new();
    
        public LoadSpecificationsWrapper(ILoadObjectsModel loadObjectsModel, string key, IPrimitiveSpecificationsCollection specificationsCollection)
        {
            _specificationsCollection = specificationsCollection;
            LoadFromJson(loadObjectsModel, key);
        }

        private async void LoadFromJson(ILoadObjectsModel loadObjectsModel, string key)
        {
            var objectModel = loadObjectsModel.Load<string>(key);
            await objectModel.LoadAwaiter;
            
            var result = new JsonParser(objectModel.Result).ParseAsDictionary();

            foreach (var element in result.GetNodes(key))
            {
                var specification = new T();
                
                specification.Fill(element);
                _specificationsCollection.Add(element.GetString("id"), specification);
            }
            
            LoadAwaiter.Complete();
        }
    }
}