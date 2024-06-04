namespace ServerCore.Main.Utilities.LoadWrapper.Object
{
    public class LoadObjectsModel : ILoadObjectsModel
    {
        private readonly ILoadObjectWrapper _wrapper;
        private readonly string _path;

        public LoadObjectsModel(ILoadObjectWrapper wrapper, string path)
        {
            _wrapper = wrapper;
            _path = path;
        }

        public ILoadObjectModel Create(string key)
        {
            return new LoadObjectModel(_wrapper, key, _path);
        }

        public void Unload(ILoadElement model)
        {
            _wrapper.Unload((IWrapperLoadModel)model);
        }
    }
}