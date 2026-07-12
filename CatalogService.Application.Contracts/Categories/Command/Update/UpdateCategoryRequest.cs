using Framework.BuildingBlock.Application.Contracts;

using Volo.Abp.Application.Dtos;

namespace CatalogService.Categories;

public class UpdateCategoryRequest : EntityDto<long>, IFrameworkRequest<CategoryModel>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid? ImageId { get; set; }
    public long BusinessTypeId { get; set; }
    public long? ParentId { get; set; }
}
