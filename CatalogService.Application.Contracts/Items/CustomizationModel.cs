using CatalogService.ObjectStorageService;

using Volo.Abp.Application.Dtos;

namespace CatalogService.Items;

public class CustomizationModel : EntityDto<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int DiscountPrecent { get; set; }
    public decimal AfterDiscount => Price - ((Price * DiscountPrecent) / 100m);
    public bool HasDiscount { get; set; }
    public int SortOrder { get; set; }
    public bool IsDisabled { get; set; }
    public FileUrlModel Image { get; set; } = new()
    {
        Role="default",
        Variants=new Dictionary<string, object>()
        {
            {"original" ,"https://image.4sough.shop/qasedfood-stage/media/customization/test/سس کچاپ تک نفره.jpg"},
            {"compressed" ,"https://image.4sough.shop/qasedfood-stage/media/customization/test/سس کچاپ تک نفره.webp"},
            { "thumbnail", "https://image.4sough.shop/qasedfood-stage/media/customization/test/سس کچاپ تک نفره_thumbnail.webp" },
        }
    };

    public virtual Guid ItemId { get; set; }
}
