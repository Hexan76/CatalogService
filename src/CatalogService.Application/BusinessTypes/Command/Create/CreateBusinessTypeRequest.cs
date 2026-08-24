using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.BusinessTypes;

public class CreateBusinessTypeRequest : IFrameworkRequest<BusinessTypeModel>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid? ImageId { get; set; }
}
