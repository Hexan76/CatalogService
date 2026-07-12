using CatalogService.ObjectStorageService;

using Volo.Abp.Application.Dtos;

namespace CatalogService.Customizations;

public class CustomizationModel : EntityDto<long>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int MaxCountRequest { get; set; }
    public int SortOrder { get; set; }
    public bool IsDisabled { get; set; }
    public FileUrlModel Image { get; set; } 
}
