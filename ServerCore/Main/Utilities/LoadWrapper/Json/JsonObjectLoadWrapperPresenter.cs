using System.IO;
using System.Threading.Tasks;
using ServerCore.Main.Utilities.Presenter;

namespace ServerCore.Main.Utilities.LoadWrapper.Json
{
    public class JsonObjectLoadWrapperPresenter : IPresenter
    {
        private readonly JsonObjectLoadWrapperModel _model;

        public JsonObjectLoadWrapperPresenter(JsonObjectLoadWrapperModel model)
        {
            _model = model;
        }
        
        public async void Init()
        {
            var path = $"{_model.LoadObjectToWrapperModel.Path}/{_model.LoadObjectToWrapperModel.Key}.json";
            var text = await File.ReadAllTextAsync(path);
            await Task.Delay(100);
            
            OnCompleted(text);
        }

        public void Dispose()
        {
            _model.LoadObjectToWrapperModel.CompleteUnload();
        }

        private void OnCompleted(string text)
        {
            _model.LoadObjectToWrapperModel.Result = text;
            _model.LoadObjectToWrapperModel.CompleteLoad();
        }
    }
}