using System.ComponentModel.DataAnnotations.Schema;

using CatalogService.Categories;

using Volo.Abp.Domain.Entities;

namespace CatalogService.Items;

[Table(nameof(ItemCategory))]
public class ItemCategory : Entity<Guid>
{
    public virtual Category Category { get; set; }
    public virtual Guid CategoryId { get; set; }
    public virtual Item Item { get; set; }
    public virtual Guid ItemId { get; set; }
}
