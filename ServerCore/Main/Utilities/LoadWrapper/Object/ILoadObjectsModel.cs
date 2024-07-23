namespace ServerCore.Main.Utilities.LoadWrapper.Object
{
    public interface ILoadObjectsModel
    {
        ILoadObjectModel Create(string key);
        void Unload(ILoadElement model);
    }
}