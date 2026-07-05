using CatalogService.CategoryCustomization;

using Framework.BuildingBlock.Repositories;

namespace CatalogService.Customizations;

public interface ICustomizationGroupRepository : IRepositoryFramework<CustomizationGroup, Guid>
{
}
