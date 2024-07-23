namespace ServerCore.Main.Utilities.LoadWrapper.Object
{
    public interface ILoadObjectWrapper
    {
        void Load(ILoadObjectToWrapperModel model);
        void Unload(IWrapperLoadModel model);
    }
}