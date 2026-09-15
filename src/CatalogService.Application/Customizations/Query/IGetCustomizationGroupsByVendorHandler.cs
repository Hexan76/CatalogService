using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Customizations;

public interface IGetCustomizationGroupsByVendorHandler
    : IFrameworkRequestHandler<GetCustomizationGroupsByVendorRequest, GetCustomizationGroupsByVendorResponse>
{
}
