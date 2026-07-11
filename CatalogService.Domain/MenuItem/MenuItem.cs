using System.ComponentModel.DataAnnotations.Schema;

using CatalogService.Items;

using Volo.Abp.Domain.Entities;

namespace CatalogService.Menus;

[Table(nameof(MenuItem))]
public class MenuItem : Entity<long>
{
    public virtual long MenuId { get; set; }
    public virtual Menu Menu { get; set; }
    public virtual Item Item { get; set; }
    public virtual long ItemId { get; set; }
}
