using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Categories;

public interface ICreateCategoryHandler : IFrameworkRequestHandler<CreateCategoryRequest, CategoryModel>
{
}
