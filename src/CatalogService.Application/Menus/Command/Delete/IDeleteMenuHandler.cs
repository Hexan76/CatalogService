using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Menus;

public interface IDeleteMenuHandler : IFrameworkRequestHandler<DeleteMenuRequest, BaseResponse<long>>
{
}
