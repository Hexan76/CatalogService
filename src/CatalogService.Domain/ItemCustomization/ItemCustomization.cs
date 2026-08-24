using System.ComponentModel.DataAnnotations.Schema;

using CatalogService.Customizations;

using Framework.BuildingBlock.Entities;

namespace CatalogService.Items;

[Table(nameof(ItemCustomization))]
public class ItemCustomization : Entity<long>
{
    public virtual Customization Customization { get; set; }
    public virtual long CustomizationId { get; set; }
    public virtual Item Item { get; set; }
    public virtual long ItemId { get; set; }
}
