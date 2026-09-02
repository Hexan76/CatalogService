using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public interface IGetItemCustomizationsWithPricingHandler
    : IFrameworkRequestHandler<GetItemCustomizationsWithPricingRequest, GetItemCustomizationsResponse>
{
}
