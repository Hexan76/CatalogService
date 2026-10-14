using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Menus;

public class MenusRoutes : BaseRoutes
{
    public MenusRoutes(string Prefix, string routeBase) : base(Prefix, routeBase)
    {
        List = $"{Default}/list";
        Bulk = $"{Default}/bulk";
    }
    public readonly string List;
    public readonly string Bulk;
}
