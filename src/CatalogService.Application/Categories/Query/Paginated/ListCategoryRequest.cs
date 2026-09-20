using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Categories;

public class ListCategoryRequest : IFrameworkRequest<ListCategoryResponse>
{
    public long[] BusinessTypeIds { get; set; } = [];
    public long? ParentId { get; set; }
}
