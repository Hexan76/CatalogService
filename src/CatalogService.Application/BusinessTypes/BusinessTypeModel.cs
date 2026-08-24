using CatalogService.Categories;
using CatalogService.ObjectStorageService;

namespace CatalogService.BusinessTypes;

public class BusinessTypeModel
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public int SortOrder { get; set; }
    public FileUrlModel Image { get; set; }
    public ICollection<CategoryModel> Categories { get; set; }
    public bool IsDisabled { get; set; }

}
