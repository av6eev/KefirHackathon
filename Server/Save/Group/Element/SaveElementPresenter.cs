using ServerCore.Main.Utilities.Logger;
using ServerCore.Main.Utilities.Presenter;

namespace Server.Save.Group.Element
{
    public class SaveElementPresenter : IPresenter
    {
        private readonly ServerGameModel _gameModel;
        private readonly SaveGroupModel _saveGroupModel;
        private readonly ISaveElementModel _model;

        public SaveElementPresenter(ServerGameModel gameModel, SaveGroupModel saveGroupModel, ISaveElementModel model)
        {
            _gameModel = gameModel;
            _saveGroupModel = saveGroupModel;
            _model = model;
        }
        
        public void Init()
        {
            _saveGroupModel.Save();
            _model.ChangeEvent.OnChanged += HandleChange;
        }

        public void Dispose()
        {
            _model.ChangeEvent.OnChanged -= HandleChange;
        }

        private void HandleChange()
        {
            Logger.Instance.Log("changed");
            _saveGroupModel.Save();
        }
    }
}