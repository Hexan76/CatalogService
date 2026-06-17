using CatalogService.Menus;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public interface IGetItemHandler : IFrameworkRequestHandler<GetMenuRequest, MenuModel>
{
}
