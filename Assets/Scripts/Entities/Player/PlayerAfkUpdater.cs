using Updater;

namespace Entities.Player
{
    public class PlayerAfkUpdater : IUpdater
    {
        private const float TickDuration = 1.7f;
        
        private readonly PlayerModel _model;

        private float _currentTickDuration;
        
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
                if (_currentTickDuration >= TickDuration)
                {
                    _model.Resources.GetModel(EntityResourceType.Amnesia).Increase(1);
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