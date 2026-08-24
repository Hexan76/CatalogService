using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Menus;

public class GetMenuRequest : IFrameworkRequest<MenuModel>
{
    public long Id { get; set; }
    
}
