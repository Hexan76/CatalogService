using System.ComponentModel.DataAnnotations.Schema;

using CatalogService.Categories;
using CatalogService.MenuItems;

using Volo.Abp.Domain.Entities;

namespace CatalogService.MenuItemCategories;

[Table(nameof(MenuItemCategory))]
public class MenuItemCategory : Entity
{
    public virtual Category Category { get; set; }
    public Guid CategoryId { get; set; }
    public virtual MenuItem MenuItem { get; set; }
    public Guid MenuItemId { get; set; }

    public override object?[] GetKeys() => [CategoryId, MenuItemId];
}
