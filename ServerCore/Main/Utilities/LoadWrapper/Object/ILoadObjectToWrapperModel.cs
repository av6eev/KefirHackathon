namespace ServerCore.Main.Utilities.LoadWrapper.Object
{
    public interface ILoadObjectToWrapperModel : IWrapperLoadModel
    {
        string Path { get; }
        string Key { get; }
        string Result { set; }
    }
}