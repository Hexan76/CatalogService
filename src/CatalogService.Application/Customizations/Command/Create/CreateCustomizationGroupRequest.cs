using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Customizations;

public class CreateCustomizationGroupRequest : IFrameworkRequest<CustomizationGroupModel>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
