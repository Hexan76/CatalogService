using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Customizations;

public class CustomizationsRoute : BaseRoutes
{
    public CustomizationsRoute(string Prefix, string routeBase) : base(Prefix, routeBase)
    {
        GetVendorCustomizations = $"{Default}/by-vendor/{{VendorId}}";
        Search = $"{Default}/search";
        SetDisabled = $"{Default}/{{Id}}/disabled";
    }
    public readonly string GetVendorCustomizations;
    public readonly string Search;
    public readonly string SetDisabled;
}
