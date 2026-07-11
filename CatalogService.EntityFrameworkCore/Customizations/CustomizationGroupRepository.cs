using CatalogService.CategoryCustomization;
using CatalogService.EntityFrameworkCore;

using Framework.BuildingBlock.Repositories;

using Microsoft.EntityFrameworkCore;

using Volo.Abp.EntityFrameworkCore;

namespace CatalogService.Customizations;

public class CustomizationGroupRepository : EfCoreRepositoryFramework<CatalogServiceDbContext, CustomizationGroup, long>, ICustomizationGroupRepository
{
    public CustomizationGroupRepository(IDbContextProvider<CatalogServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<CustomizationGroup>> WithDetailsAsync()
    {
        var query = await base.WithDetailsAsync();

        query = query
            .Include(x => x.Customizations)
            .ThenInclude(x => x.Items);
        return query;
    }
}
