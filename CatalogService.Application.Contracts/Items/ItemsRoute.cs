using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class ItemsRoute : BaseRoutes
{
    public ItemsRoute(string Prefix, string routeBase) : base(Prefix, routeBase)
    {
        List = $"{Default}/list";
        GetItemsByIds = $"{Default}/get-by-ids";
        MenuItemList = $"{Default}/menu-items";
        Search = $"{Default}/search";
        GetCustomizations = $"{Default}/{{ItemId}}/customizations";
    }
    public readonly string List;
    public readonly string GetItemsByIds;
    public readonly string MenuItemList;
    public readonly string Search;
    public readonly string GetCustomizations;

}
