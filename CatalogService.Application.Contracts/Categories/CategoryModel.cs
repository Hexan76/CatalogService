using Volo.Abp.Application.Dtos;

namespace CatalogService.Categories;

public class CategoryModel : EntityDto<Guid>
{
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }
    public int SortOrder { get; set; }
    public string IconUrl { get; set; }
    public string BannerUrl { get; set; }
    public bool IsDisabled { get; set; }
    public bool IsPopular { get; set; }
    public Guid? ParentId { get; set; }
    public virtual CategoryModel? Parent { get; set; }

}
