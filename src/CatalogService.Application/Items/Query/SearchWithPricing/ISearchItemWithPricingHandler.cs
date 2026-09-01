using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public interface ISearchItemWithPricingHandler : IFrameworkRequestHandler<SearchItemWithPricingRequest, SearchItemResponse>
{
}
