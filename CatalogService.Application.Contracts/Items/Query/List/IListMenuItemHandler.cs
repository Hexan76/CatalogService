using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public interface IListMenuItemHandler : IFrameworkRequestHandler<ListMenuItemRequest, ListMenuItemResponse>
{
}
