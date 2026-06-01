using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application.Contracts;

using Volo.Abp.Application.Dtos;

namespace CatalogService.Categories;

public class UpdateCategoryRequest : EntityDto<Guid>, IFrameworkRequest<CategoryModel>
{
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }
    public FinalizeModel? File { get; set; }
    public Guid? ParentId { get; set; }
}
