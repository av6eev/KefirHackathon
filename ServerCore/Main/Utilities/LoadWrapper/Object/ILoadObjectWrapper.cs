namespace ServerCore.Main.Utilities.LoadWrapper.Object
{
    public interface ILoadObjectWrapper
    {
        void Load<T>(ILoadObjectToWrapperModel<T> model);
        void Unload(IWrapperLoadModel model);
    }
}