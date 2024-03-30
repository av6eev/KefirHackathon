using GameScenes.Arena;
using GameScenes.GameUI;
using GameScenes.Hub;
using Presenter;
using SceneManagement;
using Specification.Scene;
using UnityEngine;

namespace LocationBuilder
{
    public class LocationLogicBuilder
    {
        private readonly GameModel _gameModel;
        private readonly PresentersList _presenters;
        private readonly SceneSpecification _specification;

        public LocationLogicBuilder(GameModel gameModel, PresentersList presenters, SceneSpecification specification)
        {
            _gameModel = gameModel;
            _presenters = presenters;
            _specification = specification;
        }

        public void Build()
        {
            var sceneView = GameObject.Find(_specification.PrefabId).GetComponent<LocationSceneView>();
            
            switch (_specification.SceneId)
            {
                case SceneConst.GameUiId:
                    _presenters.Add(new GameUiScenePresenter(_gameModel, (GameUiSceneView)sceneView));
                    break;
                case SceneConst.HubId:
                    _presenters.Add(new HubScenePresenter(_gameModel, (HubSceneView)sceneView));
                    break;
                case SceneConst.ArenaId:
                    _presenters.Add(new ArenaScenePresenter(_gameModel, (ArenaSceneView)sceneView));
                    break;
            }
        }
    }
}