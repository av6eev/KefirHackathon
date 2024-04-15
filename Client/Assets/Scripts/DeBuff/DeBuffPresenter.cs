using Entities;
using Presenter;
using Object = UnityEngine.Object;

namespace DeBuff
{
    public class DeBuffPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly DeBuffModel _model;
        private readonly DeBuffView _view;

        public DeBuffPresenter(IGameModel gameModel, DeBuffModel model, DeBuffView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            _view.Title.text = _model.Specification.Title;
            
            _gameModel.PlayerModel.Resources.GetModel(EntityResourceType.Amnesia).Amount.OnChanged += HandleAmnesiaChanged;
        }

        public void Dispose()
        {
            Object.Destroy(_view.gameObject);
            
            _gameModel.PlayerModel.Resources.GetModel(EntityResourceType.Amnesia).Amount.OnChanged -= HandleAmnesiaChanged;
        }

        private void HandleAmnesiaChanged(float newAmnesia, float oldAmnesia)
        {
            if (_model.Specification.ApplyValue <= newAmnesia)
            {
                _model.IsActive.Value = true;
                
                _view.Holder.SetActive(false);
                _view.AnimationRoot.SetActive(true);
            }
            else
            {
                _model.IsActive.Value = false;
                
                _view.Holder.SetActive(true);
                _view.AnimationRoot.SetActive(false);
            }
        }
    }
}