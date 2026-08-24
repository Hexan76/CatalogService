using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.BusinessTypes;

public interface IListBusinessTypeHandler : IFrameworkRequestHandler<ListBusinessTypeRequest, ListBusinessTypeResponse>
{
}
