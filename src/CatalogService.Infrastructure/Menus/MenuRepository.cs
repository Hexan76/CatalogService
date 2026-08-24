using CatalogService.EntityFrameworkCore;

using Framework.BuildingBlock.EntityFrameworkCore;

namespace CatalogService.Menus;

public class MenuRepository : EfGenericRepository<CatalogServiceDbContext, Menu, long>, IMenuRepository
{
    public MenuRepository(CatalogServiceDbContext context) : base(context)
    {
    }
}
