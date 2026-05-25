using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Categories;

public interface IPaginatedCategoryHandler : IFrameworkRequestHandler<PaginatedCategoryRequest, PaginatedCategoryResponse>
{
}
