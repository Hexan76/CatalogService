using System.ComponentModel.DataAnnotations.Schema;

using Framework.BuildingBlock.Domain;

using Volo.Abp.Domain.Entities;

namespace CatalogService.Items;

[Table(nameof(ItemVariant))]
public class ItemVariant : Entity<long>, IHasDisabled
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int DiscountPrecent { get; set; }
    public bool HasDiscount { get; set; }
    public int SortOrder { get; set; }
    public bool IsDisabled { get; set; }
    public virtual Item Item { get; set; }
    public virtual long ItemId { get; set; }
}
