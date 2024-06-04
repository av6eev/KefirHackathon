using ServerCore.Main.Specifications;
using ServerCore.Main.Specifications.Base;
using ServerCore.Main.Specifications.Collection;
using ServerCore.Main.Utilities.Awaiter;
using ServerCore.Main.Utilities.LoadWrapper.Object;
using ServerCore.Main.Utilities.SimpleJson;

namespace ServerCore.Main.Utilities.LoadWrapper
{
    public class LoadSpecificationsWrapper<T> where T : ISpecification, new()
    {
        private readonly ILoadObjectsModel _loadObjectsModel;
        private readonly string _key;
        private readonly IPrimitiveSpecificationsCollection _specificationsCollection;
        public readonly CustomAwaiter LoadAwaiter = new();
    
        public LoadSpecificationsWrapper(ILoadObjectsModel loadObjectsModel, string key, IPrimitiveSpecificationsCollection specificationsCollection)
        {
            _loadObjectsModel = loadObjectsModel;
            _key = key;
            _specificationsCollection = specificationsCollection;
        }

        private async void LoadFromJson()
        {
            var objectModel = _loadObjectsModel.Create(_key);
            await objectModel.Load();
            
            var result = new JsonParser(objectModel.Result).ParseAsDictionary();

            foreach (var element in result.GetNodes(_key))
            {
                var specification = new T();
                
                specification.Fill(element);
                _specificationsCollection.Add(element.GetString("id"), specification);
            }

            LoadAwaiter.Complete();
        }

        public CustomAwaiter Load()
        {
            LoadFromJson();
            
            return LoadAwaiter;
        }
    }
}