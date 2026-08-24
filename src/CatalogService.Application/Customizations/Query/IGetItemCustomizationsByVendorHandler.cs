using CatalogService.Items;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Customizations;

public interface IGetItemCustomizationsByVendorHandler : IFrameworkRequestHandler<GetItemCustomizationsByVendorRequest, GetItemCustomizationsByVendorResponse>
{

}
