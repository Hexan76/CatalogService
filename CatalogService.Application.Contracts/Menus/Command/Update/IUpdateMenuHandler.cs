using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Menus;

public interface IUpdateMenuHandler : IFrameworkRequestHandler<UpdateMenuRequest, MenuModel>
{
}
