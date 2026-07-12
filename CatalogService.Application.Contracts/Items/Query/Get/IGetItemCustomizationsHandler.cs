using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public interface IGetItemCustomizationsHandler : IFrameworkRequestHandler<GetItemCustomizationsRequest, GetItemCustomizationsResponse>
{

}
