using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Customizations;

public class CustomizationsGroupRoute : BaseRoutes
{
    public CustomizationsGroupRoute(string Prefix, string routeBase) : base(Prefix, routeBase)
    {
        GetByVendor = $"{Default}/by-vendor/{{VendorId}}";
    }

    public readonly string GetByVendor;
}
