using System.ComponentModel.DataAnnotations;

using CatalogService.Categories;
using CatalogService.Customizations;
using CatalogService.ObjectStorageService;

using Volo.Abp.Application.Dtos;

namespace CatalogService.Items;

public class ItemModel : EntityDto<long>
{
    [MaxLength(200)]
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public double Price { get; set; } = 20;
    public int AvailableQuantity { get; set; }
    public int DiscountPrecent { get; set; }
    public bool HasDiscount { get; set; }
    public double Rate { get; set; } = 4.2;
    public int SortOrder { get; set; }
    public long PreparingTime { get; set; }

    [MaxLength(500)]
    public string Ingredients { get; set; }
    public string[] IngredientsArray => Ingredients?.Split('|', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
    public bool IsDisabled { get; set; }
    public long ParentId { get; set; }
    public FileUrlModel Image { get; set; }

    public ICollection<ItemModel> Variants { get; set; } = [];
    public ICollection<CategoryModel> Categories { get; set; } = [];
    public ICollection<CustomizationGroupModel> CustomizationGroups { get; set; } = [];
    public virtual long VendorId { get; set; }

}
