using System;
using Specification;

namespace Quest.Specification
{
    [Serializable]
    public class QuestSpecification : BaseSpecification
    {
        public string Title;
        public string Description;
        public string DemandId;
        public string[] RewardIds;
    }
}