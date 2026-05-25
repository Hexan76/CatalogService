using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Categories;

public class CreateCategoryRequest : IFrameworkRequest<CategoryModel>
{
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
}
