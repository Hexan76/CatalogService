using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Customizations;

public class CustomizationsRoute : BaseRoutes
{
    public CustomizationsRoute(string Prefix, string routeBase) : base(Prefix, routeBase)
    {
        GetItemCustomizations = $"{Default}/{{ItemId}}";
    }
    public readonly string GetItemCustomizations;
}
