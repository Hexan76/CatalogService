using CatalogService.EntityFrameworkCore;

using Framework.BuildingBlock.Repositories;

using Volo.Abp.EntityFrameworkCore;

namespace CatalogService.Customizations;

public class CustomizationRepository : EfCoreRepositoryFramework<CatalogServiceDbContext, Customization, Guid>, ICustomizationRepository
{
    public CustomizationRepository(IDbContextProvider<CatalogServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
