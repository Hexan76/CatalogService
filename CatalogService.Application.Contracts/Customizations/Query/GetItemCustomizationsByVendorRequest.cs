using CatalogService.Items;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Customizations;

public class GetItemCustomizationsByVendorRequest : IFrameworkRequest<GetItemCustomizationsByVendorResponse>
{
    public long VendorId { get; set; }
}
