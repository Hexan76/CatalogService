using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class GetItemCustomizationsWithPricingRequest : IFrameworkRequest<GetItemCustomizationsResponse>
{
    public long ItemId { get; set; }
}
