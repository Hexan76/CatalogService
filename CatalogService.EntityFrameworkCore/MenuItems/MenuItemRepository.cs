using CatalogService.EntityFrameworkCore;
using Framework.BuildingBlock.Repositories;
using Volo.Abp.EntityFrameworkCore;

namespace CatalogService.MenuItems;

public class MenuItemRepository : EfCoreRepositoryFramework<CatalogServiceDbContext, MenuItem, Guid>, IMenuItemRepository
{
    public MenuItemRepository(IDbContextProvider<CatalogServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
