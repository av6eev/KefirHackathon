namespace ServerCore.Main.Utilities.LoadWrapper.Object
{
    public interface ILoadObjectToWrapperModel<in T> : IWrapperLoadModel
    {
        T Result { set; }
        string Key { get; }
    }
}