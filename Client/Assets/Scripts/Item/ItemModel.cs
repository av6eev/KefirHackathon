using Item.Specification;

namespace Item
{
    public class ItemModel : IItemModel
    {
        public string Id { get; }
        public ItemSpecification Specification { get; }

        public ItemModel(string id, ItemSpecification specification)
        {
            Id = id;
            Specification = specification;
        }
    }
}