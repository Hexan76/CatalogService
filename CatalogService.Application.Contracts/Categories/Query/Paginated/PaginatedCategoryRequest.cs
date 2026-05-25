using Framework.BuildingBlock.Application.Contracts;
using Framework.BuildingBlock.Contracts;

namespace CatalogService.Categories;

public class PaginatedCategoryRequest : PagedAndSortedResultRequestFramework, IFrameworkRequest<PaginatedCategoryResponse>
{
    public FilterGroupDto FilterGroup { get; set; }
}
