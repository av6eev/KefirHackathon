using Presenter;

namespace Entities.Player.Target
{
    public class EnemyChangePresenter : IPresenter
    {
        private readonly PlayerModel _playerModel;

        public EnemyChangePresenter(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }
        
        public void Init()
        {
            _playerModel.Target.OnChanged += HandleTargetChanged;
        }

        public void Dispose()
        {
            _playerModel.Target.OnChanged -= HandleTargetChanged;
        }

        private void HandleTargetChanged(IEntityModel newEnemy, IEntityModel oldEnemy)
        {
            if (oldEnemy != null)
            {
                oldEnemy.InTarget.Value = false;
            }

            if (newEnemy != null)
            {
                newEnemy.InTarget.Value = true;
            }
        }
    }
}