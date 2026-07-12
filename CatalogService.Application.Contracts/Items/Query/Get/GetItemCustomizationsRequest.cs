using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class GetItemCustomizationsRequest : IFrameworkRequest<GetItemCustomizationsResponse>
{
    public long ItemId { get; set; }
}
