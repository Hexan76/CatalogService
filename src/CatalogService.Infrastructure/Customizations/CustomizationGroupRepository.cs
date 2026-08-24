using CatalogService.CategoryCustomization;
using CatalogService.EntityFrameworkCore;
using Framework.BuildingBlock.EntityFrameworkCore;

namespace CatalogService.Customizations;

public class CustomizationGroupRepository : EfGenericRepository<CatalogServiceDbContext, CustomizationGroup, long>, ICustomizationGroupRepository
{
    public CustomizationGroupRepository(CatalogServiceDbContext context) : base(context)
    {
    }
}
