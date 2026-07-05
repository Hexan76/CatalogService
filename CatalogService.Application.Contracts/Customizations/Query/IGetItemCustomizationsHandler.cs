using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Customizations;

public interface IGetItemCustomizationsHandler : IFrameworkRequestHandler<GetItemCustomizationsRequest, GetItemCustomizationsResponse>
{

}
