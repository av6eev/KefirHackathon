﻿using Awaiter;

namespace SceneManagement
{
    public class SceneManagementModel : ISceneManagementModel
    {
        public CustomAwaiter LoadAwaiter { get; } = new();
        public CustomAwaiter UnloadAwaiter { get; } = new();
        
        public string SceneId { get; }

        public SceneManagementModel(string sceneId)
        {
            SceneId = sceneId;
        }

        public void Dispose()
        {
        }
    }
}