using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Menus;

public interface ICreateMenuHandler : IFrameworkRequestHandler<CreateMenuRequest, MenuModel>
{
}
