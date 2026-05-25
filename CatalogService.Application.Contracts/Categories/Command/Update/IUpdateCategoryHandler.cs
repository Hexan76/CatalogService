using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Categories;

public interface IUpdateCategoryHandler : IFrameworkRequestHandler<UpdateCategoryRequest, CategoryModel>
{
}
