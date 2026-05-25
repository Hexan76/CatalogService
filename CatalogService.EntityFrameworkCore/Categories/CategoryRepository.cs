using CatalogService.EntityFrameworkCore;
using Framework.BuildingBlock.Repositories;
using Volo.Abp.EntityFrameworkCore;

namespace CatalogService.Categories;

public class CategoryRepository : EfCoreRepositoryFramework<CatalogServiceDbContext, Category, Guid>, ICategoryRepository
{
    public CategoryRepository(IDbContextProvider<CatalogServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
