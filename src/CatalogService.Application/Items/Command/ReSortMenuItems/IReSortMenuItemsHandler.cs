using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public interface IReSortMenuItemsHandler : IFrameworkRequestHandler<ReSortMenuItemsRequest, ReSortMenuItemsResponse>
{
}
