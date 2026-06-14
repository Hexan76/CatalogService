using System.Linq.Expressions;

using CatalogService.EntityFrameworkCore;

using Framework.BuildingBlock.Repositories;

using Microsoft.EntityFrameworkCore;

using Volo.Abp.EntityFrameworkCore;

namespace CatalogService.Items;

public class ItemRepository : EfCoreRepositoryFramework<CatalogServiceDbContext, Item, Guid>, IItemRepository
{
    public ItemRepository(IDbContextProvider<CatalogServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Item>> WithDetailsAsync(params Expression<Func<Item, object>>[] propertySelectors)
    {
        return (await GetQueryableAsync())
            .Include(x => x.MenuItems);
    }
}
