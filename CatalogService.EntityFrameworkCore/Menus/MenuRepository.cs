using CatalogService.EntityFrameworkCore;
using Framework.BuildingBlock.Repositories;
using Volo.Abp.EntityFrameworkCore;

namespace CatalogService.Menus;

public class MenuRepository : EfCoreRepositoryFramework<CatalogServiceDbContext, Menu, long>, IMenuRepository
{
    public MenuRepository(IDbContextProvider<CatalogServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
