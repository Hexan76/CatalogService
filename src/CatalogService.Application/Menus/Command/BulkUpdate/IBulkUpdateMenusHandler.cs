using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Menus;

public interface IBulkUpdateMenusHandler : IFrameworkRequestHandler<BulkUpdateMenusRequest, ListMenuResponse>
{
}
