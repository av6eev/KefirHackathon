using Utilities.ModelCollection;

namespace Quest.Collection
{
    public interface IQuestsCollection : IModelCollection<QuestModel>
    {
        void AddQuest(string questId);
    }
}