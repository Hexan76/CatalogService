using CatalogService.EntityFrameworkCore;

using Framework.BuildingBlock.EntityFrameworkCore;

namespace CatalogService.Customizations;

public class CustomizationRepository : EfGenericRepository<CatalogServiceDbContext, Customization, long>, ICustomizationRepository
{
    public CustomizationRepository(CatalogServiceDbContext context) : base(context)
    {
    }
}
