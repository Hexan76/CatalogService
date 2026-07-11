using CatalogService.EntityFrameworkCore;

using Framework.BuildingBlock.Repositories;

using Volo.Abp.EntityFrameworkCore;

namespace CatalogService.Customizations;

public class CustomizationRepository : EfCoreRepositoryFramework<CatalogServiceDbContext, Customization, long>, ICustomizationRepository
{
    public CustomizationRepository(IDbContextProvider<CatalogServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
