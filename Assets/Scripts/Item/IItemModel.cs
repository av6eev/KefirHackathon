using Item.Specification;

namespace Item
{
    public interface IItemModel
    {
        string Id { get; }
        ItemSpecification Specification { get; }
    }
}