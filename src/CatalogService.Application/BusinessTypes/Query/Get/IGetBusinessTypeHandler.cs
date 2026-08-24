using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.BusinessTypes;

public interface IGetBusinessTypeHandler : IFrameworkRequestHandler<GetBusinessTypeRequest, BusinessTypeModel>
{
}
