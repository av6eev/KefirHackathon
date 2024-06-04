using ServerCore.Main.Utilities.LoadWrapper.Object;

namespace ServerCore.Main.Utilities.LoadWrapper.Json
{
    public class JsonObjectLoadWrapperModel : IAddressableObjectLoadWrapperModel
    {
        public ILoadObjectToWrapperModel LoadObjectToWrapperModel { get; }

        public JsonObjectLoadWrapperModel(ILoadObjectToWrapperModel loadObjectToWrapperModel)
        {
            LoadObjectToWrapperModel = loadObjectToWrapperModel;
        }
    }

    public interface IAddressableObjectLoadWrapperModel
    {
    }
}