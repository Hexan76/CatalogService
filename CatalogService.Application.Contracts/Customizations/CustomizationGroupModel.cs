using Volo.Abp.Application.Dtos;

namespace CatalogService.Customizations;

public class CustomizationGroupModel : EntityDto<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public int SortOrder { get; set; }
    public bool IsDisabled { get; set; }
    public ICollection<CustomizationModel> Customizations { get; set; } = [];
}
