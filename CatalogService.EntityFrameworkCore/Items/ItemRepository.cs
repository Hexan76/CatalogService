using CatalogService.EntityFrameworkCore;
using Framework.BuildingBlock.Repositories;
using Volo.Abp.EntityFrameworkCore;

namespace CatalogService.Items;

public class ItemRepository : EfCoreRepositoryFramework<CatalogServiceDbContext, Item, Guid>, IItemRepository
{
    public ItemRepository(IDbContextProvider<CatalogServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
