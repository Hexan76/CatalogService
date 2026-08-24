using System.ComponentModel.DataAnnotations.Schema;

using CatalogService.Categories;

using Framework.BuildingBlock.Entities;

namespace CatalogService.Items;

[Table(nameof(ItemCategory))]
public class ItemCategory : Entity<long>
{
    public virtual Category Category { get; set; }
    public virtual long CategoryId { get; set; }
    public virtual Item Item { get; set; }
    public virtual long ItemId { get; set; }
}
