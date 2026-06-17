using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public interface IUpdateItemHandler : IFrameworkRequestHandler<UpdateItemRequest, ItemModel>
{
}
