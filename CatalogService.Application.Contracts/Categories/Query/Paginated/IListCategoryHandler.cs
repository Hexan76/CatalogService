using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Categories;

public interface IListCategoryHandler : IFrameworkRequestHandler<ListCategoryRequest, ListCategoryResponse>
{
}
