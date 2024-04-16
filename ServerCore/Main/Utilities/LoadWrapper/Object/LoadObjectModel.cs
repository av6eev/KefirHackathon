using ServerCore.Main.Utilities.Awaiter;

namespace ServerCore.Main.Utilities.LoadWrapper.Object
{
    public class LoadObjectModel<T> : ILoadObjectToWrapperModel<T>, ILoadObjectModel<T>
    {
        public CustomAwaiter LoadAwaiter { get; } = new();
        public CustomAwaiter UnloadAwaiter { get; } = new();
        
        public T Result { get; set; }

        public string Key { get; }

        public LoadObjectModel(string key)
        {
            Key = key;
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