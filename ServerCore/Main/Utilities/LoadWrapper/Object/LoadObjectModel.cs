using ServerCore.Main.Utilities.Awaiter;

namespace ServerCore.Main.Utilities.LoadWrapper.Object
{
    public class LoadObjectModel : ILoadObjectToWrapperModel, ILoadObjectModel
    {
        private readonly ILoadObjectWrapper _wrapper;
        public CustomAwaiter LoadAwaiter { get; } = new();
        public CustomAwaiter UnloadAwaiter { get; } = new();
        
        public string Result { get; set; }
        public string Path { get; }
        public string Key { get; }

        public LoadObjectModel(ILoadObjectWrapper wrapper, string key, string path)
        {
            _wrapper = wrapper;
            Key = key;
            Path = path;
        }

        public CustomAwaiter Load()
        {
            _wrapper.Load(this);
            
            return LoadAwaiter;
        }
        
        public void CompleteLoad()
        {
            LoadAwaiter.Complete();
        }

        public void CompleteUnload()
        {
            UnloadAwaiter.Complete();
        }

        public void Dispose()
        {
            
        }
    }
}