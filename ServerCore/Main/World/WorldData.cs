using ServerCore.Main.Property;

namespace ServerCore.Main.World
{
    public class WorldData : ServerData
    {
        public DataCollection<string, CharacterServerData> CharacterDataCollection { get; } = new("character_data_collection");
        
        public WorldData() : base("world")
        {
            Dataset.Add(CharacterDataCollection.Id, CharacterDataCollection);
        }
    }
}