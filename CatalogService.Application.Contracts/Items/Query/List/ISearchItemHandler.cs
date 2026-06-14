using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public interface ISearchItemHandler : IFrameworkRequestHandler<SearchItemRequest, SearchItemResponse>
{
}
