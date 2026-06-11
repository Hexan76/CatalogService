using System.ComponentModel.DataAnnotations.Schema;

using CatalogService.Menus;

using Framework.BuildingBlock.Domain;

using Volo.Abp.Domain.Entities;

namespace CatalogService.Items;

[Table(nameof(ItemVariant))]
public class ItemVariant : Entity<Guid>, IHasDisabled
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int DiscountPrecent { get; set; }
    public bool HasDiscount { get; set; }
    public int SortOrder { get; set; }
    public bool IsDisabled { get; set; }
    public virtual Menu Menu { get; set; }
    public virtual Guid MenuId { get; set; }
    public virtual Guid VendorId { get; set; }
}
