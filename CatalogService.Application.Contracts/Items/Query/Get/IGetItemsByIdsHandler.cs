using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items.Query.Get;

public interface IGetItemsByIdsHandler : IFrameworkRequestHandler<GetItemsByIdsRequest, ListItemResponse>
{
    
}
