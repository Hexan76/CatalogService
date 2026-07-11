using System.ComponentModel.DataAnnotations.Schema;

using Framework.BuildingBlock.Domain;

using Volo.Abp.Domain.Entities.Auditing;

namespace CatalogService.Menus;

[Table(nameof(Menu))]
public class Menu : AuditedAggregateRoot<long>, IHasDisabled
{
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public int SortOrder { get; set; }
    public bool IsDisabled { get; set; }
    public virtual ICollection<MenuItem> MenuItems { get; set; } = [];
    public virtual long VendorId { get; set; }
}
