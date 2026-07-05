using Framework.BuildingBlock.Repositories;

namespace CatalogService.Customizations;

public interface ICustomizationRepository : IRepositoryFramework<Customization, Guid>
{
}
