using System.Collections.Generic;
using System.Linq;
using Awaiter;
using Presenter;
using UnityEngine;
using Updater;

namespace Cameras
{
    public class CameraPresenter : IPresenter
    {
        private readonly GameModel _gameModel;
        private readonly CameraModel _model;
        private readonly CameraView _view;

        private readonly List<IUpdater> _updaters = new();
        private CameraFollowUpdater _updater;

        public CameraPresenter(GameModel gameModel, CameraModel model, CameraView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            _updaters.Add(new CameraFollowUpdater(_model, _view));
            _updaters.Add(new CameraInfoUpdater(_model, _view));

            foreach (var updater in _updaters)
            {
                _gameModel.LateUpdatersList.Add(updater);
            }
            
            _model.IsCompleted.OnChanged += CheckComplete;
            _model.OnStateChanged += ChangeCurrentState;
        }

        public void Dispose()
        {
            foreach (var updater in _updaters)
            {
                _gameModel.LateUpdatersList.Remove(updater);
            }

            _model.CurrentTarget = null;
            _model.NextTarget = null;
            _model.CurrentState = CameraStateType.None;
            _model.NextState = CameraStateType.None;
                        
            _updaters.Clear();
            
            _model.IsCompleted.OnChanged -= CheckComplete;
            _model.OnStateChanged -= ChangeCurrentState;
        }

        private async void ChangeCurrentState(CameraStateType stateType)
        {
            if (_model.Specification != null && _model.Specification.IsAwaitable && _model.IsCompleted.Value == false)
            {
                await _model.CurrentStateCompleteAwaiter;
            }
            
            var specification = _gameModel.Specifications.CameraSpecifications.GetSpecifications().Values.First(item => item.StateType == stateType);

            _model.Specification = specification;
            _model.CurrentTarget = _model.NextTarget;
            
            _model.CurrentState = _model.NextState;
            _model.NextState = CameraStateType.None;
            
            _model.IsCompleted.Value = false;

            Debug.Log("New current state: " + _model.CurrentState);
            Debug.Log("New next state: " + _model.NextState);
        }

        private void CheckComplete(bool newState, bool oldState)
        {
            if (newState)
            {
                _model.CurrentStateCompleteAwaiter.Complete();
                _model.CurrentStateCompleteAwaiter.Dispose();
                _model.CurrentStateCompleteAwaiter = new CustomAwaiter();
            }
        }
    }
}