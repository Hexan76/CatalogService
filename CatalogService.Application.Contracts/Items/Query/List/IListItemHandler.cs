using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public interface IListItemHandler : IFrameworkRequestHandler<ListItemRequest, ListItemResponse>
{
}
