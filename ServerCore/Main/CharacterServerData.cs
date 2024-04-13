﻿using System.Collections.Generic;
using ServerCore.Main.Property;
using ServerCore.Main.Utilities;

namespace ServerCore.Main
{
    public class CharacterServerData : ServerData
    {
        public readonly Property<Vector3> Position = new(ServerConst.PositionPropertyId, new Vector3(0,0,0));
        public readonly Property<int> Rotation = new("rotation", 0);
        public readonly Property<float> Speed = new("speed", 0);
        public readonly Property<byte> AnimationState = new("animation_state", 0);

        public Property<Vector3> LatestServerPosition = new Property<Vector3>("latest_server_position", new Vector3(0,0,0));
        public Property<int> CurrentTick = new Property<int>("current_tick", 0);
        
        public CharacterServerData(string id) : base(id)
        {
            Properties.Add(Position.Id, Position);
            Properties.Add(Rotation.Id, Rotation);
            Properties.Add(Speed.Id, Speed);
            Properties.Add(AnimationState.Id, AnimationState);
            Properties.Add(LatestServerPosition.Id, LatestServerPosition);
            Properties.Add(CurrentTick.Id, CurrentTick);
        }
    }
}