using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using CatalogService.Menus;

using Framework.BuildingBlock.Domain;
using Framework.BuildingBlock.Entities;

namespace CatalogService.Items;

[Table(nameof(Item))]
public class Item : AuditedAggregateRoot<long>, IHasDisabled
{
    [MaxLength(200)]
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int AvailableQuantity { get; set; }
    public int DiscountPrecent { get; set; }
    public bool HasDiscount { get; set; }
    public int SortOrder { get; set; }
    public long PreparingTime { get; set; }

    [MaxLength(500)]
    public string Ingredients { get; set; } = default!;
    public bool IsDisabled { get; set; }


    public virtual Item? Parent { get; set; }
    public virtual long? ParentId { get; set; }
    public virtual ICollection<ItemCategory> ItemCategories { get; set; } = [];
    public virtual ICollection<MenuItem> MenuItems { get; set; } = [];
    public virtual ICollection<Item> Variants { get; set; } = [];
    public virtual ICollection<ItemCustomization> Customizations { get; set; } = [];

    public virtual long VendorId { get; set; }
}
