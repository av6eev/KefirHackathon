using Presenter;
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
            }
        }
    }
}