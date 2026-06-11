using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class ItemsRoute : BaseRoutes
{
    public ItemsRoute(string Prefix, string routeBase) : base(Prefix, routeBase)
    {
        List = $"{Default}/list";
    }
    public readonly string List;
}
