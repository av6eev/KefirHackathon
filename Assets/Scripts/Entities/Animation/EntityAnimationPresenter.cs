using Presenter;

namespace Entities.Animation
{
    public class EntityAnimationPresenter : IPresenter
    {
        private readonly EntityAnimationEventsView _animationEventsView;
        private readonly EntityAnimationEvents _animationEvents;

        public EntityAnimationPresenter(EntityAnimationEventsView animationEventsView, EntityAnimationEvents animationEvents)
        {
            _animationEventsView = animationEventsView;
            _animationEvents = animationEvents;
        }

        public void Init()
        {
            _animationEventsView.OnEndRoll += HandleEndRoll;
            _animationEventsView.OnEndAttack += HandleEndAttack;
            _animationEventsView.OnNeedEffect += HandleNeedEffect;
        }

        public void Dispose()
        {
            _animationEventsView.OnEndRoll -= HandleEndRoll;
            _animationEventsView.OnEndAttack -= HandleEndAttack;
            _animationEventsView.OnNeedEffect -= HandleNeedEffect;
        }

        private void HandleEndRoll()
        {
            _animationEvents.EndRoll();
        }

        private void HandleEndAttack()
        {
            _animationEvents.EndAttack();
        }

        private void HandleNeedEffect()
        {
            _animationEvents.NeedEffect();
        }
    }
}