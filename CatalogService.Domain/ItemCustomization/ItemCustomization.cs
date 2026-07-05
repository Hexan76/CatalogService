using System.ComponentModel.DataAnnotations.Schema;

using CatalogService.Customizations;

using Volo.Abp.Domain.Entities;

namespace CatalogService.Items;

[Table(nameof(ItemCustomization))]
public class ItemCustomization : Entity<Guid>
{
    public virtual Customization Customization { get; set; }
    public virtual Guid CustomizationId { get; set; }
    public virtual Item Item { get; set; }
    public virtual Guid ItemId { get; set; }
}
