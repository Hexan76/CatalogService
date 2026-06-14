using CatalogService.BusinessTypes;
using CatalogService.Categories;
using CatalogService.Items;
using CatalogService.Menus;

namespace CatalogService.Constants;

public class CatalogServiceApiRoutes
{
    public const string Prefix = "api";
    public const string Application = $"{Prefix}/CatalogService";

    public static CategoryRoutes CategoryRoutes = new(Application, CatalogServiceApiTags.Category);
    public static BusinessTypeRoutes BusinessTypeRoutes = new(Application, CatalogServiceApiTags.BusinessType);
    public static ItemsRoute ItemRoutes = new(Application, CatalogServiceApiTags.Item);
    public static MenusRoutes MenuRoutes = new(Application, CatalogServiceApiTags.Menu);
}
