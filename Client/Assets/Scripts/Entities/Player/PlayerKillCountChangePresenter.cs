using Presenter;

namespace Entities.Player
{
    public class PlayerKillCountChangePresenter : IPresenter
    {
        private readonly PlayerModel _model;

        public PlayerKillCountChangePresenter(PlayerModel model)
        {
            _model = model;
        }
        
        public void Init()
        {
            _model.KillCount.OnChanged += HandleKillCountChanged;
        }

        public void Dispose()
        {
            _model.KillCount.OnChanged -= HandleKillCountChanged;
        }

        private void HandleKillCountChanged(int newValue, int oldValue)
        {
            _model.Resources.GetModel(EntityResourceType.Amnesia).Decrease(10);
        }
    }
}