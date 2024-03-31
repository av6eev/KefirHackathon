using System.Collections.Generic;
using Quest.Specification;
using Save;

namespace Quest
{
    public class QuestModel : IQuestModel
    {
        public QuestSpecification Specification { get; }

        public QuestModel(QuestSpecification specification)
        {
            Specification = specification;
        }

        public IDictionary<string, object> GetSaveData()
        {
            return new Dictionary<string, object>
            {
                {"id", Specification.Id}
            };
        }
    }

    public interface IQuestModel : ISaveModel
    {
        QuestSpecification Specification { get; }
    }
}