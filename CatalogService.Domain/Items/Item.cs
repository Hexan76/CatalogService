using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using CatalogService.Menus;

using Framework.BuildingBlock.Domain;

using Volo.Abp.Domain.Entities.Auditing;

namespace CatalogService.Items;

[Table(nameof(Item))]
public class Item : AuditedAggregateRoot<Guid>, IHasDisabled
{
    [MaxLength(200)]
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public int SortOrder { get; set; }

    [MaxLength(500)]
    public string Ingredients { get; set; }
    public bool IsDisabled { get; set; }
    public virtual ICollection<ItemCategory> ItemCategories { get; set; } = [];
    public virtual Guid VendorId { get; set; }
}
