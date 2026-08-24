using CatalogService.ObjectStorageService;

using Volo.Abp.Application.Dtos;

namespace CatalogService.Categories;

public class CategoryModel : EntityDto<long>
{

    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }
    public int SortOrder { get; set; }
    public FileUrlModel Image { get; set; }
    public bool IsDisabled { get; set; }
    public bool IsPopular { get; set; }
    public long? ParentId { get; set; }
    public virtual CategoryModel? Parent { get; set; }

}
