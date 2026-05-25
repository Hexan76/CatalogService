using CatalogService.Categories;

namespace CatalogService.Constants;

public class CatalogServiceApiRoutes
{
    public const string Prefix = "api";
    public const string Application = $"{Prefix}/CatalogService";

    public static CategoryRoutes CategoryRoutes = new(Application, CatalogServiceApiTags.Category);
}
