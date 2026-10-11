using Framework.BuildingBlock.Data;

namespace CatalogService.Items;

public interface IItemRepository : IGenericRepository<Item, long>
{
    Task<Item?> FindWithDetailsAsync(long id, CancellationToken cancellationToken = default);
}
