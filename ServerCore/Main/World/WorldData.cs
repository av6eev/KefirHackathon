using ServerCore.Main.Property;

namespace ServerCore.Main.World
{
    public class WorldData : ServerData
    {
        public string Guid { get; }
        public DataCollection<string, CharacterServerData> CharacterDataCollection { get; } = new("character_data_collection");
        
        public WorldData(string guid) : base("world")
        {
            Guid = guid;
            Dataset.Add(CharacterDataCollection.Id, CharacterDataCollection);
        }
    }
}