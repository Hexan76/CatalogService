using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Customizations;

public class CreateCustomizationRequest : IFrameworkRequest<CustomizationModel>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int MaxCountRequest { get; set; }
    public Guid? ImageId { get; set; }
    public Guid GroupId { get; set; }
    public Guid[]? Items { get; set; }

}
