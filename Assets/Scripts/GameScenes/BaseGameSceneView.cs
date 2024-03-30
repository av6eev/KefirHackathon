using Cameras;
using Entities.Player;
using LocationBuilder;

namespace GameScenes
{
    public class BaseGameSceneView : LocationSceneView
    {
        public PlayerView PlayerView;
        public CameraView CameraView;
    }
}