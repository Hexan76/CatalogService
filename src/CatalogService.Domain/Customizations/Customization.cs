using System.ComponentModel.DataAnnotations.Schema;

using CatalogService.CategoryCustomization;
using CatalogService.Items;

using Framework.BuildingBlock.Domain;
using Framework.BuildingBlock.Entities;

namespace CatalogService.Customizations;

[Table(nameof(Customization))]
public class Customization : AuditedAggregateRoot<long>, IHasDisabled
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int AvailableQuantity { get; set; }
    public int MaxCountRequest { get; set; }
    public int SortOrder { get; set; }
    public bool IsDisabled { get; set; }
    public virtual ICollection<ItemCustomization> Items { get; set; }
    public virtual CustomizationGroup CustomizationGroup { get; set; }
    public virtual long CustomizationGroupId { get; set; }
    public long VendorId { get; set; }
}
