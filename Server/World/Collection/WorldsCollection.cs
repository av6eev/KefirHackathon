using ServerCore.Main.World;

namespace Server.World.Collection;

public class WorldsCollection
{
    public readonly Dictionary<string, WorldData> Worlds = new();
}