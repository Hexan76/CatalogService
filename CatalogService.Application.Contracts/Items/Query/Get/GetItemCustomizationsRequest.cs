using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class GetItemCustomizationsRequest : IFrameworkRequest<GetItemCustomizationsResponse>
{
    public Guid ItemId { get; set; }
}
