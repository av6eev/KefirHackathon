using System.Collections;
using System.Collections.Generic;
using ServerCore.Main.Property;

namespace ServerCore.Main.World
{
    public class WorldData : ServerData
    {
        public string Guid { get; }
        public DataCollection<string, CharacterServerData> CharacterDataCollection { get; } = new("character_data_collection");
        public readonly Property<string> MessageType = new("message_type", string.Empty, false);
        public readonly Property<double> Time = new("time", 0, false);
        
        public WorldData(string guid) : base("world")
        {
            Guid = guid;
            
            Dataset.Add(CharacterDataCollection.Id, CharacterDataCollection);
        }
    }
}