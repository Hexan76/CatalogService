using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Customizations;

public class CustomizationsRoute : BaseRoutes
{
    public CustomizationsRoute(string Prefix, string routeBase) : base(Prefix, routeBase)
    {
        GetVendorCustomizations = $"{Default}/by-vendor/{{VendorId}}";
    }
    public readonly string GetVendorCustomizations;
}
