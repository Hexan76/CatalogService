using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.BusinessTypes;

public interface IDeleteBusinessTypeHandler : IFrameworkRequestHandler<DeleteBusinessTypeRequest, BaseResponse>
{
}
