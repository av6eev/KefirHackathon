using System.Collections.Generic;
using Quest.Specification;
using SimpleJson;
using Utilities.ModelCollection;

namespace Quest.Collection
{
    public class QuestsCollection : ModelCollection<QuestModel>, IQuestsCollection
    {
        public const string Id = "quests";
        private readonly Dictionary<string, QuestSpecification> _specifications;

        public QuestsCollection(Dictionary<string, QuestSpecification> specifications)
        {
            _specifications = specifications;
        }
        
        public void FillFromSave(List<IDictionary<string, object>> nodes)
        {
            foreach (var node in nodes)
            {
                var id = node.GetString("id");
                
                AddQuest(id);
            }
        }

        public void AddQuest(string questId)
        {
            Add(new QuestModel(_specifications[questId]));
        }
    }
}