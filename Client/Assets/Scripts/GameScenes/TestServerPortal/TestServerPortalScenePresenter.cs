﻿using InteractiveObjects.Portal;
using LoadingScreen;

namespace GameScenes.TestServerPortal
{
    public class TestServerPortalScenePresenter : BaseGameScenePresenter
    {
        private readonly GameModel _gameModel;
        private readonly TestServerPortalSceneView _view;

        public TestServerPortalScenePresenter(GameModel gameModel, TestServerPortalSceneView view) : base(gameModel, view)
        {
            _gameModel = gameModel;
            _view = view;
        }

        protected override void AfterInit()
        {
            Presenters.Add(LoadingScreenMessageConst.PortalPresenter, new PortalPresenter(_gameModel, _view.PortalView));
        }

        protected override void AfterDispose()
        {
        }
    }
}