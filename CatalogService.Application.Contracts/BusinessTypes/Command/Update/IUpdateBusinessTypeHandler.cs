using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.BusinessTypes;

public interface IUpdateBusinessTypeHandler : IFrameworkRequestHandler<UpdateBusinessTypeRequest, BusinessTypeModel>
{
}
