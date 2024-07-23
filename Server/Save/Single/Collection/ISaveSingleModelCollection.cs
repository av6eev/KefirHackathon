namespace Server.Save.Single.Collection
{
    public interface ISaveSingleModelCollection
    {
        void Add(INotifySaveModel model);
        void RemoveElement(string saveId);
    }
}