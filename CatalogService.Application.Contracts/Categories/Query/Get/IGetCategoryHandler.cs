using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Categories;

public interface IGetCategoryHandler : IFrameworkRequestHandler<GetCategoryRequest, CategoryModel>
{
}
