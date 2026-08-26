using CatalogService.BusinessTypes;
using CatalogService.Categories;
using CatalogService.Customizations;
using CatalogService.Items;
using CatalogService.Menus;

namespace CatalogService.Constants;

public class CatalogServiceApiRoutes
{
    public const string Prefix = "";
    public const string Application = $"{Prefix}";

    public static CategoryRoutes CategoryRoutes = new(Application, CatalogServiceApiTags.Category);
    public static BusinessTypeRoutes BusinessTypeRoutes = new(Application, CatalogServiceApiTags.BusinessType);
    public static ItemsRoute ItemRoutes = new(Application, CatalogServiceApiTags.Item);
    public static MenusRoutes MenuRoutes = new(Application, CatalogServiceApiTags.Menu);
    public static CustomizationsRoute CustomizationRoutes = new(Application, CatalogServiceApiTags.Customization);
    public static CustomizationsGroupRoute CustomizationsGroupRoutes = new(Application, CatalogServiceApiTags.CustomizationGroup);
    public static SettingRoutes SettingRoutes = new(Application, CatalogServiceApiTags.Settings);
}
