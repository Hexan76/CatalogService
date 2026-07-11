using CatalogService.Categories;
using CatalogService.ObjectStorageService;

using Volo.Abp.Application.Dtos;

namespace CatalogService.BusinessTypes;

public class BusinessTypeModel : EntityDto<long>
{

    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public int SortOrder { get; set; }
    public FileUrlModel Image { get; set; }
    public ICollection<CategoryModel> Categories { get; set; }
    public bool IsDisabled { get; set; }

}
