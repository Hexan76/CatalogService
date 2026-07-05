using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Customizations;

public class GetItemCustomizationsRequest : IFrameworkRequest<GetItemCustomizationsResponse>
{
    public Guid ItemId { get; set; }
}
