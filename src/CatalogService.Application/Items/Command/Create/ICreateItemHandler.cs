using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public interface ICreateItemHandler : IFrameworkRequestHandler<CreateItemRequest, ItemModel>
{
}
