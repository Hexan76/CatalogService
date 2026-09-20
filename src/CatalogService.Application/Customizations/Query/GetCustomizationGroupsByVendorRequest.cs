using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Customizations;

public class GetCustomizationGroupsByVendorRequest : IFrameworkRequest<GetCustomizationGroupsByVendorResponse>
{
    public long VendorId { get; set; }
}
