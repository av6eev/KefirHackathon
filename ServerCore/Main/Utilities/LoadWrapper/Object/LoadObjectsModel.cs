namespace ServerCore.Main.Utilities.LoadWrapper.Object
{
    public class LoadObjectsModel : ILoadObjectsModel
    {
        private readonly ILoadObjectWrapper _wrapper;

        public LoadObjectsModel(ILoadObjectWrapper wrapper)
        {
            _wrapper = wrapper;
        }

        public ILoadObjectModel<T> Load<T>(string key)
        {
            var model = new LoadObjectModel<T>(key);
            
            _wrapper.Load(model);
            
            return model;
        }

        public void Unload(ILoadElement model)
        {
            _wrapper.Unload((IWrapperLoadModel)model);
        }
    }
}