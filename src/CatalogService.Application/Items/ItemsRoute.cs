using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class ItemsRoute : BaseRoutes
{
    public ItemsRoute(string Prefix, string routeBase) : base(Prefix, routeBase)
    {
        List = $"{Default}/list";
        GetItemsByIds = $"{Default}/get-by-ids";
        MenuItemList = $"{Default}/menu-items";
        ReSortMenuItems = $"{Default}/menu-items/sort";
        Search = $"{Default}/search";
        GetCustomizations = $"{Default}/{{ItemId}}/customizations";
        SetDisabled = $"{Default}/{{Id}}/disabled";
    }
    public readonly string List;
    public readonly string GetItemsByIds;
    public readonly string MenuItemList;
    public readonly string ReSortMenuItems;
    public readonly string Search;
    public readonly string GetCustomizations;
    public readonly string SetDisabled;

}
