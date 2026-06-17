using Volo.Abp.Application.Dtos;

namespace CatalogService.Items;

public class VariantModel : EntityDto<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int DiscountPrecent { get; set; }
    public decimal AfterDiscount => Price - ((Price * DiscountPrecent) / 100m);
    public bool HasDiscount { get; set; }
    public int SortOrder { get; set; }
    public bool IsDisabled { get; set; }
    public virtual Guid MenuId { get; set; }
    public virtual long VendorId { get; set; }
}
