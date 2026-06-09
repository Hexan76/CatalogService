using System.ComponentModel.DataAnnotations.Schema;

using CatalogService.MenuItemCategories;

using Framework.BuildingBlock.Domain;

using Volo.Abp.Domain.Entities.Auditing;

namespace CatalogService.MenuItems;

[Table(nameof(MenuItem))]
public class MenuItem : AuditedAggregateRoot<Guid>, IHasDisabled
{
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int SortOrder { get; set; }
    public bool IsDisabled { get; set; }
    public virtual ICollection<MenuItemCategory> MenuItemCategories { get; set; } = [];
    public virtual Guid VendorId { get; set; }
}
