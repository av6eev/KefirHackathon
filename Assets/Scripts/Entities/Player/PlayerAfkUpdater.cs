using Updater;

namespace Entities.Player
{
    public class PlayerAfkUpdater : IUpdater
    {
        private readonly PlayerModel _model;

        private float _tickDuration = 1f;
        private float _currentTickDuration = 0f;
        
        public PlayerAfkUpdater(PlayerModel model)
        {
            _model = model;
        }
        
        public void Update(float deltaTime)
        {
            if (!_model.IsAfk.Value)
            {
                if (_model.AfkTime.Value >= 5f)
                {
                    _model.IsAfk.Value = true;
                }
                else
                {
                    _model.AfkTime.Value += deltaTime;
                }
            }
            else
            {
                if (_currentTickDuration >= _tickDuration)
                {
                    _model.Resources.GetModel(EntityResourceType.Amnesia).Amount.Value++;
                    _currentTickDuration = 0;
                }
                else
                {
                    _currentTickDuration += deltaTime;
                }
            }
        }
    }
}