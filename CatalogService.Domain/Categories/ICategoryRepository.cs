using Framework.BuildingBlock.Repositories;

namespace CatalogService.Categories;

public interface ICategoryRepository : IRepositoryFramework<Category, Guid>
{
}
