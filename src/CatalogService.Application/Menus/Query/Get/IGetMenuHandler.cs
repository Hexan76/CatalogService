using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Menus;

public interface IGetMenuHandler : IFrameworkRequestHandler<GetMenuRequest, MenuModel>
{
}
