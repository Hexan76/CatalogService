using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.BusinessTypes;

public interface ICreateBusinessTypeHandler : IFrameworkRequestHandler<CreateBusinessTypeRequest, BusinessTypeModel>
{
}
