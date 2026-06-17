using Framework.BuildingBlock.Repositories;

namespace CatalogService.Items;

public interface IItemRepository : IRepositoryFramework<Item, Guid>
{
}
