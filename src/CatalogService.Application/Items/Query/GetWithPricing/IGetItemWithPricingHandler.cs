using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public interface IGetItemWithPricingHandler : IFrameworkRequestHandler<GetItemWithPricingRequest, ItemModel>
{
}
