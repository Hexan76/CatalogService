using Volo.Abp.Application.Dtos;

namespace CatalogService.Categories;

public class CategoryModel : EntityDto<Guid>
{

    public CategoryModel()
    {
        IconUrl = "https://image.4sough.shop/4sough-stage/markets/2025/1/1/14649b3f-8584-4304-9bff-a9f8c10098c4_watermark.png"; //TODO: handle by ObjetStorageService
        BannerUrl = "https://image.4sough.shop/4sough-stage/temp/bce56b56-9be1-43f4-aadb-d794cf591acb.jpg";//TODO: handle by ObjetStorageService

    }
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
