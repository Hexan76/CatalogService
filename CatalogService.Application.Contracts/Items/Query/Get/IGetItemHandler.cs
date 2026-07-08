using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public interface IGetItemHandler : IFrameworkRequestHandler<GetItemRequest, ItemModel>
{
}
