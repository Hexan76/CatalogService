using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public interface IDeleteItemHandler : IFrameworkRequestHandler<DeleteItemRequest, BaseResponse>
{
}
