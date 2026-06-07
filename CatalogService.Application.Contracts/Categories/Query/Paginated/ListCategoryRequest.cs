using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Categories;

public class ListCategoryRequest : IFrameworkRequest<ListCategoryResponse>
{
    public Guid? ParentId { get; set; }
}
