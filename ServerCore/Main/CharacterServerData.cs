using ServerCore.Main.Property;
using ServerCore.Main.Utilities;

namespace ServerCore.Main
{
    public class CharacterServerData : ServerData
    {
        public readonly Property<string> PlayerId = new("id", string.Empty);
        public readonly Property<string> PlayerNickname = new("nickname", string.Empty);
        public readonly Property<Vector3> Position = new("position", new Vector3(0,0,0));
        public readonly Property<int> Rotation = new("rotation", 0);
        public readonly Property<float> Speed = new("speed", 0);
        public readonly Property<byte> AnimationState = new("animation_state", 0);

        public readonly Property<Vector3> LatestServerPosition = new("latest_server_position", new Vector3(0,0,0));
        
        public CharacterServerData() : base("character_server_data")
        {
            Properties.Add(PlayerId.Id, PlayerId);
            Properties.Add(PlayerNickname.Id, PlayerNickname);
            Properties.Add(Position.Id, Position);
            Properties.Add(Rotation.Id, Rotation);
            Properties.Add(Speed.Id, Speed);
            Properties.Add(AnimationState.Id, AnimationState);
            Properties.Add(LatestServerPosition.Id, LatestServerPosition);
        }
    }
}