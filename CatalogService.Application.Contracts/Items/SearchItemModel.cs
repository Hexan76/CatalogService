using System.ComponentModel.DataAnnotations;

using CatalogService.ObjectStorageService;

using Volo.Abp.Application.Dtos;

namespace CatalogService.Items;

public class SearchItemModel : EntityDto<Guid>
{
    [MaxLength(200)]
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public int DiscountPrecent { get; set; }
    public bool HasDiscount { get; set; }
    public double Rate { get; set; } = 4.2;

    public int SortOrder { get; set; }
    public bool IsDisabled { get; set; }
    public FileUrlModel Image { get; set; }

    public ICollection<VariantModel> Variants { get; set; } = [];
    public virtual long VendorId { get; set; }
}
